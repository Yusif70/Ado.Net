using Ado.Net.Entities;
using Ado.Net.Exceptions.StudentExceptions;
using Ado.Net.Extensions;
using System.Data.SqlClient;
using Ado.Net.Abstractions;

namespace Ado.Net.Services
{
    public class StudentService : IService<Student>
    {
        private static readonly SqlConnection conn = Connection.connection;
        public void Add(string? fullName)
        {
            string insertQuery = "insert into Students " +
            $"values('{fullName}',NULL)";
            SqlCommand cmd = new(insertQuery, conn);
            cmd.ExecuteCommand(conn);
        }
        public void Update(int Id, string? newFullName)
        {
            GetById(Id);
            string updateStudentQuery = "update Students " +
            $"set FullName = '{newFullName}' " +
            $"where Id = {Id}";
            SqlCommand cmd = new(updateStudentQuery, conn);
            cmd.ExecuteCommand(conn);
        }
        public void Remove(int Id)
        {
            GetById(Id);

            string removeStudentQuery = "delete Students " +
            $"where Id = {Id}";
            SqlCommand cmd = new(removeStudentQuery, conn);
            cmd.ExecuteCommand(conn);
        }
        public List<Student> GetAll()
        {
            List<Student> students = [];
            string getStudentsQuery = "select * from Students";
            SqlCommand cmd = new(getStudentsQuery, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student student = new((int)reader[0], (string)reader[1], reader[2] == DBNull.Value ? 0 : (int)reader[2]);
                    students.Add(student);
                }
                conn.Close();
            }
            else
            {
                conn.Close();
                throw new NoStudentsException("sistemde telebe yoxdur");
            }
            return students;
        }
        public Student GetById(int Id)
        {
            Student? student = GetAll().Find(x => x.Id == Id);
            if (student != null)
            {
                return student;
            }
            throw new StudentNotFoundException("telebe tapilmadi");
        }
        public void RemoveFromGroup(int groupId)
        {
            string removeStudentFromGroupQuery = "update Students " +
            $"set GroupId = Null " +
            $"where GroupId = {groupId}";
            SqlCommand cmd = new(removeStudentFromGroupQuery, conn);
            cmd.ExecuteCommand(conn);
        }
        public void AddToGroup()
        {
            List<Student> students = GetAll();
            GroupService groupService = new();
            List<Group> groups = groupService.GetAll();
            if (students.Count > 0 && groups.Count > 0)
            {
                foreach (Student student1 in students)
                {
                    Console.WriteLine($"{student1.Id}) {student1.Name} {(student1.GroupId != 0 ? $"oldugu qrup idsi: {student1.GroupId}" : "")}");
                }
                Console.Write("telebe idsi: ");
                int.TryParse(Console.ReadLine()?.Trim(), out int studentId);
                Student student = GetById(studentId);
                if (student.GroupId == 0)
                {
                    foreach (Group group in groups)
                    {
                        Console.WriteLine($"{group.Id}){group.Name}");
                    }
                    Console.Write("qrup idsi: ");
                    int.TryParse(Console.ReadLine()?.Trim(), out int groupId);
                    string addToGroupQuery = "update Students " +
                        $"set groupId = {groupId} " +
                        $"where Id = {studentId}";
                    SqlCommand cmd = new(addToGroupQuery, conn);
                    cmd.ExecuteCommand(conn);
                }
                else
                {
                    throw new StudentAlreadyInGroupException("telebe artiq qrupdadir");
                }
            }
            else
            {
                Console.WriteLine("Sistemde en azi 1 qrup ve telebe olmalidir");
            }
        }
    }
}