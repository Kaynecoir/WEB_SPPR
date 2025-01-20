using WEB_153503_Olszewski.Extensions;
using WEB_153503_Olszewski.Services.CategoryService;
using WEB_153503_Olszewski.Services.GameService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.RegisterCustomServices();

builder.Services.AddHttpClient<IGameService, ApiGameService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UriData:ApiUri"]);
});
builder.Services.AddHttpClient<ICategoryService, ApiCategoryService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["UriData:ApiUri"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
