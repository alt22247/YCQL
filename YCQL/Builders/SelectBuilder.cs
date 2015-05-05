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
	/// Enum list of supported orders
	/// </summary>
	public enum Order
	{
		/// <summary>
		/// Order is not explicitly defined, implicit ordering of the DB will be used
		/// </summary>
		Unspecified,
		/// <summary>
		/// Orders the result ascendingly
		/// </summary>
		asc,
		/// <summary>
		/// Orders the result descendingly
		/// </summary>
		desc
	}

	/// <summary>
	/// Represents a Sql builder for select queries
	/// </summary>
	/// <seealso cref="YCQL.CreateBuilder"/>
	/// <seealso cref="YCQL.DeleteBuilder"/>
	/// <seealso cref="YCQL.InsertBuilder"/>
	/// <seealso cref="YCQL.AlterBuilder"/>
	/// <seealso cref="YCQL.UpdateBuilder"/>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.DBColumn"/>
	/// <seealso cref="YCQL.JoinDefinition"/>
	/// <seealso cref="YCQL.Order"/>
	/// <seealso cref="YCQL.SQLAlias"/>
	public class SelectBuilder : ISQLBuilder, ISupportDistinct<SelectBuilder>, IJoinable<SelectBuilder>, ISupportWhere<SelectBuilder>, ISupportHaving<SelectBuilder>
	{
		/// <summary>
		/// List of column expressions
		/// </summary>
		List<object> _columnExpressions;
		/// <summary>
		/// List of order by expressions
		/// </summary>
		List<object> _orderByExpressions;
		/// <summary>
		/// List of group by expressions
		/// </summary>
		List<object> _groupByExpressions;
		/// <summary>
		/// List of join definitions
		/// </summary>
		List<JoinDefinition> _joinDefinitions;
		/// <summary>
		/// List of tables to select from
		/// </summary>
		List<object> _fromTableExpressions;
		/// <summary>
		/// The where clause for the query
		/// </summary>
		object _whereClause;
		/// <summary>
		/// The having clause for the query
		/// </summary>
		object _havingClause;
		/// <summary>
		/// The select top value for the query (for Sql Server)
		/// </summary>
		long _topRows;
		/// <summary>
		/// The limit start value for limit operator (for MySql)
		/// </summary>
		long _limitStart;
		/// <summary>
		/// The row count value for this limit operator (for MySql)
		/// </summary>
		long _limitRowCount;
		/// <summary>
		/// The order which the result should be in
		/// </summary>
		Order _order;
		/// <summary>
		/// Flag to indicate if Select Distinct should be used
		/// </summary>
		bool _isDistinct;

		/// <summary>
		/// Initializes a new instance of the SelectBuilder class
		/// </summary>
		public SelectBuilder()
		{
			_fromTableExpressions = new List<object>();
			_columnExpressions = new List<object>();
			_orderByExpressions = new List<object>();
			_groupByExpressions = new List<object>();
			_joinDefinitions = new List<JoinDefinition>();
			_isDistinct = false;

			_topRows = -1;
			_limitStart = -1;
			_limitRowCount = -1;

			_order = YCQL.Order.Unspecified;
		}

		/// <summary>
		/// Removes all the selected expressions from the to select list and adds the all ("*") character into the list
		/// </summary>
		/// <returns>A reference to this instance after the operation has been completed</returns>
		public SelectBuilder SelectAll()
		{
			_columnExpressions.Clear();
			_columnExpressions.Add(new SQLAllColumnSymbol());
			return this;
		}

		/// <summary>
		/// Selects one or more expressions
		/// </summary>
		/// <param name="expressions">The expression to be selected</param>
		/// <returns>A reference to this instance after the expressions has been added to the to select list</returns>
		public SelectBuilder Select(params object[] expressions)
		{
			_columnExpressions.AddRange(expressions);
			return this;
		}

		/// <summary>
		/// Adds an aliased column into the select list
		/// </summary>
		/// <param name="column">Column to be selected</param>
		/// <param name="aliasName">Alias name for the column</param>
		/// <returns>A reference to this instance after the expressions has been added to the to select list</returns>
		public SelectBuilder SelectWithAlias(DBColumn column, string aliasName)
		{
			return SelectWithAlias(column, aliasName);
		}

		/// <summary>
		/// Adds an aliased expression into the select list
		/// </summary>
		/// <param name="expression">Expression to be selected</param>
		/// <param name="aliasName">Alias name for the column</param>
		/// <returns>A reference to this instance after the expressions has been added to the to select list</returns>
		public SelectBuilder SelectWithAlias(object expression, string aliasName)
		{
			return SelectWithAlias(expression, new SQLAlias(aliasName));
		}

		/// <summary>
		/// Adds an aliased column into the select list
		/// </summary>
		/// <param name="column">Column to be selected</param>
		/// <param name="alias">Alias for the column</param>
		/// <returns>A reference to this instance after the expressions has been added to the to select list</returns>
		public SelectBuilder SelectWithAlias(DBColumn column, SQLAlias alias)
		{
			return SelectWithAlias(column, alias);
		}

		/// <summary>
		/// Adds an aliased expression into the select list
		/// </summary>
		/// <param name="expression">Expression to be selected</param>
		/// <param name="alias">Alias for the column</param>
		/// <returns>A reference to this instance after the expressions has been added to the to select list</returns>
		public SelectBuilder SelectWithAlias(object expression, SQLAlias alias)
		{
			_columnExpressions.Add(new SQLSourceAliasPair(expression, alias));
			return this;
		}

		/// <summary>
		/// Adds one or more table expressions into the From list using implicit join
		/// </summary>
		/// <param name="tableExpressions">Table expressions to be added to the From list</param>
		/// <returns>A reference to this instance after the table expressions has been added to the to from list</returns>
		public SelectBuilder From(params object[] tableExpressions)
		{
			_fromTableExpressions.AddRange(tableExpressions);
			return this;
		}

		/// <summary>
		/// Adds an aliased table into the From list using implicit join
		/// </summary>
		/// <param name="table">Table to be added into the From list</param>
		/// <param name="aliasName">Alias name of the table</param>
		/// <returns>A reference to this instance after the table expressions has been added to the to from list</returns>
		public SelectBuilder FromWithAlias(DBTable table, string aliasName)
		{
			return FromWithAlias(table, new SQLAlias(aliasName));
		}

		/// <summary>
		/// Adds an aliased subquery into the From list using implicit join
		/// </summary>
		/// <param name="tableExpression">Table expression to be added into the From list</param>
		/// <param name="aliasName">Alias name of the table</param>
		/// <returns>A reference to this instance after the table expressions has been added to the to from list</returns>
		public SelectBuilder FromWithAlias(SelectBuilder tableExpression, string aliasName)
		{
			return FromWithAlias(tableExpression, new SQLAlias(aliasName));
		}

		/// <summary>
		/// Adds an aliased table into the From list using implicit join
		/// </summary>
		/// <param name="table">Table to be added into the From list</param>
		/// <param name="alias">Alias for the table</param>
		/// <returns>A reference to this instance after the table expressions has been added to the to from list</returns>
		public SelectBuilder FromWithAlias(DBTable table, SQLAlias alias)
		{
			_fromTableExpressions.Add(new SQLSourceAliasPair(table, alias));
			return this;
		}

		/// <summary>
		/// Adds an aliased subquery into the From list using implicit join
		/// </summary>
		/// <param name="tableExpression">Table expression to be added into the From list</param>
		/// <param name="alias">Alias for the table</param>
		/// <returns>A reference to this instance after the table expressions has been added to the to from list</returns>
		public SelectBuilder FromWithAlias(SelectBuilder tableExpression, SQLAlias alias)
		{
			_fromTableExpressions.Add(new SQLSourceAliasPair(tableExpression, alias));
			return this;
		}

		/// <summary>
		/// Sets the Distinct flag to true
		/// </summary>
		/// <returns>A reference to this instance after the Distinct flag is set to true</returns>
		public SelectBuilder Distinct()
		{
			_isDistinct = true;
			return this;
		}

		/// <summary>
		/// Limits the select result to only return top x rows (for Sql Server)
		/// </summary>
		/// <param name="rowCount">Number of rows starting from the beginning of the result to be returned</param>
		/// <returns>A reference to this instance after the operation is completed</returns>
		public SelectBuilder Top(long rowCount)
		{
			_topRows = rowCount;
			return this;
		}

		/// <summary>
		/// Limits the select result to only return top x rows (for MySql)
		/// </summary>
		/// <param name="rowCount">Number of rows starting from the beginning of the result to be returned</param>
		/// <returns>A reference to this instance after the operation is completed</returns>
		public SelectBuilder Limit(long rowCount)
		{
			return Limit(rowCount, -1);
		}

		/// <summary>
		/// Limits the select result to only return a range of rows
		/// </summary>
		/// <param name="begin">The number of rows return result should skip from the query result</param>
		/// <param name="numRows">The number of rows to return</param>
		/// <returns>A reference to this instance after the operation is completed</returns>
		public SelectBuilder Limit(long begin, long numRows)
		{
			_limitStart = begin;
			_limitRowCount = numRows;

			return this;
		}

		/// <summary>
		/// Joins one or more tables
		/// </summary>
		/// <param name="joinDefinitions">Join definitions to be joined</param>
		/// <returns>A reference to this instance after the join definitions has been added to the join list</returns>
		public SelectBuilder Join(params JoinDefinition[] joinDefinitions)
		{
			_joinDefinitions.AddRange(joinDefinitions);
			return this;
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="expression">A boolean expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(BooleanExpression expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="clause">A logical clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(LogicalClause clause)
		{
			return Where((object) clause);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="expression">An all operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(AllOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="expression">An any operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(AnyOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="expression">An exists operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(ExistsOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="expression">An in operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(InOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="clause">A clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public SelectBuilder Where(object clause)
		{
			_whereClause = clause;
			return this;
		}

		/// <summary>
		/// Adds one or more expressions to be used for group by clause
		/// </summary>
		/// <param name="expressions">Expressions to be used for the query</param>
		/// <returns>A reference to this instance after the expressions has been added to group by list</returns>
		public SelectBuilder GroupBy(params object[] expressions)
		{
			_groupByExpressions.AddRange(expressions);
			return this;
		}

		/// <summary>
		/// Adds a boolean expression into the having clause
		/// </summary>
		/// <param name="expression">Boolean expression to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(BooleanExpression expression)
		{
			return Having((object) expression);
		}

		/// <summary>
		/// Adds a logical clause into the having clause
		/// </summary>
		/// <param name="clause">Logical clause to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(LogicalClause clause)
		{
			return Having((object) clause);
		}

		/// <summary>
		/// Adds an all operator expression into the having clause
		/// </summary>
		/// <param name="expression">All operator expression to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(AllOperator expression)
		{
			return Having((object) expression);
		}

		/// <summary>
		/// Adds an any operator expression into the having clause
		/// </summary>
		/// <param name="expression">Any operator expression to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(AnyOperator expression)
		{
			return Having((object) expression);
		}

		/// <summary>
		/// Adds an exists operator expression into the having clause
		/// </summary>
		/// <param name="expression">Exists operator expression to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(ExistsOperator expression)
		{
			return Having((object) expression);
		}

		/// <summary>
		/// Adds an in operator expression into the having clause
		/// </summary>
		/// <param name="expression">In operator expression to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(InOperator expression)
		{
			return Having((object) expression);
		}

		/// <summary>
		/// Adds a clause into the having clause
		/// </summary>
		/// <param name="clause">Clause to be added into the having clause</param>
		/// <returns>A reference to this instance after the expression has been added to having list</returns>
		public SelectBuilder Having(object clause)
		{
			_havingClause = clause;
			return this;
		}

		/// <summary>
		/// Adds one or more expressions into the order by clause
		/// </summary>
		/// <param name="expressions">Expressions to be added into the order by clause</param>
		/// <returns>A reference to this instance after the expressions has been added to order by list</returns>
		public SelectBuilder OrderBy(params object[] expressions)
		{
			_orderByExpressions.AddRange(expressions);
			return this;
		}

		/// <summary>
		/// Orders the return result with the specified order
		/// </summary>
		/// <param name="order">Order of the result should be ordered</param>
		/// <returns>A reference to this instance after the new order has been set</returns>
		public SelectBuilder Order(Order order)
		{
			_order = order;
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
			sb.Append("SELECT");
			if (_isDistinct)
				sb.Append(" DISTINCT");

			if (_topRows >= 0 && dbHelper.DBEngine == DBEngine.SQLServer)
				sb.AppendFormat(" TOP {0}", _topRows);

			sb.AppendFormat(" {0}", dbHelper.TranslateObjectsToSQLString(_columnExpressions, parameterCollection));

			if (_fromTableExpressions.Count > 0)
				sb.AppendFormat(" FROM {0}", dbHelper.TranslateObjectsToSQLString(_fromTableExpressions, parameterCollection));
			sb.AppendLine();

			foreach (JoinDefinition jd in _joinDefinitions)
				sb.AppendLine(jd.ToSQL(dbHelper, parameterCollection));

			if (!_whereClause.IsNullOrEmpty())
				sb.AppendFormat(" WHERE {0}", dbHelper.TranslateObjectToSQLString(_whereClause, parameterCollection));

			if (_groupByExpressions.Count > 0)
				sb.AppendFormat(" GROUP BY {0}", string.Join(",", dbHelper.TranslateObjectsToSQLString(_groupByExpressions, parameterCollection)));

			if (!_havingClause.IsNullOrEmpty())
				sb.AppendFormat(" HAVING {0}", dbHelper.TranslateObjectToSQLString(_havingClause, parameterCollection));

			if (_orderByExpressions.Count > 0)
			{
				sb.AppendFormat(" ORDER BY {0}", string.Join(",", dbHelper.TranslateObjectsToSQLString(_orderByExpressions, parameterCollection)));

				if (_order != YCQL.Order.Unspecified)
					sb.Append(" " + _order.ToString());
			}

			if (dbHelper.DBEngine == DBEngine.MySQL)
			{
				if (_limitStart >= 0)
					sb.AppendFormat(" LIMIT {0}, {1}", _limitStart, _limitRowCount);
				else if (_limitRowCount > 0)
					sb.AppendFormat(" LIMIT {0}", _limitRowCount);
			}

			return sb.ToString();
		}
	}
}
