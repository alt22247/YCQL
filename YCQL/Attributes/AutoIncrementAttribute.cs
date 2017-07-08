/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

#if YCQL_MYSQL
namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates that a column is an auto increment column (for MySql)
	/// </summary>
	/// <seealso cref="Ycql.DbColumn.IsAutoIncrement"/>
	public class AutoIncrementAttribute : SqlAttributeBase
	{
		/// <summary>
		/// Initializes a new instance of the AutoIncrementAttribute class
		/// </summary>
		public AutoIncrementAttribute()
		{
		}
	}
}
#endif