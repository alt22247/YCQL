/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a Sql string which will not be escaped or treat as a parameter during Sql Translation
	/// </summary>
	/// <seealso cref="Ycql.Interfaces.ITranslateSql"/>
	public class SqlRawText : ITranslateSql
	{
		/// <summary>
		/// Initializes a new instance of the SQLRawText class using specified Sql string
		/// </summary>
		/// <param name="text">The Sql string which will not be escaped</param>
		public SqlRawText(string text)
		{
			Text = text;
		}

		/// <summary>
		/// Gets or sets the Sql string which will not be escaped
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Returns an unescaped Sql String
		/// </summary>
		/// <param name="dbVersion">Not used</param>
		/// <param name="parameterCollection">Not used</param>
		/// <returns>Not parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			return Text;
		}
	}
}
