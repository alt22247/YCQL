/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Extensions;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Enum list of all supported type of joins
	/// </summary>
	/// <seealso cref="YCQL.JoinDefinition"/>
	public enum JoinType
	{
		/// <summary>
		/// Represents inner join in Sql statement
		/// </summary>
		Inner,
		/// <summary>
		/// Represents outer join in Sql statement
		/// </summary>
		Outer,
		/// <summary>
		/// Represents left join in Sql statement
		/// </summary>
		Left,
		/// <summary>
		/// Represents right join in Sql statement
		/// </summary>
		Right,
		/// <summary>
		/// Represents cross join in Sql statement
		/// </summary>
		Cross
	}

	/// <summary>
	/// Represents a single table join in a Sql statement
	/// </summary>
	/// <seealso cref="YCQL.JoinType"/>
	/// <seealso cref="YCQL.SelectBuilder"/>
	/// <seealso cref="YCQL.UpdateBuilder"/>
	public class JoinDefinition : ITranslateSQL, ISupportOn<JoinDefinition>
	{
		object _tableExpression;
		JoinType _type;
		object _onClause;

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and a table to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="table">The table to be joined</param>
		public JoinDefinition(JoinType type, DBTable table)
			: this(type, (object) table, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and a subquery to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="tableExpression">The subquery to be joined</param>
		public JoinDefinition(JoinType type, SelectBuilder tableExpression)
			: this(type, (object) tableExpression, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and an aliased table to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="table">The table to be joined</param>
		/// <param name="tableAliasName">Alias name for the table to be joined</param>
		public JoinDefinition(JoinType type, DBTable table, string tableAliasName)
			: this(type, table, new SQLAlias(tableAliasName))
		{
		}

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and an aliased subquery to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="tableExpression">The subquery to be joined</param>
		/// <param name="tableAliasName">Alias name for the subquery to be joined</param>
		public JoinDefinition(JoinType type, SelectBuilder tableExpression, string tableAliasName)
			: this(type, tableExpression, new SQLAlias(tableAliasName))
		{
		}

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and an aliased table to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="table">The table to be joined</param>
		/// <param name="tableAlias">Alias for the table to be joined</param>
		public JoinDefinition(JoinType type, DBTable table, SQLAlias tableAlias)
			: this(type, (object) table, tableAlias)
		{
		}

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and an aliased subquery to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="tableExpression">The subquery to be joined</param>
		/// <param name="tableAlias">Alias for the subquery to be joined</param>
		public JoinDefinition(JoinType type, SelectBuilder tableExpression, SQLAlias tableAlias)
			: this(type, (object) tableExpression, tableAlias)
		{
		}

		/// <summary>
		/// Initializes a new instance of the JoinDefinition class using specified join type and an aliased table expression to be joined
		/// </summary>
		/// <param name="type">Type of join</param>
		/// <param name="tableExpression">The table expression to be joined</param>
		/// <param name="tableAlias">Alias for the table expression to be joined</param>
		public JoinDefinition(JoinType type, object tableExpression, SQLAlias tableAlias)
		{
			_tableExpression = tableAlias == null ? tableExpression : new SQLSourceAliasPair(tableExpression, tableAlias);
			_type = type;
		}

		/// <summary>
		/// Specifies the on clause to be used for the join
		/// </summary>
		/// <param name="expression">A boolean expression to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(BooleanExpression expression)
		{
			return On((object) expression);
		}

		/// <summary>
		/// Specifies the on clause to be used for the join
		/// </summary>
		/// <param name="clause">A logical clause to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(LogicalClause clause)
		{
			return On((object) clause);
		}

		/// <summary>
		/// Specifies the on clause to be used for the join
		/// </summary>
		/// <param name="expression">An all operator to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(AllOperator expression)
		{
			return On((object) expression);
		}

		/// <summary>
		/// Specifies the on clause to be used for the join
		/// </summary>
		/// <param name="expression">An any operator expression to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(AnyOperator expression)
		{
			return On((object) expression);
		}

		/// <summary>
		/// Specifies the on clause to be used for the join
		/// </summary>
		/// <param name="expression">An exists operator expression to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(ExistsOperator expression)
		{
			return On((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="expression">An in operator expression to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(InOperator expression)
		{
			return On((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the query
		/// </summary>
		/// <param name="onClause">A clause to be used as on clause</param>
		/// <returns>A reference to this instance after the new on expression has been set</returns>
		public JoinDefinition On(object onClause)
		{
			_onClause = onClause;
			return this;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("{0} JOIN {1}", _type.ToString().ToUpper(), dbHelper.TranslateObjectToSQLString(_tableExpression, parameterCollection));

			if (!_onClause.IsNullOrEmpty())
				sb.AppendFormat(" ON {0}", dbHelper.TranslateObjectToSQLString(_onClause, parameterCollection));

			return sb.ToString();
		}
	}
}
