/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Linq;
using Ycql.DbHelpers;
using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents Contains function in Sql Server which Searches for precise or fuzzy (less precise) 
	/// matches to single words and phrases, words within a certain distance of one another, or weighted matches 
	/// </summary>
	public class SqlServerFunctionContains : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionContains class using specified column and the search condition string
		/// </summary>
		/// <param name="column">A full-text indexed column of the table specified in the FROM clause</param>
		/// <param name="searchCondition">Specifies that the query will search all full-text indexed columns in the table specified in the FROM clause for the given search condition</param>
		public SqlServerFunctionContains(DbColumn column, string searchCondition)
			: this(searchCondition, column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionContains class using specified columns and the search condition string
		/// </summary>
		/// <param name="searchCondition">Specifies that the query will search all full-text indexed columns in the table specified in the FROM clause for the given search condition</param>
		/// <param name="columns">Multiple full-text indexed columns of the table specified in the FROM clause</param>
		public SqlServerFunctionContains(string searchCondition, params DbColumn[] columns)
			: base("CONTAINS", columns, searchCondition)
		{
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			//If there is only one column, use the base ToSql
			if (_parameters.Count == 2)
				return base.ToSql(dbVersion, parameterCollection);

			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);
			return string.Format("{0}(({1}), {2})", _functionName,
				dbHelper.TranslateObjectsToSqlString(_parameters.Take(_parameters.Count - 1), parameterCollection),
				dbHelper.TranslateObjectToSqlString(_parameters.Last(), parameterCollection));
		}
	}
}
