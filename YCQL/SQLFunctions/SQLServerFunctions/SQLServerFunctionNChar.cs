/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents NChar function in Sql Server which returns the Unicode character with the specified integer code, as defined by the Unicode standard
	/// </summary>
	public class SQLServerFunctionNChar : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionNChar class using specified column
		/// </summary>
		/// <param name="column">The integer column to be used</param>
		public SQLServerFunctionNChar(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionNChar class using specified value
		/// </summary>
		/// <param name="value">An integer value to be converted to char</param>
		public SQLServerFunctionNChar(int value)
			: this((object) value)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionNChar class using specified expression
		/// </summary>
		/// <param name="expression">An integer expression to be converted to char</param>
		public SQLServerFunctionNChar(object expression)
			: base("NCHAR", expression)
		{
		}
	}
}

