namespace Ado.Net.Exceptions.OperationExceptions
{
	public class OperationNotFoundException : Exception
	{
		private static readonly string _message = "Operation not found";
		public OperationNotFoundException() : base(_message) { }
		public OperationNotFoundException(string message) : base(message) { }
	}
}
