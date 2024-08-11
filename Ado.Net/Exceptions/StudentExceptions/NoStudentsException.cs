namespace Ado.Net.Exceptions.StudentExceptions
{
	public class NoStudentsException : Exception
	{
		private static readonly string _message = "There are no students on the system";
		public NoStudentsException() : base(_message) { }
		public NoStudentsException(string message) : base(message) { }
	}
}
