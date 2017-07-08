/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates that a column is a unique key
	/// </summary>
	/// <seealso cref="Ycql.Constraints.UniqueKeyConstraint"/>
	/// <seealso cref="Ycql.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.NotNullAttribute"/>
	/// <seealso cref="Ycql.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class UniqueKeyAttribute : ConstraintAttributeBase
	{
		/// <summary>
		/// Initializes a new instance of the UniqueKeyAttribute class
		/// </summary>
		public UniqueKeyAttribute()
		{
		}
	}
}
