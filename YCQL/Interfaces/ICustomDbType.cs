#if YCQL_MYSQL
using MySql.Data.MySqlClient;
#endif
using System.Data;

namespace Ycql.Interfaces
{
	internal interface ICustomDbType
	{
#if YCQL_SQLSERVER
		SqlDbType SqlDbType { get; }
#endif
#if YCQL_MYSQL
		MySqlDbType MySqlDbType { get; }
#endif
		int Size { get; set; }
		object Value { get; set; }
	}
}
