/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents DateDiff function in Sql Server which returns the count (signed integer) of the specified datepart boundaries crossed between the specified startdate and enddate
	/// </summary>
	public class SQLServerFunctionDateDiff : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateDiff class using specified date part enum, and two columns to be diff
		/// </summary>
		/// <param name="datePart">The part of startdate and enddate that specifies the type of boundary crossed</param>
		/// <param name="startDate">Column to be subtracted from enddate</param>
		/// <param name="endDate">End date column</param>
		public SQLServerFunctionDateDiff(TimeUnitEnum datePart, DBColumn startDate, DateTime endDate)
			: this(datePart, (object) startDate, (object) endDate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateAdd class using specified date part enum, start date and end date column
		/// </summary>
		/// <param name="datePart">The part of startdate and enddate that specifies the type of boundary crossed</param>
		/// <param name="startDate">Column to be subtracted from enddate</param>
		/// <param name="endDate">End date column</param>
		public SQLServerFunctionDateDiff(TimeUnitEnum datePart, DateTime startDate, DBColumn endDate)
			: this(datePart, (object) startDate, (object) endDate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateAdd class using specified date part enum, start date column and end date
		/// </summary>
		/// <param name="datePart">The part of startdate and enddate that specifies the type of boundary crossed</param>
		/// <param name="startDate">Date to be subtracted from enddate</param>
		/// <param name="endDate">End date</param>
		public SQLServerFunctionDateDiff(TimeUnitEnum datePart, DBColumn startDate, DBColumn endDate)
			: this(datePart, (object) startDate, (object) endDate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateAdd class using specified date part enum, start date column and end date
		/// </summary>
		/// <param name="datePart">The part of startdate and enddate that specifies the type of boundary crossed</param>
		/// <param name="startDate">Expression to be subtracted from enddate</param>
		/// <param name="endDate">End date expression</param>
		public SQLServerFunctionDateDiff(TimeUnitEnum datePart, object startDate, object endDate)
			: base("DATEDIFF", datePart.ToString(), startDate, endDate)
		{
		}
	}
}
