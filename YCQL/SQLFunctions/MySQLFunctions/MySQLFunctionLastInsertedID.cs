/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.MySQLFunctions
{
	/// <summary>
	/// Represents LAST_INSERT_ID function in MySql which returns the most recently generated ID in the server on a per-connection basis
	/// </summary>
	public class MySQLFunctionLastInsertedID : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the MySQLFunctionLastInsertedID class
		/// </summary>
		public MySQLFunctionLastInsertedID()
			: base("LAST_INSERT_ID")
		{
		}
	}
}
