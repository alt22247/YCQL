/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents SysDateTime function in Sql Server which returns a datetime2(7) value that contains the date and time of the computer on which the instance of SQL Server is running
	/// </summary>
	public class SqlServerFunctionSysDateTime : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionSysDateTime class
		/// </summary>
		public SqlServerFunctionSysDateTime()
			: base("SYSDATETIME")
		{
		}
	}
}
