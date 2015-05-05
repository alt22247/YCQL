/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents SwitchOffset function in Sql Server which returns a datetimeoffset value that is changed from the stored time zone offset to a specified new time zone offset
	/// </summary>
	public class SQLServerFunctionSwitchOffset : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSwitchOffset class using specified column and new time zone
		/// </summary>
		/// <param name="column">A datetimeoffset column</param>
		/// <param name="timeZone">A signed integer (of minutes) that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted</param>
		public SQLServerFunctionSwitchOffset(DBColumn column, int timeZone)
			: this((object) column, (object) timeZone)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSwitchOffset class using specified column and new time zone
		/// </summary>
		/// <param name="column">A datetimeoffset column</param>
		/// <param name="timeZone">A character string in the format [+|-]TZH:TZM that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted</param>
		public SQLServerFunctionSwitchOffset(DBColumn column, string timeZone)
			: this((object) column, (object) timeZone)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionSwitchOffset class using specified dateTimeOffset and new time zone
		/// </summary>
		/// <param name="dateTimeOffset">An expression that can be resolved to a datetimeoffset(n) value</param>
		/// <param name="timeZone">A character string in the format [+|-]TZH:TZM or a signed integer (of minutes) that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted</param>
		public SQLServerFunctionSwitchOffset(object dateTimeOffset, object timeZone)
			: base("SWITCHOFFSET", dateTimeOffset, timeZone)
		{
		}
	}
}