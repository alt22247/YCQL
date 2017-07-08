/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using Ycql.DbHelpers;

namespace Ycql.Constraints
{
	/// <summary>
	/// Represents the Primary Key constraint in SQL
	/// </summary>
	/// <seealso cref="Ycql.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="Ycql.Constraints.CheckConstraint"/>
	/// <seealso cref="Ycql.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.UniqueKeyConstraint"/>
	public class PrimaryKeyConstraint : SqlConstraint
	{
		/// <summary>
		/// Initializes a new instance of the CheckConstraint class using specified column
		/// </summary>
		/// <param name="column">The column associated with this primary key constraint</param>
		public PrimaryKeyConstraint(DbColumn column)
			: this(null, column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the named PrimaryKeyConstraint class using specified column
		/// </summary>
		/// <param name="name">The name of this primary key constraint</param>
		/// <param name="column">The column associated with this primary key constraint</param>
		public PrimaryKeyConstraint(string name, DbColumn column)
			: base(name)
		{
			Column = column;
		}

		/// <summary>
		/// Gets or sets the column associated with this primary key constraint
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

			sb.AppendFormat("PRIMARY KEY ({0})", dbHelper.QuoteIdentifier(Column.ColumnName));

			return sb.ToString();
		}
	}
}
