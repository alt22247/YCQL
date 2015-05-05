/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.MySQLFunctions
{
	/// <summary>
	/// Represents Greatest function in MySql which returns the largest (maximum-valued) argument
	/// </summary>
	public class MySQLFunctionGreatest : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the MySQLFunctionGreatest class using specified arguments. There must be at least two arguments
		/// </summary>
		/// <param name="args">Arguments to be compared</param>
		public MySQLFunctionGreatest(params object[] args)
			: base("GREATEST", args)
		{
		}
	}
}
