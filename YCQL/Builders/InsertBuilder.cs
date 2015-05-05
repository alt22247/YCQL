/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a Sql builder for insert operation
	/// </summary>
	/// <seealso cref="YCQL.CreateBuilder"/>
	/// <seealso cref="YCQL.DeleteBuilder"/>
	/// <seealso cref="YCQL.AlterBuilder"/>
	/// <seealso cref="YCQL.SelectBuilder"/>
	/// <seealso cref="YCQL.UpdateBuilder"/>
	/// <seealso cref="YCQL.DBTable"/>
	/// <seealso cref="YCQL.DBColumn"/>
	public class InsertBuilder : ISQLBuilder
	{
		/// <summary>
		/// List of objects to be inserted
		/// </summary>
		List<object> _insertValues;
		/// <summary>
		/// List of columns to have values inserted
		/// </summary>
		List<DBColumn> _insertColumns;
		/// <summary>
		/// List of columns to be outputed after insert (for Sql Server)
		/// </summary>
		List<DBColumn> _outputInsertedColumns;
		/// <summary>
		/// Table which new rows should be inserted into
		/// </summary>
		DBTable _table;

		/// <summary>
		/// Initializes a new instance of the InsertBuilder class using specified table
		/// </summary>
		/// <param name="table">The corresponding DBTable instance for the INSERT operation</param>
		public InsertBuilder(DBTable table)
		{
			_insertValues = new List<object>();
			_insertColumns = new List<DBColumn>();
			_outputInsertedColumns = new List<DBColumn>();

			_table = table;
		}

		/// <summary>
		/// Specifies the new value to be inserted into the specified column
		/// </summary>
		/// <param name="column">Column associated with the new value to be inserted</param>
		/// <param name="value">Value to be inserted</param>
		/// <returns>A reference to this instance after column value pair has been added to the to insert list</returns>
		public InsertBuilder AddPair(DBColumn column, object value)
		{
			_insertColumns.Add(column);
			_insertValues.Add(value);
			return this;
		}

		/// <summary>
		/// Ouputs the specified columns after insert (for Sql Server)
		/// </summary>
		/// <param name="columns">Columns to be outputed</param>
		/// <returns>A reference to this instance after the columns have been added to the to output list</returns>
		public InsertBuilder OutputInserted(params DBColumn[] columns)
		{
			_outputInsertedColumns.AddRange(columns);
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
			sb.Append("INSERT INTO ");
			sb.Append(dbHelper.QuoteIdentifier(_table.Name));
			if (_insertColumns.Count > 0)
				sb.AppendFormat("({0})", string.Join(",", _insertColumns.Select(column => column.ToSQL(dbHelper, parameterCollection))));

			if (_outputInsertedColumns.Count > 0 && dbHelper.DBEngine == DBEngine.SQLServer)
			{
				sb.Append(" OUTPUT ");
				sb.AppendLine(string.Join(", ", _outputInsertedColumns.Select(x =>
					string.Format("{0}.{1}", dbHelper.QuoteIdentifier("INSERTED"), dbHelper.QuoteIdentifier(x.Name)))));
			}

			sb.AppendFormat(" VALUES ({0})", dbHelper.TranslateObjectsToSQLString(_insertValues, parameterCollection));

			return sb.ToString();
		}
	}
}
