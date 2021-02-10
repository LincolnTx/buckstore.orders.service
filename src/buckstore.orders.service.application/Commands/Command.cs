using FluentValidation.Results;

namespace buckstore.orders.service.application.Commands
{
	public abstract class Command
	{
		protected ValidationResult ValidationResult { get; set; }

		public ValidationResult GetValidationResult()
		{
			return ValidationResult;
		}

		public abstract bool IsValid();
	}
}