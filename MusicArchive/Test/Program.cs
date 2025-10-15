using Microsoft.Data.Sqlite;
using MusicArchive.Data;

namespace Test;

class Program {
    static void Main(string[] args) {

        // GlobalConnector.SetDataSource(GlobalConnectorDataSource.Sqlite);
        GlobalConnector.SetDataSource(GlobalConnectorDataSource.Text);
        
        var artistDao = GlobalConnector.CreateConnection().CreateArtistDao();
        foreach (var artist in artistDao.GetArtists()) {
            Console.WriteLine(artist);
        }

        Console.WriteLine(artistDao.GetArtist(0));
        
        var genreDao = GlobalConnector.CreateConnection().CreateGenreDao();
        foreach (var genre in genreDao.GetGenres()) {
            Console.WriteLine(genre);
        }
        
        
        var releaseDao = GlobalConnector.CreateConnection().CreateReleaseDao();
        foreach (var release in releaseDao.GetReleases()) {
            Console.WriteLine(release);
        }
    }
}