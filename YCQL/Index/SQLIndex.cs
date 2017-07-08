/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using Ycql.Extensions;

namespace Ycql
{
	/// <summary>
	/// Represents a single or composite index in Sql
	/// </summary>
	/// <seealso cref="Ycql.SqlIndexType"/>
	/// <seealso cref="Ycql.CreateBuilder"/>
	/// <seealso cref="Ycql.AlterBuilder"/>
	public class SqlIndex
	{
		/// <summary>
		/// Initializes a new instance of the SQLIndex class using specified index name, table and columns
		/// </summary>
		/// <param name="name">Name of this index</param>
		/// <param name="table">The table associated with this index</param>
		/// <param name="columns">One single column if it is a single index or multiple columns if it is a composite index</param>
		public SqlIndex(string name, DbTable table, params DbColumn[] columns)
			: this(name, SqlIndexType.NONCLUSTERED, table, columns)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLIndex class using specified index name, index type, table and columns
		/// </summary>
		/// <param name="name">Name of this index</param>
		/// <param name="indexType">Type of this index</param>
		/// <param name="table">The table associated with this index</param>
		/// <param name="columns">One single column if it is a single index or multiple columns if it is a composite index</param>
		public SqlIndex(string name, SqlIndexType indexType, DbTable table, params DbColumn[] columns)
		{
			Name = name;
			IndexType = indexType;
			Table = table;
			Columns = columns.Unwrap<DbColumn>();
		}

		/// <summary>
		/// Gets or sets the name of this index
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the type of this index
		/// </summary>
		public SqlIndexType IndexType { get; set; }
		/// <summary>
		/// Gets or sets the table associated with this index
		/// </summary>
		public DbTable Table { get; set; }
		/// <summary>
		/// Gets or sets the list of columns associated with this index
		/// </summary>
		public List<DbColumn> Columns { get; set; }
		/// <summary>
		/// Gets or sets whether or not this index is an unique index
		/// </summary>
		public bool IsUnique { get; set; }
	}
}
