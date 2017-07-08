/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

#if YCQL_SQLSERVER
using System.Data.Common;
using Ycql.Interfaces;

namespace Ycql.Hints
{
	/// <summary>
	/// Enum list of table hints for Sql Server
	/// </summary>
	public enum SqlServerTableHintEnum
	{
		/// <summary>
		/// Forces the optimizer to use an index for an indexed view
		/// </summary>
		NOEXPAND
	}

	/// <summary>
	/// Represents a table hint in Sql Server
	/// </summary>
	public abstract class SqlServerTableHint : ITranslateSql
	{
		SqlServerTableHintEnum _hintEnum;
		/// <summary>
		/// Initializes a new instance of the SqlServerTableHint class
		/// </summary>
		/// <param name="hintEnum"></param>
		protected SqlServerTableHint(SqlServerTableHintEnum hintEnum)
		{
			_hintEnum = hintEnum;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public virtual string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			return _hintEnum.ToString();
		}
	}
}
#endif