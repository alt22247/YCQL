/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents DateTimeFromParts function in Sql Server which returns a datetime value for the specified date and time
	/// </summary>
	public class SqlServerFunctionDateTimeFromParts : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateTimeFromParts class using specified attributes
		/// </summary>
		/// <param name="year">An specifying a year</param>
		/// <param name="month">An integer specifying a month</param>
		/// <param name="day">An integer specifying a day</param>
		/// <param name="hour">An integer specifying hours</param>
		/// <param name="minute">An integer specifying minutes</param>
		/// <param name="seconds">An integer specifying seconds</param>
		/// <param name="milliseconds">An integer specifying milliseconds</param>
		public SqlServerFunctionDateTimeFromParts(int year, int month, int day, int hour, int minute, int seconds, int milliseconds)
			: this((object) year, (object) month, (object) day, (object) hour, (object) minute, (object) seconds, (object) milliseconds)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateTimeFromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer expression specifying a year</param>
		/// <param name="month">An integer expression specifying a month</param>
		/// <param name="day">An integer expression specifying a day</param>
		/// <param name="hour">An integer expression specifying hours</param>
		/// <param name="minute">An integer expression specifying minutes</param>
		/// <param name="seconds">An integer expression  specifying seconds</param>
		/// <param name="milliseconds">An integer expression specifying milliseconds</param>
		public SqlServerFunctionDateTimeFromParts(object year, object month, object day, object hour,
			object minute, object seconds, object milliseconds)
			: base("DATETIMEFROMPARTS", year, month, day, hour, minute, seconds, milliseconds)
		{
		}
	}
}