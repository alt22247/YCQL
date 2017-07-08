/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System;
using System.Collections.Generic;
using System.Data.Common;
using Ycql.DbHelpers;
using Ycql.Exceptions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// A dummy version of DbParameterCollection for Debug.ToSql since DbParameterCollection's constructor is protected
	/// </summary>
	internal class DebugParameterCollection : DbParameterCollection
	{
		List<object> _parameters;
		internal DebugParameterCollection()
		{
			_parameters = new List<object>();
		}

		public override int Add(object value)
		{
			_parameters.Add(value);
			return _parameters.Count - 1;
		}

		public override void AddRange(Array values)
		{
			throw new System.NotImplementedException();
		}

		public override void Clear()
		{
			throw new System.NotImplementedException();
		}

		public override bool Contains(string value)
		{
			throw new System.NotImplementedException();
		}

		public override bool Contains(object value)
		{
			throw new System.NotImplementedException();
		}

		public override void CopyTo(System.Array array, int index)
		{
			throw new System.NotImplementedException();
		}

		public override int Count
		{
			get
			{
				return _parameters.Count;
			}
		}

		public override System.Collections.IEnumerator GetEnumerator()
		{
			return _parameters.GetEnumerator();
		}

		protected override DbParameter GetParameter(string parameterName)
		{
			throw new System.NotImplementedException();
		}

		protected override DbParameter GetParameter(int index)
		{
			return (DbParameter) _parameters[index];
		}

		public override int IndexOf(string parameterName)
		{
			throw new System.NotImplementedException();
		}

		public override int IndexOf(object value)
		{
			throw new System.NotImplementedException();
		}

		public override void Insert(int index, object value)
		{
			throw new System.NotImplementedException();
		}

		public override bool IsFixedSize
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsReadOnly
		{
			get { throw new System.NotImplementedException(); }
		}

		public override bool IsSynchronized
		{
			get { throw new System.NotImplementedException(); }
		}

		public override void Remove(object value)
		{
			throw new System.NotImplementedException();
		}

		public override void RemoveAt(string parameterName)
		{
			throw new System.NotImplementedException();
		}

		public override void RemoveAt(int index)
		{
			throw new System.NotImplementedException();
		}

		protected override void SetParameter(string parameterName, DbParameter value)
		{
			throw new System.NotImplementedException();
		}

		protected override void SetParameter(int index, DbParameter value)
		{
			throw new System.NotImplementedException();
		}

		public override object SyncRoot
		{
			get { throw new System.NotImplementedException(); }
		}
	}

	/// <summary>
	/// Extension class which provides convinient methods for Sql statement generation
	/// </summary>
	/// <seealso cref="Ycql.Interfaces.ITranslateSql"/>
	public static class ITranslateSqlExtension
	{
		/// <summary>
		/// Transforms current object into a parameterized Sql statement using the DB Engine specified in YCQLSettings.DefaultDB
		/// </summary>
		/// <param name="ob">The ITranslateSQL object associated with this extension method call</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <exception cref="Ycql.Exceptions.YCQLException">Thrown when the YCQLSettings.DefaultDB property is not set</exception>
		public static string ToSql(this ITranslateSql ob, DbParameterCollection parameterCollection)
		{
			if ( YcqlSettings.DefaultDb == DbVersion.Unknown )
				throw new YCQLException("Default DB is not set. Please set the YCQLSettings.DefaultDB property before calling this method");

			return ob.ToSql(YcqlSettings.DefaultDb, parameterCollection);
		}


		/// <summary>
		/// Transforms the current object into parameterized Sql string for the DB Engine specified in YCQLSettings.DefaultDB and append it to the specified DbCommand. 
		/// A semicolon will be appended to the end of the string
		/// </summary>
		/// <param name="ob">The ITranslateSQL object associated with this extension method call</param>
		/// <param name="cmd">The DbCommand object to append the Sql string to</param>
		/// <exception cref="Ycql.Exceptions.YCQLException">Thrown when the YCQLSettings.DefaultDB property is not set</exception>
		public static void AppendToCmd(this ITranslateSql ob, DbCommand cmd)
		{
			if (YcqlSettings.DefaultDb ==  DbVersion.Unknown)
				throw new YCQLException("Default DB is not set. Please set the YCQLSettings.DefaultDB property before calling this method");

			cmd.CommandText += ob.ToSql(YcqlSettings.DefaultDb, cmd.Parameters) + ";";
		}

		/// <summary>
		/// Transforms the current object into parameterized Sql string and append it to the specified DbCommand. A semicolon will be appended to the end of the string
		/// </summary>
		/// <param name="ob">The ITranslateSQL object associated with this extension method call</param>
		/// <param name="dBVersion">The DBMS's sql query to produce</param>
		/// <param name="cmd">The DbCommand object to append the Sql string to</param>
		public static void AppendToCmd(this ITranslateSql ob, DbVersion dBVersion, DbCommand cmd)
		{
			cmd.CommandText += ob.ToSql(dBVersion, cmd.Parameters) + ";";
		}

		/// <summary>
		/// Transforms the current object into a NON-parameterized Sql string by calling the ToString method for all parameters. This should only be called for debugging purpose
		/// </summary>
		/// <param name="ob">The ITranslateSQL object associated with this extension method call</param>
		/// <param name="dBVersion">The DBMS's sql query to produce</param>
		/// <returns>A NONE parameterized SQL string</returns>
		public static string DebugToSql(this ITranslateSql ob, DbVersion dBVersion)
		{
			DbParameterCollection parameterCollection = new DebugParameterCollection();
			string commandText = ob.ToSql(dBVersion, parameterCollection);
			foreach (DbParameter parameter in parameterCollection)
				commandText = commandText.Replace(parameter.ParameterName, parameter.Value.ToString());

			return commandText;
		}
	}
}