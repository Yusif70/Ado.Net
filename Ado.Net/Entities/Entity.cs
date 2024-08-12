namespace Ado.Net.Entities
{
	public class Entity(int id, string name)
	{
		public int Id { get; set; } = id;
		public string Name { get; set; } = name;
	}
}