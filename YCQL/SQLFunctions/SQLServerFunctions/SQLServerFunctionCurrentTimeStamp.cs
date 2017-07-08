/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents CurrentTimeStamp function(variable) in Sql Server which returns the current database system timestamp as a datetime value without the database time zone offset
	/// </summary>
	public class SqlServerFunctionCurrentTimeStamp : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCurrentTimeStamp class
		/// </summary>
		public SqlServerFunctionCurrentTimeStamp()
			: base("CURRENT_TIMESTAMP", false)
		{
		}
	}
}