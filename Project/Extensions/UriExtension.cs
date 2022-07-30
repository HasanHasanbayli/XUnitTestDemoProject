using Project.Services;
using Project.Services.Interfaces;

namespace Project.Extensions;

public static class UriExtension
{
    public static void AddUri(this IServiceCollection services)
    {
        services.AddSingleton<IUriService>(o =>
        {
            var accessor = o.GetRequiredService<IHttpContextAccessor>();

            var request = accessor.HttpContext!.Request;

            var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

            return new UriService(uri);
        });
    }
}