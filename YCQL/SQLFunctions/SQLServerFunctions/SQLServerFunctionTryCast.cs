/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents TRY_CAST function in Sql Server which returns a value cast to the specified data type if the cast succeeds; otherwise, returns null
	/// </summary>
	public class SQLServerFunctionTryCast : SQLFunctionBase
	{
		object _expression;
		DataType _dataType;

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTryCast class using specified column and new data type
		/// </summary>
		/// <param name="column">The column to be cast</param>
		/// <param name="dataType">The data type into which to cast the column</param>
		public SQLServerFunctionTryCast(DBColumn column, DataType dataType)
			: this((object) column, dataType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTryCast class using specified column and new data type
		/// </summary>
		/// <param name="expression">The value to be cast</param>
		/// <param name="dataType">The data type into which to cast expression</param>
		public SQLServerFunctionTryCast(object expression, DataType dataType)
			: base("TRY_CAST")
		{
			_expression = expression;
			_dataType = dataType;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			return string.Format("TRY_CAST({0} AS {1})", dbHelper.TranslateObjectToSQLString(_expression, parameterCollection), _dataType.ToSQL(dbHelper, parameterCollection));
		}
	}
}
