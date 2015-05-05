/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Month function in Sql Server which returns an integer that represents the month of the specified date
	/// </summary>
	public class SQLServerFunctionMonth : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionMonth class using specified column
		/// </summary>
		/// <param name="column">A column that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SQLServerFunctionMonth(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionMonth class using specified date expression
		/// </summary>
		/// <param name="dateExpression">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value</param>
		public SQLServerFunctionMonth(object dateExpression)
			: base("MONTH", dateExpression)
		{
		}
	}
}