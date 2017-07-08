/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Hints;
using Ycql.Interfaces;

namespace Ycql
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
	/// <seealso cref="Ycql.CreateBuilder"/>
	/// <seealso cref="Ycql.DeleteBuilder"/>
	/// <seealso cref="Ycql.InsertBuilder"/>
	/// <seealso cref="Ycql.AlterBuilder"/>
	/// <seealso cref="Ycql.UpdateBuilder"/>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.DbColumn"/>
	/// <seealso cref="Ycql.JoinDefinition"/>
	/// <seealso cref="Ycql.Order"/>
	/// <seealso cref="Ycql.SqlAlias"/>
	public class SelectBuilder : ISqlBuilder, ISupportDistinct<SelectBuilder>, IJoinable<SelectBuilder>, ISupportWhere<SelectBuilder>, ISupportHaving<SelectBuilder>
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
		/// List index and order for order by expressions
		/// </summary>
		Dictionary<int, Order> _indexOrderDict;
		/// <summary>
		/// List of group by expressions
		/// </summary>
		List<object> _groupByExpressions;
		/// <summary>
		/// List of join definitions
		/// </summary>
		List<JoinDefinition> _joinDefinitions;
		/// <summary>
		/// List of unions
		/// </summary>
		List<Tuple<bool, SelectBuilder>> _unions;
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
#if YCQL_SQLSERVER
		/// <summary>
		/// The select top value for the query (for Sql Server)
		/// </summary>
		long _topRows;
		/// <summary>
		/// The number of rows to offset (for Sql Server)
		/// </summary>
		object _offsetNumRows;
		/// <summary>
		/// The number of rows to fetch after the offset (for Sql Server)
		/// </summary>
		object _fetchNextNumRows;
		/// <summary>
		/// List of table expression index and list of Sql Server table hints
		/// </summary>
		Dictionary<int, SqlServerTableHint[]> _indexSSHintDict;
#endif
#if YCQL_MYSQL
		/// <summary>
		/// The limit start value for limit operator (for MySql)
		/// </summary>
		long _limitStart;
		/// <summary>
		/// The row count value for this limit operator (for MySql)
		/// </summary>
		long _limitRowCount;
#endif
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
			_indexOrderDict = new Dictionary<int, Order>();
			_groupByExpressions = new List<object>();
			_joinDefinitions = new List<JoinDefinition>();
			_unions = new List<Tuple<bool, SelectBuilder>>();
			_isDistinct = false;
#if YCQL_SQLSERVER
			_topRows = -1;
			_indexSSHintDict = new Dictionary<int, SqlServerTableHint[]>();
#endif
#if YCQL_MYSQL
			_limitStart = -1;
			_limitRowCount = -1;
#endif
		}

		/// <summary>
		/// Removes all the selected expressions from the to select list and adds the all ("*") character into the list
		/// </summary>
		/// <returns>A reference to this instance after the operation has been completed</returns>
		public SelectBuilder SelectAll()
		{
			_columnExpressions.Clear();
			_columnExpressions.Add(new SqlAllColumnSymbol());
			return this;
		}

		/// <summary>
		/// Selects one or more expressions
		/// </summary>
		/// <param name="expressions">The expression to be selected</param>
		/// <returns>A reference to this instance after the expressions has been added to the to select list</returns>
		public SelectBuilder Select(params object[] expressions)
		{
			if (expressions == null)
				return this;

			_columnExpressions.AddRange(expressions.Unwrap());
			return this;
		}

		/// <summary>
		/// Adds one or more table expressions into the From list using implicit join
		/// </summary>
		/// <param name="tableExpressions">Table expressions to be added to the From list</param>
		/// <returns>A reference to this instance after the table expressions has been added to the to from list</returns>
		public SelectBuilder From(params object[] tableExpressions)
		{
			if (tableExpressions == null)
				return this;

			_fromTableExpressions.AddRange(tableExpressions.Unwrap());
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

#if YCQL_SQLSERVER
		/// <summary>
		/// Adds one table expressions into the From list with given table hints
		/// </summary>
		/// <param name="tableExpression">Table expression to be added to the From list</param>
		/// <param name="tableHints">Table hints to be used for that table expression</param>
		/// <returns></returns>
		public SelectBuilder FromWith(object tableExpression, params SqlServerTableHint[] tableHints)
		{
			_indexSSHintDict.Add(_fromTableExpressions.Count, tableHints);
			_fromTableExpressions.Add(tableExpression);
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
		/// Specifies the number of rows to skip, before starting to return rows from the query expression (for Sql Server)
		/// </summary>
		/// <param name="numRowsExpression">Number of rows to skip</param>
		/// <returns></returns>
		public SelectBuilder Offset(object numRowsExpression)
		{
			_offsetNumRows = numRowsExpression;
			return this;
		}

		/// <summary>
		/// Specifies the number of rows to skip, before starting to return rows from the query expression (for Sql Server)
		/// </summary>
		/// <param name="numRows">Number of rows to skip</param>
		/// <returns></returns>
		public SelectBuilder Offset(long numRows)
		{
			return Offset((object) numRows);
		}

		/// <summary>
		/// Specifies the number of rows to return, after processing the OFFSET clause
		/// </summary>
		/// <param name="numRowsExpression">Number of rows to return</param>
		/// <returns></returns>
		public SelectBuilder FetchNext(object numRowsExpression)
		{
			_fetchNextNumRows = numRowsExpression;
			return this;
		}

		/// <summary>
		/// Specifies the number of rows to return, after processing the OFFSET clause
		/// </summary>
		/// <param name="numRows">Number of rows to return</param>
		/// <returns></returns>
		public SelectBuilder FetchNext(long numRows)
		{
			return FetchNext((object) numRows);
		}
#endif

#if YCQL_MYSQL
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
		/// Limits the select result to only return a range of rows (for MySql)
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
#endif

		/// <summary>
		/// Joins one or more tables
		/// </summary>
		/// <param name="joinDefinitions">Join definitions to be joined</param>
		/// <returns>A reference to this instance after the join definitions has been added to the join list</returns>
		public SelectBuilder Join(params JoinDefinition[] joinDefinitions)
		{
			_joinDefinitions.AddRange(joinDefinitions.Unwrap<JoinDefinition>());
			return this;
		}

		/// <summary>
		/// Unions one or more queries
		/// </summary>
		/// <param name="queriesToUnion">Queries to be unioned</param>
		/// <returns>A reference to this instance after the queries has been added to the union list</returns>
		public SelectBuilder Union(params SelectBuilder[] queriesToUnion)
		{
			foreach (SelectBuilder sb in queriesToUnion.Unwrap<SelectBuilder>())
				_unions.Add(new Tuple<bool, SelectBuilder>(false, sb));

			return this;
		}

		/// <summary>
		/// Union All one or more queries
		/// </summary>
		/// <param name="queriesToUnion">Queries to be unioned</param>
		/// <returns>A reference to this instance after the queries has been added to the union list</returns>
		public SelectBuilder UnionAll(params SelectBuilder[] queriesToUnion)
		{
			foreach (SelectBuilder sb in queriesToUnion.Unwrap<SelectBuilder>())
				_unions.Add(new Tuple<bool, SelectBuilder>(true, sb));

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
			if (expressions == null)
				return this;

			_groupByExpressions.AddRange(expressions.Unwrap());
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
		/// Adds an expression into the order by clause using given order
		/// </summary>
		/// <param name="expression">Expression to be added into the order by clause</param>
		/// <param name="order">Order to be used</param>
		/// <returns>A reference to this instance after the expressions has been added to order by list</returns>
		public SelectBuilder OrderBy(object expression, Order order)
		{
			_indexOrderDict[_orderByExpressions.Count] = order;
			return OrderBy(expression);
		}

		/// <summary>
		/// Adds one or more expressions into the order by clause using default order
		/// </summary>
		/// <param name="expressions">Expression to be added into the order by clause</param>
		/// <returns>A reference to this instance after the expressions has been added to order by list</returns>
		public SelectBuilder OrderBy(params object[] expressions)
		{
			_orderByExpressions.AddRange(expressions);
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
			sb.Append("SELECT");
			if (_isDistinct)
				sb.Append(" DISTINCT");

#if YCQL_SQLSERVER
			if (_topRows >= 0 && dbHelper.DbEngine == DbEngine.SqlServer)
				sb.AppendFormat(" TOP {0}", _topRows);
#endif

			sb.AppendFormat(" {0}", dbHelper.TranslateObjectsToSqlString(_columnExpressions, parameterCollection));

			if (_fromTableExpressions.Count > 0)
			{
				sb.Append(" FROM ");
				for (int i = 0; i < _fromTableExpressions.Count; i++)
				{
					sb.Append(dbHelper.TranslateObjectToSqlString(_fromTableExpressions[i], parameterCollection));
#if YCQL_SQLSERVER
					if (_indexSSHintDict.ContainsKey(i))
						sb.AppendFormat(" WITH ({0})", dbHelper.TranslateObjectsToSqlString(_indexSSHintDict[i], parameterCollection));
#endif
					if (i != _fromTableExpressions.Count - 1)
						sb.Append(", ");
				}
			}
			sb.AppendLine();

			foreach (JoinDefinition jd in _joinDefinitions)
				sb.AppendLine(jd.ToSql(dbVersion, parameterCollection));

			if (!_whereClause.IsNullOrEmpty())
				sb.AppendFormat(" WHERE {0}", dbHelper.TranslateObjectToSqlString(_whereClause, parameterCollection));

			if (_groupByExpressions.Count > 0)
				sb.AppendFormat(" GROUP BY {0}", string.Join(",", dbHelper.TranslateObjectsToSqlString(_groupByExpressions, parameterCollection)));

			if (!_havingClause.IsNullOrEmpty())
				sb.AppendFormat(" HAVING {0}", dbHelper.TranslateObjectToSqlString(_havingClause, parameterCollection));

			foreach (Tuple<bool, SelectBuilder> union in _unions)
			{
				sb.AppendLine();
				sb.Append("UNION ");
				if (union.Item1)
					sb.Append("ALL ");
				sb.AppendLine("(");
				sb.Append(union.Item2.ToSql(dbVersion, parameterCollection));
				sb.Append(")");
			}

			if (_orderByExpressions.Count > 0)
			{
				List<string> orderByStrings = new List<string>();
				for (int i = 0; i < _orderByExpressions.Count; i++)
				{
					string orderByString = dbHelper.TranslateObjectToSqlString(_orderByExpressions[i], parameterCollection);

					if(_indexOrderDict.ContainsKey(i))
					{
						Order order = _indexOrderDict[i];
						if (order != Order.Unspecified)
							orderByString += " " + order.ToString();
					}

					orderByStrings.Add(orderByString);
				}

				sb.AppendFormat(" ORDER BY {0}", string.Join(",", orderByStrings));

#if YCQL_SQLSERVER
				if (_offsetNumRows != null)
					sb.AppendFormat(" OFFSET {0} ROWS", dbHelper.TranslateObjectToSqlString(_offsetNumRows, parameterCollection));

				if (_fetchNextNumRows != null)
					sb.AppendFormat(" FETCH NEXT {0} ROWS ONLY", dbHelper.TranslateObjectToSqlString(_fetchNextNumRows, parameterCollection));
#endif
			}

#if YCQL_MYSQL
			if (dbHelper.DbEngine == DbEngine.MySql)
			{
				if (_limitStart >= 0)
					sb.AppendFormat(" LIMIT {0}, {1}", _limitStart, _limitRowCount);
				else if (_limitRowCount > 0)
					sb.AppendFormat(" LIMIT {0}", _limitRowCount);
			}
#endif
			return sb.ToString();
		}
	}
}
