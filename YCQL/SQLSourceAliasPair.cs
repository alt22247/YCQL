/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a pair of sql aliasable source and its alias
	/// </summary>
	/// <seealso cref="Ycql.SqlAlias"/>
	public class SqlSourceAliasPair : ITranslateSql
	{
		/// <summary>
		/// Initializes a new instance of the SQLSourceAliasPair class using specified source and alias string
		/// </summary>
		/// <param name="source">The Sql aliasable source</param>
		/// <param name="aliasName">Alias string for the source</param>
		public SqlSourceAliasPair(object source, string aliasName)
			: this(source, new SqlAlias(aliasName))
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLSourceAliasPair class using specified source and alias
		/// </summary>
		/// <param name="source">The Sql aliasable source</param>
		/// <param name="alias">Alias object for the source</param>
		public SqlSourceAliasPair(object source, SqlAlias alias)
		{
			Source = source;
			Alias = alias;
		}

		/// <summary>
		/// Gets or sets the Sql aliasable source
		/// </summary>
		public object Source { get; set; }

		/// <summary>
		/// Gets or sets the alias for the source
		/// </summary>
		public SqlAlias Alias { get; set; }

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		/// <exception cref="System.NullReferenceException">Thrown when either Alias or Source is null</exception>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			if (Source == null)
				throw new NullReferenceException("Source cannot be null");

			if (Alias == null)
				throw new NullReferenceException("Alias cannot be null");

			StringBuilder sb = new StringBuilder();
			if (Source is SelectBuilder)
				sb.AppendFormat("({0})", dbHelper.TranslateObjectToSqlString(Source, parameterCollection));
			else
				sb.Append(dbHelper.TranslateObjectToSqlString(Source, parameterCollection));

			sb.AppendFormat(" AS {0}", dbHelper.QuoteIdentifier(Alias.AliasName));

			return sb.ToString();
		}
	}
}
