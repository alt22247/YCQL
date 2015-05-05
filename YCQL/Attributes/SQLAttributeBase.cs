/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;

namespace YCQL.Attributes
{
	/// <summary>
	/// Base class for Sql attributes
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public abstract class SQLAttributeBase : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the SQLAttributeBase class
		/// </summary>
		protected SQLAttributeBase()
		{
		}
	}
}
