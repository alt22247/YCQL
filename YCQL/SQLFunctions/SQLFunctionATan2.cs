﻿/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;

namespace Ycql.SqlFunctions
{
	/// <summary>
	/// Represents ATan2 function in Sql
	/// </summary>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionAbs"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionACos"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionASCII"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionASin"/>
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionATan"/>
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
	/// <seealso cref="Ycql.SqlFunctions.SqlFunctionMax"/>
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
	public class SqlFunctionATan2 : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">Column which will be used as y value</param>
		/// <param name="x">x value</param>
		public SqlFunctionATan2(DbColumn y, float x)
			: this((object) y, (object) x)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">y value</param>
		/// <param name="x">Column which will be used as x value</param>
		public SqlFunctionATan2(float y, DbColumn x)
			: this((object) y, (object) x)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">Column which will be used as y value</param>
		/// <param name="x">Column which will be used as x value</param>
		public SqlFunctionATan2(DbColumn y, DbColumn x)
			: this((object) y, (object) x)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlFunctionATan2 class using specified y and x values
		/// </summary>
		/// <param name="y">y value</param>
		/// <param name="x">x value</param>
		public SqlFunctionATan2(object y, object x)
			: base("ATAN2", y, x)
		{
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);
#if YCQL_SQLSERVER
			_functionName = dbHelper.DbEngine == DbEngine.SqlServer ? "ATN2" : "ATAN2";
#endif
			return base.ToSql(dbVersion, parameterCollection);
		}
	}
}