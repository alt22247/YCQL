﻿/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Convert function in Sql Server which converts an expression of one data type to another
	/// </summary>
	public class SQLServerFunctionConvert : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionConvert class using specified dataType and the column to be converted
		/// </summary>
		/// <param name="dataType">The new data type</param>
		/// <param name="column">The column to have its data type to be converted</param>
		public SQLServerFunctionConvert(DataType dataType, DBColumn column)
			: this(dataType, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionConvert class using specified dataType, the column to be converted and the style
		/// </summary>
		/// <param name="dataType">The new data type</param>
		/// <param name="column">The column to have its data type to be converted</param>
		/// <param name="style">An integer expression that specifies how the CONVERT function is to translate expression</param>
		public SQLServerFunctionConvert(DataType dataType, DBColumn column, int style)
			: this(dataType, (object) column, style)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionConvert class using specified dataType and the expression to be converted
		/// </summary>
		/// <param name="dataType">The new data type</param>
		/// <param name="expression">The expression to have its data type to be converted</param>
		public SQLServerFunctionConvert(DataType dataType, object expression)
			: base("CONVERT", dataType, expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionConvert class using specified dataType, the expression to be converted and the style
		/// </summary>
		/// <param name="dataType">The new data type</param>
		/// <param name="expression">The expression to have its data type to be converted</param>
		/// <param name="style">An integer expression that specifies how the CONVERT function is to translate expression</param>
		public SQLServerFunctionConvert(DataType dataType, object expression, int style)
			: base("CONVERT", dataType, expression, style)
		{
		}
	}
}
