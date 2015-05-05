/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents SysDateTimeOffset function in Sql Server which returns a datetimeoffset(7) value that contains the date and time of the computer on which the instance of SQL Server is running. 
	/// The time zone offset is included
	/// </summary>
	public class SQLServerFunctionSysDateTimeOffset : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSysDateTimeOffset class
		/// </summary>
		public SQLServerFunctionSysDateTimeOffset()
			: base("SYSDATETIMEOFFSET")
		{
		}
	}
}