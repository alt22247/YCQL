/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents the identity property of a column in SQL Server
	/// </summary>
	/// <seealso cref="YCQL.DBColumn"/>
	/// <seealso cref="YCQL.DBColumn.IsAutoIncrement"/>
	public class Identity : ITranslateSQL
	{
		/// <summary>
		/// Initializes a new instance of the Identity class
		/// </summary>
		public Identity()
			: this(1, 1)
		{
		}

		/// <summary>
		/// Initializes a new instance of the Identity class using specified seed and increment value
		/// </summary>
		/// <param name="seed">The seed value of this identity property</param>
		/// <param name="increment">The increment value of this identity property</param>
		public Identity(int seed, int increment)
		{
			Seed = seed;
			Increment = increment;
		}

		/// <summary>
		/// Gets or sets the value that is used for the very first row loaded into the table
		/// </summary>
		public int Seed { get; set; }
		/// <summary>
		/// Gets or sets the incremental value that is added to the identity value of the previous row that was loaded
		/// </summary>
		public int Increment { get; set; }
		/// <summary>
		/// Returns a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			return (string.Format("IDENTITY({0},{1})", dbHelper.TranslateObjectToSQLString(Seed, parameterCollection),
													   dbHelper.TranslateObjectToSQLString(Increment, parameterCollection)));
		}
	}

	/// <summary>
	/// Represents a single column in a Database table.
	/// </summary>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.SQLAlias"/>
	/// <seealso cref="YCQL.DataType"/>
	/// <seealso cref="YCQL.Identity"/>
	/// <seealso cref="YCQL.Attributes.AutoIncrementAttribute"/>
	/// <seealso cref="YCQL.Attributes.DataTypeAttribute"/>
	/// <seealso cref="YCQL.Attributes.IdentityAttribute"/>
	/// <seealso cref="YCQL.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.NotNullAttribute"/>
	/// <seealso cref="YCQL.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="YCQL.Attributes.UniqueKeyAttribute"/>
	public class DBColumn : ITranslateSQL
	{
		/// <summary>
		/// Gets the parent table object of this column
		/// </summary>
		public readonly DBTable ParentTable;
		/// <summary>
		/// Gets the name of this column
		/// </summary>
		public readonly string Name;
		/// <summary>
		/// Gets the default Alias object of this column
		/// </summary>
		public readonly SQLAlias DefaultAlias;
		/// <summary>
		/// Initializes a new instance of the DBColumn class using specified parent object and column name
		/// </summary>
		/// <param name="parentTable">The table object which this column belongs to</param>
		/// <param name="name">The name of this column</param>
		public DBColumn(DBTable parentTable, string name)
		{
			ParentTable = parentTable;
			Name = name;

			DefaultAlias = new SQLAlias(parentTable.Name + "." + name);
		}

		/// <summary>
		/// Gets or sets the DataType of this column for table creation
		/// </summary>
		public DataType DataType { get; set; }
		/// <summary>
		/// Gets or sets whether this column is an AutoIncrement column or not for table creation (for MySql only)
		/// </summary>
		public bool IsAutoIncrement { get; set; }
		/// <summary>
		/// Gets or sets the Identity property of this column for table creation. Set it to Null if it is not an Identity column (for Sql Server only)
		/// </summary>
		public Identity Identity { get; set; }
		/// <summary>
		/// Gets or sets whether this column is nullable or not for table creation
		/// </summary>
		public bool IsNotNull { get; set; }

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			return string.Format("{0}.{1}", dbHelper.QuoteIdentifier(ParentTable.Name),
											dbHelper.QuoteIdentifier(Name));
		}
	}
}
