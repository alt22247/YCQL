/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Data.Common;
using System.Text;
using YCQL.DBHelpers;
using YCQL.Interfaces;

namespace YCQL
{
	/// <summary>
	/// Enum list of all supported data types for all supported DBMS
	/// </summary>
	/// <seealso cref="YCQL.DBColumn"/>
	/// <seealso cref="YCQL.AlterBuilder"/>
	public enum DataTypeEnum
	{
		/// <summary>
		/// Represents BigInt data type
		/// </summary>
		BigInt,
		/// <summary>
		/// Represents Binary data type
		/// </summary>
		Binary,
		/// <summary>
		/// Represents Bit data type
		/// </summary>
		Bit,
		/// <summary>
		/// Represents Boolean data type
		/// </summary>
		Boolean,
		/// <summary>
		/// Represents Char data type
		/// </summary>
		Char,
		/// <summary>
		/// Represents Date data type
		/// </summary>
		Date,
		/// <summary>
		/// Represents DateTime data type
		/// </summary>
		DateTime,
		/// <summary>
		/// Represents DateTime2 data type
		/// </summary>
		DateTime2,
		/// <summary>
		/// Represents DateTimeOffset data type
		/// </summary>
		DateTimeOffset,
		/// <summary>
		/// Represents Decimal data type
		/// </summary>
		Decimal,
		/// <summary>
		/// Represents Double data type
		/// </summary>
		Double,
		/// <summary>
		/// Represents Enum data type
		/// </summary>
		Enum,
		/// <summary>
		/// Represents Float data type
		/// </summary>
		Float,
		/// <summary>
		/// Represents Image data type
		/// </summary>
		Image,
		/// <summary>
		/// Represents Int data type
		/// </summary>
		Int,
		/// <summary>
		/// Represents Interval data type
		/// </summary>
		Interval,
		/// <summary>
		/// Represents LongBlob data type
		/// </summary>
		LongBlob,
		/// <summary>
		/// Represents LongText data type
		/// </summary>
		LongText,
		/// <summary>
		/// Represents MediumInt data type
		/// </summary>
		MediumInt,
		/// <summary>
		/// Represents MediumBlob data type
		/// </summary>
		MediumBlob,
		/// <summary>
		/// Represents MediumText data type
		/// </summary>
		MediumText,
		/// <summary>
		/// Represents Money data type
		/// </summary>
		Money,
		/// <summary>
		/// Represents NChar data type
		/// </summary>
		NChar,
		/// <summary>
		/// Represents NText data type
		/// </summary>
		NText,
		/// <summary>
		/// Represents NVarchar data type
		/// </summary>
		NVarchar,
		/// <summary>
		/// Represents Numeric data type
		/// </summary>
		Numeric,
		/// <summary>
		/// Represents Real data type
		/// </summary>
		Real,
		/// <summary>
		/// Represents Set data type
		/// </summary>
		Set,
		/// <summary>
		/// Represents SmallDateTime data type
		/// </summary>
		SmallDateTime,
		/// <summary>
		/// Represents SmallInt data type
		/// </summary>
		SmallInt,
		/// <summary>
		/// Represents SmallMoney data type
		/// </summary>
		SmallMoney,
		/// <summary>
		/// Represents Text data type
		/// </summary>
		Text,
		/// <summary>
		/// Represents Time data type
		/// </summary>
		Time,
		/// <summary>
		/// Represents TimeStamp data type
		/// </summary>
		TimeStamp,
		/// <summary>
		/// Represents TinyBlob data type
		/// </summary>
		TinyBlob,
		/// <summary>
		/// Represents TinyInt data type
		/// </summary>
		TinyInt,
		/// <summary>
		/// Represents TinyText data type
		/// </summary>
		TinyText,
		/// <summary>
		/// Represents VarChar data type
		/// </summary>
		VarChar,
		/// <summary>
		/// Represents VarBinary data type
		/// </summary>
		VarBinary,
		/// <summary>
		/// Represents XML data type
		/// </summary>
		XML,
		/// <summary>
		/// Represents Year data type
		/// </summary>
		Year
	}

	/// <summary>
	/// Represents the Sql data type of a column.
	/// </summary>
	/// <seealso cref="YCQL.DataTypeEnum"/>
	/// <seealso cref="YCQL.DBColumn"/>
	public class DataType : ITranslateSQL
	{
		/// <summary>
		/// Initializes a new instance of the DataType class using specified data type enum and additional arguments
		/// </summary>
		/// <param name="dataTypeEnum">The appropriate DataTypeEnum of the column</param>
		/// <param name="args">Additional parameters for the data type</param>
		public DataType(DataTypeEnum dataTypeEnum, params object[] args)
		{
			DataTypeEnum = dataTypeEnum;
			Arguments = args;
		}

		/// <summary>
		/// Gets or sets the data type enum
		/// </summary>
		public DataTypeEnum DataTypeEnum { get; set; }
		/// <summary>
		/// Gets or sets the array of arguments for this data type
		/// </summary>
		public object[] Arguments { get; set; }

		/// <summary>
		/// Transforms current object into a parameterized Sql statement where parameter objects are added into parameterCollection
		/// </summary>
		/// <param name="dbHelper">The corresponding DBHelper instance to which DBMS's sql query you want to produce</param>
		/// <param name="parameterCollection">The collection which will hold all the parameters for the sql query</param>
		/// <returns>Parameterized Sql string</returns>
		public string ToSQL(DBHelper dbHelper, DbParameterCollection parameterCollection)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(DataTypeEnum.ToString());
			if (Arguments.Length > 0)
				sb.AppendFormat("({0})", dbHelper.TranslateObjectsToSQLString(Arguments, parameterCollection));

			return sb.ToString();
		}
	}
}
