/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents DateTime2FromParts function in Sql Server which returns a datetime2 value for the specified date and time and with the specified precision
	/// </summary>
	public class SQLServerFunctionDateTime2FromParts : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateTime2FromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer specifying a year</param>
		/// <param name="month">An integer specifying a month</param>
		/// <param name="day">An integer specifying a day</param>
		/// <param name="hour">An integer specifying hours</param>
		/// <param name="minute">An integer specifying minutes</param>
		/// <param name="seconds">An integer specifying seconds</param>
		/// <param name="fractions">An integer specifying fractions</param>
		/// <param name="precision">An integer specifying the precision of the datetime2 value to be returned</param>
		public SQLServerFunctionDateTime2FromParts(int year, int month, int day, int hour,
			int minute, int seconds, int fractions, int precision)
			: this((object) year, (object) month, (object) day, (object) hour, (object) minute, (object) seconds, (object) fractions, (object) precision)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateTime2FromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer expression specifying a year</param>
		/// <param name="month">An integer expression specifying a month</param>
		/// <param name="day">An integer expression specifying a day</param>
		/// <param name="hour">An integer expression specifying hours</param>
		/// <param name="minute">An integer expression specifying minutes</param>
		/// <param name="seconds">An integer expression specifying seconds</param>
		/// <param name="fractions">An integer expression specifying fractions</param>
		/// <param name="precision">An integer expression specifying the precision of the datetime2 value to be returned</param>
		public SQLServerFunctionDateTime2FromParts(object year, object month, object day, object hour,
			object minute, object seconds, object fractions, object precision)
			: base("DATETIME2FROMPARTS", year, month, day, hour, minute, seconds, fractions, precision)
		{
		}
	}
}