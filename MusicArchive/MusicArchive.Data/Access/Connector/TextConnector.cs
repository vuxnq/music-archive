namespace MusicArchive.Data;

public class TextConnector : IDataConnector
{
    private string textFilesPath;
    public TextConnector() {
        this.textFilesPath = GlobalConnector.GetFilesPath();
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

    public ITrackDao CreateTrackDao() {
        return new TrackTextDao(textFilesPath + "track.json");
    }

    public IUserDao CreateUserDao() {
        return new UserTextDao(textFilesPath + "user.json");
    }

    public void Dispose() {}

    public void BeginTransaction() {}
    public void Commit() {}
    public void Rollback() {}
}