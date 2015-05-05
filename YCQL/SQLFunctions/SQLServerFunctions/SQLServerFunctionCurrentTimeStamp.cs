/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents CurrentTimeStamp function(variable) in Sql Server which returns the current database system timestamp as a datetime value without the database time zone offset
	/// </summary>
	public class SQLServerFunctionCurrentTimeStamp : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionCurrentTimeStamp class
		/// </summary>
		public SQLServerFunctionCurrentTimeStamp()
			: base("CURRENT_TIMESTAMP", false)
		{
		}
	}
}