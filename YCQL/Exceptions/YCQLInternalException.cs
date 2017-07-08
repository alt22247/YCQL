/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Exceptions
{
	/// <summary>
	/// The exception that is thrown when YCQL encounters some internal errors and it cannot proceed. Usually thrown when an enum entry is missing in a switch statement
	/// </summary>
	/// <seealso cref="Ycql.Exceptions.YCQLException"/>
	public class YCQLInternalException : YCQLException
	{
		/// <summary>
		/// Initializes a new instance of the YCQLInternalException class with a specified error message
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception</param>
		public YCQLInternalException(string message)
			: base(message)
		{
		}
	}
}