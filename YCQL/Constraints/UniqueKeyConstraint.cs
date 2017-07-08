/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.DbHelpers;
using System.Data.Common;
using System.Text;

namespace Ycql.Constraints
{
	/// <summary>
	/// Represents the Unique Key constraint in SQL
	/// </summary>
	/// <seealso cref="Ycql.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="Ycql.Constraints.CheckConstraint"/>
	/// <seealso cref="Ycql.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.PrimaryKeyConstraint"/>
	public class UniqueKeyConstraint : SqlConstraint
	{
		/// <summary>
		/// Initializes a new instance of the UniqueKeyConstraint class using specified column
		/// </summary>
		/// <param name="column">The column associated with this unique key constraint</param>
		public UniqueKeyConstraint(DbColumn column)
			: this(null, column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the named UniqueKeyConstraint class using specified column
		/// </summary>
		/// <param name="name">The name for this unique key constraint</param>
		/// <param name="column">The column associated with this unique key constraint</param>
		public UniqueKeyConstraint(string name, DbColumn column)
			: base(name)
		{
			Name = name;
			Column = column;
		}

		/// <summary>
		/// Gets or sets the column associated with this unique key constraint
		/// </summary>
		public DbColumn Column { get; set; }

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(Name))
				sb.AppendFormat("CONSTRAINT {0} ", dbHelper.QuoteIdentifier(Name));

			sb.AppendFormat("UNIQUE ({0})", dbHelper.QuoteIdentifier(Column.ColumnName));

			return sb.ToString();
		}
	}
}
