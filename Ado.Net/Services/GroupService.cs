using Ado.Net.Entities;
using Ado.Net.Exceptions.GroupExceptions;
using Ado.Net.Extensions;
using System.Data.SqlClient;

namespace Ado.Net.Services
{
	public class GroupService : IService<Group>
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
		public void Update(int id, string newName)
		{
			GetById(id);
			SqlConnection conn = new(connectionString);
			string updateGroupQuery = "update Groups " +
			$"set Name = '{newName}' " +
			$"where Id = {id}";
			SqlCommand cmd = new(updateGroupQuery, conn);
			cmd.ExecuteCommand();
		}
		public void Remove(int id)
		{
			GetById(id);
			SqlConnection conn = new(connectionString);
			string removeGroupQuery = "delete Groups " +
			$"where Id = {id}";
			SqlCommand cmd = new(removeGroupQuery, conn);
			cmd.ExecuteCommand();
		}
		public List<Group> GetAll()
		{
			List<Group> groups = [];
			SqlConnection conn = new(connectionString);
			string getGroupsQuery = "select * from Groups";
			SqlCommand cmd = new(getGroupsQuery, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Group group = new((int)reader[0], (string)reader[1]);
					groups.Add(group);
				}
				conn.Close();
			}
			else
			{
				throw new NoGroupsExceptions("sistemde qrup yoxdur");
			}
			return groups;
		}
		public Group GetById(int id)
		{
			Group? group = GetAll().Find(x => x.Id == id);
			if(group != null)
			{
				return group;
			}
			throw new GroupNotFoundException("qrup tapilmadi");
		}
	}
}