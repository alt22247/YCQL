/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents PatIndex function in Sql Server which returns the starting position of the first occurrence of a pattern in a specified expression, 
	/// or zeros if the pattern is not found, on all valid text and character data types
	/// </summary>
	public class SqlServerFunctionPatIndex : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionPatIndex class using specified pattern and column
		/// </summary>
		/// <param name="pattern">Is a character expression that contains the sequence to be found. Wildcard characters can be used; however, 
		/// the % character must come before and follow pattern (except when you search for first or last characters). </param>
		/// <param name="column">The column that is searched for the specified pattern</param>
		public SqlServerFunctionPatIndex(string pattern, DbColumn column)
			: this((object) pattern, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionPatIndex class using specified pattern and expression
		/// </summary>
		/// <param name="pattern">Is a character expression that contains the sequence to be found. Wildcard characters can be used; however, 
		/// the % character must come before and follow pattern (except when you search for first or last characters). </param>
		/// <param name="expression">The expression that is searched for the specified pattern</param>
		public SqlServerFunctionPatIndex(object pattern, object expression)
			: base("PATINDEX", pattern, expression)
		{
		}
	}
}