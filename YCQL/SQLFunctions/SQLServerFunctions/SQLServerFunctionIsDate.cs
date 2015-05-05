/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents IsDate function in Sql Server which returns 1 if the expression is a valid date, time, or datetime value; otherwise, 0.
	/// </summary>
	public class SQLServerFunctionIsDate : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionIsDate class using specified column
		/// </summary>
		/// <param name="column">The column to be determine if it is a valid date, time, or datetime value. Date and time data types, except datetime and smalldatetime, are not allowed</param>
		public SQLServerFunctionIsDate(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionIsDate class using specified expression
		/// </summary>
		/// <param name="expression">A character string or expression that can be converted to a character string. Date and time data types, except datetime and smalldatetime, are not allowed</param>
		public SQLServerFunctionIsDate(object expression)
			: base("ISDATE", expression)
		{
		}
	}
}