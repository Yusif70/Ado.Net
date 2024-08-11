using Ado.Net.Exceptions.StudentExceptions;
using Ado.Net.Extensions;
using System.Data.SqlClient;

namespace Ado.Net.Services
{
	public class StudentService : IService
	{
		private readonly string connectionString = "Server=DESKTOP-MNIP7P0;Database=ExampleDB;Integrated Security = true";
		public void Add(string? fullName)
		{
			SqlConnection conn = new(connectionString);
			string insertQuery = "insert into Students " +
			$"values('{fullName}')";
			SqlCommand cmd = new(insertQuery, conn);
			cmd.ExecuteCommand();
		}
		public void Update(int Id, string newFullName)
		{
			SqlConnection conn = new(connectionString);
			string updateStudentQuery = "update Students " +
			$"set FullName = '{newFullName}' " +
			$"where Id = {Id}";
			SqlCommand cmd = new(updateStudentQuery, conn);
			cmd.ExecuteCommand();
		}
		public void Remove(int Id)
		{
			SqlConnection conn = new(connectionString);
			string removeStudentQuery = "delete Students " +
			$"where Id = {Id}";
			SqlCommand cmd = new(removeStudentQuery, conn);
			cmd.ExecuteCommand();
		}
		public void GetAll()
		{
			SqlConnection conn = new(connectionString);
			string getStudentsQuery = "select * from Students";
			SqlCommand cmd = new(getStudentsQuery, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				while (reader.Read())
				{
					Console.WriteLine($"{reader[0]}) {reader[1]} {(reader[2] != DBNull.Value ? $"oldugu qrup idsi:{reader[2]}" : "")}");
				}
				conn.Close();
			}
			else
			{
				throw new NoStudentsException("sistemde telebe yoxdur");
			}
		}
		public void RemoveFromGroup(int groupId)
		{
			SqlConnection conn = new(connectionString);
			string removeStudentFromGroupQuery = "update Students " +
			$"set GroupId = Null " +
			$"where GroupId = {groupId}";
			SqlCommand cmd = new(removeStudentFromGroupQuery, conn);
			cmd.ExecuteCommand();
		}
		public void AddToGroup()
		{
			SqlConnection conn = new(connectionString);
			string query = "select max(s.Id), max(g.Id) from Students s " +
				"full join Groups g " +
				"on s.GroupId = g.Id";
			SqlCommand cmd = new(query, conn);
			conn.Open();
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				if (reader[0] != DBNull.Value && reader[1] != DBNull.Value)
				{
					GetAll();
					Console.Write("telebe idsi: ");
					int.TryParse(Console.ReadLine(), out int studentId);
					SqlConnection conn2 = new(connectionString);
					string getStudentQuery = "select * from Students " +
						$"where Id = {studentId}";
					SqlCommand cmd2 = new(getStudentQuery, conn2);
					conn2.Open();
					SqlDataReader reader2 = cmd2.ExecuteReader();
					while (reader2.Read())
					{
						if (reader2[2] == DBNull.Value)
						{
							GroupService groupService = new();
							groupService.GetAll();
							int.TryParse(Console.ReadLine(), out int groupId);
							string addToGroupQuery = "update Students " +
								$"set groupId = {groupId} " +
								$"where Id = {studentId}";
							cmd.CommandText = addToGroupQuery;
							cmd.ExecuteCommand();
						}
						else
						{
							throw new StudentAlreadyInGroupException("telebe artiq qrupdadir");
						}
					}
					conn2.Close();
				}
				else
				{
					Console.WriteLine("Sistemde en azi 1 qrup ve telebe olmalidir");
				}
			}
			conn.Close();
		}
	}
}
