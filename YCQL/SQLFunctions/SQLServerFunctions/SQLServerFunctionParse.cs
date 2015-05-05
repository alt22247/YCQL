/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Parse function in Sql Server which returns the result of an expression, translated to the requested data type in SQL Server
	/// </summary>
	public class SQLServerFunctionParse : SQLFunctionBase
	{
		object _value;
		object _dataType;
		object _culture;

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionParse class using specified value and data type
		/// </summary>
		/// <param name="string_value">The formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SQLServerFunctionParse(string string_value, DataType dataType)
			: this(string_value, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionParse class using specified expression and data type
		/// </summary>
		/// <param name="expression">The formatted expression to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SQLServerFunctionParse(object expression, object dataType)
			: this(expression, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionParse class using specified expression, data type and culture
		/// </summary>
		/// <param name="stringValue">The formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		/// <param name="culture">String that identifies the culture in which stringValue is formatted</param>
		public SQLServerFunctionParse(string stringValue, DataType dataType, string culture)
			: this((object) stringValue, (object) dataType, (object) culture)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionParse class using specified expression, data type and culture
		/// </summary>
		/// <param name="stringExpression">The formatted expression to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		/// <param name="culture">String expression that identifies the culture in which stringExpression is formatted</param>
		public SQLServerFunctionParse(object stringExpression, object dataType, object culture)
			: base("PARSE")
		{
			_value = stringExpression;
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
			sb.AppendFormat("PARSE ({0} AS {1}", dbHelper.TranslateObjectToSQLString(_value, parameterCollection),
				dbHelper.TranslateObjectToSQLString(_dataType, parameterCollection));

			if (_culture != null)
				sb.AppendFormat(" USING {0}", dbHelper.TranslateObjectToSQLString(_culture, parameterCollection));

			sb.Append(")");
			return sb.ToString();
		}
	}
}
