﻿/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents IsDate function in Sql Server which returns 1 if the expression is a valid date, time, or datetime value; otherwise, 0.
	/// </summary>
	public class SqlServerFunctionIsDate : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionIsDate class using specified column
		/// </summary>
		/// <param name="column">The column to be determine if it is a valid date, time, or datetime value. Date and time data types, except datetime and smalldatetime, are not allowed</param>
		public SqlServerFunctionIsDate(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionIsDate class using specified expression
		/// </summary>
		/// <param name="expression">A character string or expression that can be converted to a character string. Date and time data types, except datetime and smalldatetime, are not allowed</param>
		public SqlServerFunctionIsDate(object expression)
			: base("ISDATE", expression)
		{
		}
	}
}