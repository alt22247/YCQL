/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/


namespace Ycql
{
	/// <summary>
	/// Represents the * character (all column) in Sql string
	/// </summary>
	/// <seealso cref="Ycql.SqlRawText"/>
	public class SqlAllColumnSymbol : SqlRawText
	{
		/// <summary>
		/// Initializes a new instance of the SQLAllColumnSymbol class
		/// </summary>
		public SqlAllColumnSymbol()
			: base("*")
		{
		}
	}
}
