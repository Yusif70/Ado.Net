using Ado.Net.Entities;
using Ado.Net.Exceptions.GroupExceptions;
using Ado.Net.Extensions;
using System.Data.SqlClient;
using Ado.Net.Abstractions;

namespace Ado.Net.Services
{
	public class GroupService : IService<Group>
    {
        private readonly SqlConnection conn = Connection.connection;
		public void Add(string name)
		{
			string insertGroupQuery = "insert into Groups " +
			$"Values('{name}')";
			SqlCommand cmd = new(insertGroupQuery, conn);
			cmd.ExecuteCommand(conn);
		}
		public void Update(int id, string newName)
		{
			GetById(id);
			string updateGroupQuery = "update Groups " +
			$"set Name = '{newName}' " +
			$"where Id = {id}";
			SqlCommand cmd = new(updateGroupQuery, conn);
			cmd.ExecuteCommand(conn);
		}
		public void Remove(int id)
		{
			GetById(id);
			string removeGroupQuery = "delete Groups " +
			$"where Id = {id}";
			SqlCommand cmd = new(removeGroupQuery, conn);
			cmd.ExecuteCommand(conn);
		}
		public List<Group> GetAll()
		{
			List<Group> groups = [];
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