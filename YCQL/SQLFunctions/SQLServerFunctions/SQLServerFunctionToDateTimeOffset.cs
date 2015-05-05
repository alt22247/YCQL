/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents ToDateTimeOffset function in Sql Server which returns a datetimeoffset value that is translated from a datetime2 expression
	/// </summary>
	public class SQLServerFunctionToDateTimeOffset : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionToDateTimeOffset class using specified column and new time zone
		/// </summary>
		/// <param name="column">A DateTime2 column</param>
		/// <param name="timeZone">A signed integer (of minutes) that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted</param>
		public SQLServerFunctionToDateTimeOffset(DBColumn column, int timeZone)
			: this((object) column, (object) timeZone)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionToDateTimeOffset class using specified column and new time zone
		/// </summary>
		/// <param name="column">A DateTime2 column</param>
		/// <param name="timeZone">A character string in the format [+|-]TZH:TZM that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted</param>
		public SQLServerFunctionToDateTimeOffset(DBColumn column, string timeZone)
			: this((object) column, (object) timeZone)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionToDateTimeOffset class using specified expression and new time zone
		/// </summary>
		/// <param name="expression">An expression that resolves to a datetime2 value</param>
		/// <param name="timeZone">A character string in the format [+|-]TZH:TZM or a signed integer (of minutes) that represents the time zone offset, and is assumed to be daylight-saving aware and adjusted</param>
		public SQLServerFunctionToDateTimeOffset(object expression, object timeZone)
			: base("TODATETIMEOFFSET", expression, timeZone)
		{
		}
	}
}