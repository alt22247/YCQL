/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.DBHelpers;
using YCQL.Interfaces;
using System.Data.Common;
using System.Text;

namespace YCQL
{
	/// <summary>
	/// Represents the In operator in Sql
	/// </summary>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.AllOperator"/>
	/// <seealso cref="YCQL.ExistsOperator"/>
	public class InOperator : IProduceBoolean<InOperator>, ITranslateSQL
	{
		object _lhsExpression;
		object[] _inExpressions;
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
			_inExpressions = inExpressions;
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
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			sb.Append(dbHelper.TranslateObjectToSQLString(_lhsExpression, parameterCollection));

			if (_not)
				sb.Append(" NOT ");

			sb.AppendFormat(" IN ({0}))", dbHelper.TranslateObjectsToSQLString(_inExpressions, parameterCollection));
			return sb.ToString();
		}
	}
}
