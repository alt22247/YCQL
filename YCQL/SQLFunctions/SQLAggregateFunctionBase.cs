/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql.SqlFunctions
{
	/// <summary>
	/// Base class for Sql aggregate functions
	/// </summary>
	/// <typeparam name="T">The type of derived class</typeparam>
	public abstract class SQLAggregateFunctionBase<T> : SqlFunctionBase, ISupportDistinct<T> where T : SQLAggregateFunctionBase<T>
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
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			if (!_isDistinct)
				return base.ToSql(dbVersion, parameterCollection);
			else
				return string.Format("{0}(DISTINCT {1})", _functionName, dbHelper.TranslateObjectToSqlString(_expression, parameterCollection));
		}
	}
}
