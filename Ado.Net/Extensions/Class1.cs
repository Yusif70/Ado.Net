using System.Data.SqlClient;

namespace Ado.Net.Extensions
{
	public static class Class1
	{
		public static void ExecuteCommand(this SqlCommand cmd)
		{
			cmd.Connection.Open();
			cmd.ExecuteNonQuery();
			cmd.Connection.Close();
		}
	}
}
