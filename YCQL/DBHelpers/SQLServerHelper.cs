/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

#if YCQL_SQLSERVER
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Ycql.Interfaces;

namespace Ycql.DbHelpers
{
#pragma warning disable 1574
	/// <summary>
	/// Represents the helper class for Sql Server during Sql translation
	/// </summary>
	/// <seealso cref="Ycql.Interfaces.ITranslateSql"/>
	/// <seealso cref="Ycql.DbHelpers.MySqlHelper"/>
#pragma warning restore 1574
	internal class SqlServerHelper : DbHelper
	{
		SqlCommandBuilder _commandBuilder = new SqlCommandBuilder();
		/// <summary>
		/// Initializes a new instance of the SQLServerHelper class using specified DBVersion
		/// </summary>
		/// <param name="version">Version of the Sql Server query which .ToSql should output</param>
		internal SqlServerHelper(DbVersion version)
			: base(DbEngine.SqlServer, version)
		{
			_commandBuilder = new SqlCommandBuilder();
		}

		protected override DbParameter CreateDbParameter(string name, object value, object dbType)
		{
			SqlParameter parameter = new SqlParameter(name, value);
			if (dbType is SqlDbType)
				parameter.SqlDbType = (SqlDbType) dbType;
			return parameter;
		}

		protected override object GetParameterCustomDbType(ICustomDbType customDbType)
		{
			return customDbType.SqlDbType;
		}

		/// <summary>
		/// Escapes any Sql Server identifier using QuoteIdentifier method of Sql Server's connector
		/// </summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier</param>
		/// <returns>A string escaped by corresponding DB's connector</returns>
		internal override string QuoteIdentifier(string unquotedIdentifier)
		{
			return _commandBuilder.QuoteIdentifier(unquotedIdentifier);
		}
	}
}
#endif