/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Space function in Sql Server which returns a string of repeated spaces
	/// </summary>
	public class SqlServerFunctionSpace : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionSpace class using specified number of spaces
		/// </summary>
		/// <param name="spaces">A positive integer that indicates the number of spaces</param>
		public SqlServerFunctionSpace(int spaces)
			: this((object) spaces)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionSpace class using specified number of spaces
		/// </summary>
		/// <param name="spaces">A positive integer that indicates the number of spaces</param>
		public SqlServerFunctionSpace(object spaces)
			: base("SPACE", spaces)
		{
		}
	}
}
