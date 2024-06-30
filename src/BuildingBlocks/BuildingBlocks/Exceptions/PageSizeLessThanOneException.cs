namespace BuildingBlocks.Exceptions;
public class PageSizeLessThanOneException : BadRequestException
{
    public PageSizeLessThanOneException(string paramName) : base(paramName, "PageSize cannot be below 1.") { }

    public static void ThrowIfLessThanOne(int pageSize, [CallerArgumentExpression("pageSize")] string? paramName = null)
    {
        if (pageSize < 1)
        {
            throw new PageSizeLessThanOneException(paramName ?? nameof(pageSize));
        }
    }
}
