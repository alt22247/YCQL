/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;

namespace YCQL.SQLFunctions
{
	/// <summary>
	/// Represents ATan2 function in Sql
	/// </summary>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionAbs"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionACos"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionASCII"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionASin"/>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionATan"/>
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
	public class SQLFunctionATan2 : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">Column which will be used as y value</param>
		/// <param name="x">x value</param>
		public SQLFunctionATan2(DBColumn y, float x)
			: this((object) y, (object) x)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">y value</param>
		/// <param name="x">Column which will be used as x value</param>
		public SQLFunctionATan2(float y, DBColumn x)
			: this((object) y, (object) x)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">Column which will be used as y value</param>
		/// <param name="x">Column which will be used as x value</param>
		public SQLFunctionATan2(DBColumn y, DBColumn x)
			: this((object) y, (object) x)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">y value</param>
		/// <param name="x">x value</param>
		public SQLFunctionATan2(object y, object x)
			: base("ATAN2", y, x)
		{
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			_functionName = dbHelper.DBEngine == DBEngine.SQLServer ? "ATN2" : "ATAN2";
			return base.ToSQL(dbHelper, parameterCollection);
		}
	}
}