/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents SmallDateTimeFromParts function in Sql Server which returns a smalldatetime value for the specified date and time
	/// </summary>
	public class SqlServerFunctionSmallDateTimeFromParts : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionSmallDateTimeFromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer specifying a year</param>
		/// <param name="month">An integer specifying a month</param>
		/// <param name="day">An integer specifying a day</param>
		/// <param name="hour">An integer specifying hours</param>
		/// <param name="minute">An integer specifying minutes</param>
		public SqlServerFunctionSmallDateTimeFromParts(int year, int month, int day, int hour, int minute)
			: this((object) year, (object) month, (object) day, (object) hour, (object) minute)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionSmallDateTimeFromParts class using specified attributes
		/// </summary>
		/// <param name="year">An integer expression specifying a year</param>
		/// <param name="month">An integer expression specifying a month</param>
		/// <param name="day">An integer expression specifying a day</param>
		/// <param name="hour">An integer expression specifying hours</param>
		/// <param name="minute">An integer expression specifying minutes</param>
		public SqlServerFunctionSmallDateTimeFromParts(object year, object month, object day, object hour, object minute)
			: base("SMALLDATETIMEFROMPARTS", year, month, day, hour, minute)
		{
		}
	}
}
