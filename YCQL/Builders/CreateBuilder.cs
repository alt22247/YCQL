/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Ycql.Constraints;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a Sql builder for creating table and index
	/// </summary>
	/// <seealso cref="Ycql.AlterBuilder"/>
	/// <seealso cref="Ycql.DeleteBuilder"/>
	/// <seealso cref="Ycql.InsertBuilder"/>
	/// <seealso cref="Ycql.SelectBuilder"/>
	/// <seealso cref="Ycql.UpdateBuilder"/>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.DbColumn"/>
	/// <seealso cref="Ycql.SqlIndex"/>
	public class CreateBuilder : ISqlBuilder
	{
		/// <summary>
		/// List of tables to be created
		/// </summary>
		List<DbTable> _tables;
		/// <summary>
		/// List of indexes to be created
		/// </summary>
		List<SqlIndex> _indexes;

		/// <summary>
		/// Initializes a new instance of the CreateBuilder class
		/// </summary>
		public CreateBuilder()
		{
			_tables = new List<DbTable>();
			_indexes = new List<SqlIndex>();
		}

		/// <summary>
		/// Creates one or more new tables
		/// </summary>
		/// <param name="tables">Tables to be created</param>
		/// <returns>A reference to this instance after the tables has been added to the table to create list</returns>
		public CreateBuilder CreateTable(params DbTable[] tables)
		{
			_tables.AddRange(tables.Unwrap<DbTable>());
			return this;
		}

		/// <summary>
		/// Creates one or more new indexes
		/// </summary>
		/// <param name="indexes">Indexes to be created</param>
		/// <returns>A reference to this instance after the indexes has been added to the index to create list</returns>
		public CreateBuilder CreateIndex(params SqlIndex[] indexes)
		{
			_indexes.AddRange(indexes.Unwrap<SqlIndex>());
			return this;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			List<string> statements = new List<string>();
			foreach (DbTable table in _tables)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("CREATE TABLE {0}", dbHelper.QuoteIdentifier(table.TableName));
				sb.AppendLine("(");

				List<string> columnDefStrings = new List<string>();
				foreach (DbColumn column in table.ColumnArray)
				{
					StringBuilder columnDefSB = new StringBuilder();
					columnDefSB.Tab();
					columnDefSB.AppendFormat(" {0} {1} ", dbHelper.QuoteIdentifier(column.ColumnName), column.DataType.ToSql(dbVersion, parameterCollection));
					if (column.IsNotNull)
						columnDefSB.Append("NOT NULL ");

					if (column.DefaultValue != null)
						columnDefSB.AppendFormat("DEFAULT {0} ", dbHelper.TranslateObjectToSqlString(column.DefaultValue, parameterCollection));
#if YCQL_MYSQL
					if (dbHelper.DbEngine == DbEngine.MySql && column.IsAutoIncrement)
						columnDefSB.Append("AUTO_INCREMENT");
#endif

#if YCQL_SQLSERVER
					if (dbHelper.DbEngine == DbEngine.SqlServer && column.Identity != null)
						columnDefSB.Append(column.Identity.ToSql(dbVersion, parameterCollection));
#endif

					columnDefStrings.Add(columnDefSB.ToString());
				}

				List<string> constraintDefStrings = new List<string>();
				foreach (SqlConstraint constraint in table.Constraints)
				{
					StringBuilder constraintDefSB = new StringBuilder();
					constraintDefSB.Tab();
					constraintDefSB.Append(constraint.ToSql(dbVersion, parameterCollection));

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

			foreach (SqlIndex index in _indexes)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("CREATE");
				if (index.IsUnique)
					sb.Append(" UNIQUE");

#if YCQL_SQLSERVER
				if (dbHelper.DbEngine == DbEngine.SqlServer && index.IndexType == SqlIndexType.CLUSTERED)
					sb.Append(" CLUSTERED");
#endif

				sb.AppendFormat(" INDEX {0}", index.Name);
				sb.AppendLine();
				sb.AppendFormat("ON {0} ({1})", dbHelper.QuoteIdentifier(index.Table.TableName),
					string.Join(",", index.Columns.Select(x => x.ToSql(dbVersion, parameterCollection))));

#if YCQL_MYSQL
				if (dbHelper.DbEngine == DbEngine.MySql &&
					index.IndexType == SqlIndexType.BTREE || index.IndexType == SqlIndexType.HASH)
					sb.AppendFormat(" USING {0}", index.IndexType.ToString());
#endif

				statements.Add(sb.ToString());
			}

			return string.Join(";" + Environment.NewLine, statements);
		}
	}
}
