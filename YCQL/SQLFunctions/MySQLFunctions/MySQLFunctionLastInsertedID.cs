/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.MySqlFunctions
{
	/// <summary>
	/// Represents LAST_INSERT_ID function in MySql which returns the most recently generated ID in the server on a per-connection basis
	/// </summary>
	public class MySqlFunctionLastInsertedID : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the MySqlFunctionLastInsertedID class
		/// </summary>
		public MySqlFunctionLastInsertedID()
			: base("LAST_INSERT_ID")
		{
		}
	}
}
