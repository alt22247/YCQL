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
	/// Represents Cast function in Sql Server which converts an expression of one data type to another
	/// </summary>
	public class SqlServerFunctionCast : SqlFunctionBase
	{
		object _expression;
		DataType _dataType;
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCast class using specified column and data type to be casted to
		/// </summary>
		/// <param name="column">Column to have its type casted</param>
		/// <param name="dataType">Data type to be casted to</param>
		public SqlServerFunctionCast(DbColumn column, DataType dataType)
			: this((object) column, dataType)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCast class using specified expression and type to cast to
		/// </summary>
		/// <param name="expression">Expression to have its type casted</param>
		/// <param name="dataType">Data type to be casted to</param>
		public SqlServerFunctionCast(object expression, DataType dataType)
			: base("CAST")
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

			return string.Format("CAST({0} AS {1})", dbHelper.TranslateObjectToSqlString(_expression, parameterCollection), _dataType.ToSql(dbVersion, parameterCollection));
		}
	}
}
