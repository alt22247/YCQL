/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates that a column is not null
	/// </summary>
	/// <seealso cref="Ycql.DbColumn.IsNotNull"/>
	/// <seealso cref="Ycql.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class NotNullAttribute : SqlAttributeBase
	{
		/// <summary>
		/// Initializes a new instance of the NotNullAttribute class
		/// </summary>
		public NotNullAttribute()
		{
		}
	}
}
