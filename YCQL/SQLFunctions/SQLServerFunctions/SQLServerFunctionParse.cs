/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;
using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Parse function in Sql Server which returns the result of an expression, translated to the requested data type in SQL Server
	/// </summary>
	public class SqlServerFunctionParse : SqlFunctionBase
	{
		object _value;
		object _dataType;
		object _culture;

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionParse class using specified value and data type
		/// </summary>
		/// <param name="string_value">The formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SqlServerFunctionParse(string string_value, DataType dataType)
			: this(string_value, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionParse class using specified expression and data type
		/// </summary>
		/// <param name="expression">The formatted expression to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		public SqlServerFunctionParse(object expression, object dataType)
			: this(expression, dataType, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionParse class using specified expression, data type and culture
		/// </summary>
		/// <param name="stringValue">The formatted value to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		/// <param name="culture">String that identifies the culture in which stringValue is formatted</param>
		public SqlServerFunctionParse(string stringValue, DataType dataType, string culture)
			: this((object) stringValue, (object) dataType, (object) culture)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionParse class using specified expression, data type and culture
		/// </summary>
		/// <param name="stringExpression">The formatted expression to parse into the specified data type</param>
		/// <param name="dataType">The data type requested for the result</param>
		/// <param name="culture">String expression that identifies the culture in which stringExpression is formatted</param>
		public SqlServerFunctionParse(object stringExpression, object dataType, object culture)
			: base("PARSE")
		{
			_value = stringExpression;
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
			sb.AppendFormat("PARSE ({0} AS {1}", dbHelper.TranslateObjectToSqlString(_value, parameterCollection),
				dbHelper.TranslateObjectToSqlString(_dataType, parameterCollection));

			if (_culture != null)
				sb.AppendFormat(" USING {0}", dbHelper.TranslateObjectToSqlString(_culture, parameterCollection));

			sb.Append(")");
			return sb.ToString();
		}
	}
}
