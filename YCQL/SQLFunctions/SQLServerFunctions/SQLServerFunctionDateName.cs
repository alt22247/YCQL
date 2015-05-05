/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents DateName function in Sql Server which returns a character string that represents the specified datepart of the specified date 
	/// </summary>
	public class SQLServerFunctionDateName : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateName class using specified date part enum and column
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="column">A column that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SQLServerFunctionDateName(TimeUnitEnum datepart, DBColumn column)
			: this(datepart, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDateAdd class using specified date part enum and date expression
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="dateExpression">An expression that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SQLServerFunctionDateName(TimeUnitEnum datepart, object dateExpression)
			: base("DATENAME", datepart.ToString(), dateExpression)
		{
		}
	}
}