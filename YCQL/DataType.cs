/*
 * Copyright © 2015 by YuXiang Chen 
 * All rights reserved
*/

using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Ycql.DbHelpers;
using Ycql.Extensions;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Enum list of all supported data types for all supported DBMS
	/// </summary>
	/// <seealso cref="Ycql.DbColumn"/>
	/// <seealso cref="Ycql.AlterBuilder"/>
	public enum DataTypeEnum
	{
		/// <summary>
		/// Represents BigInt data type
		/// </summary>
		BigInt,
		/// <summary>
		/// Represents Bit data type
		/// </summary>
		Bit,
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
		/// Represents Decimal data type
		/// </summary>
		Decimal,
		/// <summary>
		/// Represents Float data type
		/// </summary>
		Float,
		/// <summary>
		/// Represents Int data type
		/// </summary>
		Int,
		/// <summary>
		/// Represents Numeric data type
		/// </summary>
		Numeric,
		/// <summary>
		/// Represents Real data type
		/// </summary>
		Real,
		/// <summary>
		/// Represents SmallInt data type
		/// </summary>
		SmallInt,
		/// <summary>
		/// Represents Text data type
		/// </summary>
		Text,
		/// <summary>
		/// Represents Time data type
		/// </summary>
		Time,
		/// <summary>
		/// Represents TinyInt data type
		/// </summary>
		TinyInt,
		/// <summary>
		/// Represents VarChar data type
		/// </summary>
		VarChar,
#if YCQL_MYSQL
		/// <summary>
		/// Represents Boolean data type
		/// </summary>
		Boolean,
		/// <summary>
		/// Represents DateTime2 data type
		/// </summary>
		DateTime2,
		/// <summary>
		/// Represents Enum data type
		/// </summary>
		Enum,
		/// <summary>
		/// Represents Double data type
		/// </summary>
		Double,
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
		/// Represents Set data type
		/// </summary>
		Set,
		/// <summary>
		/// Represents TimeStamp data type
		/// </summary>
		TimeStamp,
		/// <summary>
		/// Represents TinyBlob data type
		/// </summary>
		TinyBlob,
		/// <summary>
		/// Represents TinyText data type
		/// </summary>
		TinyText,
		/// <summary>
		/// Represents Year data type
		/// </summary>
		Year,
#endif
#if YCQL_SQLSERVER
		/// <summary>
		/// Represents Binary data type
		/// </summary>
		Binary,
		/// <summary>
		/// Represents DateTimeOffset data type
		/// </summary>
		DateTimeOffset,
		/// <summary>
		/// Represents Image data type
		/// </summary>
		Image,
		/// <summary>
		/// Represents Interval data type
		/// </summary>
		Interval,
		/// <summary>
		/// Represents Money data type
		/// </summary>
		Money,
		/// <summary>
		/// Represents SmallDateTime data type
		/// </summary>
		SmallDateTime,
		/// <summary>
		/// Represents SmallMoney data type
		/// </summary>
		SmallMoney,
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
		/// Represents VarBinary data type
		/// </summary>
		VarBinary,
		/// <summary>
		/// Represents XML data type
		/// </summary>
		XML,
#endif
	}

	/// <summary>
	/// Represents the Sql data type of a column.
	/// </summary>
	/// <seealso cref="Ycql.DataTypeEnum"/>
	/// <seealso cref="Ycql.DbColumn"/>
	public class DataType : ITranslateSql
	{
		/// <summary>
		/// Initializes a new instance of the DataType class using specified data type enum and additional arguments
		/// </summary>
		/// <param name="dataTypeEnum">The appropriate DataTypeEnum of the column</param>
		/// <param name="args">Additional parameters for the data type</param>
		public DataType(DataTypeEnum dataTypeEnum, params object[] args)
		{
			DataTypeEnum = dataTypeEnum;
			if (args != null)
				Arguments = args.Unwrap();
		}

		/// <summary>
		/// Gets or sets the data type enum
		/// </summary>
		public DataTypeEnum DataTypeEnum { get; set; }
		/// <summary>
		/// Gets or sets the arguments for this data type
		/// </summary>
		public IEnumerable<object> Arguments { get; set; }

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
			sb.Append(DataTypeEnum.ToString());
			if (Arguments.Count() > 0)
				sb.AppendFormat("({0})", string.Join(",", Arguments));

			return sb.ToString();
		}
	}
}
