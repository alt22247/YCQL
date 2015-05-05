/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using YCQL.Constraints;
using YCQL.DBHelpers;
using YCQL.Extensions;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a Sql builder for creating table and index
	/// </summary>
	/// <seealso cref="YCQL.AlterBuilder"/>
	/// <seealso cref="YCQL.DeleteBuilder"/>
	/// <seealso cref="YCQL.InsertBuilder"/>
	/// <seealso cref="YCQL.SelectBuilder"/>
	/// <seealso cref="YCQL.UpdateBuilder"/>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.DBColumn"/>
	/// <seealso cref="YCQL.SQLIndex"/>
	public class CreateBuilder : ISQLBuilder
	{
		/// <summary>
		/// List of tables to be created
		/// </summary>
		List<DBTable> _tables;
		/// <summary>
		/// List of indexes to be created
		/// </summary>
		List<SQLIndex> _indexes;

		/// <summary>
		/// Initializes a new instance of the CreateBuilder class
		/// </summary>
		public CreateBuilder()
		{
			_tables = new List<DBTable>();
			_indexes = new List<SQLIndex>();
		}

		/// <summary>
		/// Creates one or more new tables
		/// </summary>
		/// <param name="tables">Tables to be created</param>
		/// <returns>A reference to this instance after the tables has been added to the table to create list</returns>
		public CreateBuilder CreateTable(params DBTable[] tables)
		{
			_tables.AddRange(tables);
			return this;
		}

		/// <summary>
		/// Creates one or more new indexes
		/// </summary>
		/// <param name="indexes">Indexes to be created</param>
		/// <returns>A reference to this instance after the indexes has been added to the index to create list</returns>
		public CreateBuilder CreateIndex(params SQLIndex[] indexes)
		{
			_indexes.AddRange(indexes);
			return this;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			List<string> statements = new List<string>();
			foreach (DBTable table in _tables)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("CREATE TABLE {0}", dbHelper.QuoteIdentifier(table.Name));
				sb.AppendLine("(");

				List<string> columnDefStrings = new List<string>();
				foreach (DBColumn column in table.ColumnArray)
				{
					StringBuilder columnDefSB = new StringBuilder();
					columnDefSB.Tab();
					columnDefSB.AppendFormat(" {0} {1} ", dbHelper.QuoteIdentifier(column.Name), column.DataType.ToSQL(dbHelper, parameterCollection));
					if (column.IsNotNull)
						columnDefSB.Append("NOT NULL");

					if (dbHelper.DBEngine == DBEngine.MySQL && column.IsAutoIncrement)
						columnDefSB.Append("AUTO_INCREMENT");

					if (dbHelper.DBEngine == DBEngine.SQLServer && column.Identity != null)
						columnDefSB.Append(column.Identity.ToSQL(dbHelper, parameterCollection));

					columnDefStrings.Add(columnDefSB.ToString());
				}

				List<string> constraintDefStrings = new List<string>();
				foreach (SQLConstraint constraint in table.Constraints)
				{
					StringBuilder constraintDefSB = new StringBuilder();
					constraintDefSB.Tab();
					constraintDefSB.Append(constraint.ToSQL(dbHelper, parameterCollection));

					constraintDefStrings.Add(constraintDefSB.ToString());
				}

				sb.Append(string.Join("," + Environment.NewLine, columnDefStrings));
				if (constraintDefStrings.Count > 0)
				{
					sb.AppendLine(",");
					sb.Append(string.Join("," + Environment.NewLine, constraintDefStrings));
				}

				sb.AppendLine();
				sb.Append(")");

				statements.Add(sb.ToString());
			}

			foreach (SQLIndex index in _indexes)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("CREATE");
				if (index.IsUnique)
					sb.Append(" UNIQUE");

				if (dbHelper.DBEngine == DBEngine.SQLServer && index.IndexType == SQLIndexType.CLUSTERED)
					sb.Append(" CLUSTERED");

				sb.AppendFormat(" INDEX {0}", index.Name);
				sb.AppendLine();
				sb.AppendFormat("ON {0} ({1})", dbHelper.QuoteIdentifier(index.Table.Name),
					string.Join(",", index.Columns.Select(x => x.ToSQL(dbHelper, parameterCollection))));

				if (dbHelper.DBEngine == DBEngine.MySQL &&
					index.IndexType == SQLIndexType.BTREE || index.IndexType == SQLIndexType.HASH)
					sb.AppendFormat(" USING {0}", index.IndexType.ToString());

				statements.Add(sb.ToString());
			}

			return string.Join(";" + Environment.NewLine, statements);
		}
	}
}
