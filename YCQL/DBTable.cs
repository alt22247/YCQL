/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Ycql.Attributes;
using Ycql.Constraints;
using Ycql.DbHelpers;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a table in Database.
	/// </summary>
	/// <seealso cref="Ycql.DbColumn"/>
	public class DbTable : ITranslateSql
	{
		/// <summary>
		/// Gets the name of this table
		/// </summary>
		public readonly string TableName;
		/// <summary>
		/// Gets the list of constraints associated with this table
		/// </summary>
		public readonly List<SqlConstraint> Constraints;
		Dictionary<string, DbColumn> _nameColumnDict;
		/// <summary>
		/// Initializes a new instance of the DBTable class
		/// </summary>
		public DbTable()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the DBTable class using specified table name
		/// </summary>
		/// <param name="tableName">Name of the table</param>
		public DbTable(string tableName)
		{
			TableName = string.IsNullOrEmpty(tableName) ? GetType().Name : tableName;
			_nameColumnDict = new Dictionary<string, DbColumn>();
			Constraints = new List<SqlConstraint>();
			InitializeColumns();
		}

		/// <summary>
		/// Gets or sets the column object specified by name (can be used to add new columns into this DBTable object)
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <exception cref="System.ArgumentNullException">Thrown when columnName is null</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown when there is no column in this table with that name</exception>
		public DbColumn this[string columnName]
		{
			get
			{
				return _nameColumnDict[columnName];
			}
			set
			{
				_nameColumnDict[columnName] = value;
			}
		}

		/// <summary>
		/// Gets the array of columns in this table
		/// </summary>
		public DbColumn[] ColumnArray
		{
			get
			{
				return _nameColumnDict.Values.ToArray();
			}
		}

		/// <summary>
		/// Helper function to generate a DBColumn object based on the name and attributes
		/// </summary>
		/// <param name="name">Name of the column</param>
		/// <param name="attributes">Attributes associated with the colulmn</param>
		DbColumn GenerateDbColumn(string name, SqlAttributeBase[] attributes)
		{
			DbColumn column = new DbColumn(this, name);

			foreach (SqlAttributeBase attribute in attributes)
			{
				if (attribute is DataTypeAttribute)
				{
					DataTypeAttribute dataTypeAttribute = (DataTypeAttribute) attribute;
					column.DataType = new DataType(dataTypeAttribute.DataType, dataTypeAttribute.Arguments);
				}
#if YCQL_MYSQL
				else if (attribute is AutoIncrementAttribute)
				{
					column.IsAutoIncrement = true;
				}
#endif
#if YCQL_SQLSERVER
				else if (attribute is IdentityAttribute)
				{
					IdentityAttribute identityAttribute = (IdentityAttribute) attribute;
					column.Identity = new Identity(identityAttribute.Seed, identityAttribute.Increment);
				}
#endif
				else if (attribute is NotNullAttribute)
				{
					column.IsNotNull = true;
				}
				else if (attribute is DefaultAttribute)
				{
					column.DefaultValue = ((DefaultAttribute) attribute).DefaultValue;
				}
				else if (attribute is ForeignKeyAttribute)
				{
					ForeignKeyAttribute foreignKeyAttribute = (ForeignKeyAttribute) attribute;
					Constraints.Add(new ForeignKeyConstraint(foreignKeyAttribute.Name, column, foreignKeyAttribute.RefColumn));
				}
				else if (attribute is PrimaryKeyAttribute)
				{
					PrimaryKeyAttribute primaryKeyAttribute = (PrimaryKeyAttribute) attribute;
					Constraints.Add(new PrimaryKeyConstraint(primaryKeyAttribute.Name, column));
				}
				else if (attribute is UniqueKeyAttribute)
				{
					UniqueKeyAttribute uniqueKeyAttribute = (UniqueKeyAttribute) attribute;
					Constraints.Add(new UniqueKeyConstraint(uniqueKeyAttribute.Name, column));
				}
			}

			return column;
		}

		/// <summary>
		/// Populates the column dictionary with all static columns using reflection
		/// </summary>
		void InitializeColumns()
		{
			foreach (MemberInfo memberInfo in GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance))
			{
				if (memberInfo.MemberType == MemberTypes.Property)
				{
					PropertyInfo info = (PropertyInfo) memberInfo;
					if (info.PropertyType != typeof(DbColumn) || info.GetIndexParameters().Length > 0)
						continue;

					if (info.GetCustomAttribute(typeof(SkipInitAttribute), true) != null)
						continue;

					info.SetValue(this, GenerateDbColumn(info.Name, (SqlAttributeBase[]) info.GetCustomAttributes(typeof(SqlAttributeBase), true)));
				}
				else if (memberInfo.MemberType == MemberTypes.Field)
				{
					FieldInfo info = (FieldInfo) memberInfo;
					if (info.FieldType != typeof(DbColumn))
						continue;

					if (info.GetCustomAttribute(typeof(SkipInitAttribute), true) != null)
						continue;

					info.SetValue(this, GenerateDbColumn(info.Name, (SqlAttributeBase[]) info.GetCustomAttributes(typeof(SqlAttributeBase), true)));
				}
			}
		}

		/// <summary>
		/// Returns an Sql escaped table name
		/// </summary>
		/// <param name="dbVersion">The corresponding DBMS enum which the outputed query is for</param>
		/// <param name="parameterCollection">Not used</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSql(DbVersion dbVersion, DbParameterCollection parameterCollection)
		{
			DbHelper dbHelper = DbHelper.GetDbHelper(dbVersion);

			return dbHelper.QuoteIdentifier(TableName);
		}
	}
}
