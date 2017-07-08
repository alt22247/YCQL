/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents EOMonth function in Sql Server which returns the last day of the month that contains the specified date, with an optional offset
	/// </summary>
	public class SqlServerFunctionEOMonth : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionEOMonth class using specified column
		/// </summary>
		/// <param name="startDateColumn">A column which specifies the date for which to return the last day of the month</param>
		public SqlServerFunctionEOMonth(DbColumn startDateColumn)
			: this((object) startDateColumn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionEOMonth class using specified startDate expression
		/// </summary>
		/// <param name="startDate">Date expression specifying the date for which to return the last day of the month</param>
		public SqlServerFunctionEOMonth(object startDate)
			: base("EOMONTH", startDate)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionEOMonth class using specified column and month to add
		/// </summary>
		/// <param name="column">A column which specifies the date for which to return the last day of the month</param>
		/// <param name="monthToAdd">An integer specifying the number of months to add to start_date</param>
		public SqlServerFunctionEOMonth(DbColumn column, int monthToAdd)
			: this((object) column, (object) monthToAdd)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionEOMonth class using specified start date and month to add
		/// </summary>
		/// <param name="startDate">Date expression specifying the date for which to return the last day of the month</param>
		/// <param name="monthToAdd">An integer expression specifying the number of months to add to start_date</param>
		public SqlServerFunctionEOMonth(object startDate, object monthToAdd)
			: base("EOMONTH", startDate, monthToAdd)
		{
		}
	}
}