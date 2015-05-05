/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;

namespace YCQL
{
	/// <summary>
	/// Represents a single or composite index in Sql
	/// </summary>
	/// <seealso cref="YCQL.SQLIndexType"/>
	/// <seealso cref="YCQL.CreateBuilder"/>
	/// <seealso cref="YCQL.AlterBuilder"/>
	public class SQLIndex
	{
		/// <summary>
		/// Initializes a new instance of the SQLIndex class using specified index name, table and columns
		/// </summary>
		/// <param name="name">Name of this index</param>
		/// <param name="table">The table associated with this index</param>
		/// <param name="columns">One single column if it is a single index or multiple columns if it is a composite index</param>
		public SQLIndex(string name, DBTable table, params DBColumn[] columns)
			: this(name, SQLIndexType.NONCLUSTERED, table, columns)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLIndex class using specified index name, index type, table and columns
		/// </summary>
		/// <param name="name">Name of this index</param>
		/// <param name="indexType">Type of this index</param>
		/// <param name="table">The table associated with this index</param>
		/// <param name="columns">One single column if it is a single index or multiple columns if it is a composite index</param>
		public SQLIndex(string name, SQLIndexType indexType, DBTable table, params DBColumn[] columns)
		{
			Name = name;
			IndexType = indexType;
			Table = table;
			Columns = new List<DBColumn>();
			Columns.AddRange(columns);
		}

		/// <summary>
		/// Gets or sets the name of this index
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the type of this index
		/// </summary>
		public SQLIndexType IndexType { get; set; }
		/// <summary>
		/// Gets or sets the table associated with this index
		/// </summary>
		public DBTable Table { get; set; }
		/// <summary>
		/// Gets or sets the list of columns associated with this index
		/// </summary>
		public List<DBColumn> Columns { get; set; }
		/// <summary>
		/// Gets or sets whether or not this index is an unique index
		/// </summary>
		public bool IsUnique { get; set; }
	}
}
