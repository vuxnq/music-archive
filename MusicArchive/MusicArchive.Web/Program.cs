using System.Net.Mime;
using MusicArchive.Data;
using MusicArchive.Domain.Services;

namespace MusicArchive.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IDataConnector, SqlConnector>();
        // builder.Services.AddScoped<IDataConnector, TextConnector>();
        builder.Services.AddScoped<IUserService, UserService>();

        // Configure cookie authentication for simple login
        builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/User/Login";
            });

        builder.Services.AddScoped<IArtistService, ArtistService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IReleaseService, ReleaseService>();
        builder.Services.AddScoped<ITrackService, TrackService>();

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

// TODO: udelat trivialni login
// - pridat user do databaze,
// - vytvorit user model bude mit Id, string Username, string Password (nebude zahashovany nic, pro jednoduchost)
// - rucne pridat do databaze usera admin:password1
