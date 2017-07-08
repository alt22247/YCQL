/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Attributes
{
	/// <summary>
	/// Base class for constraint attributes
	/// </summary>
	/// <seealso cref="Ycql.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.NotNullAttribute"/>
	/// <seealso cref="Ycql.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public abstract class ConstraintAttributeBase : SqlAttributeBase
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
