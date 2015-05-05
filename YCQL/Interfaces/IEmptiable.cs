/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Interfaces
{
	/// <summary>
	/// An internal interface which indicates the class could be empty (nothing to output during Sql translation)
	/// </summary>
	internal interface IEmptiable
	{
		/// <summary>
		/// Checks if the object is empty or not
		/// </summary>
		/// <returns>A boolean indicating if the object is empty or not</returns>
		bool HasContent();
	}
}
