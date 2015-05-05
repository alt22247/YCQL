/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Extensions;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a Sql builder for update statement
	/// </summary>
	/// <seealso cref="YCQL.CreateBuilder"/>
	/// <seealso cref="YCQL.DeleteBuilder"/>
	/// <seealso cref="YCQL.InsertBuilder"/>
	/// <seealso cref="YCQL.SelectBuilder"/>
	/// <seealso cref="YCQL.AlterBuilder"/>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.DBColumn"/>
	/// <seealso cref="YCQL.JoinDefinition"/>
	public class UpdateBuilder : ISQLBuilder, IJoinable<UpdateBuilder>, ISupportWhere<UpdateBuilder>
	{
		/// <summary>
		/// Table to be updated
		/// </summary>
		DBTable _table;
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
		Dictionary<DBColumn, object> _setColumnValDict;
		/// <summary>
		/// Initializes a new instance of the UpdateBuilder class using specified table
		/// </summary>
		/// <param name="table">The table to be updated</param>
		public UpdateBuilder(DBTable table)
		{
			_table = table;
			_setColumnValDict = new Dictionary<DBColumn, object>();
			_joinDefinitions = new List<JoinDefinition>();
		}

		/// <summary>
		/// Sets the new value of a column
		/// </summary>
		/// <param name="column">Column to have its value updated</param>
		/// <param name="value">New value for the column</param>
		/// <returns>A reference to this instance after the operation is completed</returns>
		public UpdateBuilder Set(DBColumn column, object value)
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
			_joinDefinitions.AddRange(joinDefinitions);
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
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("UPDATE {0}", dbHelper.QuoteIdentifier(_table.Name));
			sb.AppendLine();

			if (_joinDefinitions.Count > 0 && dbHelper.DBEngine == DBEngine.MySQL)
			{
				foreach (JoinDefinition jd in _joinDefinitions)
					sb.AppendLine(jd.ToSQL(dbHelper, parameterCollection));
			}


			List<string> setstatements = new List<string>();
			foreach (KeyValuePair<DBColumn, object> pair in _setColumnValDict)
			{
				setstatements.Add(dbHelper.TranslateObjectToSQLString(pair.Key, parameterCollection) + " = " +
								 dbHelper.TranslateObjectToSQLString(pair.Value, parameterCollection));
			}

			sb.AppendFormat("SET {0}", string.Join(",", setstatements));
			sb.AppendLine();

			if (_joinDefinitions.Count > 0 && dbHelper.DBEngine == DBEngine.SQLServer)
			{
				sb.AppendFormat("FROM {0}", dbHelper.QuoteIdentifier(_table.Name));
				sb.AppendLine();

				foreach (JoinDefinition jd in _joinDefinitions)
					sb.AppendLine(jd.ToSQL(dbHelper, parameterCollection));
			}

			if (!_whereClause.IsNullOrEmpty())
				sb.AppendFormat(" WHERE {0}", dbHelper.TranslateObjectToSQLString(_whereClause, parameterCollection));

			return sb.ToString();
		}
	}
}
