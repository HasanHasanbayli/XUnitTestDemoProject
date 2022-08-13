namespace Project.Wrappers;

public class PagedResponse<T>
{
    public PagedResponse(T data, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Data = data;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public Uri FirstPage { get; set; } = null!;
    public Uri LastPage { get; set; } = null!;
    public int TotalPages { get; set; }
    public int TotalRecords { get; set; }
    public Uri NextPage { get; set; } = null!;
    public Uri PreviousPage { get; set; } = null!;
    public T Data { get; }
}