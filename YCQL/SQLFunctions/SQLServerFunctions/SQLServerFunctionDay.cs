/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Day function in Sql Server which returns an integer representing the day (day of the month) of the specified date
	/// </summary>
	public class SQLServerFunctionDay : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDay class using specified column
		/// </summary>
		/// <param name="column">A column that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SQLServerFunctionDay(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDay class using specified date expression
		/// </summary>
		/// <param name="dateExpression">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value</param>
		public SQLServerFunctionDay(object dateExpression)
			: base("DAY", dateExpression)
		{
		}
	}
}


