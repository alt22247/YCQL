/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents GetDate function in Sql Server which returns the current database system timestamp as a datetime value without the database time zone offset
	/// </summary>
	public class SQLServerFunctionGetDate : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionGetDate class
		/// </summary>
		public SQLServerFunctionGetDate()
			: base("GETDATE")
		{
		}
	}
}
