namespace BuildingBlocks.Exceptions;
public class PageNumberLessThanZeroException : BadRequestException
{
    public PageNumberLessThanZeroException(string paramName) : base(paramName, "PageNumber cannot be below 0.") { }

    public static void ThrowIfLessThanZero(int pageNumber, [CallerArgumentExpression("pageNumber")] string? paramName = null)
    {
        if (pageNumber < 0)
        {
            throw new PageNumberLessThanZeroException(paramName ?? nameof(pageNumber));
        }
    }
}
