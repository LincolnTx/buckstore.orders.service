using System;
using buckstore.orders.service.api.v1.Filters.ErrorsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace buckstore.orders.service.api.v1.Filters
{
	public class GlobalExceptionFilterAttribute : Attribute, IExceptionFilter
	{
		public GlobalExceptionFilterAttribute() { }

		public void OnException(ExceptionContext context)
		{
			context.Result = new BadRequestObjectResult(
				new DefaultError(false, 
					new ErrorsResponse[]
					{
						new ErrorsResponse(Environment.GetEnvironmentVariable("GlobalErrorCode"),
							Environment.GetEnvironmentVariable("GlobalErrorMessage"),
							DateTime.Now)
					}
				)
			);
		}
	}
}