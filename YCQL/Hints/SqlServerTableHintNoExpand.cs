/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

#if YCQL_SQLSERVER

namespace Ycql.Hints
{
	/// <summary>
	/// Represents NOEXPAND table hint in Sql Server
	/// </summary>
	public class SqlServerTableHintNoExpand : SqlServerTableHint
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerTableHintNoExpand class
		/// </summary>
		public SqlServerTableHintNoExpand()
			: base(SqlServerTableHintEnum.NOEXPAND)
		{
		}
	}
}
#endif