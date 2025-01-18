using WEB_153503_Olszewski.Services.CategoryService;
using WEB_153503_Olszewski.Services.GameService;

namespace WEB_153503_Olszewski.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(
        this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<IGameService, MemoryGameService>();

        }
    }

}
