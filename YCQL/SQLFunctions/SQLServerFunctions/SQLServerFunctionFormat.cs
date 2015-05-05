/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Format function in Sql Server which returns a value formatted with the specified format and optional culture
	/// </summary>
	public class SQLServerFunctionFormat : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionFormat class using specified column and format
		/// </summary>
		/// <param name="column">Column to be formatted</param>
		/// <param name="format">A valid .NET Framework format string</param>
		public SQLServerFunctionFormat(DBColumn column, string format)
			: this((object) column, (object) format)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionFormat class using specified value and format
		/// </summary>
		/// <param name="value">Expression of a supported data type to format</param>
		/// <param name="format">A valid .NET Framework format string</param>
		public SQLServerFunctionFormat(object value, object format)
			: base("FORMAT", value, format)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionFormat class using specified column, format and culture
		/// </summary>
		/// <param name="column">Column to be formatted</param>
		/// <param name="format">A valid .NET Framework format string</param>
		/// <param name="culture">A nvarchar argument specifying a culture</param>
		public SQLServerFunctionFormat(DBColumn column, string format, string culture)
			: this((object) column, (object) format, (object) culture)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionFormat class using specified value, format and culture
		/// </summary>
		/// <param name="value">Expression of a supported data type to format</param>
		/// <param name="format">A valid .NET Framework format string</param>
		/// <param name="culture">A nvarchar argument specifying a culture</param>
		public SQLServerFunctionFormat(object value, object format, object culture)
			: base("FORMAT", value, format, culture)
		{
		}
	}
}

