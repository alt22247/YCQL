/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.DBHelpers;
using System.Data.Common;
using System.Text;

namespace YCQL.Constraints
{
	/// <summary>
	/// Represents the Unique Key constraint in SQL
	/// </summary>
	/// <seealso cref="YCQL.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="YCQL.Constraints.CheckConstraint"/>
	/// <seealso cref="YCQL.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.PrimaryKeyConstraint"/>
	public class UniqueKeyConstraint : SQLConstraint
	{
		/// <summary>
		/// Initializes a new instance of the UniqueKeyConstraint class using specified column
		/// </summary>
		/// <param name="column">The column associated with this unique key constraint</param>
		public UniqueKeyConstraint(DBColumn column)
			: this(null, column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the named UniqueKeyConstraint class using specified column
		/// </summary>
		/// <param name="name">The name for this unique key constraint</param>
		/// <param name="column">The column associated with this unique key constraint</param>
		public UniqueKeyConstraint(string name, DBColumn column)
			: base(name)
		{
			Name = name;
			Column = column;
		}

		/// <summary>
		/// Gets or sets the column associated with this unique key constraint
		/// </summary>
		public DBColumn Column { get; set; }

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

			sb.AppendFormat("UNIQUE ({0})", dbHelper.QuoteIdentifier(Column.Name));

			return sb.ToString();
		}
	}
}
