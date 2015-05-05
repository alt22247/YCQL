/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Interfaces
{
	/// <summary>
	/// Indicates that the Sql statement associated with the class supports Distinct keyword
	/// </summary>
	/// <typeparam name="T">The type of the class which supports Distinct operation</typeparam>
	internal interface ISupportDistinct<T>
	{
		/// <summary>
		/// Applies Distinct keyword to the statement
		/// </summary>
		/// <returns>A reference to this instance after the Distinct flag is set to true</returns>
		T Distinct();
	}
}
