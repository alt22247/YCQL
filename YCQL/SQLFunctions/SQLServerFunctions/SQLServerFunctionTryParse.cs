/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.DbHelpers;
using Ycql.SqlFunctions;
using System.Data.Common;
using System.Text;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents TRY_PARSE function in Sql Server which returns the result of an expression, translated to the requested data type, or null if the cast fails
	/// Use TRY_PARSE only for converting from string to date/time and number types
	/// </summary>
	public class SqlServerFunctionTryParse : SqlFunctionBase
	{
		object _value;
		DataType _dataType;
		object _culture;

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryParse class using specified column to parse and the requested data type
		/// </summary>
		/// <param name="column">A string column</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SqlServerFunctionTryParse(DbColumn column, DataType dataType)
			: this(column, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryParse class using specified string value and the requested data type
		/// </summary>
		/// <param name="value">nvarchar(4000)  value representing the formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SqlServerFunctionTryParse(string value, DataType dataType)
			: this(value, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionTryParse class using specified string value, the requested data type and culture
		/// </summary>
		/// <param name="value">nvarchar(4000)  value representing the formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		/// <param name="culture">String that identifies the culture in which string_value is formatted</param>
		public SqlServerFunctionTryParse(object value, DataType dataType, object culture)
			: base("TRY_PARSE")
		{
			_value = value;
			_dataType = dataType;
			_culture = culture;
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

			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("TRY_PARSE ({0} AS {1}", dbHelper.TranslateObjectToSqlString(_value, parameterCollection),
				_dataType.ToSql(dbVersion, parameterCollection));

			if (_culture != null)
				sb.AppendFormat(" USING {0}", dbHelper.TranslateObjectToSqlString(_culture, parameterCollection));

			sb.Append(")");
			return sb.ToString();
		}
	}
}
