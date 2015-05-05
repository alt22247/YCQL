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
	/// Represents a Sql builder for delete operations
	/// </summary>
	/// <seealso cref="YCQL.CreateBuilder"/>
	/// <seealso cref="YCQL.AlterBuilder"/>
	/// <seealso cref="YCQL.InsertBuilder"/>
	/// <seealso cref="YCQL.SelectBuilder"/>
	/// <seealso cref="YCQL.UpdateBuilder"/>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.DBColumn"/>
	public class DeleteBuilder : ISQLBuilder, ISupportWhere<DeleteBuilder>
	{
		/// <summary>
		/// Table which should have rows deleted
		/// </summary>
		DBTable _table;
		/// <summary>
		/// Where clause of the delete statement
		/// </summary>
		object _whereClause;
		/// <summary>
		/// Initializes a new instance of the DeleteBuilder class using specified table
		/// </summary>
		/// <param name="table">Table which should have rows deleted</param>
		public DeleteBuilder(DBTable table)
			: this(table, false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the DeleteBuilder class using specified table and useTruncate flag
		/// </summary>
		/// <param name="table">Table which should have rows deleted</param>
		/// <param name="useTruncate">Whether or not to use truncate option for the delete</param>
		public DeleteBuilder(DBTable table, bool useTruncate)
		{
			_table = table;
			UseTruncate = useTruncate;
		}

		/// <summary>
		/// Gets or sets whether to use truncate option for the delete
		/// </summary>
		public bool UseTruncate { get; set; }

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">A boolean expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(BooleanExpression expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="clause">A logical clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(LogicalClause clause)
		{
			return Where((object) clause);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An all operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(AllOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An any operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(AnyOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An exists operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(ExistsOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="expression">An in operator expression to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(InOperator expression)
		{
			return Where((object) expression);
		}

		/// <summary>
		/// Specifies the where clause to be used for the statement
		/// </summary>
		/// <param name="clause">A clause to be used as where clause</param>
		/// <returns>A reference to this instance after the new where expression has been set</returns>
		public DeleteBuilder Where(object clause)
		{
			_whereClause = clause;
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
			if (UseTruncate)
				sb.Append("TRUNCATE TABLE ");
			else
				sb.Append("DELETE FROM ");

			sb.Append(dbHelper.TranslateObjectToSQLString(_table, parameterCollection));

			if (!_whereClause.IsNullOrEmpty())
				sb.AppendFormat(" WHERE {0}", dbHelper.TranslateObjectToSQLString(_whereClause, parameterCollection));

			return sb.ToString();
		}
	}
}
