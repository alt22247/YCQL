/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

#if YCQL_MYSQL
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using Ycql.Interfaces;

namespace Ycql.DbHelpers
{
#pragma warning disable 1574
	/// <summary>
	/// Represents the helper class for MySql during Sql translation
	/// </summary>
	/// <seealso cref="Ycql.Interfaces.ITranslateSql"/>
	/// <seealso cref="Ycql.DbHelpers.SqlServerHelper"/>
#pragma warning restore 1574
	internal class MySqlHelper : DbHelper
	{
		MySqlCommandBuilder _commandBuilder;
		/// <summary>
		/// Initializes a new instance of the MySQLHelper class using specified DBVersion
		/// </summary>
		/// <param name="version">Version of the MySql query which .ToSql should output</param>
		internal MySqlHelper(DbVersion version)
			: base(DbEngine.MySql, version)
		{
			_commandBuilder = new MySqlCommandBuilder();
		}

		protected override DbParameter CreateDbParameter(string name, object value, object dbType)
		{
			MySqlParameter parameter = new MySqlParameter(name, value);
			if (dbType is MySqlDbType)
				parameter.MySqlDbType = (MySqlDbType) dbType;
			return parameter;
		}

		protected override object GetParameterCustomDbType(ICustomDbType customDbType)
		{
			return customDbType.MySqlDbType;
		}

		/// <summary>
		/// Escapes any MySql identifier using QuoteIdentifier method of MySql's connector
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