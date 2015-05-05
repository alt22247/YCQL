/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents GetUtcDate function in Sql Server which returns the current database system timestamp as a datetime value
	/// </summary>
	public class SQLServerFunctionGetUtcDate : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionGetUtcDate class
		/// </summary>
		public SQLServerFunctionGetUtcDate()
			: base("GETUTCDATE")
		{
		}
	}
}