/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a Sql alias
	/// </summary>
	public class SqlAlias : ITranslateSql
	{
		/// <summary>
		/// Initializes a new instance of the SQLSourceAlias class using specified alias string
		/// </summary>
		/// <param name="aliasName">The sql alias name</param>
		public SqlAlias(string aliasName)
		{
			if (string.IsNullOrEmpty(aliasName))
				throw new ArgumentNullException("aliasName");

			AliasName = aliasName;
		}

		/// <summary>
		/// Gets or sets the alias name
		/// </summary>
		public string AliasName { get; set; }

		/// <summary>
		/// Returns an Sql escaped alias name
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">Not used</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			return dbHelper.QuoteIdentifier(AliasName);
		}

		/// <summary>
		/// Returns the AliasName string
		/// </summary>
		/// <returns>the AliasName string</returns>
		public override string ToString()
		{
			return AliasName;
		}
	}
}
