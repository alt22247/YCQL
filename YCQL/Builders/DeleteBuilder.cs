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
	/// Represents a Sql builder for delete operations
	/// </summary>
	/// <seealso cref="Ycql.CreateBuilder"/>
	/// <seealso cref="Ycql.AlterBuilder"/>
	/// <seealso cref="Ycql.InsertBuilder"/>
	/// <seealso cref="Ycql.SelectBuilder"/>
	/// <seealso cref="Ycql.UpdateBuilder"/>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class DeleteBuilder : ISqlBuilder, ISupportWhere<DeleteBuilder>
	{
		/// <summary>
		/// Table which should have rows deleted
		/// </summary>
		DbTable _table;
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
		/// Where clause of the delete statement
		/// </summary>
		object _whereClause;
		/// <summary>
		/// Initializes a new instance of the DeleteBuilder class using specified table
		/// </summary>
		/// <param name="table">Table which should have rows deleted</param>
		public DeleteBuilder(DbTable table)
			: this(table, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the DeleteBuilder class using specified table and useTruncate flag
		/// </summary>
		/// <param name="table">Table which should have rows deleted</param>
		/// <param name="useTruncate">Whether or not to use truncate option for the delete</param>
		public DeleteBuilder(DbTable table, bool useTruncate)
		{
			_table = table;
			UseTruncate = useTruncate;
#if YCQL_SQLSERVER
			_outputExpressions = new List<object>();
#endif
		}

		/// <summary>
		/// Gets or sets whether to use truncate option for the delete
		/// </summary>
		public bool UseTruncate { get; set; }

#if YCQL_SQLSERVER
		/// <summary>
		/// Ouputs the specified columns after insert (for Sql Server)
		/// </summary>
		/// <param name="expressions">expressions to be outputed, DBColumn will be outputed as INSERTED.{ColumnName} (for Sql Server)</param>
		/// <returns>A reference to this instance after the columns have been added to the to output list</returns>
		public DeleteBuilder Output(params object[] expressions)
		{
			if (expressions == null)
				return this;

			foreach (object expression in expressions.Unwrap())
				_outputExpressions.Add(expression is DbColumn ? ((DbColumn) expression).ToDeletedColumn() : expression);

			return this;
		}

		/// <summary>
		/// Sets the table to be used for OUTPUT INTO statement (for Sql Server)
		/// </summary>
		/// <param name="tableExpression">Table to be output into</param>
		/// <returns></returns>
		public DeleteBuilder OutputInto(object tableExpression)
		{
			_outputIntoTableExpression = tableExpression;
			return this;
		}
#endif

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">A boolean expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(BooleanExpression expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="clause">A logical clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(LogicalClause clause)
		{
			return Where((object) clause);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An all operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(AllOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An any operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(AnyOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An exists operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(ExistsOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An in operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(InOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="clause">A clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(object clause)
		{
			_whereClause = clause;
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

			StringBuilder sb = new StringBuilder();
			if (UseTruncate)
				sb.Append("TRUNCATE TABLE ");
			else
				sb.Append("DELETE FROM ");

			sb.Append(dbHelper.TranslateObjectToSqlString(_table, parameterCollection));

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

			if (!_whereClause.IsNullOrEmpty())
				sb.AppendFormat(" WHERE {0}", dbHelper.TranslateObjectToSqlString(_whereClause, parameterCollection));

			return sb.ToString();
		}
	}
}
