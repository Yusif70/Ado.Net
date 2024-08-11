namespace Ado.Net.Exceptions.GroupExceptions
{
	public class NoGroupsExceptions : Exception
	{
		private static readonly string _message = "There are no groups on the system";
		public NoGroupsExceptions() : base(_message) { }
		public NoGroupsExceptions(string message) : base(message) { }
	}
}
