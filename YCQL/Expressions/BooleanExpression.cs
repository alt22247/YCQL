/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;
using Ycql;

namespace Ycql
{
	/// <summary>
	/// Represents an expression which will produce boolean result
	/// </summary>
	/// <seealso cref="Ycql.ComparisonOperator"/>
	/// <seealso cref="Ycql.MathExpression"/>
	/// <seealso cref="Ycql.AllOperator"/>
	/// <seealso cref="Ycql.AnyOperator"/>
	/// <seealso cref="Ycql.ExistsOperator"/>
	/// <seealso cref="Ycql.InOperator"/>
	public class BooleanExpression : IProduceBoolean<BooleanExpression>, ITranslateSql
	{
		object _lhs;
		object _rhs;
		ComparisonOperator _op;
		bool _isNot;

		/// <summary>
		/// Initializes a new instance of the BooleanExpression class using specified leftHandSide, comparison operator and rightHandSide value
		/// </summary>
		/// <param name="leftHandSide">Column which would act as left hand side expression</param>
		/// <param name="op">comparison operator</param>
		/// <param name="rightHandSide">Right hand side expression</param>
		public BooleanExpression(DbColumn leftHandSide, ComparisonOperator op, object rightHandSide)
			: this((object) leftHandSide, op, rightHandSide)
		{
		}

		/// <summary>
		/// Initializes a new instance of the BooleanExpression class using specified leftHandSide, comparison operator and rightHandSide value
		/// </summary>
		/// <param name="leftHandSide">Left hand side expression</param>
		/// <param name="op">comparison operator</param>
		/// <param name="rightHandSide">Column which would act as right hand side expression</param>
		public BooleanExpression(object leftHandSide, ComparisonOperator op, DbColumn rightHandSide)
			: this(leftHandSide, op, (object) rightHandSide)
		{
		}

		/// <summary>
		/// Initializes a new instance of the BooleanExpression class using specified leftHandSide, comparison operator and rightHandSide value
		/// </summary>
		/// <param name="leftHandSide">Column which would act as left hand side expression</param>
		/// <param name="op">comparison operator</param>
		/// <param name="rightHandSide">Column which would act as right hand side expression</param>
		public BooleanExpression(DbColumn leftHandSide, ComparisonOperator op, DbColumn rightHandSide)
			: this((object) leftHandSide, op, (object) rightHandSide)
		{
		}

		/// <summary>
		/// Initializes a new instance of the BooleanExpression class using specified leftHandSide, comparison operator and rightHandSide value
		/// </summary>
		/// <param name="leftHandSide">Left hand side expression</param>
		/// <param name="op">comparison operator</param>
		/// <param name="rightHandSide">Right hand side expression</param>
		public BooleanExpression(object leftHandSide, ComparisonOperator op, object rightHandSide)
		{
			_lhs = leftHandSide;
			_rhs = rightHandSide;
			_op = op;
			_isNot = false;
		}

		/// <summary>
		/// Negates the expression result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		public BooleanExpression Not()
		{
			_isNot = true;
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
			if (_isNot)
				sb.Append(" NOT ");

			sb.AppendFormat("{0} {1} ", dbHelper.TranslateObjectToSqlString(_lhs, parameterCollection), _op.ToSql());
			if (_op == ComparisonOperator.Is && _rhs == null)
				sb.Append("NULL");
			else
				sb.Append(dbHelper.TranslateObjectToSqlString(_rhs, parameterCollection));

			sb.Append(")");

			return sb.ToString();
		}
	}
}
