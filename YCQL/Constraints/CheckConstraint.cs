/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;

namespace YCQL.Constraints
{
	/// <summary>
	/// Represents the Check constraint in SQL
	/// </summary>
	/// <seealso cref="YCQL.Constraints.ForeignKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.PrimaryKeyConstraint"/>
	/// <seealso cref="YCQL.Constraints.UniqueKeyConstraint"/>
	public class CheckConstraint : SQLConstraint
	{
		/// <summary>
		/// The constraint expression
		/// </summary>
		object _expression;
		/// <summary>
		/// Initializes a new instance of the CheckConstraint class using specified expression
		/// </summary>
		/// <param name="expression">The constraint expression</param>
		public CheckConstraint(BooleanExpression expression)
			: this(null, (object) expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CheckConstraint class using specified clause
		/// </summary>
		/// <param name="clause">The constraint clause</param>
		public CheckConstraint(LogicalClause clause)
			: this(null, (object) clause)
		{
		}

		/// <summary>
		/// Initializes a new instance of the CheckConstraint class using specified expression
		/// </summary>
		/// <param name="expression">The constraint expression</param>
		public CheckConstraint(object expression)
			: this(null, (object) expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of named CheckConstraint class using specified expression
		/// </summary>
		/// <param name="name">The name for this check constraint</param>
		/// <param name="expression">The constraint expression</param>
		public CheckConstraint(string name, BooleanExpression expression)
			: this(name, (object) expression)
		{
		}

		/// <summary>
		/// Initializes a new instance of named CheckConstraint class using specified clause
		/// </summary>
		/// <param name="name">The name for this check constraint</param>
		/// <param name="clause">The constraint clause</param>
		public CheckConstraint(string name, LogicalClause clause)
			: this(name, (object) clause)
		{
		}

		/// <summary>
		/// Initializes a new instance of named CheckConstraint class using specified expression
		/// </summary>
		/// <param name="name">The name for this check constraint</param>
		/// <param name="expression">The constraint expression</param>
		public CheckConstraint(string name, object expression)
			: base(name)
		{
			_expression = expression;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public override string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(Name))
				sb.AppendFormat("CONSTRAINT {0} ", dbHelper.QuoteIdentifier(Name));

			sb.AppendFormat("CHECK {0}", dbHelper.TranslateObjectToSQLString(_expression, parameterCollection));

			return sb.ToString();
		}
	}
}
