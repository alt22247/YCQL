/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.Interfaces;

namespace YCQL.Extensions
{
	/// <summary>
	/// Internal extension class which provides helper functions for any object
	/// </summary>
	internal static class ObjectExtension
	{
		/// <summary>
		/// Checks if the object has content or not
		/// </summary>
		/// <param name="ob">The ITranslateSQL object associated with this extension method call</param>
		/// <returns>A boolean indicating if the object has content or not</returns>
		internal static bool IsNullOrEmpty(this object ob)
		{
			if (ob is string)
				return string.IsNullOrEmpty((string) ob);
			else if (ob is IEmptiable)
				return !((IEmptiable) ob).HasContent();
			else
				return ob == null;
		}
	}
}
