/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a Sql string which will not be escaped or treat as a parameter during Sql Translation
	/// </summary>
	/// <seealso cref="YCQL.Interfaces.ITranslateSQL"/>
	public class SQLRawText : ITranslateSQL
	{
		/// <summary>
		/// Initializes a new instance of the SQLRawText class using specified Sql string
		/// </summary>
		/// <param name="text">The Sql string</param>
		public SQLRawText(string text)
		{
			Text = text;
		}

		/// <summary>
		/// Gets or sets the Sql string
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Returns an unescaped Sql String
		/// </summary>
		/// <param name="dbHelper">Not used</param>
		/// <param name="parameterCollection">Not used</param>
		/// <returns>Not parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			return Text;
		}
	}
}
