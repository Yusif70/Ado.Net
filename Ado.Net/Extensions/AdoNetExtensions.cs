using System.Data.SqlClient;

namespace Ado.Net.Extensions
{
	public static class AdoNetExtensions
	{
		public static void ExecuteCommand(this SqlCommand cmd, SqlConnection connection)
		{
            connection.Open();
			cmd.ExecuteNonQuery();
            connection.Close();
		}
	}
}