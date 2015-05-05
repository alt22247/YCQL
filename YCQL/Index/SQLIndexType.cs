/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL
{
	/// <summary>
	/// Enum list of all supported index types for all supported DBMS
	/// </summary>
	/// <seealso cref="YCQL.SQLIndex"/>
	public enum SQLIndexType
	{
		/// <summary>
		/// Represents a NonClustered index in Sql
		/// </summary>
		NONCLUSTERED,
		/// <summary>
		/// Represents a Clustered(regular) index in Sql
		/// </summary>
		CLUSTERED,
		/// <summary>
		/// Represents a BTree index in Sql
		/// </summary>
		BTREE,
		/// <summary>
		/// Represents a Clustered BTree index in Sql
		/// </summary>
		CLUSTEREDBTREE,
		/// <summary>
		/// Represents a Hash index in Sql
		/// </summary>
		HASH,
		/// <summary>
		/// Represents a Clustered Hash index in Sql
		/// </summary>
		CLUSTEREDHASH
	}
}
