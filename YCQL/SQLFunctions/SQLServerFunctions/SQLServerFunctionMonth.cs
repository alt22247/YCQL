/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Month function in Sql Server which returns an integer that represents the month of the specified date
	/// </summary>
	public class SqlServerFunctionMonth : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionMonth class using specified column
		/// </summary>
		/// <param name="column">A column that is time, date, smalldatetime, datetime, datetime2, or datetimeoffset</param>
		public SqlServerFunctionMonth(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionMonth class using specified date expression
		/// </summary>
		/// <param name="dateExpression">An expression that can be resolved to a time, date, smalldatetime, datetime, datetime2, or datetimeoffset value</param>
		public SqlServerFunctionMonth(object dateExpression)
			: base("MONTH", dateExpression)
		{
		}
	}
}