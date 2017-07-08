/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace Ycql.SqlFunctions
{
	/// <summary>
	/// Enum list of all supported time unites for all supported DBMS
	/// </summary>
	public enum TimeUnitEnum
	{
		/// <summary>
		/// Represents 'Day' in Sql
		/// </summary>
		Day,
		/// <summary>
		/// Represents 'DayOfYear' in Sql
		/// </summary>
		DayOfYear,
		/// <summary>
		/// Represents 'Hour' in Sql
		/// </summary>
		Hour,
		/// <summary>
		/// Represents 'ISO_WEEK' in Sql
		/// </summary>
		ISO_WEEK,
		/// <summary>
		/// Represents 'Microsecond' in Sql
		/// </summary>
		Microsecond,
		/// <summary>
		/// Represents 'Millisecond' in Sql
		/// </summary>
		Millisecond,
		/// <summary>
		/// Represents 'Minute' in Sql
		/// </summary>
		Minute,
		/// <summary>
		/// Represents 'Month' in Sql
		/// </summary>
		Month,
		/// <summary>
		/// Represents 'Nanosecond' in Sql
		/// </summary>
		Nanosecond,
		/// <summary>
		/// Represents 'Quarter' in Sql
		/// </summary>
		Quarter,
		/// <summary>
		/// Represents 'Second' in Sql
		/// </summary>
		Second,
		/// <summary>
		/// Represents 'TZoffset' in Sql
		/// </summary>
		TZoffset,
		/// <summary>
		/// Represents 'Week' in Sql
		/// </summary>
		Week,
		/// <summary>
		/// Represents 'Weekday' in Sql
		/// </summary>
		Weekday,
		/// <summary>
		/// Represents 'Year' in Sql
		/// </summary>
		Year,
	}
}
