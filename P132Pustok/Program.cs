using Microsoft.EntityFrameworkCore;
using P132Pustok.DAL;
using P132Pustok.Middlewares;
using P132Pustok.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PustokContext>(opt =>
{
    opt.UseSqlServer(@"Server=DESKTOP-BU4GQ1K\SQLEXPRESS;Database=P132Pustok;Trusted_Connection=TRUE") ;
});
//builder.Services.AddSingleton<LayoutService>();
builder.Services.AddScoped<LayoutService>();
//builder.Services.AddTransient<LayoutService>();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromMinutes(5);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();

//app.Use(async (context, next) =>
//{
//    string path = context.Request.Path.Value;

//    if (path.StartsWith("/manage"))
//    {
//        string username = context.Session.GetString("username");
//        if(username == null)
//        {
//            context.Response.Redirect("/manage/account/login");
//        }
//    }
//    await next();
//});

app.UseMiddleware<AuthenticationMiddleware>();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
       name: "areas",
       pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
