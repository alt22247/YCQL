/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.DbHelpers;
using Ycql.Resources;

namespace Ycql.Exceptions
{
	/// <summary>
	/// The exception that is thrown when YCQL is not able to fully translate the object into Sql statement for specified database or database version
	/// </summary>
	/// <seealso cref="Ycql.Exceptions.YCQLException"/>
	public class DbNotSupportedException : YCQLException
	{
		/// <summary>
		/// Initializes a new instance of the DBNotSupportedException class with a specified database version and the expression not able to be generated
		/// </summary>
		/// <param name="version">Database version which the expression is not able to be generated</param>
		/// <param name="expression">The expression not able to be generated</param>
		public DbNotSupportedException(DbVersion version, string expression)
			: base(string.Format(StringResources.DBVersionNotSupportedExceptionMsg, expression, version))
		{
		}

		/// <summary>
		/// Initializes a new instance of the DBNotSupportedException class with a specified database engine and the expression not able to be generated
		/// </summary>
		/// <param name="engine">Database engine which the expression is not able to be generated</param>
		/// <param name="expression">The expression not able to be generated</param>
		public DbNotSupportedException(DbEngine engine, string expression)
			: base(string.Format(StringResources.DBVersionNotSupportedExceptionMsg, expression, engine))
		{
		}

		/// <summary>
		/// Initializes a new instance of the DBNotSupportedException class with a specified error message
		/// </summary>
		/// <param name="message">A String that describes the error</param>
		public DbNotSupportedException(string message)
			: base(message)
		{
		}
	}
}