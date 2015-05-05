/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL
{
	/// <summary>
	/// Enum list of all supported DBMS
	/// </summary>
	/// <seealso cref="YCQL.DBVersion"/>
	public enum DBEngine
	{
		/// <summary>
		/// Represents Microsoft SQL Server DBMS
		/// </summary>
		SQLServer,
		/// <summary>
		/// Represents MySql DBMS
		/// </summary>
		MySQL
	}

	//Version should be in increasing order, so that we can do something like DBVersion > DBVersion.SQLServer2012
	/// <summary>
	/// Enum list of all supported versions of all supported DBMS
	/// </summary>
	/// <seealso cref="YCQL.DBEngine"/>
	public enum DBVersion
	{
		/// <summary>
		/// Unknown DBVersion. Usually acts as null
		/// </summary>
		Unknown,
		/// <summary>
		/// Microsoft SQL Server 2012
		/// </summary>
		SQLServer2012,
		/// <summary>
		/// MySql 5.6
		/// </summary>
		MySQL5_6,
	}

	/// <summary>
	/// The class which holds all the global settings for YCQL
	/// </summary>
	public static class YCQLSettings
	{
		static YCQLSettings()
		{
			DefaultDB = DBVersion.Unknown;
		}

		/// <summary>
		/// Gets or sets the default DBEngine to use when calling AppendToCmd(DbCommand)
		/// </summary>
		/// <seealso cref="YCQL.ITranslateSQLExtension.AppendToCmd(YCQL.Interfaces.ITranslateSQL, System.Data.Common.DbCommand)"/>
		public static DBVersion DefaultDB { get; set; }
	}
}
