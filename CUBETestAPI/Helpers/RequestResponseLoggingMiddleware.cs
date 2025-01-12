namespace CUBETestAPI.Helpers
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the request body
            context.Request.EnableBuffering(); // Allow multiple reads of the request body
            var requestBody = await ReadStreamAsync(context.Request.Body);
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} {requestBody}");

            // Set the response body
            var originalResponseBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context); // Call the next middleware in the pipeline

            // Log the response body
            var responseBodyText = await ReadStreamAsync(responseBody);
            _logger.LogInformation($"Response: {context.Response.StatusCode} {responseBodyText}");

            // Copy the response body
            await responseBody.CopyToAsync(originalResponseBodyStream);
        }

        private async Task<string> ReadStreamAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(stream).ReadToEndAsync();
            stream.Seek(0, SeekOrigin.Begin);
            return text;
        }
    }
}
