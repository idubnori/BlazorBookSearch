using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using OldBookSearch.Services.Product;
using OldBookSearch.Services.Book;

namespace OldBookSearch
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductServiceFactory, ProductServiceFactory>();
            services.AddScoped<IBookGet, OpenBDService>();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
