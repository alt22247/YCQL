/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents TRY_CAST function in Sql Server which returns a value cast to the specified data type if the cast succeeds; otherwise, returns null
	/// </summary>
	public class SqlServerFunctionTryCast : SqlFunctionBase
	{
		object _expression;
		DataType _dataType;

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryCast class using specified column and new data type
		/// </summary>
		/// <param name="column">The column to be cast</param>
		/// <param name="dataType">The data type into which to cast the column</param>
		public SqlServerFunctionTryCast(DbColumn column, DataType dataType)
			: this((object) column, dataType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryCast class using specified column and new data type
		/// </summary>
		/// <param name="expression">The value to be cast</param>
		/// <param name="dataType">The data type into which to cast expression</param>
		public SqlServerFunctionTryCast(object expression, DataType dataType)
			: base("TRY_CAST")
		{
			_expression = expression;
			_dataType = dataType;
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

			return string.Format("TRY_CAST({0} AS {1})", dbHelper.TranslateObjectToSqlString(_expression, parameterCollection), _dataType.ToSql(dbVersion, parameterCollection));
		}
	}
}
