/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql.Constraints
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
	/// <seealso cref="Ycql.Constraints.CheckConstraint"/>
	/// <seealso cref="Ycql.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="Ycql.Constraints.UniqueKeyConstraint"/>
	public abstract class SqlConstraint : ITranslateSql
	{
		//Dont need a zero param constructor since all class inheriting would have an overload to call this anyways
		/// <summary>
		/// Initializes a new instance of the named SQLConstraint class
		/// </summary>
		/// <param name="name">The name the constraint</param>
		protected SqlConstraint(string name)
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
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public abstract string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection);
	}
}
