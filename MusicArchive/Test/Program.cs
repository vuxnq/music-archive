using Microsoft.Data.Sqlite;
using MusicArchive.Data;
using MusicArchive.Domain;

namespace Test;

class Program {
    static void Main(string[] args) {
        GlobalConnector.SetDataSource(GlobalConnectorDataSource.Sqlite);
        using var connector = GlobalConnector.CreateConnection();

        Console.WriteLine("artists");
        var artistDao = connector.CreateArtistDao();
        var domainArtists = artistDao.GetArtists().ToDomain();
        foreach (var a in domainArtists) Console.WriteLine(a);

        Console.WriteLine("\nreleases");
        var releaseDao = connector.CreateReleaseDao();
        var domainReleases = releaseDao.GetReleases().ToDomain();
        foreach (var r in domainReleases) Console.WriteLine(r);
        
        Console.WriteLine("\ngenres");
        var genreDao = connector.CreateGenreDao();
        var domainGenres = genreDao.GetGenres().ToDomain();
        foreach (var g in domainGenres) Console.WriteLine(g);
        
        
        // Console.WriteLine("\nartists");
        // foreach (var a in artistDao.GetArtists()) Console.WriteLine(a);
        //
        // Console.WriteLine("\nreleases");
        // foreach (var r in releaseDao.GetReleases()) Console.WriteLine(r);
        //
        // Console.WriteLine("\ngenres");
        // foreach (var g in genreDao.GetGenres()) Console.WriteLine(g);
    }
}
