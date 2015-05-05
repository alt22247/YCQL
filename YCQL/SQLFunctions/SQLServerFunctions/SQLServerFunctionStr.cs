/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Str function in Sql Server which returns character data converted from numeric data
	/// </summary>
	public class SQLServerFunctionStr : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStr class using specified column
		/// </summary>
		/// <param name="column">A numeric column</param>
		public SQLServerFunctionStr(DBColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStr class using specified expression
		/// </summary>
		/// <param name="expression">An expression of approximate numeric (float) data type with a decimal point</param>
		public SQLServerFunctionStr(object expression)
			: base("STR", expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStr class using specified column and length
		/// </summary>
		/// <param name="column">A numeric column</param>
		/// <param name="length">The total length</param>
		public SQLServerFunctionStr(DBColumn column, int length)
			: this((object) column, (object) length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStr class using specified expression and length
		/// </summary>
		/// <param name="expression">An expression of approximate numeric (float) data type with a decimal point</param>
		/// <param name="length">The total length</param>
		public SQLServerFunctionStr(object expression, object length)
			: base("STR", expression, length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStr class using specified column, length and decimal places
		/// </summary>
		/// <param name="column">A numeric column</param>
		/// <param name="length">The total length</param>
		/// <param name="decimalPlaces">The number of places to the right of the decimal point</param>
		public SQLServerFunctionStr(DBColumn column, int length, int decimalPlaces)
			: this((object) column, (object) length, (object) decimalPlaces)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStr class using specified expression, length and decimal places
		/// </summary>
		/// <param name="expression">An expression of approximate numeric (float) data type with a decimal point</param>
		/// <param name="length">The total length</param>
		/// <param name="decimalPlaces">The number of places to the right of the decimal point</param>
		public SQLServerFunctionStr(object expression, object length, object decimalPlaces)
			: base("STR", expression, length, decimalPlaces)
		{
		}
	}
}
