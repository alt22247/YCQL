/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using Ycql.Extensions;

namespace Ycql.Attributes
{
	/// <summary>
	/// Indicates the data type of a column
	/// </summary>
	/// <seealso cref="Ycql.DbColumn.DataType"/>
	/// <seealso cref="Ycql.DataType"/>
	/// <seealso cref="Ycql.DataTypeEnum"/>
	public class DataTypeAttribute : SqlAttributeBase
	{
		/// <summary>
		/// Gets the argument array of the data type
		/// </summary>
		internal readonly IEnumerable<object> Arguments;
		/// <summary>
		/// Gets the data type enum
		/// </summary>
		internal readonly DataTypeEnum DataType;

		/// <summary>
		/// Initializes a new instance of the DataTypeAttribute class using specified data type enum and additional arguments
		/// </summary>
		/// <param name="dataTypeEnum">The appropriate DataTypeEnum of the column</param>
		/// <param name="args">Additional parameters for the data type</param>
		public DataTypeAttribute(DataTypeEnum dataTypeEnum, params object[] args)
		{
			DataType = dataTypeEnum;
			if (args != null)
				Arguments = args.Unwrap();
		}
	}
}
