/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Interfaces
{
	/// <summary>
	/// Exposes a Not method to negate the resulting boolean
	/// </summary>
	/// <typeparam name="T">The type of the class which supports Not operation</typeparam>
	internal interface IProduceBoolean<T>
	{
		/// <summary>
		/// Negates the expression result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		T Not();
	}
}
