/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.SqlFunctions
{
	/// <summary>
	/// Represents Max aggregate function in Sql
	/// </summary>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionAbs"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionACos"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionASCII"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionASin"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionATan"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionATan2"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionAvg"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionCeiling"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionChar"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionCoallesce"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionConcat"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionCos"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionCot"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionCount"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionDegrees"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionExp"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionFloor"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLeft"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLength"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLog"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLog10"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLog2"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLower"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionLTrim"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionMin"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionPI"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionPower"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionRadians"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionRand"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionReplace"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionReverse"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionRight"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionRound"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionRTrim"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionSign"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionSin"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionSoundEX"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionSqrt"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionSubString"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionSum"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionTan"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionTrim"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionUpper"/>
	public class SqlFunctionMax : SQLAggregateFunctionBase<SqlFunctionMax>
	{
		/// <summary>
		/// Initializes a new instance of the SqlFunctionMax class using specified column
		/// </summary>
		/// <param name="column">Column which Max should be applied to</param>
		public SqlFunctionMax(DbColumn column)
			: this((object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlFunctionMax class using specified expression
		/// </summary>
		/// <param name="expression">Expression which Max should be applied to</param>
		public SqlFunctionMax(object expression)
			: base("MAX", expression)
		{
		}
	}
}
