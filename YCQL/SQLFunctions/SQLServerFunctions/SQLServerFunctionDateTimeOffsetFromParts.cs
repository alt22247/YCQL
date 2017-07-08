/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents DateTimeOffsetFromParts function in Sql Server which returns a datetimeoffset value for the specified date and time and with the specified offsets and precision
	/// </summary>
	public class SqlServerFunctionDateTimeOffsetFromParts : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateTimeOffsetFromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer specifying a year</param>
		/// <param name="month">An integer specifying a month</param>
		/// <param name="day">An integer specifying a day</param>
		/// <param name="hour">An integer specifying hours</param>
		/// <param name="minute">An integer specifying minutes</param>
		/// <param name="seconds">An integer specifying seconds</param>
		/// <param name="fractions">An integer specifying fractions</param>
		/// <param name="hour_offset">An integer specifying the hour portion of the time zone offset</param>
		/// <param name="minute_offset">An integer specifying the minute portion of the time zone offset</param>
		/// <param name="precision">An integer literal specifying the precision of the datetimeoffset value to be returned</param>
		public SqlServerFunctionDateTimeOffsetFromParts(int year, int month, int day, int hour,
			int minute, int seconds, int fractions, int hour_offset, int minute_offset, int precision)
			: this((object) year, (object) month, (object) day, (object) hour, (object) minute, (object) seconds,
			(object) fractions, (object) hour_offset, (object) minute_offset, (object) precision)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateTimeOffsetFromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer expression specifying a year</param>
		/// <param name="month">An integer expression specifying a month</param>
		/// <param name="day">An integer expression specifying a day</param>
		/// <param name="hour">An integer expression specifying hours</param>
		/// <param name="minute">An integer expression specifying minutes</param>
		/// <param name="seconds">An integer expression specifying seconds</param>
		/// <param name="fractions">An integer expression specifying fractions</param>
		/// <param name="hour_offset">An integer expression specifying the hour portion of the time zone offset</param>
		/// <param name="minute_offset">An integer expression specifying the minute portion of the time zone offset</param>
		/// <param name="precision">An integer literal specifying the precision of the datetimeoffset value to be returned</param>
		public SqlServerFunctionDateTimeOffsetFromParts(object year, object month, object day, object hour,
			object minute, object seconds, object fractions, object hour_offset, object minute_offset, object precision)
			: base("DATETIMEOFFSETFROMPARTS", year, month, day, hour, minute, seconds, fractions, hour_offset, minute_offset, precision)
		{
		}
	}
}
