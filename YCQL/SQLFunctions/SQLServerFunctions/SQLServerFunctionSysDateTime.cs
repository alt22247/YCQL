/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents SysDateTime function in Sql Server which returns a datetime2(7) value that contains the date and time of the computer on which the instance of SQL Server is running
	/// </summary>
	public class SQLServerFunctionSysDateTime : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSysDateTime class
		/// </summary>
		public SQLServerFunctionSysDateTime()
			: base("SYSDATETIME")
		{
		}
	}
}
