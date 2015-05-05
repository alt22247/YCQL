/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.DBHelpers;
using YCQL.Resources;

namespace YCQL.Exceptions
{
	/// <summary>
	/// The exception that is thrown when YCQL is not able to fully translate the object into Sql statement for specified database or database version
	/// </summary>
	/// <seealso cref="YCQL.Exceptions.YCQLException"/>
	public class DBNotSupportedException : YCQLException
	{
		/// <summary>
		/// Initializes a new instance of the DBNotSupportedException class with a specified database version and the expression not able to be generated
		/// </summary>
		/// <param name="version">Database version which the expression is not able to be generated</param>
		/// <param name="expression">The expression not able to be generated</param>
		public DBNotSupportedException(DBVersion version, string expression)
			: base(string.Format(StringResources.DBVersionNotSupportedExceptionMsg, expression, version))
		{
		}

		/// <summary>
		/// Initializes a new instance of the DBNotSupportedException class with a specified database engine and the expression not able to be generated
		/// </summary>
		/// <param name="engine">Database engine which the expression is not able to be generated</param>
		/// <param name="expression">The expression not able to be generated</param>
		public DBNotSupportedException(DBEngine engine, string expression)
			: base(string.Format(StringResources.DBVersionNotSupportedExceptionMsg, expression, engine))
		{
		}

		/// <summary>
		/// Initializes a new instance of the DBNotSupportedException class with a specified error message
		/// </summary>
		/// <param name="message">A String that describes the error</param>
		public DBNotSupportedException(string message)
			: base(message)
		{
		}
	}
}