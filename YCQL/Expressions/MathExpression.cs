/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Extensions;
using YCQL.Interfaces;
using YCQL;

namespace YCQL
{
	/// <summary>
	/// Represents one or more expressions which will be connected with math operator
	/// </summary>
	/// <seealso cref="YCQL.MathOperator"/>
	/// <seealso cref="YCQL.BooleanExpression"/>
	/// <seealso cref="YCQL.AllOperator"/>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.ExistsOperator"/>
	/// <seealso cref="YCQL.InOperator"/>
	public class MathExpression : ITranslateSQL
	{
		object _initialElement;
		List<Tuple<MathOperator, object>> _terms;
		/// <summary>
		/// Initializes a new instance of the MathExpression class using specified column as initial term
		/// </summary>
		/// <param name="initialElement">The left most term of this expression</param>
		public MathExpression(DBColumn initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the MathExpression class using specified initial term
		/// </summary>
		/// <param name="initialElement">The left most term of this expression</param>
		public MathExpression(object initialElement)
		{
			_initialElement = initialElement;
			_terms = new List<Tuple<MathOperator, object>>();
		}

		/// <summary>
		/// Appends an additional term to this expression
		/// </summary>
		/// <param name="op">The math operator to be applied to the new value</param>
		/// <param name="value">The new value to be appended to this expression</param>
		/// <returns>A reference to this instance after the new term has been added</returns>
		public MathExpression AddTerm(MathOperator op, object value)
		{
			_terms.Add(new Tuple<MathOperator, object>(op, value));
			return this;
		}

		/// <summary>
		/// Appends an additional term to this expression and connect it using the '+' operator
		/// </summary>
		/// <param name="term">The new value to be appended to this expression</param>
		/// <returns>A reference to this instance after the new term has been added</returns>
		public MathExpression Plus(object term)
		{
			AddTerm(MathOperator.Plus, term);
			return this;
		}

		/// <summary>
		/// Appends an additional term to this expression and connect it using the '-' operator
		/// </summary>
		/// <param name="term">The new value to be appended to this expression</param>
		/// <returns>A reference to this instance after the new term has been added</returns>
		public MathExpression Minus(object term)
		{
			AddTerm(MathOperator.Minus, term);
			return this;
		}

		/// <summary>
		/// Appends an additional term to this expression and connect it using the '*' operator
		/// </summary>
		/// <param name="term">The new value to be appended to this expression</param>
		/// <returns>A reference to this instance after the new term has been added</returns>
		public MathExpression Multiply(object term)
		{
			AddTerm(MathOperator.Multiply, term);
			return this;
		}

		/// <summary>
		/// Appends an additional term to this expression and connect it using the '/' operator
		/// </summary>
		/// <param name="term">The new value to be appended to this expression</param>
		/// <returns>A reference to this instance after the new term has been added</returns>
		public MathExpression Divide(object term)
		{
			AddTerm(MathOperator.Divide, term);
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
			sb.Append(dbHelper.TranslateObjectToSQLString(_initialElement, parameterCollection));

			foreach (Tuple<MathOperator, object> term in _terms)
				sb.AppendFormat(" {0} {1}", term.Item1.ToSQL(), dbHelper.TranslateObjectToSQLString(term.Item2, parameterCollection));

			sb.Append(")");

			return sb.ToString();
		}
	}
}
