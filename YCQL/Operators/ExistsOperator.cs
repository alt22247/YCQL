/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using Ycql;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents the Exists operator in Sql
	/// </summary>
	/// <seealso cref="Ycql.AnyOperator"/>
	/// <seealso cref="Ycql.AllOperator"/>
	/// <seealso cref="Ycql.InOperator"/>
	public class ExistsOperator : IProduceBoolean<ExistsOperator>, ITranslateSql
	{
		SelectBuilder _subQuery;
		bool _not;
		/// <summary>
		/// Initializes a new instance of the ExistsOperator class using specified subquery
		/// </summary>
		/// <param name="subQuery">A subquery</param>
		public ExistsOperator(SelectBuilder subQuery)
		{
			_not = false;

			_subQuery = subQuery;
		}

		/// <summary>
		/// Negates the expression result
		/// </summary>
		/// <returns>A reference to this instance after the not flag has been set</returns>
		public ExistsOperator Not()
		{
			_not = true;
			return this;
		}

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			if (_not)
				sb.Append(" NOT ");

			sb.AppendFormat(" EXISTS ({0}))", _subQuery.ToSql(dbVersion, parameterCollection));

			return sb.ToString();
		}
	}
}
