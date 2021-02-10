using buckstore.orders.service.domain.Events;

namespace buckstore.orders.service.domain.Exceptions
{
	public class ExceptionNotification : Event
	{
		public string Code { get; private set; }
		public string Message { get; private set; }
		public string ParamName { get; private set; }

		public ExceptionNotification(string code, string message, string paramName = null)
		{
			Code = code;
			Message = message;
			ParamName = paramName;
		}
	}
}