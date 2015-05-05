/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Unicode function in Sql Server which returns the integer value, as defined by the Unicode standard, for the first character of the input expression
	/// </summary>
	public class SQLServerFunctionUnicode : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionUnicode class using specified column
		/// </summary>
		/// <param name="column">A nchar or nvarchar column</param>
		public SQLServerFunctionUnicode(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionUnicode class using specified expression
		/// </summary>
		/// <param name="expression">A nchar or nvarchar expression</param>
		public SQLServerFunctionUnicode(object expression)
			: base("UNICODE", expression)
		{
		}
	}
}