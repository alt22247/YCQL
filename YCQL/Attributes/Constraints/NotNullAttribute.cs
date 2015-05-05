/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Indicates that a column is not null
	/// </summary>
	/// <seealso cref="YCQL.DBColumn.IsNotNull"/>
	/// <seealso cref="YCQL.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="YCQL.DBColumn"/>
	public class NotNullAttribute : SQLAttributeBase
	{
		/// <summary>
		/// Initializes a new instance of the NotNullAttribute class
		/// </summary>
		public NotNullAttribute()
		{
		}
	}
}
