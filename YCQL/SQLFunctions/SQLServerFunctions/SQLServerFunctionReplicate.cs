/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Replicate function in Sql Server which repeats a string value a specified number of times
	/// </summary>
	public class SqlServerFunctionReplicate : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionReplicate class using specified column and number of times to repeat
		/// </summary>
		/// <param name="column">A string or binary column</param>
		/// <param name="times">Number of times to repeat</param>
		public SqlServerFunctionReplicate(DbColumn column, int times)
			: this((object) column, (object) times)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionReplicate class using specified expression and number of times to repeat
		/// </summary>
		/// <param name="expression">An expression of a character string or binary data type</param>
		/// <param name="times">An expression of any integer type, including bigint. If integer_expression is negative, NULL is returned</param>
		public SqlServerFunctionReplicate(object expression, object times)
			: base("REPLICATE", expression, times)
		{
		}
	}
}