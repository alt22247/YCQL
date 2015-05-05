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

namespace YCQL
{
	enum LogicalConnective
	{
		AND,
		OR
	}

	/// <summary>
	/// Represents one or more expressions joined by logical connectives
	/// </summary>
	/// <seealso cref="YCQL.BooleanExpression"/>
	/// <seealso cref="YCQL.AllOperator"/>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.ExistsOperator"/>
	/// <seealso cref="YCQL.InOperator"/>
	public class LogicalClause : ITranslateSQL, IEmptiable, IProduceBoolean<LogicalClause>
	{
		/// <summary>
		/// The initial element of the clause
		/// </summary>
		object _initialElement;
		/// <summary>
		/// List of expressions and the connective associated with them
		/// </summary>
		List<Tuple<LogicalConnective, object>> _expressions;
		/// <summary>
		/// Flag to indicate if this clause result should be negated
		/// </summary>
		bool _isNot;

		/// <summary>
		/// Initializes a new instance of the LogicalClause class
		/// </summary>
		public LogicalClause()
			: this((object) null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(BooleanExpression initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(LogicalClause initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(AllOperator initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(AnyOperator initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(ExistsOperator initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(InOperator initialElement)
			: this((object) initialElement)
		{
		}

		/// <summary>
		/// Initializes a new instance of the LogicalClause class using specified initial element
		/// </summary>
		/// <param name="initialElement">The initial element of this clause</param>
		public LogicalClause(object initialElement)
		{
			_initialElement = initialElement;
			_expressions = new List<Tuple<LogicalConnective, object>>();
		}

		/// <summary>
		/// Appends one more element to the clause using specified logical connective
		/// </summary>
		/// <param name="connective">Logical connective to connect the clause and the new element</param>
		/// <param name="element">The element to be added to the clause</param>
		/// <returns>A reference to this instance after the new element has been added</returns>
		LogicalClause AddElement(LogicalConnective connective, object element)
		{
			if (_initialElement == null)
				_initialElement = element;
			else
				_expressions.Add(new Tuple<LogicalConnective, object>(connective, element));

			return this;
		}

		/// <summary>
		/// Append an element to the clause using And connective
		/// </summary>
		/// <param name="element">Element to be added to the clause</param>
		/// <returns>A reference to this instance after the new element has been added</returns>
		public LogicalClause And(object element)
		{
			return AddElement(LogicalConnective.AND, element);
		}

		/// <summary>
		/// Append an element to the clause using Or connective
		/// </summary>
		/// <param name="element">Element to be added to the clause</param>
		/// <returns>A reference to this instance after the new element has been added</returns>
		public LogicalClause Or(object element)
		{
			return AddElement(LogicalConnective.OR, element);
		}

		/// <summary>
		/// Negates the clause result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		public LogicalClause Not()
		{
			_isNot = true;
			return this;
		}

		/// <summary>
		/// Checks if the expression is empty or not
		/// </summary>
		/// <returns>A boolean indicating if the expression is empty or not</returns>
		public bool HasContent()
		{
			if (_expressions.Count == 0)
				return false;

			foreach (object element in _expressions)
			{
				if (!element.IsNullOrEmpty())
					return true;
			}

			return false;
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
			if (_isNot)
				sb.Append("NOT");

			sb.Append("(");
			sb.Append(dbHelper.TranslateObjectToSQLString(_initialElement, parameterCollection));

			foreach (Tuple<LogicalConnective, object> expression in _expressions)
			{
				if (expression.Item2.IsNullOrEmpty())
					continue;

				sb.AppendFormat(" {0} {1}", expression.Item1, dbHelper.TranslateObjectToSQLString(expression.Item2, parameterCollection));
			}

			sb.Append(" )");

			return sb.ToString();
		}
	}
}
