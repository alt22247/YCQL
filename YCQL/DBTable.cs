/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using YCQL.Attributes;
using YCQL.Constraints;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Represents a table in Database.
	/// </summary>
	/// <seealso cref="YCQL.DBColumn"/>
	public class DBTable : ITranslateSQL
	{
		/// <summary>
		/// Gets the name of this table
		/// </summary>
		public readonly string Name;
		/// <summary>
		/// Gets the list of constraints associated with this table
		/// </summary>
		public readonly List<SQLConstraint> Constraints;
		Dictionary<string, DBColumn> _nameColumnDict;
		/// <summary>
		/// Initializes a new instance of the DBTable class
		/// </summary>
		public DBTable()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the DBTable class using specified table name
		/// </summary>
		/// <param name="name">Name of the table</param>
		public DBTable(string name)
		{
			Name = string.IsNullOrEmpty(name) ? GetType().Name : name;
			_nameColumnDict = new Dictionary<string, DBColumn>();
			Constraints = new List<SQLConstraint>();
			InitializeColumns();
		}

		/// <summary>
		/// Gets or sets the column object specified by name (can be used to add new columns into this DBTable object)
		/// </summary>
		/// <param name="columnName">The name of the column</param>
		/// <exception cref="System.ArgumentNullException">Thrown when columnName is null</exception>
		/// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown when there is no column in this table with that name</exception>
		public DBColumn this[string columnName]
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
		public DBColumn[] ColumnArray
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
		DBColumn GenerateDBColumn(string name, SQLAttributeBase[] attributes)
		{
			DBColumn column = new DBColumn(this, name);
			_nameColumnDict[name] = column;

			foreach (SQLAttributeBase attribute in attributes)
			{
				if (attribute is DataTypeAttribute)
				{
					DataTypeAttribute dataTypeAttribute = (DataTypeAttribute) attribute;
					column.DataType = new DataType(dataTypeAttribute.DataType, dataTypeAttribute.Arguments);
				}
				else if (attribute is AutoIncrementAttribute)
				{
					column.IsAutoIncrement = true;
				}
				else if (attribute is IdentityAttribute)
				{
					IdentityAttribute identityAttribute = (IdentityAttribute) attribute;
					column.Identity = new Identity(identityAttribute.Seed, identityAttribute.Increment);
				}
				else if (attribute is NotNullAttribute)
				{
					column.IsNotNull = true;
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
					if (info.PropertyType != typeof(DBColumn) || info.GetIndexParameters().Length > 0)
						continue;

					info.SetValue(this, GenerateDBColumn(info.Name, (SQLAttributeBase[]) info.GetCustomAttributes(typeof(SQLAttributeBase), true)));
				}
				else if (memberInfo.MemberType == MemberTypes.Field)
				{
					FieldInfo info = (FieldInfo) memberInfo;
					if (info.FieldType != typeof(DBColumn))
						continue;

					info.SetValue(this, GenerateDBColumn(info.Name, (SQLAttributeBase[]) info.GetCustomAttributes(typeof(SQLAttributeBase), true)));
				}
			}
		}

		/// <summary>
		/// Returns an Sql escaped table name
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">Not used</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			return dbHelper.QuoteIdentifier(Name);
		}
	}
}
