/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL.Attributes
{
	/// <summary>
	/// Indicates that a column is an identity column (for Sql Server)
	/// </summary>
	/// <seealso cref="YCQL.Identity"/>
	/// <seealso cref="YCQL.DBColumn.Identity"/>
	public class IdentityAttribute : SQLAttributeBase
	{
		/// <summary>
		/// Gets the seed of the identity property
		/// </summary>
		internal readonly int Seed;
		/// <summary>
		/// Gets the increment of the identity property
		/// </summary>
		internal readonly int Increment;
		/// <summary>
		/// Initializes a new instance of the IdentityAttribute class with default value for seed and increment (1, 1)
		/// </summary>
		public IdentityAttribute()
			: this(1, 1)
		{
		}

		/// <summary>
		/// Initializes a new instance of the IdentityAttribute class using specified seed and increment value
		/// </summary>
		/// <param name="seed">The seed value of the identity property</param>
		/// <param name="increment">The increment value of the identity property</param>
		public IdentityAttribute(int seed, int increment)
		{
			Seed = seed;
			Increment = increment;
		}
	}
}
