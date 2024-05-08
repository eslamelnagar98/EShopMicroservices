namespace BuildingBlocks.Exceptions;
public class BadRequestException : Exception
{
    public string Details { get; } = string.Empty;
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string message, string details) : base(message)
    {
        Details = details;
    }

    public static (string detail, string title, int statusCode) Generate(string message, string typeName, HttpContext context)
    {
        return context.GenerateCustomeException(message, typeName, StatusCodes.Status400BadRequest);
    }
}
