/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a Sql builder for insert operation
	/// </summary>
	/// <seealso cref="Ycql.CreateBuilder"/>
	/// <seealso cref="Ycql.DeleteBuilder"/>
	/// <seealso cref="Ycql.AlterBuilder"/>
	/// <seealso cref="Ycql.SelectBuilder"/>
	/// <seealso cref="Ycql.UpdateBuilder"/>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class InsertBuilder : ISqlBuilder
	{
		/// <summary>
		/// List of objects to be inserted
		/// </summary>
		List<object> _insertValues;
		/// <summary>
		/// List of columns to have values inserted
		/// </summary>
		List<DbColumn> _insertColumns;
#if YCQL_SQLSERVER
		/// <summary>
		/// List of columns to be outputed after insert (for Sql Server)
		/// </summary>
		List<object> _outputExpressions;
		/// <summary>
		/// Table to be outputed into for OUTPUT statement (for Sql Server)
		/// </summary>
		object _outputIntoTableExpression;
#endif
		/// <summary>
		/// Table which new rows should be inserted into
		/// </summary>
		object _tableExpression;

		object _subQuery;

		/// <summary>
		/// Initializes a new instance of the InsertBuilder class using specified table
		/// </summary>
		/// <param name="table">The corresponding DBTable instance for the INSERT operation</param>
		public InsertBuilder(DbTable table)
			: this((object) table)
		{
		}

		/// <summary>
		/// Initializes a new instance of the InsertBuilder class using specified table expression
		/// </summary>
		/// <param name="tableExpression">The table expression for the INSERT operation</param>
		public InsertBuilder(object tableExpression)
		{
			_insertValues = new List<object>();
			_insertColumns = new List<DbColumn>();
#if YCQL_SQLSERVER
			_outputExpressions = new List<object>();
#endif

			_tableExpression = tableExpression;
		}

		/// <summary>
		/// Sets the subquery to use for the INSERT operation
		/// </summary>
		/// <param name="subQuery">the subquery to use</param>
		/// <returns></returns>
		public InsertBuilder SubQuery(object subQuery)
		{
			_subQuery = subQuery;
			return this;
		}

		/// <summary>
		/// Specifies the new value to be inserted into the specified column
		/// </summary>
		/// <param name="column">Column associated with the new value to be inserted</param>
		/// <param name="value">Value to be inserted</param>
		/// <returns>A reference to this instance after column value pair has been added to the to insert list</returns>
		public InsertBuilder AddPair(DbColumn column, object value)
		{
			_insertColumns.Add(column);
			_insertValues.Add(value);
			return this;
		}

		/// <summary>
		/// Adds specified columns into the insert column list
		/// </summary>
		/// <param name="columns">Columns to be added for insert</param>
		/// <returns>A reference to this instance after column value pair has been added to the to insert list</returns>
		public InsertBuilder AddColumns(params DbColumn[] columns)
		{
			_insertColumns.AddRange(columns);
			return this;
		}

#if YCQL_SQLSERVER
		/// <summary>
		/// Ouputs the specified columns after insert (for Sql Server)
		/// </summary>
		/// <param name="expressions">expressions to be outputed, DBColumn will be outputed as INSERTED.{ColumnName} (for Sql Server)</param>
		/// <returns>A reference to this instance after the columns have been added to the to output list</returns>
		public InsertBuilder Output(params object[] expressions)
		{
			if (expressions == null)
				return this;

			foreach (object expression in expressions.Unwrap())
				_outputExpressions.Add(expression is DbColumn ? ((DbColumn) expression).ToInsertedColumn() : expression);

			return this;
		}

		/// <summary>
		/// Sets the table to be used for OUTPUT INTO statement (for Sql Server)
		/// </summary>
		/// <param name="tableExpression">Table to be output into</param>
		/// <returns></returns>
		public InsertBuilder OutputInto(object tableExpression)
		{
			_outputIntoTableExpression = tableExpression;
			return this;
		}
#endif

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("INSERT INTO {0} ", dbHelper.TranslateObjectToSqlString(_tableExpression, parameterCollection));
			if (_insertColumns.Count > 0)
				sb.AppendFormat("({0})", string.Join(",", _insertColumns.Select(column => column.ToSql(dbVersion, parameterCollection))));

			if (_subQuery != null)
				sb.Append(dbHelper.TranslateObjectToSqlString(_subQuery, parameterCollection));

#if YCQL_SQLSERVER
			if (_outputExpressions.Count > 0 && dbHelper.DbEngine == DbEngine.SqlServer)
			{
				sb.Append(" OUTPUT ");
				sb.Append(dbHelper.TranslateObjectsToSqlString(_outputExpressions, parameterCollection));

				if (_outputIntoTableExpression != null)
					sb.Append(" INTO " + dbHelper.TranslateObjectToSqlString(_outputIntoTableExpression, parameterCollection));

				sb.AppendLine();
			}
#endif

			if (_insertValues.Count > 0)
				sb.AppendFormat(" VALUES ({0})", dbHelper.TranslateObjectsToSqlString(_insertValues, parameterCollection));

			return sb.ToString();
		}
	}
}
