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

        builder.Services.AddSingleton<IDataConnector, SqlConnector>();
        // builder.Services.AddSingleton<IDataConnector, TextConnector>();

        // Configure cookie authentication
        builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => {
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = "/";
            });

        builder.Services.AddScoped<IArtistService, ArtistService>();
        builder.Services.AddScoped<IGenreService, GenreService>();
        builder.Services.AddScoped<IReleaseService, ReleaseService>();
        builder.Services.AddScoped<ITrackService, TrackService>();
        builder.Services.AddScoped<IUserService, UserService>();

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
