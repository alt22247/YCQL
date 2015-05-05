/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

namespace YCQL
{
	/// <summary>
	/// Enum list of all supported comparison operators
	/// </summary>
	/// <seealso cref="YCQL.LogicalClause"/>
	/// <seealso cref="YCQL.BooleanExpression"/>
	/// <seealso cref="YCQL.AllOperator"/>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.ExistsOperator"/>
	/// <seealso cref="YCQL.InOperator"/>
	public enum ComparisonOperator
	{
		/// <summary>
		/// Represents the &apos;=&apos; operator in Sql
		/// </summary>
		EqualsTo,
		/// <summary>
		/// Represents the &apos;&lt;&gt;&apos; operator in Sql
		/// </summary>
		NotEqualsTo,
		/// <summary>
		/// Represents the &apos;&lt;&apos; operator in Sql
		/// </summary>
		LessThan,
		/// <summary>
		/// Represents the &apos;&gt;&apos; operator in Sql
		/// </summary>
		GreaterThan,
		/// <summary>
		/// Represents the &apos;&lt;=&apos; operator in Sql
		/// </summary>
		LessThanOrEqualTo,
		/// <summary>
		/// Represents the &apos;&gt;=&apos; operator in Sql
		/// </summary>
		GreaterThanOrEqualTo,

		/// <summary>
		/// Represents the &apos;LIKE&apos; operator in Sql
		/// </summary>
		Like,
		/// <summary>
		/// Represents the &apos;IS&apos; operator in Sql
		/// </summary>
		Is
	}

	/// <summary>
	/// Enum list of all supported math operators
	/// </summary>
	/// <seealso cref="YCQL.MathExpression"/>
	public enum MathOperator
	{
		/// <summary>
		/// Represents the &apos;+&apos; operator in Sql
		/// </summary>
		Plus,
		/// <summary>
		/// Represents the &apos;-&apos; operator in Sql
		/// </summary>
		Minus,
		/// <summary>
		/// Represents the &apos;*&apos; operator in Sql
		/// </summary>
		Multiply,
		/// <summary>
		/// Represents the &apos;/&apos; operator in Sql
		/// </summary>
		Divide,
	}
}
