/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using Ycql;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents the all operator in Sql. Something like A >= ANY (SELECT B FROM C)
	/// </summary>
	/// <seealso cref="Ycql.AllOperator"/>
	/// <seealso cref="Ycql.ExistsOperator"/>
	/// <seealso cref="Ycql.InOperator"/>
	public class AnyOperator : IProduceBoolean<AnyOperator>, ITranslateSql
	{
		object _expression;
		ComparisonOperator _op;
		SelectBuilder _subQuery;
		bool _not;

		/// <summary>
		/// Initializes a new instance of the AnyOperator class using specified DBColumn, ComparisonOperator and subquery
		/// </summary>
		/// <param name="column">The column which will be on the left hand side of this operator</param>
		/// <param name="op">A comparison operator</param>
		/// <param name="subQuery">A subquery that returns a result set of one column</param>
		public AnyOperator(DbColumn column, ComparisonOperator op, SelectBuilder subQuery)
			: this((object) column, op, subQuery)
		{
		}

		/// <summary>
		/// Initializes a new instance of the AnyOperator class using specified expression, ComparisonOperator and subquery
		/// </summary>
		/// <param name="expression">The expression which will be on the left hand side of this operator</param>
		/// <param name="op">A comparison operator</param>
		/// <param name="subQuery">A subquery that returns a result set of one column</param>
		public AnyOperator(object expression, ComparisonOperator op, SelectBuilder subQuery)
		{
			_expression = expression;
			_op = op;
			_subQuery = subQuery;
		}

		/// <summary>
		/// Negates the expression result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		public AnyOperator Not()
		{
			_not = true;
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
			sb.Append("(");
			if (_not)
				sb.Append(" NOT ");

			sb.AppendFormat("{0} {1} ANY ({2})", dbHelper.TranslateObjectToSqlString(_expression, parameterCollection),
														_op.ToSql(), _subQuery.ToSql(dbVersion, parameterCollection));

			sb.Append(")");
			return sb.ToString();
		}
	}
}
