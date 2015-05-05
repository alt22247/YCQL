/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a Sql alias
	/// </summary>
	public class SQLAlias : ITranslateSQL
	{
		/// <summary>
		/// Initializes a new instance of the SQLSourceAlias class using specified alias string
		/// </summary>
		/// <param name="aliasName">The sql alias name</param>
		public SQLAlias(string aliasName)
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
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">Not used</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
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
