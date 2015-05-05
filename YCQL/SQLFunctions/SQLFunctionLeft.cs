/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.SQLFunctions
{
	/// <summary>
	/// Represents Left function in Sql
	/// </summary>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionAbs"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionACos"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionASCII"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionASin"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionATan"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionATan2"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionAvg"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionCeiling"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionChar"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionCoallesce"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionConcat"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionCos"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionCot"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionCount"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionDegrees"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionExp"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionFloor"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLength"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLog"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLog10"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLog2"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLower"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLTrim"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionMax"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionMin"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionPI"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionPower"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionRadians"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionRand"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionReplace"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionReverse"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionRight"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionRound"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionRTrim"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSign"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSin"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSoundEX"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSqrt"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSubString"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSum"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionTan"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionTrim"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionUpper"/>
	public class SQLFunctionLeft : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLFunctionLeft class using specified column and length
		/// </summary>
		/// <param name="column">Column which Left should be applied to</param>
		/// <param name="length">Number of characters should be returned</param>
		public SQLFunctionLeft(DBColumn column, int length)
			: this((object) column, (object) length)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionLeft class using specified expression and length
		/// </summary>
		/// <param name="expression">Expression which Left should be applied to</param>
		/// <param name="length">Number of characters should be returned</param>
		public SQLFunctionLeft(object expression, object length)
			: base("LEFT", expression, length)
		{
		}
	}
}