/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents NChar function in Sql Server which returns the Unicode character with the specified integer code, as defined by the Unicode standard
	/// </summary>
	public class SqlServerFunctionNChar : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionNChar class using specified column
		/// </summary>
		/// <param name="column">The integer column to be used</param>
		public SqlServerFunctionNChar(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionNChar class using specified value
		/// </summary>
		/// <param name="value">An integer value to be converted to char</param>
		public SqlServerFunctionNChar(int value)
			: this((object) value)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionNChar class using specified expression
		/// </summary>
		/// <param name="expression">An integer expression to be converted to char</param>
		public SqlServerFunctionNChar(object expression)
			: base("NCHAR", expression)
		{
		}
	}
}

