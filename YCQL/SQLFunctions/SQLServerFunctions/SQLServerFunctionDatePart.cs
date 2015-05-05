/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents DatePart function in Sql Server which returns a character string that represents the specified datepart of the specified date
	/// </summary>
	public class SQLServerFunctionDatePart : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDatePart class using specified datepart and date column
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="column">The column to be used</param>
		public SQLServerFunctionDatePart(TimeUnitEnum datepart, DBColumn column)
			: this(datepart, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDatePart class using specified datepart and date
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="date">The DateTime object to be used</param>
		public SQLServerFunctionDatePart(TimeUnitEnum datepart, DateTime date)
			: this(datepart, (object) date)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDatePart class using specified datepart and date
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="date">The date expression to be used</param>
		public SQLServerFunctionDatePart(TimeUnitEnum datepart, object date)
			: base("DATEPART", datepart.ToString(), date)
		{
		}
	}
}