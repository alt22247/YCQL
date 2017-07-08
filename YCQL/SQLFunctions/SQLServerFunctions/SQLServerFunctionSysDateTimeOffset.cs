/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents SysDateTimeOffset function in Sql Server which returns a datetimeoffset(7) value that contains the date and time of the computer on which the instance of SQL Server is running. 
	/// The time zone offset is included
	/// </summary>
	public class SqlServerFunctionSysDateTimeOffset : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionSysDateTimeOffset class
		/// </summary>
		public SqlServerFunctionSysDateTimeOffset()
			: base("SYSDATETIMEOFFSET")
		{
		}
	}
}