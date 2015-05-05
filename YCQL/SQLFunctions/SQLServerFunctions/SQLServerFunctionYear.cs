/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Unicode function in Sql Server which returns an integer that represents the year of the specified date
	/// </summary>
	public class SQLServerFunctionYear : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionYear class using specified column
		/// </summary>
		/// <param name="column">A time, date, smalldatetime, datetime, datetime2, or datetimeoffset column</param>
		public SQLServerFunctionYear(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionYear class using specified date expression
		/// </summary>
		/// <param name="date">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value.</param>
		public SQLServerFunctionYear(object date)
			: base("YEAR", date)
		{
		}
	}
}
