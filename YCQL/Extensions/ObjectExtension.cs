/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections;
using System.Collections.Generic;
using Ycql.Interfaces;

namespace Ycql.Extensions
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

		internal static List<object> Unwrap(this IEnumerable parameters)
		{
			return Unwrap<object>(parameters);
		}

		internal static List<T> Unwrap<T>(this IEnumerable parameters)
		{
			List<T> unwrappedParams = new List<T>();
			foreach (object parameter in parameters)
			{
				if (parameter is IEnumerable && 
					!(parameter is string || parameter is byte[]))
					unwrappedParams.AddRange(Unwrap<T>((IEnumerable) parameter));
				else
					unwrappedParams.Add((T) parameter);
			}

			return unwrappedParams;
		}
	}
}
