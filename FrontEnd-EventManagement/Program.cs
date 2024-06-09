using FrontEnd_EventManagement.Service;
using FrontEnd_EventManagement.Service.IService;
using FrontEnd_EventManagement.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IEventManagementAPI,EventManagementAPISerivce>();
ApiType.EventManagementAPI = builder.Configuration["ServiceUrls:EventManagementAPI"];

builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<IEventManagementAPI, EventManagementAPISerivce>();

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

app.Run();
