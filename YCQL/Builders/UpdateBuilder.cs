/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a Sql builder for update statement
	/// </summary>
	/// <seealso cref="Ycql.CreateBuilder"/>
	/// <seealso cref="Ycql.DeleteBuilder"/>
	/// <seealso cref="Ycql.InsertBuilder"/>
	/// <seealso cref="Ycql.SelectBuilder"/>
	/// <seealso cref="Ycql.AlterBuilder"/>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.DbColumn"/>
	/// <seealso cref="Ycql.JoinDefinition"/>
	public class UpdateBuilder : ISqlBuilder, IJoinable<UpdateBuilder>, ISupportWhere<UpdateBuilder>
	{
		/// <summary>
		/// Table to be updated
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
		/// List of join definitions for the update
		/// </summary>
		List<JoinDefinition> _joinDefinitions;
		/// <summary>
		/// The where clause for the statement
		/// </summary>
		object _whereClause;
		/// <summary>
		/// Dictionary of columns and their new value
		/// </summary>
		Dictionary<DbColumn, object> _setColumnValDict;
		/// <summary>
		/// Initializes a new instance of the UpdateBuilder class using specified table
		/// </summary>
		/// <param name="table">The table to be updated</param>
		public UpdateBuilder(DbTable table)
		{
			_table = table;
			_setColumnValDict = new Dictionary<DbColumn, object>();
			_joinDefinitions = new List<JoinDefinition>();
#if YCQL_SQLSERVER
			_outputExpressions = new List<object>();
#endif
		}

#if YCQL_SQLSERVER
		/// <summary>
		/// Ouputs the specified columns after insert (for Sql Server)
		/// </summary>
		/// <param name="expressions">expressions to be outputed (for Sql Server)</param>
		/// <returns>A reference to this instance after the columns have been added to the to output list</returns>
		public UpdateBuilder Output(params object[] expressions)
		{
			if (expressions == null)
				return this;

			_outputExpressions.AddRange(expressions.Unwrap());
			return this;
		}

		/// <summary>
		/// Sets the table to be used for OUTPUT INTO statement (for Sql Server)
		/// </summary>
		/// <param name="tableExpression">Table to be output into</param>
		/// <returns></returns>
		public UpdateBuilder OutputInto(object tableExpression)
		{
			_outputIntoTableExpression = tableExpression;
			return this;
		}
#endif

		/// <summary>
		/// Sets the new value of a column
		/// </summary>
		/// <param name="column">Column to have its value updated</param>
		/// <param name="value">New value for the column</param>
		/// <returns>A reference to this instance after the operation is completed</returns>
		public UpdateBuilder Set(DbColumn column, object value)
		{
			_setColumnValDict.Add(column, value);
			return this;
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">A boolean expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(BooleanExpression expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="clause">A logical clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(LogicalClause clause)
		{
			return Where((object) clause);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An all operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(AllOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An any operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(AnyOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An exists operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(ExistsOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An in operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(InOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="clause">A clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public UpdateBuilder Where(object clause)
		{
			_whereClause = clause;
			return this;
		}

		/// <summary>
		/// Joins one or more tables to the statement
		/// </summary>
		/// <param name="joinDefinitions">Join definitions to be joined</param>
		/// <returns>A reference to this instance after the join definitions has been added to the join list</returns>
		public UpdateBuilder Join(params JoinDefinition[] joinDefinitions)
		{
			_joinDefinitions.AddRange(joinDefinitions.Unwrap<JoinDefinition>());
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
			sb.AppendFormat("UPDATE {0}", dbHelper.QuoteIdentifier(_table.TableName));
			sb.AppendLine();

#if YCQL_MYSQL
			if (_joinDefinitions.Count > 0 && dbHelper.DbEngine == DbEngine.MySql)
			{
				foreach (JoinDefinition jd in _joinDefinitions)
					sb.AppendLine(jd.ToSql(dbVersion, parameterCollection));
			}
#endif

			List<string> setstatements = new List<string>();
			foreach (KeyValuePair<DbColumn, object> pair in _setColumnValDict)
			{
				setstatements.Add(dbHelper.TranslateObjectToSqlString(pair.Key, parameterCollection) + " = " +
								 dbHelper.TranslateObjectToSqlString(pair.Value, parameterCollection));
			}

			sb.AppendFormat("SET {0}", string.Join(",", setstatements));
			sb.AppendLine();

#if YCQL_SQLSERVER
			if (_outputExpressions.Count > 0 && dbHelper.DbEngine == DbEngine.SqlServer)
			{
				sb.Append(" OUTPUT ");
				sb.Append(dbHelper.TranslateObjectsToSqlString(_outputExpressions, parameterCollection));

				if (_outputIntoTableExpression != null)
					sb.Append(" INTO " + dbHelper.TranslateObjectToSqlString(_outputIntoTableExpression, parameterCollection));

				sb.AppendLine();
			}

			if (_joinDefinitions.Count > 0 && dbHelper.DbEngine == DbEngine.SqlServer)
			{
				sb.AppendFormat("FROM {0}", dbHelper.QuoteIdentifier(_table.TableName));
				sb.AppendLine();

				foreach (JoinDefinition jd in _joinDefinitions)
					sb.AppendLine(jd.ToSql(dbVersion, parameterCollection));
			}
#endif

			if (!_whereClause.IsNullOrEmpty())
				sb.AppendFormat(" WHERE {0}", dbHelper.TranslateObjectToSqlString(_whereClause, parameterCollection));

			return sb.ToString();
		}
	}
}
