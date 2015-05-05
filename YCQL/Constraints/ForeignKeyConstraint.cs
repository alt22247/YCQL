/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;

namespace YCQL.Constraints
{
	/// <summary>
	/// An enum of possible actions for ON DELETE and ON UPDATE in a foreign key constraint
	/// </summary>
	public enum OnDeleteUpdateAction
	{
		/// <summary>
		/// No explicit action specified
		/// </summary>
		Unspecified,
		/// <summary>
		/// An error is raised and the delete action on the row in the parent table is rolled back
		/// </summary>
		NoAction,
		/// <summary>
		/// Corresponding rows are deleted/updated from the referencing table if that row is deleted/updated from the parent table
		/// </summary>
		Cascade,
		/// <summary>
		/// All the values that make up the foreign key are set to NULL when the corresponding row in the parent table is deleted/updated
		/// </summary>
		SetNull,
		/// <summary>
		/// All the values that make up the foreign key are set to their default values when the corresponding row in the parent table is deleted/updated
		/// </summary>
		SetDefault
	}

	/// <summary>
	/// Represents the Foreign Key constraint in SQL
	/// </summary>
	/// <seealso cref="YCQL.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="YCQL.Constraints.CheckConstraint"/>
	/// <seealso cref="YCQL.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.UniqueKeyConstraint"/>
	public class ForeignKeyConstraint : SQLConstraint
	{
		/// <summary>
		/// Initializes a new instance of the ForeignKeyConstraint class using specified source column and referenced column
		/// </summary>
		/// <param name="column">The source column of this foreign key constraint</param>
		/// <param name="refColumn">The referenced column of this foreign key constraint</param>
		public ForeignKeyConstraint(DBColumn column, DBColumn refColumn)
			: this(null, column, refColumn)
		{
		}

		/// <summary>
		/// Initializes a new instance of the named ForeignKeyConstraint class using specified source column and referenced column
		/// </summary>
		/// <param name="name">The name for this foreign key constraint</param>
		/// <param name="column">The source column of this foreign key constraint</param>
		/// <param name="refColumn">The referenced column of this foreign key constraint</param>
		public ForeignKeyConstraint(string name, DBColumn column, DBColumn refColumn)
			: base(name)
		{
			Column = column;
			RefColumn = refColumn;
		}

		/// <summary>
		/// Gets or sets the source column of this foreign key constraint
		/// </summary>
		public DBColumn Column { get; set; }
		/// <summary>
		/// Gets or sets the referenced column of this foreign key constraint
		/// </summary>
		public DBColumn RefColumn { get; set; }

		/// <summary>
		/// Gets or sets the action for ON DELETE expression. Set this to null to if ON DELETE action is not specified (default is null)
		/// </summary>
		public OnDeleteUpdateAction OnDelete { get; set; }
		/// <summary>
		/// Gets or sets the action for ON UPDATE expression. Set this to null to if ON UPDATE action is not specified (default is null)
		/// </summary>
		public OnDeleteUpdateAction OnUpdate { get; set; }

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(Name))
				sb.AppendFormat("CONSTRAINT {0} ", dbHelper.QuoteIdentifier(Name));

			sb.AppendFormat("FOREIGN KEY ({0}) REFERENCES {1}({2})", dbHelper.QuoteIdentifier(Column.Name),
				dbHelper.QuoteIdentifier(RefColumn.ParentTable.Name), dbHelper.QuoteIdentifier(RefColumn.Name));

			if (OnDelete != OnDeleteUpdateAction.Unspecified)
			{
				sb.AppendLine();
				sb.AppendFormat(" ON DELETE {0}", OnDelete.ToString());
				sb.AppendLine();
			}

			if (OnUpdate != OnDeleteUpdateAction.Unspecified)
			{
				sb.AppendLine();
				sb.AppendFormat(" ON UPDATE {0}", OnUpdate.ToString());
				sb.AppendLine();
			}

			return sb.ToString();
		}
	}
}
