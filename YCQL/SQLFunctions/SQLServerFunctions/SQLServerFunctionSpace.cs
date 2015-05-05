/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Space function in Sql Server which returns a string of repeated spaces
	/// </summary>
	public class SQLServerFunctionSpace : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSpace class using specified number of spaces
		/// </summary>
		/// <param name="spaces">A positive integer that indicates the number of spaces</param>
		public SQLServerFunctionSpace(int spaces)
			: this((object) spaces)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSpace class using specified number of spaces
		/// </summary>
		/// <param name="spaces">A positive integer that indicates the number of spaces</param>
		public SQLServerFunctionSpace(object spaces)
			: base("SPACE", spaces)
		{
		}
	}
}
