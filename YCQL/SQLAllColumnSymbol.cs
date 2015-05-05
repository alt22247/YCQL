/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/


namespace YCQL
{
	/// <summary>
	/// Represents the * character (all column) in Sql string
	/// </summary>
	/// <seealso cref="YCQL.SQLRawText"/>
	public class SQLAllColumnSymbol : SQLRawText
	{
		/// <summary>
		/// Initializes a new instance of the SQLAllColumnSymbol class
		/// </summary>
		public SQLAllColumnSymbol()
			: base("*")
		{
		}
	}
}
