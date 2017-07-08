/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents DatePart function in Sql Server which returns a character string that represents the specified datepart of the specified date
	/// </summary>
	public class SqlServerFunctionDatePart : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDatePart class using specified datepart and date column
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="column">The column to be used</param>
		public SqlServerFunctionDatePart(TimeUnitEnum datepart, DbColumn column)
			: this(datepart, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDatePart class using specified datepart and date
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="date">The DateTime object to be used</param>
		public SqlServerFunctionDatePart(TimeUnitEnum datepart, DateTime date)
			: this(datepart, (object) date)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDatePart class using specified datepart and date
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="date">The date expression to be used</param>
		public SqlServerFunctionDatePart(TimeUnitEnum datepart, object date)
			: base("DATEPART", datepart.ToString(), date)
		{
		}
	}
}