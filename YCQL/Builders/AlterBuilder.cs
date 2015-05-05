/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using YCQL.Constraints;
using YCQL.DBHelpers;
using YCQL.Exceptions;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a Sql builder for operations related to making change to the table schema
	/// </summary>
	/// <seealso cref="YCQL.CreateBuilder"/>
	/// <seealso cref="YCQL.DeleteBuilder"/>
	/// <seealso cref="YCQL.InsertBuilder"/>
	/// <seealso cref="YCQL.SelectBuilder"/>
	/// <seealso cref="YCQL.UpdateBuilder"/>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.DBColumn"/>
	/// <seealso cref="YCQL.SQLIndex"/>
	/// <seealso cref="YCQL.Constraints.CheckConstraint"/>
	/// <seealso cref="YCQL.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.UniqueKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.ConstraintType"/>
	public class AlterBuilder : ITranslateSQL
	{
		/// <summary>
		/// The table to be altered
		/// </summary>
		DBTable _table;
		/// <summary>
		/// List of columns to be added
		/// </summary>
		List<DBColumn> _newColumns;
		/// <summary>
		/// List of columns to be dropped
		/// </summary>
		List<DBColumn> _dropColumns;
		/// <summary>
		/// List of columns to have their data types changed and their new data type
		/// </summary>
		List<Tuple<DBColumn, DataType>> _changeDataTypeColumns;
		/// <summary>
		/// List of new constraints to be added to the table
		/// </summary>
		List<SQLConstraint> _newConstraints;
		/// <summary>
		/// List of indexes' name to be dropped
		/// </summary>
		List<string> _dropIndexNames;
		/// <summary>
		/// List of constraints to be dropped. ConstraintType param is for MySql
		/// </summary>
		List<Tuple<string, ConstraintType?>> _dropConstraints;

		/// <summary>
		/// Initializes a new instance of the AlterBuilder class using specified table
		/// </summary>
		/// <param name="table">The table to be altered</param>
		public AlterBuilder(DBTable table)
		{
			_table = table;
			_newColumns = new List<DBColumn>();
			_dropColumns = new List<DBColumn>();
			_changeDataTypeColumns = new List<Tuple<DBColumn, DataType>>();
			_newConstraints = new List<SQLConstraint>();
			_dropIndexNames = new List<string>();
			_dropConstraints = new List<Tuple<string, ConstraintType?>>();
		}

		/// <summary>
		/// Adds a new column to the table
		/// </summary>
		/// <param name="column">Column to be added</param>
		/// <returns>A reference to this instance after the column has been added to the column to add list</returns>
		public AlterBuilder AddColumn(DBColumn column)
		{
			_newColumns.Add(column);
			return this;
		}

		/// <summary>
		/// Drops a column from the table
		/// </summary>
		/// <param name="column">Column to be dropped</param>
		/// <returns>A reference to this instance after the column has been added to the column to drop list</returns>
		public AlterBuilder DropColumn(DBColumn column)
		{
			_dropColumns.Add(column);
			return this;
		}

		/// <summary>
		/// Modify the data type of a column
		/// </summary>
		/// <param name="column">The column which would have its data type modified</param>
		/// <param name="newDataType">New data type for the column</param>
		/// <returns>A reference to this instance after the column has been added to the column to change list</returns>
		public AlterBuilder ChangeColumnDataType(DBColumn column, DataType newDataType)
		{
			_changeDataTypeColumns.Add(new Tuple<DBColumn, DataType>(column, newDataType));
			return this;
		}

		/// <summary>
		/// Adds a new constraint to the table
		/// </summary>
		/// <param name="constraints">Constraint to be added</param>
		/// <returns>A reference to this instance after the constraints has been added to the new constraint list</returns>
		public AlterBuilder AddConstraint(params SQLConstraint[] constraints)
		{
			_newConstraints.AddRange(constraints);
			return this;
		}

		/// <summary>
		/// Drops a constraint from the table
		/// </summary>
		/// <param name="constraint">Constraint to be dropped</param>
		/// <returns>A reference to this instance after the constraints has been added to the constraint to drop list</returns>
		/// <exception cref="YCQL.Exceptions.YCQLInternalException">Thrown when the constraint type is not reconized. Most likely due to newly added constraint class</exception>
		public AlterBuilder DropConstraint(SQLConstraint constraint)
		{
			ConstraintType constraintType;
			if (constraint is PrimaryKeyConstraint)
				constraintType = ConstraintType.PrimaryKey;
			else if (constraint is ForeignKeyConstraint)
				constraintType = ConstraintType.ForeignKey;
			else if (constraint is UniqueKeyConstraint)
				constraintType = ConstraintType.UniqueKey;
			else if (constraint is CheckConstraint)
				constraintType = ConstraintType.Check;
			else
				throw new YCQLInternalException("Unknown Constraint type : " + constraint.GetType().Name);

			return DropConstraint(constraint.Name, constraintType);
		}

		/// <summary>
		/// Drops a constraint from the table
		/// </summary>
		/// <param name="constraintName">Name of the constraint</param>
		/// <returns>A reference to this instance after the constraints has been added to the constraint to drop list</returns>
		public AlterBuilder DropConstraint(string constraintName)
		{
			return DropConstraint(constraintName, null);
		}

		/// <summary>
		/// Drops a constraint from the table
		/// </summary>
		/// <param name="constraintName">Name of the constraint</param>
		/// <param name="constraintType">Type of the constraint</param>
		/// <returns>A reference to this instance after the constraints has been added to the constraint to drop list</returns>
		public AlterBuilder DropConstraint(string constraintName, ConstraintType? constraintType)
		{
			_dropConstraints.Add(new Tuple<string, ConstraintType?>(constraintName, constraintType));
			return this;
		}

		/// <summary>
		/// Drops an index from the table
		/// </summary>
		/// <param name="index">Index to be dropped</param>
		/// <returns>A reference to this instance after the index has been added to the index to drop list</returns>
		public AlterBuilder DropIndex(SQLIndex index)
		{
			return DropIndex(index.Name);
		}

		/// <summary>
		/// Drops an index from the table
		/// </summary>
		/// <param name="indexName">Name of the index</param>
		/// <returns>A reference to this instance after the index has been added to the index to drop list</returns>
		public AlterBuilder DropIndex(string indexName)
		{
			_dropIndexNames.Add(indexName);
			return this;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		/// <exception cref="YCQL.Exceptions.DBNotSupportedException">Thrown when the translation cannot be done for destinated DBMS</exception>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			string alterTableStatement = string.Format("ALTER TABLE {0}", dbHelper.QuoteIdentifier(_table.Name));
			List<string> statements = new List<string>();
			foreach (DBColumn newColumn in _newColumns)
				statements.Add(alterTableStatement + string.Format(" ADD {0} {1}", dbHelper.QuoteIdentifier(newColumn.Name), newColumn.DataType));

			foreach (DBColumn dropColumn in _dropColumns)
				statements.Add(alterTableStatement + string.Format(" DROP COLUMN {0}", dbHelper.QuoteIdentifier(dropColumn.Name)));

			foreach (Tuple<DBColumn, DataType> changeDataTypeColumn in _changeDataTypeColumns)
			{
				string command;
				switch (dbHelper.DBEngine)
				{
					case DBEngine.SQLServer:
						command = "ALTER COLUMN";
						break;
					case DBEngine.MySQL:
						command = "MODIFY COLUMN";
						break;
					default:
						throw new DBNotSupportedException(dbHelper.DBEngine, "ChangeColumnDataType");
				}

				statements.Add(alterTableStatement + string.Format(" {0} {1} {2}", command, dbHelper.QuoteIdentifier(changeDataTypeColumn.Item1.Name), changeDataTypeColumn.Item2));
			}


			foreach (SQLConstraint newConstraint in _newConstraints)
				statements.Add(alterTableStatement + string.Format(" ADD {0}", newConstraint.ToSQL(dbHelper, parameterCollection)));

			foreach (string indexName in _dropIndexNames)
				statements.Add(string.Format("DROP INDEX {0} ON {1}", dbHelper.QuoteIdentifier(indexName), dbHelper.QuoteIdentifier(_table.Name)));

			foreach (Tuple<string, ConstraintType?> dropConstraint in _dropConstraints)
			{
				string statement = alterTableStatement;
				if (dbHelper.DBEngine == DBEngine.MySQL)
				{
					if (dropConstraint.Item2 == null)
						throw new DBNotSupportedException(DBEngine.MySQL, "Constraint type must be specified for MySql's DROP Constraint statement");

					switch (dropConstraint.Item2)
					{
						case ConstraintType.PrimaryKey:
							statement += " DROP PRIMARY KEY";
							break;
						case ConstraintType.ForeignKey:
							statement += string.Format(" DROP FOREIGN KEY {0}", dbHelper.QuoteIdentifier(dropConstraint.Item1));
							break;
						case ConstraintType.UniqueKey:
							statement += string.Format("DROP INDEX {0}", dbHelper.QuoteIdentifier(dropConstraint.Item1));
							break;
						default:
							throw new DBNotSupportedException(dbHelper.DBEngine, "Drop " + dropConstraint.Item2.ToString());
					}
				}
				else
				{
					statement += string.Format(" DROP CONSTRAINT {0}", dbHelper.QuoteIdentifier(dropConstraint.Item1));
				}

				statements.Add(statement);
			}

			return string.Join(";" + Environment.NewLine, statements);
		}
	}
}
