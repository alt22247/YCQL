/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents GetUtcDate function in Sql Server which returns the current database system timestamp as a datetime value
	/// </summary>
	public class SqlServerFunctionGetUtcDate : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionGetUtcDate class
		/// </summary>
		public SqlServerFunctionGetUtcDate()
			: base("GETUTCDATE")
		{
		}
	}
}