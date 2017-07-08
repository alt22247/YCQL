/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates that a column is a primary key
	/// </summary>
	/// <seealso cref="Ycql.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="Ycql.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.NotNullAttribute"/>
	/// <seealso cref="Ycql.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class PrimaryKeyAttribute : ConstraintAttributeBase
	{
		/// <summary>
		/// Initializes a new instance of the PrimaryKeyAttribute class
		/// </summary>
		public PrimaryKeyAttribute()
		{
		}
	}
}
