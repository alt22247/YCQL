/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace YCQL.DBHelpers
{
	/// <summary>
	/// Represents the helper class for MySql during Sql translation
	/// </summary>
	/// <seealso cref="YCQL.Interfaces.ITranslateSQL"/>
	/// <seealso cref="YCQL.DBHelpers.SQLServerHelper"/>
	public class MySQLHelper : DBHelper
	{
		MySqlCommandBuilder _commandBuilder;
		/// <summary>
		/// Initializes a new instance of the MySQLHelper class using specified DBVersion
		/// </summary>
		/// <param name="version">Version of the MySql query which ToSQL should output</param>
		public MySQLHelper(DBVersion version)
			: base(DBEngine.MySQL, version)
		{
			_commandBuilder = new MySqlCommandBuilder();
		}

		/// <summary>
		/// Escapes any MySql identifier using QuoteIdentifier method of MySql's connector
		/// </summary>
		/// <param name="unquotedIdentifier">The original unquoted identifier</param>
		/// <returns>A string escaped by corresponding DB's connector</returns>
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return _commandBuilder.QuoteIdentifier(unquotedIdentifier);
		}

		/// <summary>
		/// Adds the specified value into parameterCollection with appropiate name. DBNull will be added if value is null
		/// </summary>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <param name="value">The value to be added into parameterCollection</param>
		/// <returns>Name of the new added parameter</returns>
		protected override string InsertDBParameter(DbParameterCollection parameterCollection, object value)
		{
			string name = ParameterNamePrefix + parameterCollection.Count;
			MySqlParameter parameter = new MySqlParameter(name, value ?? DBNull.Value);
			parameterCollection.Add(parameter);
			return name;
		}
	}
}
