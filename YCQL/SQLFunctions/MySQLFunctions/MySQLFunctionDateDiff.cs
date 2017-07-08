/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.MySqlFunctions
{
	/// <summary>
	/// Represents DateDiff function in MySql which returns expr1 − expr2 expressed as a value in days from one date to the other
	/// </summary>
	public class MySqlFunctionDateDiff : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the MySqlFunctionDateDiff class using specified expressions
		/// </summary>
		/// <param name="expression1">Date or date-and-time expressions</param>
		/// <param name="expression2">Date or date-and-time expressions</param>
		public MySqlFunctionDateDiff(object expression1, object expression2)
			: base("DATEDIFF", expression1, expression2)
		{
		}
	}
}
