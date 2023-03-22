using Newtonsoft.Json;
using SuperCarga.Application.Exceptions;
using System.Net;

namespace SuperCarga.Api.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlerMiddleware> logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await ConvertException(context, ex);
            }
        }

        private Task ConvertException(HttpContext context, Exception exception)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            context.Response.ContentType = "application/json";

            var result = string.Empty;

            switch (exception)
            {
                case ValidationException validationException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    var error = string.Join("; ", validationException.ValdationErrors);
                    result = JsonConvert.SerializeObject(error);
                    break;
                case BadRequestException badRequestException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = badRequestException.Message;
                    break;
                case NotFoundException notFoundException:
                    httpStatusCode = HttpStatusCode.NotFound;
                    result = notFoundException.Message;
                    break;
                case AlreadyExistsException alreadyExistsException:
                    httpStatusCode = HttpStatusCode.BadRequest;
                    result = alreadyExistsException.Message;
                    break;
                case ForbiddenException forbiddenException:
                    httpStatusCode = HttpStatusCode.Forbidden;
                    result = forbiddenException.Message;
                    break;
                case Exception ex:
                    httpStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)httpStatusCode;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(exception.Message);
            }

            logger.LogError(exception, result);

            return context.Response.WriteAsync(result);
        }
    }
}

