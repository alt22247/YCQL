/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Str function in Sql Server which returns character data converted from numeric data
	/// </summary>
	public class SqlServerFunctionStr : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionStr class using specified column
		/// </summary>
		/// <param name="column">A numeric column</param>
		public SqlServerFunctionStr(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionStr class using specified expression
		/// </summary>
		/// <param name="expression">An expression of approximate numeric (float) data type with a decimal point</param>
		public SqlServerFunctionStr(object expression)
			: base("STR", expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionStr class using specified column and length
		/// </summary>
		/// <param name="column">A numeric column</param>
		/// <param name="length">The total length</param>
		public SqlServerFunctionStr(DbColumn column, int length)
			: this((object) column, (object) length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionStr class using specified expression and length
		/// </summary>
		/// <param name="expression">An expression of approximate numeric (float) data type with a decimal point</param>
		/// <param name="length">The total length</param>
		public SqlServerFunctionStr(object expression, object length)
			: base("STR", expression, length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionStr class using specified column, length and decimal places
		/// </summary>
		/// <param name="column">A numeric column</param>
		/// <param name="length">The total length</param>
		/// <param name="decimalPlaces">The number of places to the right of the decimal point</param>
		public SqlServerFunctionStr(DbColumn column, int length, int decimalPlaces)
			: this((object) column, (object) length, (object) decimalPlaces)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionStr class using specified expression, length and decimal places
		/// </summary>
		/// <param name="expression">An expression of approximate numeric (float) data type with a decimal point</param>
		/// <param name="length">The total length</param>
		/// <param name="decimalPlaces">The number of places to the right of the decimal point</param>
		public SqlServerFunctionStr(object expression, object length, object decimalPlaces)
			: base("STR", expression, length, decimalPlaces)
		{
		}
	}
}
