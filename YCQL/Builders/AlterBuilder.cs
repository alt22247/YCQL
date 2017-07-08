/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using Ycql.Constraints;
using Ycql.DbHelpers;
using Ycql.Exceptions;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a Sql builder for operations related to making change to the table schema
	/// </summary>
	/// <seealso cref="Ycql.CreateBuilder"/>
	/// <seealso cref="Ycql.DeleteBuilder"/>
	/// <seealso cref="Ycql.InsertBuilder"/>
	/// <seealso cref="Ycql.SelectBuilder"/>
	/// <seealso cref="Ycql.UpdateBuilder"/>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.DbColumn"/>
	/// <seealso cref="Ycql.SqlIndex"/>
	/// <seealso cref="Ycql.Constraints.CheckConstraint"/>
	/// <seealso cref="Ycql.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.UniqueKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.ConstraintType"/>
	public class AlterBuilder : ITranslateSql
	{
		/// <summary>
		/// The table to be altered
		/// </summary>
		DbTable _table;
		/// <summary>
		/// List of columns to be added
		/// </summary>
		List<DbColumn> _newColumns;
		/// <summary>
		/// List of columns to be dropped
		/// </summary>
		List<DbColumn> _dropColumns;
		/// <summary>
		/// List of columns to have their data types changed and their new data type
		/// </summary>
		List<Tuple<DbColumn, DataType>> _changeDataTypeColumns;
		/// <summary>
		/// List of new constraints to be added to the table
		/// </summary>
		List<SqlConstraint> _newConstraints;
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
		public AlterBuilder(DbTable table)
		{
			_table = table;
			_newColumns = new List<DbColumn>();
			_dropColumns = new List<DbColumn>();
			_changeDataTypeColumns = new List<Tuple<DbColumn, DataType>>();
			_newConstraints = new List<SqlConstraint>();
			_dropIndexNames = new List<string>();
			_dropConstraints = new List<Tuple<string, ConstraintType?>>();
		}

		/// <summary>
		/// Adds a new column to the table
		/// </summary>
		/// <param name="column">Column to be added</param>
		/// <returns>A reference to this instance after the column has been added to the column to add list</returns>
		public AlterBuilder AddColumn(DbColumn column)
		{
			_newColumns.Add(column);
			return this;
		}

		/// <summary>
		/// Drops a column from the table
		/// </summary>
		/// <param name="column">Column to be dropped</param>
		/// <returns>A reference to this instance after the column has been added to the column to drop list</returns>
		public AlterBuilder DropColumn(DbColumn column)
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
		public AlterBuilder ChangeColumnDataType(DbColumn column, DataType newDataType)
		{
			_changeDataTypeColumns.Add(new Tuple<DbColumn, DataType>(column, newDataType));
			return this;
		}

		/// <summary>
		/// Adds a new constraint to the table
		/// </summary>
		/// <param name="constraints">Constraint to be added</param>
		/// <returns>A reference to this instance after the constraints has been added to the new constraint list</returns>
		public AlterBuilder AddConstraint(params SqlConstraint[] constraints)
		{
			_newConstraints.AddRange(constraints.Unwrap<SqlConstraint>());
			return this;
		}

		/// <summary>
		/// Drops a constraint from the table
		/// </summary>
		/// <param name="constraint">Constraint to be dropped</param>
		/// <returns>A reference to this instance after the constraints has been added to the constraint to drop list</returns>
		/// <exception cref="Ycql.Exceptions.YCQLInternalException">Thrown when the constraint type is not reconized. Most likely due to newly added constraint class</exception>
		public AlterBuilder DropConstraint(SqlConstraint constraint)
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
#if YCQL_MYSQL
		public AlterBuilder DropConstraint(string constraintName, ConstraintType? constraintType)
#else
		AlterBuilder DropConstraint(string constraintName, ConstraintType? constraintType)
#endif
		{
			_dropConstraints.Add(new Tuple<string, ConstraintType?>(constraintName, constraintType));
			return this;
		}


		/// <summary>
		/// Drops an index from the table
		/// </summary>
		/// <param name="index">Index to be dropped</param>
		/// <returns>A reference to this instance after the index has been added to the index to drop list</returns>
		public AlterBuilder DropIndex(SqlIndex index)
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
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		/// <exception cref="Ycql.Exceptions.DbNotSupportedException">Thrown when the translation cannot be done for destinated DBMS</exception>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			string alterTableStatement = string.Format("ALTER TABLE {0}", dbHelper.QuoteIdentifier(_table.TableName));
			List<string> statements = new List<string>();
			foreach (DbColumn newColumn in _newColumns)
				statements.Add(alterTableStatement + string.Format(" ADD {0} {1}", dbHelper.QuoteIdentifier(newColumn.ColumnName), newColumn.DataType));

			foreach (DbColumn dropColumn in _dropColumns)
				statements.Add(alterTableStatement + string.Format(" DROP COLUMN {0}", dbHelper.QuoteIdentifier(dropColumn.ColumnName)));

			foreach (Tuple<DbColumn, DataType> changeDataTypeColumn in _changeDataTypeColumns)
			{
				string command;
				switch (dbHelper.DbEngine)
				{
#if YCQL_SQLSERVER
					case DbEngine.SqlServer:
						command = "ALTER COLUMN";
						break;
#endif

#if YCQL_MYSQL
					case DbEngine.MySql:
						command = "MODIFY COLUMN";
						break;
#endif
					default:
						throw new DbNotSupportedException(dbHelper.DbEngine, "ChangeColumnDataType");
				}

				statements.Add(alterTableStatement + string.Format(" {0} {1} {2}", command, dbHelper.QuoteIdentifier(changeDataTypeColumn.Item1.ColumnName), changeDataTypeColumn.Item2));
			}


			foreach (SqlConstraint newConstraint in _newConstraints)
				statements.Add(alterTableStatement + string.Format(" ADD {0}", newConstraint.ToSql(dbVersion, parameterCollection)));

			foreach (string indexName in _dropIndexNames)
				statements.Add(string.Format("DROP INDEX {0} ON {1}", dbHelper.QuoteIdentifier(indexName), dbHelper.QuoteIdentifier(_table.TableName)));

			foreach (Tuple<string, ConstraintType?> dropConstraint in _dropConstraints)
			{
				string statement = alterTableStatement;
#if YCQL_MYSQL
				if (dbHelper.DbEngine == DbEngine.MySql)
				{
					if (dropConstraint.Item2 == null)
						throw new DbNotSupportedException(DbEngine.MySql, "Constraint type must be specified for MySql's DROP Constraint statement");

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
							throw new DbNotSupportedException(dbHelper.DbEngine, "Drop " + dropConstraint.Item2.ToString());
					}
				}
				else
				{
					statement += string.Format(" DROP CONSTRAINT {0}", dbHelper.QuoteIdentifier(dropConstraint.Item1));
				}
#else
				statement += string.Format(" DROP CONSTRAINT {0}", dbHelper.QuoteIdentifier(dropConstraint.Item1));
#endif
				statements.Add(statement);
			}

			return string.Join(";" + Environment.NewLine, statements);
		}
	}
}
