/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using Ycql.SqlFunctions;

namespace Ycql.MySqlFunctions
{
	/// <summary>
	/// Represents Greatest function in MySql which returns the largest (maximum-valued) argument
	/// </summary>
	public class MySqlFunctionGreatest : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the MySqlFunctionGreatest class using specified arguments. There must be at least two arguments
		/// </summary>
		/// <param name="args">Arguments to be compared</param>
		public MySqlFunctionGreatest(params object[] args)
			: base("GREATEST", args)
		{
		}
	}
}
