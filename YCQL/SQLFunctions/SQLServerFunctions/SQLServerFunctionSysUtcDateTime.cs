/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents SysUtcDateTime function in Sql Server which returns a datetime2 value that contains the date and time of the computer on which the instance of SQL Server is running. 
	/// The date and time is returned as UTC time (Coordinated Universal Time).
	/// </summary>
	public class SQLServerFunctionSysUtcDateTime : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSysUtcDateTime class
		/// </summary>
		public SQLServerFunctionSysUtcDateTime()
			: base("SYSUTCDATETIME")
		{
		}
	}
}