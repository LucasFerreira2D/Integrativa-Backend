namespace Integrativa.Application.Common;

public record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalItems)
{
    public int TotalPages
    {
        get
        {
            if (PageSize <= 0)
                return 0;

            return (int)Math.Ceiling(TotalItems / (double)PageSize);
        }
    }
}
