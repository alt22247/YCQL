/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.Attributes
{
	/// <summary>
	/// Specifies the default value for the column
	/// </summary>
	public class DefaultAttribute : SqlAttributeBase
	{
		internal readonly SqlRawText DefaultValue;
		/// <summary>
		/// Initializes a new instance of the DefaultAttribute class using specified default value
		/// </summary>
		/// <param name="defaultValueRawString">The default value for the column. WARNING: Will be converted to Sql Raw Text</param>
		public DefaultAttribute(string defaultValueRawString)
		{
			DefaultValue = new SqlRawText(defaultValueRawString);
		}
	}
}
