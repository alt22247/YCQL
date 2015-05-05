/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Base class for constraint attributes
	/// </summary>
	/// <seealso cref="YCQL.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.NotNullAttribute"/>
	/// <seealso cref="YCQL.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="YCQL.DBColumn"/>
	public abstract class ConstraintAttributeBase : SQLAttributeBase
	{
		/// <summary>
		/// Gets or sets the Name of this constraint
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Initializes a new instance of the ConstraintAttributeBase class
		/// </summary>
		protected ConstraintAttributeBase()
		{
		}
	}
}
