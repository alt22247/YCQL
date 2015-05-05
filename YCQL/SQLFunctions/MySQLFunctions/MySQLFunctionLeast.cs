/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.MySQLFunctions
{
	/// <summary>
	/// Represents Greatest function in MySql which returns the smallest (minimum-valued) argument
	/// </summary>
	public class MySQLFunctionLeast : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the MySQLFunctionLeast class using specified arguments. There must be at least two arguments
		/// </summary>
		/// <param name="args">Arguments to be compared</param>
		public MySQLFunctionLeast(params object[] args)
			: base("LEAST", args)
		{
		}
	}
}
