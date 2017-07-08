/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/
using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents IsNull function in Sql Server which replaces NULL with the specified replacement value
	/// </summary>
	public class SqlServerFunctionIsNull : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionIsDate class using specified expression
		/// </summary>
		/// <param name="check_expression">The expression to be checked for NULL. check_expression can be of any type</param>
		/// <param name="replacement_value">the expression to be returned if check_expression is NULL. replacement_value must be of a type that is implicitly convertible to the type of check_expresssion</param>
		public SqlServerFunctionIsNull(object check_expression, object replacement_value)
			: base("ISNULL", check_expression, replacement_value)
		{
		}
	}
}
