﻿/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.SQLFunctions
{
	/// <summary>
	/// Represents Round function in Sql
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
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionLeft"/>
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
	public class SQLFunctionRound : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLFunctionRound class using specified column and default decimals of rounding
		/// </summary>
		/// <param name="column">Column to be rounded</param>
		public SQLFunctionRound(DBColumn column)
			: this((object) column, 0)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionRound class using specified expression and default decimals of rounding
		/// </summary>
		/// <param name="expression">Expression to be rounded</param>
		public SQLFunctionRound(object expression)
			: this(expression, 0)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionRound class using specified column and decimals of rounding
		/// </summary>
		/// <param name="column">Column to be rounded</param>
		/// <param name="decimals">Number of decimal places to round to</param>
		public SQLFunctionRound(DBColumn column, int decimals)
			: this((object) column, decimals)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionRound class using specified expression and decimals of rounding
		/// </summary>
		/// <param name="expression">Expression to be rounded</param>
		/// <param name="decimals">Number of decimal places to round to</param>
		public SQLFunctionRound(object expression, int decimals)
			: base("ROUND", expression, decimals)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionRound class using specified column, decimals of rounding and rounding function to use (for Sql Server)
		/// </summary>
		/// <param name="column">Column to be rounded</param>
		/// <param name="decimals">Number of decimal places to round to</param>
		/// <param name="function">The type of operation to perform (See Sql Server documentation for details)</param>
		public SQLFunctionRound(DBColumn column, int decimals, int function)
			: this((object) column, decimals, function)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionRound class using specified expression, decimals of rounding and rounding function to use (for Sql Server)
		/// </summary>
		/// <param name="expression">Expression to be rounded</param>
		/// <param name="decimals">Number of decimal places to round to</param>
		/// <param name="function">The type of operation to perform (See Sql Server documentation for details)</param>
		public SQLFunctionRound(object expression, int decimals, int function)
			: base("ROUND", expression, decimals, function)
		{
		}
	}
}