namespace BuildingBlocks.Exceptions;
public static class Extension
{
    public static (string detail, string title, int statusCode) GenerateCustomeException(this HttpContext context, string message, string typeName, int statusCode)
    {
        return (message, typeName, context.Response.StatusCode = statusCode);
    }
}
