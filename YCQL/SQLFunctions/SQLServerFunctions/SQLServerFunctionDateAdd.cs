/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents DateAdd function in Sql Server which returns a specified date with the specified number interval (signed integer) added to a specified datepart of that date
	/// </summary>
	public class SqlServerFunctionDateAdd : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateAdd class using specified date part enum, add amount, and the date column to be added
		/// </summary>
		/// <param name="datepart">Part of date to which an integer number is added</param>
		/// <param name="number">An integer that is added to a datepart of date</param>
		/// <param name="column">The column which is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SqlServerFunctionDateAdd(TimeUnitEnum datepart, int number, DbColumn column)
			: this(datepart, number, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateAdd class using specified date part enum, add amount, and the date expression to be added
		/// </summary>
		/// <param name="datepart">Part of date to which an integer number is added</param>
		/// <param name="number">An expression that can be resolved to an int that is added to a datepart of date</param>
		/// <param name="date">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value</param>
		public SqlServerFunctionDateAdd(TimeUnitEnum datepart, object number, object date)
			: base("DATEADD", new SqlRawText(datepart.ToString()), number, date)
		{
		}
	}
}
