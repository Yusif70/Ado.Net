namespace Ado.Net.Services
{
	public interface IService
	{
		public void Add(string name);
		public void Update(int Id, string newName);
		public void Remove(int Id);
		public void GetAll();
	}
}
