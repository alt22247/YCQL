/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Extensions;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents the all operator in Sql. Something like A >= ALL (SELECT B FROM C)
	/// </summary>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.ExistsOperator"/>
	/// <seealso cref="YCQL.InOperator"/>
	public class AllOperator : IProduceBoolean<AllOperator>, ITranslateSQL
	{
		object _expression;
		ComparisonOperator _op;
		SelectBuilder _subQuery;
		bool _not;

		/// <summary>
		/// Initializes a new instance of the AllOperator class using specified DBColumn, ComparisonOperator and subquery
		/// </summary>
		/// <param name="column">The column which will be on the left hand side of this operator</param>
		/// <param name="op">A comparison operator</param>
		/// <param name="subQuery">A subquery that returns a result set of one column</param>
		public AllOperator(DBColumn column, ComparisonOperator op, SelectBuilder subQuery)
			: this((object) column, op, subQuery)
		{
		}

		/// <summary>
		/// Initializes a new instance of the AllOperator class using specified expression, ComparisonOperator and subquery
		/// </summary>
		/// <param name="expression">The expression which will be on the left hand side of this operator</param>
		/// <param name="op">A comparison operator</param>
		/// <param name="subQuery">A subquery that returns a result set of one column</param>
		public AllOperator(object expression, ComparisonOperator op, SelectBuilder subQuery)
		{
			_expression = expression;
			_op = op;
			_subQuery = subQuery;
		}

		/// <summary>
		/// Negates the expression result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		public AllOperator Not()
		{
			_not = true;
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
			sb.Append("(");
			if (_not)
				sb.Append(" NOT ");

			sb.AppendFormat("{0} {1} ALL ({2})", dbHelper.TranslateObjectToSQLString(_expression, parameterCollection),
														_op.ToSQL(), _subQuery.ToSQL(dbHelper, parameterCollection));

			sb.Append(")");
			return sb.ToString();
		}
	}
}
