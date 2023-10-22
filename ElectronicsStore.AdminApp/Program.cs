using ElectronicsStore.ApiServices;
using ElectronicsStore.Models.Users;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddSession(x => x.IdleTimeout = TimeSpan.FromMinutes(60));
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddTransient<IUserApiService, UserApiService>();
builder.Services.AddTransient<IRoleApiService, RoleApiService>();
builder.Services.AddTransient<ICategoryApiService, CategoryApiService>();
builder.Services.AddTransient<IBrandApiService, BrandApiService>();
builder.Services.AddTransient<IProductApiService, ProductApiService>();
builder.Services.AddTransient<ISupplierApiService, SupplierApiService>();
builder.Services.AddTransient<IImportBillApiService, ImportBillApiService>();
builder.Services.AddTransient<ISaleBillApiService, SaleBillApiService>();
builder.Services.AddTransient<IStatusApiService,StatusApiService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.LoginPath = "/Account/Login";
});

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
