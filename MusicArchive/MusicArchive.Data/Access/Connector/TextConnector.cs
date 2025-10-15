namespace MusicArchive.Data;

public class TextConnector : IDataConnector
{
    private string textFilesPath;
    public TextConnector(string textFilesPath) {
        this.textFilesPath = textFilesPath;
    }
    
    public IReleaseDao CreateReleaseDao() {
        return new ReleaseTextDao(textFilesPath + "release.json");
    }

    public IArtistDao CreateArtistDao() {
        return new ArtistTextDao(textFilesPath + "artist.json");
    }

    public IGenreDao CreateGenreDao() {
        return new GenreTextDao(textFilesPath + "genre.json");
    }
}