/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;

namespace YCQL.Interfaces
{
	/// <summary>
	/// Allows the class to transform itself into a parameterized Sql statement
	/// </summary>
	public interface ITranslateSQL
	{
		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection);
	}
}
