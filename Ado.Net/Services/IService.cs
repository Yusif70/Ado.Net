using Ado.Net.Entities;

namespace Ado.Net.Services
{
	public interface IService<T> where T : Entity
	{
		public void Add(string name);
		public void Update(int Id, string newName);
		public void Remove(int Id);
		public List<T> GetAll();
		public T GetById(int Id);
	}
}