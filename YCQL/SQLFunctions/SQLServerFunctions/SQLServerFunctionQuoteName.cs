/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents QuoteName function in Sql Server which returns a Unicode string with the delimiters added to make the input string a valid SQL Server delimited identifier
	/// </summary>
	public class SQLServerFunctionQuoteName : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionReplicate class using specified expression
		/// </summary>
		/// <param name="expression">A string of Unicode character data</param>
		public SQLServerFunctionQuoteName(string expression)
			: this((object) expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionReplicate class using specified expression and quote character
		/// </summary>
		/// <param name="expression">A string of Unicode character data</param>
		/// <param name="quote_character">A one-character string to use as the delimiter</param>
		public SQLServerFunctionQuoteName(string expression, string quote_character)
			: this((object) expression, (object) quote_character)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionReplicate class using specified expression
		/// </summary>
		/// <param name="expression">A string of Unicode character data</param>
		public SQLServerFunctionQuoteName(object expression)
			: base("QUOTENAME", expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionReplicate class using specified expression and quote character
		/// </summary>
		/// <param name="expression">A string of Unicode character data</param>
		/// <param name="quote_character">A one-character string to use as the delimiter</param>
		public SQLServerFunctionQuoteName(object expression, object quote_character)
			: base("QUOTENAME", expression, quote_character)
		{
		}
	}
}