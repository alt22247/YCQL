/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Indicates that a column is a unique key
	/// </summary>
	/// <seealso cref="YCQL.Constraints.UniqueKeyConstraint"/>
	/// <seealso cref="YCQL.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.NotNullAttribute"/>
	/// <seealso cref="YCQL.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="YCQL.DBColumn"/>
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
