/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Text;
using Ycql.Exceptions;
using Ycql.Resources;

namespace Ycql.Extensions
{
	/// <summary>
	/// 
	/// </summary>
	internal static class StringBuilderExtension
	{
		/// <summary>
		/// Appends a tab character to the specified StringBuilder
		/// </summary>
		/// <param name="sb">StringBuilder object to append tab character</param>
		internal static void Tab(this StringBuilder sb)
		{
			Tab(sb, 1);
		}

		/// <summary>
		/// Appends multiple tab characters to the specified StringBuilder
		/// </summary>
		/// <param name="sb">StringBuilder object to append tab character</param>
		/// <param name="numTabs">Number of tabs to append</param>
		internal static void Tab(this StringBuilder sb, int numTabs)
		{
			if (numTabs < 0)
				throw new YCQLException(StringResources.ErrorTabCannotBeNegative);

			for (int i = 0; i < numTabs; i++)
				sb.Append("\t");
		}
	}
}
