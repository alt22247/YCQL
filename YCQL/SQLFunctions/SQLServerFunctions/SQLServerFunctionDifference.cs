/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Difference function in Sql Server which returns an integer value that indicates the difference between the SOUNDEX values of two character expressions
	/// </summary>
	/// <seealso cref="YCQL.SQLFunctions.SQLFunctionSoundEX"/>
	public class SQLServerFunctionDifference : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDifference class using specified character expression and column
		/// </summary>
		/// <param name="charExpression">An alphanumeric expression of character data</param>
		/// <param name="column">Column to be compared</param>
		public SQLServerFunctionDifference(string charExpression, DBColumn column)
			: this((object) charExpression, (object) column)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDifference class using specified column and character expression
		/// </summary>
		/// <param name="column">Column to be compared</param>
		/// <param name="charExpression">An alphanumeric expression of character data</param>
		public SQLServerFunctionDifference(DBColumn column, string charExpression)
			: this((object) column, (object) charExpression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDifference class using specified columns
		/// </summary>
		/// <param name="column1">Column1 to be compared</param>
		/// <param name="column2">Column2 to be compared</param>
		public SQLServerFunctionDifference(DBColumn column1, DBColumn column2)
			: this((object) column1, (object) column2)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionDifference class using specified character expressions
		/// </summary>
		/// <param name="charExpression1">An alphanumeric expression of character data</param>
		/// <param name="charExpression2">An alphanumeric expression of character data</param>
		public SQLServerFunctionDifference(object charExpression1, object charExpression2)
			: base("DIFFERENCE", charExpression1, charExpression2)
		{
		}
	}
}
