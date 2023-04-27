//Para autenticación de usuarios
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

#region autenticación de usuarios
builder.Services.AddSession(s =>
{
    s.IdleTimeout = TimeSpan.FromMinutes(20);
    s.Cookie.Name = ".EventPlanner.Session"; /*You can change this variable*/
    s.Cookie.Expiration = TimeSpan.FromMinutes(20);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllOrigins",
        builder =>
        {
            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options =>
               {
                   options.Cookie.SameSite = SameSiteMode.None;
                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                   options.LoginPath = "/Login"; /*this option indicates where is the login page*/
                   options.AccessDeniedPath = "/Home/Privacy";
               });
#endregion

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

//Para autenticación de usuarios
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}");

app.Run();
