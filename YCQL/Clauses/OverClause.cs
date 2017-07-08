/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

#if YCQL_SQLSERVER
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents an over clause in Sql (for Sql Server)
	/// </summary>
	/// <seealso cref="Ycql.DbColumn"/>
	public class OverClause : ITranslateSql
	{
		/// <summary>
		/// Left hand side value of the clause
		/// </summary>
		object _lhs;
		/// <summary>
		/// List of partition by expressions
		/// </summary>
		List<object> _partitionByExpressions;
		/// <summary>
		/// List of order by expressions
		/// </summary>
		List<object> _orderByExpressions;
		/// <summary>
		/// Order to be used for the over clause
		/// </summary>
		Order _order;

		/// <summary>
		/// Initializes a new instance of the OverClause class using specified column
		/// </summary>
		/// <param name="column">Column to be used for the over clause</param>
		public OverClause(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the OverClause class using specified left hand side expression
		/// </summary>
		/// <param name="leftHandSide">Left hand side expression to be used for the over clause</param>
		public OverClause(object leftHandSide)
		{
			_lhs = leftHandSide;
			_orderByExpressions = new List<object>();
			_partitionByExpressions = new List<object>();

			_order = 0;
		}

		/// <summary>
		/// Adds one or more expressions to the partition by clause
		/// </summary>
		/// <param name="expressions">Expressions to be added to partitioned by clause</param>
		/// <returns>A reference to this instance after the expressions have been added to the partition by list</returns>
		public OverClause PartitionBy(params object[] expressions)
		{
			if (expressions == null)
				return this;

			_partitionByExpressions.AddRange(expressions.Unwrap());
			return this;
		}

		/// <summary>
		/// Adds one or more expressions to the order by clause
		/// </summary>
		/// <param name="expressions">Expressions to be added to order by clause</param>
		/// <returns>A reference to this instance after the expressions have been added to the order by list</returns>
		public OverClause OrderBy(params object[] expressions)
		{
			if (expressions == null)
				return this;

			_orderByExpressions.AddRange(expressions.Unwrap());
			return this;
		}

		/// <summary>
		/// Sets the order to be used for the over clause
		/// </summary>
		/// <param name="order">Order to be used for the over clause</param>
		/// <returns>A reference to this instance after the new order has been set</returns>
		public OverClause Order(Order order)
		{
			_order = order;
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
			sb.AppendFormat("{0} OVER (", dbHelper.TranslateObjectToSqlString(_lhs, parameterCollection));
			if (_partitionByExpressions.Count > 0)
			{
				sb.AppendFormat(" PARTITION BY {0}", dbHelper.TranslateObjectsToSqlString(_partitionByExpressions, parameterCollection));
			}

			if (_orderByExpressions.Count > 0)
			{
				sb.AppendFormat(" ORDER BY {0}", dbHelper.TranslateObjectsToSqlString(_orderByExpressions, parameterCollection));

				if (_order != 0)
					sb.AppendFormat(" {0}", _order);
			}

			return sb.ToString();
		}
	}
}
#endif