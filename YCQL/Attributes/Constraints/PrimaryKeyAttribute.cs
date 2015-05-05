/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Indicates that a column is a primary key
	/// </summary>
	/// <seealso cref="YCQL.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="YCQL.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.NotNullAttribute"/>
	/// <seealso cref="YCQL.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="YCQL.DBColumn"/>
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
