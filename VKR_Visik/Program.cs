using Microsoft.EntityFrameworkCore;
using VKR_Visik;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDdContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DC")));

// Add services to the container.
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddSession();

builder.Services.AddMemoryCache();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sections}/{action=Index}/{id?}");

app.Run();
