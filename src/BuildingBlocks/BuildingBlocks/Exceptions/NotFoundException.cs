namespace BuildingBlocks.Exceptions;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
    public static (string detail, string title, int statusCode) Generate(string message, string typeName, HttpContext context)
    {
        return context.GenerateCustomeException(message, typeName, StatusCodes.Status404NotFound);
    }

}
