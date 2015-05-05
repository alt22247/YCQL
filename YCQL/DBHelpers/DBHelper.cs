/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using YCQL;
using YCQL.Interfaces;

namespace YCQL.DBHelpers
{
	/// <summary>
	/// Base class for Sql translate helper of all DBMS
	/// </summary>
	/// <seealso cref="YCQL.DBHelpers.SQLServerHelper"/>
	/// <seealso cref="YCQL.DBHelpers.MySQLHelper"/>
	/// <seealso cref="YCQL.DBVersion"/>
	/// <seealso cref="YCQL.DBEngine"/>
	/// <seealso cref="YCQL.Interfaces.ITranslateSQL"/>
	public abstract class DBHelper
	{
		/// <summary>
		/// Gets the DBEngine associated with this helper
		/// </summary>
		public readonly DBEngine DBEngine;
		/// <summary>
		/// Gets the DBVersion associated with this helper
		/// </summary>
		public readonly DBVersion Version;
		/// <summary>
		/// Gets or sets the prefix for the name of DB parameters
		/// </summary>
		public string ParameterNamePrefix { get; set; }
		/// <summary>
		/// Initializes a new instance of the DBHelper class using specified DBEngine and DBVersion
		/// </summary>
		/// <param name="dbEngine">The DBEngine associated with this helper</param>
		/// <param name="version">The DBVersion associated with this helper</param>
		protected DBHelper(DBEngine dbEngine, DBVersion version)
		{
			DBEngine = dbEngine;
			Version = version;
			ParameterNamePrefix = "@param";
		}

		/// <summary>
		/// Escapes any sql identifier using QuoteIdentifier method of corresponding DB's connector
		/// </summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier</param>
		/// <returns>A string escaped by corresponding DB's connector</returns>
		public abstract string QuoteIdentifier(string unquotedIdentifier);
		/// <summary>
		/// Adds the specified value into parameterCollection with appropiate name. DBNull will be added if value is null
		/// </summary>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <param name="value">The value to be added into parameterCollection</param>
		/// <returns>Name of the new added parameter</returns>
		protected abstract string InsertDBParameter(DbParameterCollection parameterCollection, object value);

		/// <summary>
		/// Transforms specified object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="ob">Object to be translated into parameterized SQL</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string TranslateObjectToSQLString(object ob, DbParameterCollection parameterCollection)
		{
			if (ob is SelectBuilder)
				return string.Format("({0})", ((SelectBuilder) ob).ToSQL(this, parameterCollection));
			else if (ob is ITranslateSQL)
				return ((ITranslateSQL) ob).ToSQL(this, parameterCollection);
			else
				return InsertDBParameter(parameterCollection, ob);
		}

		/// <summary>
		/// Transforms specified objects into a parameterized Sql statements and joined with "," where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="obs">Objects to be translated into parameterized SQL</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string TranslateObjectsToSQLString(IEnumerable<object> obs, DbParameterCollection parameterCollection)
		{
			return TranslateObjectsToSQLString(",", obs, parameterCollection);
		}

		/// <summary>
		/// Transforms specified objects into a parameterized Sql statements and joined with specified separator where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="separator">Separator to join the translated Sql statements</param>
		/// <param name="obs">Objects to be translated into parameterized SQL</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string TranslateObjectsToSQLString(string separator, IEnumerable<object> obs, DbParameterCollection parameterCollection)
		{
			List<string> sqlStrings = new List<string>();
			foreach (object ob in obs)
				sqlStrings.Add(TranslateObjectToSQLString(ob, parameterCollection));

			return string.Join(separator, sqlStrings);
		}
	}
}
