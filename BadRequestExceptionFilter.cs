using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using TP1.ResponseExceptions;

namespace TP1
{
    public class BadRequestExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            if (exception is BaseResponseException)
            {
                var responseMediaTypeCollection = new MediaTypeCollection();
                responseMediaTypeCollection.Add("application/json");
                BaseResponseException responseException = (BaseResponseException)exception;
                var body = new { message = responseException.Message};

                context.Result = new ObjectResult(body)
                {
                    StatusCode = (int) responseException.StatusCode,
                    ContentTypes = responseMediaTypeCollection
                };

                context.ExceptionHandled = true;
            }
        }
    }
}
