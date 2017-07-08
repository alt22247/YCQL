/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql
{
#pragma warning disable 1574
	/// <summary>
	/// Represents the identity property of a column in SQL Server
	/// </summary>
	/// <seealso cref="Ycql.DbColumn"/>
	/// <seealso cref="Ycql.DbColumn.IsAutoIncrement"/>
#pragma warning restore 1574
	public class Identity : ITranslateSql
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
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			return (string.Format("IDENTITY({0},{1})", Seed, Increment));
		}
	}

#pragma warning disable 1574
	/// <summary>
	/// Represents a single column in a Database table.
	/// </summary>
	/// <seealso cref="Ycql.DbTable"/>
	/// <seealso cref="Ycql.SqlAlias"/>
	/// <seealso cref="Ycql.DataType"/>
	/// <seealso cref="Ycql.Identity"/>
	/// <seealso cref="Ycql.Attributes.DataTypeAttribute"/>
	/// <seealso cref="Ycql.Attributes.ForeignKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.NotNullAttribute"/>
	/// <seealso cref="Ycql.Attributes.PrimaryKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.UniqueKeyAttribute"/>
	/// <seealso cref="Ycql.Attributes.IdentityAttribute"/>
	/// <seealso cref="Ycql.Attributes.AutoIncrementAttribute"/>
#pragma warning restore 1574
	public class DbColumn : ITranslateSql
	{
		/// <summary>
		/// Gets the parent table object of this column
		/// </summary>
		public readonly DbTable ParentTable;
		/// <summary>
		/// Gets the name of this column
		/// </summary>
		public readonly string ColumnName;
		/// <summary>
		/// Gets the default Alias object of this column
		/// </summary>
		public readonly SqlAlias DefaultAlias;
		/// <summary>
		/// Initializes a new instance of the DBColumn class using specified parent object and column name
		/// </summary>
		/// <param name="parentTable">The table object which this column belongs to</param>
		/// <param name="columnName">The name of this column</param>
		public DbColumn(DbTable parentTable, string columnName)
		{
			ParentTable = parentTable;
			ColumnName = columnName;

			DefaultAlias = new SqlAlias(parentTable.TableName + "." + columnName);

			parentTable[columnName] = this;
		}

		/// <summary>
		/// Gets or sets the DataType of this column for table creation
		/// </summary>
		public DataType DataType { get; set; }

		/// <summary>
		/// Gets or sets whether this column is nullable or not for table creation
		/// </summary>
		public bool IsNotNull { get; set; }

		/// <summary>
		/// Gets or sets the default value for this column for table creation. Please use SqlRawText for all constant values
		/// </summary>
		public ITranslateSql DefaultValue { get; set; }
#if YCQL_MYSQL
		/// <summary>
		/// Gets or sets whether this column is an AutoIncrement column or not for table creation (for MySql only)
		/// </summary>
		public bool IsAutoIncrement { get; set; }
#endif
#if YCQL_SQLSERVER
		/// <summary>
		/// Gets or sets the Identity property of this column for table creation. Set it to Null if it is not an Identity column (for Sql Server only)
		/// </summary>
		public Identity Identity { get; set; }

		/// <summary>
		/// Generate a new DBColumn object whose parent table is set to INSERTED (for Sql Server output statement)
		/// </summary>
		public DbColumn ToInsertedColumn()
		{
			if (ParentTable.TableName == "INSERTED")
				return this;

			DbTable insertedTable = new DbTable("INSERTED");
			return new DbColumn(insertedTable, ColumnName);
		}

		/// <summary>
		/// Generate a new DBColumn object whose parent table is set to DELETED (for Sql Server output statement)
		/// </summary>
		public DbColumn ToDeletedColumn()
		{
			if (ParentTable.TableName == "DELETED")
				return this;

			DbTable insertedTable = new DbTable("DELETED");
			return new DbColumn(insertedTable, ColumnName);
		}
#endif
		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			return string.Format("{0}.{1}", dbHelper.QuoteIdentifier(ParentTable.TableName),
											dbHelper.QuoteIdentifier(ColumnName));
		}
	}
}
