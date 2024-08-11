namespace Ado.Net.Exceptions.GroupExceptions
{
	public class GroupNotFoundException : Exception
	{
		private static readonly string _message = "Group not found";
		public GroupNotFoundException() : base(_message) { }
		public GroupNotFoundException(string message) : base(message) { }
	}
}
