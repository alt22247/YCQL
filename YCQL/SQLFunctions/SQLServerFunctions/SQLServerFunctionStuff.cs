/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Stuff function in Sql Server which inserts a string into another string. 
	/// It deletes a specified length of characters in the first string at the start position and then inserts the second string into the first string at the start position
	/// </summary>
	public class SQLServerFunctionStuff : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStuff class using specified column, start position, length and the string to replace with
		/// </summary>
		/// <param name="column">A string column</param>
		/// <param name="start">An integer value that specifies the location to start deletion and insertion</param>
		/// <param name="length">An integer that specifies the number of characters to delete</param>
		/// <param name="replaceWith">String to replace with</param>
		public SQLServerFunctionStuff(DBColumn column, int start, int length, string replaceWith)
			: this((object) column, (object) start, (object) length, (object) replaceWith)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionStuff class using specified expression, start position, length and the string to replace with
		/// </summary>
		/// <param name="expression">An expression of character data</param>
		/// <param name="start">An integer value that specifies the location to start deletion and insertion</param>
		/// <param name="length">An integer that specifies the number of characters to delete</param>
		/// <param name="replaceWith">An expression of character data to replace with</param>
		public SQLServerFunctionStuff(object expression, object start, object length, object replaceWith)
			: base("STUFF", expression)
		{
		}
	}
}