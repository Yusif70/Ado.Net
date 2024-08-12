namespace Ado.Net.Entities
{
	public class Student(int id, string name, int groupId) : Entity(id, name)
	{
		public int GroupId { get; set; } = groupId;
	}
}