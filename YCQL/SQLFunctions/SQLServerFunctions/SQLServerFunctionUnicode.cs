/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Unicode function in Sql Server which returns the integer value, as defined by the Unicode standard, for the first character of the input expression
	/// </summary>
	public class SqlServerFunctionUnicode : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionUnicode class using specified column
		/// </summary>
		/// <param name="column">A nchar or nvarchar column</param>
		public SqlServerFunctionUnicode(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionUnicode class using specified expression
		/// </summary>
		/// <param name="expression">A nchar or nvarchar expression</param>
		public SqlServerFunctionUnicode(object expression)
			: base("UNICODE", expression)
		{
		}
	}
}