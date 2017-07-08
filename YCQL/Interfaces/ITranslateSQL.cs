/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;

namespace Ycql.Interfaces
{
	/// <summary>
	/// Allows the class to transform itself into a parameterized Sql statement
	/// </summary>
	public interface ITranslateSql
	{
        /// <summary>
        /// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
        /// </summary>
        /// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
        /// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
        /// <returns>Parameterized Sql string</returns>
        string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection);
	}
}
