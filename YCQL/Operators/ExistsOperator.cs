/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents the Exists operator in Sql
	/// </summary>
	/// <seealso cref="YCQL.AnyOperator"/>
	/// <seealso cref="YCQL.AllOperator"/>
	/// <seealso cref="YCQL.InOperator"/>
	public class ExistsOperator : IProduceBoolean<ExistsOperator>, ITranslateSQL
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
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("(");
			if (_not)
				sb.Append(" NOT ");

			sb.AppendFormat(" EXISTS ({0}))", _subQuery.ToSQL(dbHelper, parameterCollection));

			return sb.ToString();
		}
	}
}
