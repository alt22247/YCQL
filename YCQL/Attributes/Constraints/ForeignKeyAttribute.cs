/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates that a column is a foreign key
	/// </summary>
	/// <seealso cref="Ycql.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="Ycql.Attributes.NotNullAttribute"/>
	/// <seealso cref="Ycql.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class ForeignKeyAttribute : ConstraintAttributeBase
	{
		/// <summary>
		/// Temperate DbColumn object associated with the Foreign key constraint
		/// </summary>
		internal readonly DbColumn RefColumn;
		/// <summary>
		/// Initializes a new instance of the ForeignKeyAttribute class using specified reference table and table name
		/// </summary>
		/// <param name="refTableName">Name of the table referenced by the foreign key constraint</param>
		/// <param name="refColumnName">Name of the column referenced by the foreign key constraint</param>
		public ForeignKeyAttribute(string refTableName, string refColumnName)
		{
			RefColumn = new DbColumn(new DbTable(refTableName), refColumnName);
		}
	}
}
