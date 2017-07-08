/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents IIF function in Sql Server which returns one of two values, depending on whether the boolean expression evaluates to true or false 
	/// </summary>
	public class SqlServerFunctionIIF : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionIIF class using specified clause and two values might be returned
		/// </summary>
		/// <param name="clause">A logical clause which its result will determine which value to be returned</param>
		/// <param name="trueValue">Value to return if logical clause evaluates to true</param>
		/// <param name="falseValue">Value to return if logical clause evaluates to false</param>
		public SqlServerFunctionIIF(LogicalClause clause, object trueValue, object falseValue)
			: this((object) clause, trueValue, falseValue)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionIIF class using specified expression and two values might be returned
		/// </summary>
		/// <param name="booleanExpression">A boolean expression which its result will determine which value to be returned</param>
		/// <param name="trueValue">Value to return if logical clause evaluates to true</param>
		/// <param name="falseValue">Value to return if logical clause evaluates to false</param>
		public SqlServerFunctionIIF(BooleanExpression booleanExpression, object trueValue, object falseValue)
			: this((object) booleanExpression, trueValue, falseValue)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionIIF class using specified expression and two values might be returned
		/// </summary>
		/// <param name="booleanExpression">A boolean expression which its result will determine which value to be returned</param>
		/// <param name="trueValue">Value to return if logical clause evaluates to true</param>
		/// <param name="falseValue">Value to return if logical clause evaluates to false</param>
		public SqlServerFunctionIIF(object booleanExpression, object trueValue, object falseValue)
			: base("IIF")
		{
		}
	}
}