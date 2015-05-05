/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Indicates that a column is a foreign key
	/// </summary>
	/// <seealso cref="YCQL.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="YCQL.Attributes.NotNullAttribute"/>
	/// <seealso cref="YCQL.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="YCQL.DBColumn"/>
	public class ForeignKeyAttribute : ConstraintAttributeBase
	{
		/// <summary>
		/// Temperate DBColumn object associated with the Foreign key constraint
		/// </summary>
		internal readonly DBColumn RefColumn;
		/// <summary>
		/// Initializes a new instance of the ForeignKeyAttribute class using specified reference table and table name
		/// </summary>
		/// <param name="refTableName">Name of the table referenced by the foreign key constraint</param>
		/// <param name="refColumnName">Name of the column referenced by the foreign key constraint</param>
		public ForeignKeyAttribute(string refTableName, string refColumnName)
		{
			RefColumn = new DBColumn(new DBTable(refTableName), refColumnName);
		}
	}
}
