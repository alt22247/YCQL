using System;

namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates that a column should not be auto initialized
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class SkipInitAttribute : Attribute
	{
		/// <summary>
		/// Initialize a new instance of SkipInitAttribute
		/// </summary>
		public SkipInitAttribute()
		{
		}
	}
}
