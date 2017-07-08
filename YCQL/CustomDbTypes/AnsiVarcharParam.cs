using MySql.Data.MySqlClient;
using System.Data;
using Ycql.Interfaces;

namespace Ycql
{
	/// <summary>
	/// Represents a non unicode string parameter (varchar)
	/// </summary>
	public class AnsiVarcharParam : ICustomDbType
	{
		/// <summary>
		/// Gets or sets the value of this parameter
		/// </summary>
		public object Value { get; set; }
		/// <summary>
		/// Gets or sets the size of this parameter. Implicit size will be used for negative values.
		/// </summary>
		public int Size { get; set; }
		/// <summary>
		/// Initialize a new instance of AnsiVarcharParam class using specified value and implicit size
		/// </summary>
		/// <param name="value">string value of this parameter</param>
		public AnsiVarcharParam(string value)
			: this((object) value)
		{
		}

		/// <summary>
		/// Initialize a new instance of AnsiVarcharParam class using specified value and implicit size
		/// </summary>
		/// <param name="value">value of this parameter</param>
		public AnsiVarcharParam(object value)
			: this(value, -1)
		{
		}

		/// <summary>
		/// Initialize a new instance of AnsiVarcharParam class using specified value and size
		/// </summary>
		/// <param name="value">value of this parameter</param>
		/// <param name="size">size of the string</param>
		public AnsiVarcharParam(object value, int size)
		{
			Value = value;
			Size = size;
		}

		MySqlDbType ICustomDbType.MySqlDbType
		{
			get
			{
				return MySqlDbType.VarChar;
			}
		}

		SqlDbType ICustomDbType.SqlDbType
		{
			get
			{
				return SqlDbType.VarChar;
			}
		}
	}
}
