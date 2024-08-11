namespace Ado.Net.Exceptions.StudentExceptions
{
	public class StudentAlreadyInGroupException : Exception
	{
		private static readonly string _message = "Student is already in a group";
		public StudentAlreadyInGroupException() : base(_message) { }
		public StudentAlreadyInGroupException(string message) : base(message) { }
	}
}
