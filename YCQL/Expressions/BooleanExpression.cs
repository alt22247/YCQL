/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Extensions;
using YCQL.Interfaces;
using YCQL;

namespace YCQL
{
	/// <summary>
	/// Represents an expression which will produce boolean result
	/// </summary>
	/// <seealso cref="YCQL.ComparisonOperator"/>
	/// <seealso cref="YCQL.MathExpression"/>
	/// <seealso cref="YCQL.AllOperator"/>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.ExistsOperator"/>
	/// <seealso cref="YCQL.InOperator"/>
	public class BooleanExpression : IProduceBoolean<BooleanExpression>, ITranslateSQL
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
		public BooleanExpression(DBColumn leftHandSide, ComparisonOperator op, object rightHandSide)
			: this((object) leftHandSide, op, rightHandSide)
		{
		}

		/// <summary>
		/// Initializes a new instance of the BooleanExpression class using specified leftHandSide, comparison operator and rightHandSide value
		/// </summary>
		/// <param name="leftHandSide">Left hand side expression</param>
		/// <param name="op">comparison operator</param>
		/// <param name="rightHandSide">Column which would act as right hand side expression</param>
		public BooleanExpression(object leftHandSide, ComparisonOperator op, DBColumn rightHandSide)
			: this(leftHandSide, op, (object) rightHandSide)
		{
		}

		/// <summary>
		/// Initializes a new instance of the BooleanExpression class using specified leftHandSide, comparison operator and rightHandSide value
		/// </summary>
		/// <param name="leftHandSide">Column which would act as left hand side expression</param>
		/// <param name="op">comparison operator</param>
		/// <param name="rightHandSide">Column which would act as right hand side expression</param>
		public BooleanExpression(DBColumn leftHandSide, ComparisonOperator op, DBColumn rightHandSide)
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
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			if (_isNot)
				sb.Append(" NOT ");

			sb.AppendFormat("{0} {1} {2}", dbHelper.TranslateObjectToSQLString(_lhs, parameterCollection), _op.ToSQL(),
									dbHelper.TranslateObjectToSQLString(_rhs, parameterCollection));
			sb.Append(")");

			return sb.ToString();
		}
	}
}
