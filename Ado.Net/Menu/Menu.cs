using Ado.Net.Exceptions.GroupExceptions;
using Ado.Net.Exceptions.OperationExceptions;
using Ado.Net.Exceptions.StudentExceptions;
using Ado.Net.Services;
using System.Data.SqlClient;

namespace Ado.Net.Menu
{
	public class Menu
	{
		private static readonly string menu = "\t1.Group Menu\n" +
		"\t2.Student Menu\n" +
		"\t0.Exit\n";
		private static readonly string groupMenu = "\t1.Group Add\n" +
				"\t2.Group Update\n" +
				"\t3.Group Remove\n" +
				"\t4.Get All Groups\n" +
				"\t0.Exit\n";
		private static readonly string studentMenu = "\t1.Student Add\n" +
					"\t2.Student Add to group\n" +
					"\t3.Student Update\n" +
					"\t4.Student Remove\n" +
					"\t5.Get All Students\n" +
					"\t0.Exit\n";
		public static void MainMenu()
		{
			string connectionString = "Server=DESKTOP-MNIP7P0;Database=ExampleDB;Integrated Security = true";
			bool loop = true;
			while (loop)
			{
				Console.WriteLine(menu);
				Console.Write("emeliyyat daxil edin: ");
				int.TryParse(Console.ReadLine(), out int op);
				try
				{
					switch (op)
					{
						case 1:
							try
							{
								GroupMenu(connectionString);
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 2:
							try
							{
								StudentMenu(connectionString);
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 0:
							loop = false;
							break;
						default:
							throw new OperationNotFoundException("emeliyyat tapilmadi");
					}
				}
				catch (OperationNotFoundException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
		private static void GroupMenu(string connectionString)
		{
			GroupService groupService = new();
			StudentService studentService = new();
			bool loop = true;
			while (loop)
			{
				Console.WriteLine(groupMenu);
				Console.Write("emeliyyat daxil edin: ");
				int.TryParse(Console.ReadLine(), out int op);
				try
				{
					switch (op)
					{
						case 1:
							try
							{
								Console.WriteLine("legv etmek ucun bos qoyun");
								Console.Write("qrup adi: ");
								string? groupName = Console.ReadLine();
								if (!string.IsNullOrEmpty(groupName?.Trim()))
								{
									groupService.Add(groupName);
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 2:
							try
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
										Console.WriteLine($"{reader[0]}) {reader[1]}");
									}
									conn.Close();
									Console.Write("qrup idsi: ");
									int.TryParse(Console.ReadLine(), out int groupId);
									string getGroupQuery = "select * from Groups " +
										$"where Id = {groupId}";
									cmd.CommandText = getGroupQuery;
									conn.Open();
									reader = cmd.ExecuteReader();
									if(reader.HasRows && reader.Read())
									{
										Console.WriteLine($"kohne ad: {reader[1]}");
									}
									conn.Close();
									Console.WriteLine("legv etmek ucun bos qoyun");
									Console.Write("yeni ad: ");
									string? newName = Console.ReadLine();
									if (!string.IsNullOrEmpty(newName?.Trim()))
									{
										groupService.Update(groupId, newName);
									}
								}
								else
								{
									Console.WriteLine("sistemde qrup yoxdur");
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 3:
							try
							{
								string getGroupsQuery = "select * from Groups";
								SqlConnection conn = new(connectionString);
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
									Console.Write("qrup idsi: ");
									int.TryParse(Console.ReadLine(), out int groupId);
									studentService.RemoveFromGroup(groupId);
									groupService.Remove(groupId);
								}
								else
								{
									Console.WriteLine("sistemde qrup yoxdur");
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 4:
							try
							{
								groupService.GetAll();
							}
							catch (NoGroupsExceptions ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 0:
							loop = false;
							break;
						default:
							throw new OperationNotFoundException("emeliyyat tapilmadi");
					}
				}
				catch (OperationNotFoundException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
		private static void StudentMenu(string connectionString)
		{
			StudentService studentService = new();
			bool loop = true;
			while (loop)
			{
				Console.WriteLine(studentMenu);
				Console.Write("emeliyyat daxil edin: ");
				int.TryParse(Console.ReadLine(), out int op);
				try
				{
					switch (op)
					{
						case 1:
							try
							{
								Console.Write("ad soyad: ");
								string? fullName = Console.ReadLine();
								loop = false;
								studentService.Add(fullName);
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 2:
							try
							{
								studentService.AddToGroup();
							}
							catch (StudentAlreadyInGroupException ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 3:
							try
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
										Console.WriteLine($"{reader[0]}) {reader[1]}");
									}
									conn.Close();
									Console.Write("telebe idsi: ");
									int.TryParse(Console.ReadLine(), out int studentId);
									string getGroupQuery = "select * from Students " +
										$"where Id = {studentId}";
									cmd.CommandText = getGroupQuery;
									conn.Open();
									reader = cmd.ExecuteReader();
									if(reader.HasRows && reader.Read())
									{
										Console.WriteLine($"kohne ad soyad: {reader[1]}");
									}
									conn.Close();
									Console.WriteLine("legv etmek ucun bos qoyun");
									Console.Write("yeni ad ve soyad: ");
									string? newFullName = Console.ReadLine();
									if (!string.IsNullOrEmpty(newFullName?.Trim()))
									{
										studentService.Update(studentId, newFullName);
									}
								}
								else
								{
									Console.WriteLine("sistemde telebe yoxdur");
								}
								conn.Close();
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}

							break;
						case 4:
							try
							{
								SqlConnection conn = new(connectionString);
								string getStudentsQuery = "select * from Students";
								conn = new(connectionString);
								SqlCommand cmd = new(getStudentsQuery, conn);
								conn.Open();
								SqlDataReader reader = cmd.ExecuteReader();
								if (reader.HasRows)
								{
									while (reader.Read())
									{
										Console.WriteLine($"{reader[0]}) {reader[1]} {(reader[2] != DBNull.Value ? $"oldugu qrup idsi: {reader[2]}" : "")}");
									}
									conn.Close();
									Console.Write("telebe idsi: ");
									int.TryParse(Console.ReadLine(), out int studentId);
									studentService.Remove(studentId);
								}
								else
								{
									Console.WriteLine("sistemde telebe yoxdur");
								}
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}

							break;
						case 5:
							try
							{
								studentService.GetAll();
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 0:
							loop = false;
							break;
						default:
							throw new OperationNotFoundException("emeliyyat tapilmadi");
					}
				}
				catch (OperationNotFoundException ex)
				{
					Console.WriteLine(ex.Message);
				}
			}
		}
	}
}
