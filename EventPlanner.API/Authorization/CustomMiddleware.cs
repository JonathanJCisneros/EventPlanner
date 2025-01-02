using EventPlanner.API.Models;

namespace EventPlanner.API.Authorization
{
    public class CustomMiddleware
    {
        #region Fields

        private readonly RequestDelegate next;

        #endregion Fields

        #region Constructors

        public CustomMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        #endregion Constructors

        #region Public Methods

        public async Task InvokeAsync(HttpContext context)
        {
            int status = StatusCodes.Status200OK;
            string? error = null;

            if (context.Request.Headers.Accept.Count == 0)
            {
                status = StatusCodes.Status406NotAcceptable;
                error = "Missing Accept Header in Request";
            }
            else if (!context.Request.Headers.Accept.Contains("application/json"))
            {
                status = StatusCodes.Status406NotAcceptable;
                error = "Invalid Accept Header in Request";
            }
            else if (context.Request.Headers.ContentType.Count == 0)
            {
                status = StatusCodes.Status415UnsupportedMediaType;
                error = "Missing Content Type Header in Request";
            }
            else if (!context.Request.Headers.ContentType.Contains("application/json"))
            {
                status = StatusCodes.Status415UnsupportedMediaType;
                error = "Invalid Content Type Header in Request";
            }

            if (status != StatusCodes.Status200OK && !String.IsNullOrWhiteSpace(error)) 
            { 
                context.Response.StatusCode = status;
                await context.Response.WriteAsJsonAsync(new ErrorModel(error));
                return;
            }

            await this.next(context);
        }

        #endregion Public Methods        
    }
}
