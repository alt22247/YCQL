/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents DateAdd function in Sql Server which returns a specified date with the specified number interval (signed integer) added to a specified datepart of that date
	/// </summary>
	public class SQLServerFunctionDateAdd : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateAdd class using specified date part enum, add amount, and the date column to be added
		/// </summary>
		/// <param name="datepart">Part of date to which an integer number is added</param>
		/// <param name="number">An integer that is added to a datepart of date</param>
		/// <param name="column">The column which is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SQLServerFunctionDateAdd(TimeUnitEnum datepart, int number, DBColumn column)
			: this(datepart, number, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateAdd class using specified date part enum, add amount, and the date expression to be added
		/// </summary>
		/// <param name="datepart">Part of date to which an integer number is added</param>
		/// <param name="number">An expression that can be resolved to an int that is added to a datepart of date</param>
		/// <param name="date">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value</param>
		public SQLServerFunctionDateAdd(TimeUnitEnum datepart, object number, object date)
			: base("DATEADD", datepart.ToString(), number, date)
		{
		}
	}
}
