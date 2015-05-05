/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL.SQLFunctions
{
	/// <summary>
	/// Base class for Sql aggregate functions
	/// </summary>
	/// <typeparam name="T">The type of derived class</typeparam>
	public abstract class SQLAggregateFunctionBase<T> : SQLFunctionBase, ISupportDistinct<T> where T : SQLAggregateFunctionBase<T>
	{
		bool _isDistinct;
		object _expression;

		internal SQLAggregateFunctionBase(string functionName, object expression)
			: base(functionName, expression)
		{
			_expression = expression;
		}

		/// <summary>
		/// Applies Distinct keyword to the function
		/// </summary>
		/// <returns>A reference to this instance after the distinct flag has been set</returns>
		public T Distinct()
		{
			_isDistinct = true;
			return (T) this;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			if (!_isDistinct)
				return base.ToSQL(dbHelper, parameterCollection);
			else
				return string.Format("{0}(DISTINCT {1})", _functionName, dbHelper.TranslateObjectToSQLString(_expression, parameterCollection));
		}
	}
}
