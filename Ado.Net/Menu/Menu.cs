using Ado.Net.Entities;
using Ado.Net.Exceptions.GroupExceptions;
using Ado.Net.Exceptions.OperationExceptions;
using Ado.Net.Exceptions.StudentExceptions;
using Ado.Net.Services;

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
			bool loop = true;
			while (loop)
			{
				Console.WriteLine(menu);
				Console.Write("emeliyyat daxil edin: ");
				int.TryParse(Console.ReadLine()?.Trim(), out int op);
				try
				{
					switch (op)
					{
						case 1:
							try
							{
								GroupMenu();
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 2:
							try
							{
								StudentMenu();
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
		private static void GroupMenu()
		{
			GroupService groupService = new();
			StudentService studentService = new();
			bool loop = true;
			while (loop)
			{
				Console.WriteLine(groupMenu);
				Console.Write("emeliyyat daxil edin: ");
				int.TryParse(Console.ReadLine()?.Trim(), out int op);
				try
				{
					switch (op)
					{
						case 1:
							try
							{
								Console.WriteLine("legv etmek ucun bos qoyun");
								Console.Write("qrup adi: ");
								string? groupName = Console.ReadLine()?.Trim();
								if (!string.IsNullOrEmpty(groupName))
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
								List<Group> groups = groupService.GetAll();
								foreach (Group group1 in groups)
								{
									Console.WriteLine($"{group1.Id}) {group1.Name}");
								}
								Console.Write("qrup idsi: ");
								int.TryParse(Console.ReadLine()?.Trim(), out int groupId);
								Group group = groupService.GetById(groupId);
								Console.WriteLine($"kohne ad: {group.Name}");
								Console.WriteLine("legv etmek ucun bos qoyun");
								Console.Write("yeni ad: ");
								string? newName = Console.ReadLine()?.Trim();
								if (!string.IsNullOrEmpty(newName))
								{
									groupService.Update(groupId, newName);
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
								List<Group> groups = groupService.GetAll();
								foreach (Group group in groups)
								{
									Console.WriteLine($"{group.Id}){group.Name}");
								}
								Console.Write("qrup idsi: ");
								int.TryParse(Console.ReadLine()?.Trim(), out int groupId);
								studentService.RemoveFromGroup(groupId);
								groupService.Remove(groupId);
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 4:
							try
							{
								List<Group> groups = groupService.GetAll();
								foreach (Group group in groups)
								{
									Console.WriteLine($"{group.Id}){group.Name}");
								}
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
		private static void StudentMenu()
		{
			StudentService studentService = new();
			bool loop = true;
			while (loop)
			{
				Console.WriteLine(studentMenu);
				Console.Write("emeliyyat daxil edin: ");
				int.TryParse(Console.ReadLine()?.Trim(), out int op);
				try
				{
					switch (op)
					{
						case 1:
							try
							{
								Console.Write("ad soyad: ");
								string? fullName = Console.ReadLine()?.Trim();
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
								List<Student> students = studentService.GetAll();
								foreach (Student student1 in students)
								{
									Console.WriteLine($"{student1.Id}) {student1.Name} {(student1.GroupId != 0 ? $"oldugu qrup idsi: {student1.GroupId}" : "")}");
								}
								Console.Write("telebe idsi: ");
								int.TryParse(Console.ReadLine()?.Trim(), out int studentId);
								Student student = studentService.GetById(studentId);
								Console.WriteLine($"kohne ad soyad: {student.Name}");
								Console.WriteLine("legv etmek ucun bos qoyun");
								Console.Write("yeni ad ve soyad: ");
								string? newFullName = Console.ReadLine()?.Trim();
								if (!string.IsNullOrEmpty(newFullName))
								{
									studentService.Update(studentId, newFullName);
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
								List<Student> students = studentService.GetAll();
								foreach (Student student in students)
								{
									Console.WriteLine($"{student.Id}) {student.Name} {(student.GroupId != 0 ? $"oldugu qrup idsi: {student.GroupId}" : "")}");
								}
								Console.Write("telebe idsi: ");
								int.TryParse(Console.ReadLine(), out int studentId);
								studentService.Remove(studentId);
							}
							catch (Exception ex)
							{
								Console.WriteLine(ex.Message);
							}
							break;
						case 5:
							try
							{
								List<Student> students = studentService.GetAll();
								foreach (Student student in students)
								{
									Console.WriteLine($"{student.Id}) {student.Name} {(student.GroupId != 0 ? $"oldugu qrup idsi:{student.GroupId}" : "")}");
								}
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