/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents TRY_CONVERT function in Sql Server which returns a value cast to the specified data type if the cast succeeds; otherwise, returns null
	/// </summary>
	public class SqlServerFunctionTryConvert : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryConvert class using specified new data type and column to be cast
		/// </summary>
		/// <param name="dataType">The data type into which to cast the column</param>
		/// <param name="column">The column to be cast</param>
		public SqlServerFunctionTryConvert(DataType dataType, DbColumn column)
			: this(dataType, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryConvert class using specified new data type, column to be cast and the style
		/// </summary>
		/// <param name="dataType">The data type into which to cast the column</param>
		/// <param name="column">The column to be cast</param>
		/// <param name="style">An integer that specifies how the TRY_CONVERT function is to translate expression</param>
		public SqlServerFunctionTryConvert(DataType dataType, DbColumn column, int style)
			: this(dataType, (object) column, style)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryConvert class using specified new data type and expression to be cast
		/// </summary>
		/// <param name="dataType">The data type into which to cast the expression</param>
		/// <param name="expression">The value to be cast</param>
		public SqlServerFunctionTryConvert(DataType dataType, object expression)
			: base("TRY_CONVERT", dataType, expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryConvert class using specified new data type, expression to be cast and the style
		/// </summary>
		/// <param name="dataType">The data type into which to cast the expression</param>
		/// <param name="expression">The value to be cast</param>
		/// <param name="style">An integer expression that specifies how the TRY_CONVERT function is to translate expression</param>
		public SqlServerFunctionTryConvert(DataType dataType, object expression, int style)
			: base("TRY_CONVERT", dataType, expression, style)
		{
		}
	}
}
