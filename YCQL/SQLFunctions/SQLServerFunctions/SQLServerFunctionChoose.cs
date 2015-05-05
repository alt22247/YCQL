/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Linq;
using YCQL.SQLFunctions;

namespace YCQL.SQLServerFunctions
{
	/// <summary>
	/// Represents Choose function in Sql Server which returns the item at the specified index from a list of values 
	/// </summary>
	public class SQLServerFunctionChoose : SQLFunctionBase
	{
		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionChoose class using specified index and values
		/// </summary>
		/// <param name="index">the 1-based index into the list of the items following it</param>
		/// <param name="values">List of values to be choose from</param>
		public SQLServerFunctionChoose(int index, params object[] values)
			: this((object) index, values)
		{
		}

		/// <summary>
		/// Initializes a new instance of the SQLServerFunctionChoose class using specified index expression and values
		/// </summary>
		/// <param name="indexExpression">An integer expression that represents a 1-based index into the list of the items following it</param>
		/// <param name="values">List of values to be choose from</param>
		public SQLServerFunctionChoose(object indexExpression, params object[] values)
			: base("CHOOSE", new object[] { indexExpression }.Union(values).ToArray())
		{
		}
	}
}