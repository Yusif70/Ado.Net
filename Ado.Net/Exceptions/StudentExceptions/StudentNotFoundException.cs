namespace Ado.Net.Exceptions.StudentExceptions
{
	public class StudentNotFoundException : Exception
	{
		private static readonly string _message = "Student not found";
		public StudentNotFoundException() : base(_message) { }
		public StudentNotFoundException(string message) : base(message) { }
	}
}
