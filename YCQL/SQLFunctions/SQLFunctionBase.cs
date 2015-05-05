/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL.SQLFunctions
{
	/// <summary>
	/// Base class for all SQLFunctions
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
	public abstract class SQLFunctionBase : ITranslateSQL
	{
		/// <summary>
		/// Gets the list of the parameters to be applied to the current function
		/// </summary>
		protected readonly List<object> _parameters;
		/// <summary>
		/// Gets or sets the name of the Sql function name of this instance
		/// </summary>
		protected string _functionName { get; set; }
		bool _useBrackets;

		/// <summary>
		/// Initializes a new instance of the SQLFunctionBase class using specified Sql function name
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		protected SQLFunctionBase(string functionName)
			: this(functionName, true)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionBase class using specified Sql function name and indicates if the function has brackets following the function name
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		/// <param name="useBrackets">Indicates if the function has brackets following the function name</param>
		protected SQLFunctionBase(string functionName, bool useBrackets)
			: this(functionName, useBrackets, new object[] { })
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionBase class using specified Sql function name and parameters to be applied to the function
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		/// <param name="parameters">Parameters to be applied to the function</param>
		protected SQLFunctionBase(string functionName, params object[] parameters)
			: this(functionName, true, parameters)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLFunctionBase class using specified Sql function name, parameters to be applied to the function
		/// and indicates if the function has brackets following the function name
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		/// <param name="useBrackets">Indicates if the function has brackets following the function name</param>
		/// <param name="parameters">Parameters to be applied to the function</param>
		protected SQLFunctionBase(string functionName, bool useBrackets, params object[] parameters)
		{
			_functionName = functionName;
			_useBrackets = useBrackets;
			_parameters = new List<object>();
			_parameters.AddRange(parameters);
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public virtual string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			if (!_useBrackets)
				return _functionName;
			else
				return string.Format("{0}({1})", _functionName, dbHelper.TranslateObjectsToSQLString(_parameters, parameterCollection));
		}
	}
}
