/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using Ycql.Interfaces;

namespace Ycql.DbHelpers
{
#pragma warning disable 1574
	/// <summary>
	/// Base class for Sql translate helper of all DBMS
	/// </summary>
	/// <seealso cref="Ycql.DbHelpers.SqlServerHelper"/>
	/// <seealso cref="Ycql.DbHelpers.MySqlHelper"/>
	/// <seealso cref="Ycql.DbVersion"/>
	/// <seealso cref="Ycql.DbEngine"/>
	/// <seealso cref="Ycql.Interfaces.ITranslateSql"/>
#pragma warning restore 1574
	internal abstract class DbHelper
	{
		/// <summary>
		/// Gets the DBEngine associated with this helper
		/// </summary>
		internal readonly DbEngine DbEngine;
		/// <summary>
		/// Gets the DBVersion associated with this helper
		/// </summary>
		internal readonly DbVersion Version;
		/// <summary>
		/// Gets the prefix for the name of DB parameters
		/// </summary>
		protected const string ParameterNamePrefix = "@param";
		/// <summary>
		/// Initializes a new instance of the DBHelper class using specified DBEngine and DBVersion
		/// </summary>
		/// <param name="dbEngine">The DBEngine associated with this helper</param>
		/// <param name="version">The DBVersion associated with this helper</param>
		protected DbHelper(DbEngine dbEngine, DbVersion version)
		{
			DbEngine = dbEngine;
			Version = version;
		}


		static readonly Dictionary<DbVersion, DbHelper> _dbEngineHelperDict;
		static DbHelper()
		{
			_dbEngineHelperDict = new Dictionary<DbVersion, DbHelper>();
#if YCQL_MYSQL
			_dbEngineHelperDict.Add(DbVersion.MySql5_6, new MySqlHelper(DbVersion.MySql5_6));
#endif
#if YCQL_SQLSERVER
			_dbEngineHelperDict.Add(DbVersion.SqlServer2012, new SqlServerHelper(DbVersion.SqlServer2012));
#endif
		}

		internal static DbHelper GetDbHelper(DbVersion dbVersion)
		{
			return _dbEngineHelperDict[dbVersion];
		}

		/// <summary>
		/// Escapes any sql identifier using QuoteIdentifier method of corresponding DB's connector
		/// </summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier</param>
		/// <returns>A string escaped by corresponding DB's connector</returns>
		internal abstract string QuoteIdentifier(string unquotedIdentifier);

		/// <summary>
		/// Returns the corresponding DbType from the interface properties
		/// </summary>
		/// <param name="customDbType">ICustomDbType interface</param>
		/// <returns></returns>
		protected abstract object GetParameterCustomDbType(ICustomDbType customDbType);

		/// <summary>
		/// Creates a corresponding DbParameter instance for the DBMS
		/// </summary>
		/// <param name="name">Name of the parameter</param>
		/// <param name="value">Value of the parameter</param>
		/// <param name="dbType">Custom Db type for the parameter</param>
		/// <returns></returns>
		protected abstract DbParameter CreateDbParameter(string name, object value, object dbType);

		/// <summary>
		/// Adds the specified value into parameterCollection with appropiate name. DBNull will be added if value is null
		/// </summary>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <param name="value">The value to be added into parameterCollection</param>
		/// <returns>Name of the new added parameter</returns>
		string InsertDBParameter(DbParameterCollection parameterCollection, object value)
		{
			return InsertDBParameter(parameterCollection, value, null, -1);
		}

		/// <summary>
		/// Adds the specified value into parameterCollection with appropiate name. DBNull will be added if value is null
		/// </summary>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <param name="value">The value to be added into parameterCollection</param>
		/// <param name="dbType">The explicit DbType for the param</param>
		/// <param name="size">The explicit size for the param or -1 to use the implicit size</param>
		/// <returns></returns>
		string InsertDBParameter(DbParameterCollection parameterCollection, object value, object dbType, int size)
		{
			string name = ParameterNamePrefix + parameterCollection.Count;
			DbParameter parameter = CreateDbParameter(name, value ?? DBNull.Value, dbType);
			if (size >= 0)
				parameter.Size = size;
			parameterCollection.Add(parameter);
			return name;
		}

		/// <summary>
		/// Transforms specified object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="ob">Object to be translated into parameterized SQL</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		internal string TranslateObjectToSqlString(object ob, DbParameterCollection parameterCollection)
		{
			if (ob is ITranslateSql)
			{
				return ((ITranslateSql) ob).ToSql(Version, parameterCollection);
			}
			else if (ob is ICustomDbType)
			{
				ICustomDbType icustomDbType = (ICustomDbType) ob;
				return InsertDBParameter(parameterCollection, icustomDbType.Value, GetParameterCustomDbType(icustomDbType), icustomDbType.Size);
			}
			else
			{
				return InsertDBParameter(parameterCollection, ob);
			}
		}

		/// <summary>
		/// Transforms specified objects into a parameterized Sql statements and joined with "," where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="obs">Objects to be translated into parameterized SQL</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		internal string TranslateObjectsToSqlString(IEnumerable<object> obs, DbParameterCollection parameterCollection)
		{
			return TranslateObjectsToSqlString(",", obs, parameterCollection);
		}

		/// <summary>
		/// Transforms specified objects into a parameterized Sql statements and joined with specified separator where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="separator">Separator to join the translated Sql statements</param>
		/// <param name="obs">Objects to be translated into parameterized SQL</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		internal string TranslateObjectsToSqlString(string separator, IEnumerable<object> obs, DbParameterCollection parameterCollection)
		{
			if (obs == null)
				return string.Empty;

			List<string> sqlStrings = new List<string>();
			foreach (object ob in obs)
				sqlStrings.Add(TranslateObjectToSqlString(ob, parameterCollection));

			return string.Join(separator, sqlStrings);
		}
	}
}
