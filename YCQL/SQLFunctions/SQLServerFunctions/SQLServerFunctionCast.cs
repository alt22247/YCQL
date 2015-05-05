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
	/// Represents Cast function in Sql Server which converts an expression of one data type to another
	/// </summary>
	public class SQLServerFunctionCast : SQLFunctionBase
	{
		object _expression;
		DataType _dataType;
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionCast class using specified column and data type to be casted to
		/// </summary>
		/// <param name="column">Column to have its type casted</param>
		/// <param name="dataType">Data type to be casted to</param>
		public SQLServerFunctionCast(DBColumn column, DataType dataType)
			: this((object) column, dataType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionCast class using specified expression and type to cast to
		/// </summary>
		/// <param name="expression">Expression to have its type casted</param>
		/// <param name="dataType">Data type to be casted to</param>
		public SQLServerFunctionCast(object expression, DataType dataType)
			: base("CAST")
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
			return string.Format("CAST({0} AS {1})", dbHelper.TranslateObjectToSQLString(_expression, parameterCollection), _dataType.ToSQL(dbHelper, parameterCollection));
		}
	}
}
