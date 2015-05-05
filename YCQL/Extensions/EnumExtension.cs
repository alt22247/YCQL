/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using YCQL.Exceptions;
using YCQL;
using System;

namespace YCQL.Extensions
{
	/// <summary>
	/// Internal extension class which contains methods to convert enums into Sql string if they can't be directly converted using .ToString()
	/// </summary>
	internal static class EnumExtension
	{
		/// <summary>
		/// Converts ComparisonOperator enum into Sql string
		/// </summary>
		/// <param name="op">ComparisonOperator enum to be converted</param>
		/// <exception cref="YCQL.Exceptions.YCQLInternalException">Thrown when the specified enum is missing in switch statement</exception>
		/// <returns>Parameterized Sql string</returns>
		internal static string ToSQL(this ComparisonOperator op)
		{
			switch (op)
			{
				case ComparisonOperator.EqualsTo:
					return "=";
				case ComparisonOperator.GreaterThan:
					return ">";
				case ComparisonOperator.GreaterThanOrEqualTo:
					return ">=";
				case ComparisonOperator.LessThan:
					return "<";
				case ComparisonOperator.LessThanOrEqualTo:
					return "<=";
				case ComparisonOperator.NotEqualsTo:
					return "<>";
				case ComparisonOperator.Like:
					return "LIKE";
				case ComparisonOperator.Is:
					return "IS";
				default:
					throw new YCQLInternalException(string.Format("Unknown operator {0}", op.ToSQL()));
			}
		}

		/// <summary>
		/// Converts MathOperator enum into Sql string
		/// </summary>
		/// <param name="op">MathOperator enum to be converted</param>
		/// <exception cref="YCQL.Exceptions.YCQLInternalException">Thrown when the specified enum is missing in switch statement</exception>
		/// <returns>Parameterized Sql string</returns>
		internal static string ToSQL(this MathOperator op)
		{
			switch (op)
			{
				case MathOperator.Plus:
					return "+";
				case MathOperator.Minus:
					return "-";
				case MathOperator.Multiply:
					return "*";
				case MathOperator.Divide:
					return "/";
				default:
					throw new YCQLInternalException(string.Format("Unknown operator {0}", op.ToSQL()));
			}
		}
	}
}
