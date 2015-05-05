/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.DBHelpers;
using YCQL.SQLFunctions;
using System.Data.Common;
using System.Text;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents TRY_PARSE function in Sql Server which returns the result of an expression, translated to the requested data type, or null if the cast fails
	/// Use TRY_PARSE only for converting from string to date/time and number types
	/// </summary>
	public class SQLServerFunctionTryParse : SQLFunctionBase
	{
		object _value;
		DataType _dataType;
		object _culture;

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTryParse class using specified column to parse and the requested data type
		/// </summary>
		/// <param name="column">A string column</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SQLServerFunctionTryParse(DBColumn column, DataType dataType)
			: this(column, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTryParse class using specified string value and the requested data type
		/// </summary>
		/// <param name="value">nvarchar(4000)  value representing the formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SQLServerFunctionTryParse(string value, DataType dataType)
			: this(value, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionTryParse class using specified string value, the requested data type and culture
		/// </summary>
		/// <param name="value">nvarchar(4000)  value representing the formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		/// <param name="culture">String that identifies the culture in which string_value is formatted</param>
		public SQLServerFunctionTryParse(object value, DataType dataType, object culture)
			: base("TRY_PARSE")
		{
			_value = value;
			_dataType = dataType;
			_culture = culture;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("TRY_PARSE ({0} AS {1}", dbHelper.TranslateObjectToSQLString(_value, parameterCollection),
				_dataType.ToSQL(dbHelper, parameterCollection));

			if (_culture != null)
				sb.AppendFormat(" USING {0}", dbHelper.TranslateObjectToSQLString(_culture, parameterCollection));

			sb.Append(")");
			return sb.ToString();
		}
	}
}
