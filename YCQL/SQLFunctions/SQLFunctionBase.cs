/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql.SqlFunctions
{
	/// <summary>
	/// Base class for all SqlFunctions
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
	public abstract class SqlFunctionBase : ITranslateSql
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
		/// Initializes a new instance of the SqlFunctionBase class using specified Sql function name
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		protected SqlFunctionBase(string functionName)
			: this(functionName, true)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlFunctionBase class using specified Sql function name and indicates if the function has brackets following the function name
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		/// <param name="useBrackets">Indicates if the function has brackets following the function name</param>
		protected SqlFunctionBase(string functionName, bool useBrackets)
			: this(functionName, useBrackets, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlFunctionBase class using specified Sql function name and parameters to be applied to the function
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		/// <param name="parameters">Parameters to be applied to the function</param>
		protected SqlFunctionBase(string functionName, params object[] parameters)
			: this(functionName, true, parameters)
		{
		}


		/// <summary>
		/// Initializes a new instance of the SqlFunctionBase class using specified Sql function name, parameters to be applied to the function
		/// and indicates if the function has brackets following the function name
		/// </summary>
		/// <param name="functionName">The name of the Sql function name of this instance</param>
		/// <param name="useBrackets">Indicates if the function has brackets following the function name</param>
		/// <param name="parameters">Parameters to be applied to the function</param>
		protected SqlFunctionBase(string functionName, bool useBrackets, params object[] parameters)
		{
			_functionName = functionName;
			_useBrackets = useBrackets;
			if (parameters != null)
				_parameters = parameters.Unwrap();
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public virtual string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			if (!_useBrackets)
				return _functionName;
			else
				return string.Format("{0}({1})", _functionName, dbHelper.TranslateObjectsToSqlString(_parameters, parameterCollection));
		}
	}
}
