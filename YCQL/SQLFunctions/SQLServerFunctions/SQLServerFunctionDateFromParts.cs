/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents DateFromParts function in Sql Server which returns a date value for the specified year, month, and day
	/// </summary>
	public class SqlServerFunctionDateFromParts : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateFromParts using specified year, month and day
		/// </summary>
		/// <param name="year">An integer specifying a year</param>
		/// <param name="month">An integer specifying a month, from 1 to 12</param>
		/// <param name="day">An integer specifying a day</param>
		public SqlServerFunctionDateFromParts(int year, int month, int day)
			: this((object) year, (object) month, (object) day)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateAdd class using specified year, month and day
		/// </summary>
		/// <param name="year">An integer expression specifying a year</param>
		/// <param name="month">An integer expression specifying a month, from 1 to 12</param>
		/// <param name="day">An integer expression specifying a day</param>
		public SqlServerFunctionDateFromParts(object year, object month, object day)
			: base("DATEFROMPARTS", year, month, day)
		{
		}
	}
}
