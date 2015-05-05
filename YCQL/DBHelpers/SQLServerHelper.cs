/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace YCQL.DBHelpers
{
	/// <summary>
	/// Represents the helper class for Sql Server during Sql translation
	/// </summary>
	/// <seealso cref="YCQL.Interfaces.ITranslateSQL"/>
	/// <seealso cref="YCQL.DBHelpers.MySQLHelper"/>
	public class SQLServerHelper : DBHelper
	{
		SqlCommandBuilder _commandBuilder = new SqlCommandBuilder();
		/// <summary>
		/// Initializes a new instance of the SQLServerHelper class using specified DBVersion
		/// </summary>
		/// <param name="version">Version of the Sql Server query which ToSQL should output</param>
		public SQLServerHelper(DBVersion version)
			: base(DBEngine.SQLServer, version)
		{
			_commandBuilder = new SqlCommandBuilder();
		}

		/// <summary>
		/// Escapes any Sql Server identifier using QuoteIdentifier method of Sql Server's connector
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
			SqlParameter parameter = new SqlParameter(name, value ?? DBNull.Value);
			parameterCollection.Add(parameter);
			return name;
		}
	}
}
