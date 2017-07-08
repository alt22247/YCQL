/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents the In operator in Sql
	/// </summary>
	/// <seealso cref="Ycql.AnyOperator"/>
	/// <seealso cref="Ycql.AllOperator"/>
	/// <seealso cref="Ycql.ExistsOperator"/>
	public class InOperator : IProduceBoolean<InOperator>, ITranslateSql
	{
		object _lhsExpression;
		IEnumerable<object> _inExpressions;
		bool _not;

		/// <summary>
		/// Initializes a new instance of the InOperator class using specified left hand side expression and one or more expressions to test for a match
		/// </summary>
		/// <param name="lhsExpression">The expression at the left hand side of the In operator</param>
		/// <param name="inExpressions">A single subquery or a list of expressions to test for a match</param>
		public InOperator(object lhsExpression, params object[] inExpressions)
		{
			_not = false;
			_lhsExpression = lhsExpression;
			if (inExpressions != null)
				_inExpressions = inExpressions.Unwrap();
		}

		/// <summary>
		/// Negates the expression result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		public InOperator Not()
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
			sb.Append(dbHelper.TranslateObjectToSqlString(_lhsExpression, parameterCollection));

			if (_not)
				sb.Append(" NOT ");

			sb.AppendFormat(" IN ({0}))", dbHelper.TranslateObjectsToSqlString(_inExpressions, parameterCollection));
			return sb.ToString();
		}
	}
}
