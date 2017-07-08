/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents GetDate function in Sql Server which returns the current database system timestamp as a datetime value without the database time zone offset
	/// </summary>
	public class SqlServerFunctionGetDate : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionGetDate class
		/// </summary>
		public SqlServerFunctionGetDate()
			: base("GETDATE")
		{
		}
	}
}
