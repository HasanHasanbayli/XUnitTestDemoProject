using Project.Filter;

namespace Project.Services.Interfaces;

public interface IUriService
{
    public Uri GetPageUri(PaginationFilter filter, string route);
}