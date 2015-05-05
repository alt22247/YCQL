/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL.Constraints
{
	/// <summary>
	/// Enum list of all supported SQL constraint types for all supported DBMS
	/// </summary>
	public enum ConstraintType
	{
		/// <summary>
		/// Represents Primary Key constraint
		/// </summary>
		PrimaryKey,
		/// <summary>
		/// Represents Foreign Key constraint
		/// </summary>
		ForeignKey,
		/// <summary>
		/// Represents Unique Key constraint
		/// </summary>
		UniqueKey,
		/// <summary>
		/// Represents Check constraint
		/// </summary>
		Check,
	}

	/// <summary>
	/// Base class for SQL constraints
	/// </summary>
	/// <seealso cref="YCQL.Constraints.CheckConstraint"/>
	/// <seealso cref="YCQL.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.UniqueKeyConstraint"/>
	public abstract class SQLConstraint : ITranslateSQL
	{
		//Dont need a zero param constructor since all class inheriting would have an overload to call this anyways
		/// <summary>
		/// Initializes a new instance of the named SQLConstraint class
		/// </summary>
		/// <param name="name">The name the constraint</param>
		protected SQLConstraint(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets or sets the name for this constraint
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public abstract string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection);
	}
}
