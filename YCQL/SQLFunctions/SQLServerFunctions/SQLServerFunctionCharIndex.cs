/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using Ycql.SqlFunctions;

namespace Ycql.SqlServerFunctions
{
	/// <summary>
	/// Represents CharIndex function in Sql Server which searches an expression for another expression and returns its starting position if found
	/// </summary>
	public class SqlServerFunctionCharIndex : SqlFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCharIndex class using specified expression to find and the column to be searched
		/// </summary>
		/// <param name="expressionToFind">The expression to be find</param>
		/// <param name="column">The column to be searched</param>
		public SqlServerFunctionCharIndex(string expressionToFind, DbColumn column)
			: this((object) expressionToFind, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCharIndex class using specified expression to find and the expression to be searched
		/// </summary>
		/// <param name="expressionToFind">The expression to be find</param>
		/// <param name="expressionToSearch">The expression to be searched</param>
		public SqlServerFunctionCharIndex(object expressionToFind, object expressionToSearch)
			: base("CHARINDEX", expressionToFind, expressionToSearch)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCharIndex class using specified expression to find, the column to be searched, and the start location
		/// </summary>
		/// <param name="expressionToFind">The expression to be find</param>
		/// <param name="column">The column to be searched</param>
		/// <param name="startLocation">The location which the search starts</param>
		public SqlServerFunctionCharIndex(string expressionToFind, DbColumn column, long startLocation)
			: this((object) expressionToFind, (object) column, (object) startLocation)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SqlServerFunctionCharIndex class using specified expression to find, the column to be searched, and the start location
		/// </summary>
		/// <param name="expressionToFind">The expression to be find</param>
		/// <param name="expressionToSearch">The column to be searched</param>
		/// <param name="startLocation">The expression at which the search starts</param>
		public SqlServerFunctionCharIndex(object expressionToFind, object expressionToSearch, object startLocation)
			: base("CHARINDEX", expressionToFind, expressionToSearch, startLocation)
		{
		}
	}
}