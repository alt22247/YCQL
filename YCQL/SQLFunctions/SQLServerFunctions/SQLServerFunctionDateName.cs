/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents DateName function in Sql Server which returns a character string that represents the specified datepart of the specified date 
	/// </summary>
	public class SqlServerFunctionDateName : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateName class using specified date part enum and column
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="column">A column that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SqlServerFunctionDateName(TimeUnitEnum datepart, DbColumn column)
			: this(datepart, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionDateAdd class using specified date part enum and date expression
		/// </summary>
		/// <param name="datepart">The part of the date to return</param>
		/// <param name="dateExpression">An expression that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SqlServerFunctionDateName(TimeUnitEnum datepart, object dateExpression)
			: base("DATENAME", datepart.ToString(), dateExpression)
		{
		}
	}
}