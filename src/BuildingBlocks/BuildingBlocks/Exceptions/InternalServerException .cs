namespace BuildingBlocks.Exceptions;
public class InternalServerException : Exception
{
    public string? Details { get; }
    public InternalServerException(string message) : base(message) { }

    public InternalServerException(string message, string details) : base(message)
    {
        Details = details;
    }

    public static (string detail, string title, int statusCode) Generate(string message, string typeName, HttpContext context)
    {
        return context.GenerateCustomeException(message, typeName, StatusCodes.Status500InternalServerError);
    }

}
