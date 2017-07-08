/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql
{
	/// <summary>
	/// Enum list of all supported DBMS
	/// </summary>
	/// <seealso cref="Ycql.DbVersion"/>
	public enum DbEngine
	{
#if YCQL_SQLSERVER
		/// <summary>
		/// Represents Microsoft Sql Server DBMS
		/// </summary>
		SqlServer,
#endif

#if YCQL_MYSQL
		/// <summary>
		/// Represents MySql DBMS
		/// </summary>
		MySql
#endif
	}

	//Version should be in increasing order, so that we can do something like DBVersion > DBVersion.SQLServer2012
	/// <summary>
	/// Enum list of all supported versions of all supported DBMS
	/// </summary>
	/// <seealso cref="Ycql.DbEngine"/>
	public enum DbVersion
	{
		/// <summary>
		/// Unknown DBVersion. Usually acts as null
		/// </summary>
		Unknown,
#if YCQL_SQLSERVER
		/// <summary>
		/// Microsoft SQL Server 2012
		/// </summary>
		SqlServer2012,
#endif

#if YCQL_MYSQL
		/// <summary>
		/// MySql 5.6
		/// </summary>
		MySql5_6,
#endif
	}

	/// <summary>
	/// The class which holds all the global settings for YCQL
	/// </summary>
	public static class YcqlSettings
	{
		static YcqlSettings()
		{
			DefaultDb = DbVersion.Unknown;
		}

		/// <summary>
		/// Gets or sets the default DBEngine to use when calling AppendToCmd(DbCommand)
		/// </summary>
		/// <seealso cref="Ycql.ITranslateSqlExtension.AppendToCmd(Ycql.Interfaces.ITranslateSql, System.Data.Common.DbCommand)"/>
		public static DbVersion DefaultDb { get; set; }
	}
}
