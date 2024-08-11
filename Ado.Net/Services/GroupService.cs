using Ado.Net.Exceptions.GroupExceptions;
using Ado.Net.Extensions;
using System.Data.SqlClient;

namespace Ado.Net.Services
{
	public class GroupService : IService
	{
		private readonly string connectionString = "Server=DESKTOP-MNIP7P0;Database=ExampleDB;Integrated Security = true";
		public void Add(string name)
		{
			SqlConnection conn = new(connectionString);
			string insertGroupQuery = "insert into Groups " +
			$"Values('{name}')";
			SqlCommand cmd = new(insertGroupQuery, conn);
			cmd.ExecuteCommand();
		}
		public void Update(int Id, string newName)
		{
			SqlConnection conn = new(connectionString);
			string updateGroupQuery = "update Groups " +
			$"set Name = '{newName}' " +
			$"where Id = {Id}";
			SqlCommand cmd = new(updateGroupQuery, conn);
			cmd.ExecuteCommand();
		}
		public void Remove(int id)
		{
			SqlConnection conn = new(connectionString);
			string removeGroupQuery = "delete Groups " +
			$"where Id = {id}";
			SqlCommand cmd = new(removeGroupQuery, conn);
			cmd.ExecuteCommand();
		}
		public void GetAll()
		{
			SqlConnection conn = new(connectionString);
			string getGroupsQuery = "select * from Groups";
			SqlCommand cmd = new(getGroupsQuery, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Console.WriteLine($"{reader[0]}){reader[1]}");
				}
				conn.Close();
			}
			else
			{
				throw new NoGroupsExceptions("sistemde qrup yoxdur");
			}
		}
	}
}
