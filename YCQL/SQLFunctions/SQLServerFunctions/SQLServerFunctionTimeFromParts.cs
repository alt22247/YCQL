﻿/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents TimeFromParts function in Sql Server which returns a time value for the specified time and with the specified precision
	/// </summary>
	public class SQLServerFunctionTimeFromParts : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTimeFromParts class using specified attributes
		/// </summary>
		/// <param name="hour">An integer specifying hours</param>
		/// <param name="minute">An integer specifying minutes</param>
		/// <param name="seconds">An integer specifying seconds</param>
		/// <param name="fractions">An integer specifying fractions</param>
		/// <param name="precision">An integer specifying the precision of the time value to be returned</param>
		public SQLServerFunctionTimeFromParts(int hour, int minute, int seconds, int fractions, int precision)
			: this((object) hour, (object) minute, (object) seconds, (object) fractions, (object) precision)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTimeFromParts class using specified attributes
		/// </summary>
		/// <param name="hour">An integer expression specifying hours</param>
		/// <param name="minute">An integer expression specifying minutes</param>
		/// <param name="seconds">An integer expression specifying seconds</param>
		/// <param name="fractions">An integer expression specifying fractions</param>
		/// <param name="precision">An integer literal specifying the precision of the time value to be returned</param>
		public SQLServerFunctionTimeFromParts(object hour, object minute, object seconds, object fractions, object precision)
			: base("TIMEFROMPARTS", hour, minute, seconds, fractions, precision)
		{
		}
	}
}