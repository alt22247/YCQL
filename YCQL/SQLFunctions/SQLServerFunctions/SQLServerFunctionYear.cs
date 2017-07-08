/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Unicode function in Sql Server which returns an integer that represents the year of the specified date
	/// </summary>
	public class SqlServerFunctionYear : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionYear class using specified column
		/// </summary>
		/// <param name="column">A time, date, smalldatetime, datetime, datetime2, or datetimeoffset column</param>
		public SqlServerFunctionYear(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionYear class using specified date expression
		/// </summary>
		/// <param name="date">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value.</param>
		public SqlServerFunctionYear(object date)
			: base("YEAR", date)
		{
		}
	}
}
