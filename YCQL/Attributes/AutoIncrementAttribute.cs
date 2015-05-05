/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Indicates that a column is an auto increment column (for MySql)
	/// </summary>
	/// <seealso cref="YCQL.DBColumn.IsAutoIncrement"/>
	public class AutoIncrementAttribute : SQLAttributeBase
	{
		/// <summary>
		/// Initializes a new instance of the AutoIncrementAttribute class
		/// </summary>
		public AutoIncrementAttribute()
		{
		}
	}
}
