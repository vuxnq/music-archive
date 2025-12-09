namespace MusicArchive.Data;

public class TextConnector : IDataConnector
{
    private readonly string _textFilesPath;
    public TextConnector() {
        _textFilesPath = GlobalConnector.GetFilesPath();
    }

    public IReleaseDao CreateReleaseDao() {
        return new ReleaseTextDao(_textFilesPath + "release.json");
    }

    public IArtistDao CreateArtistDao() {
        return new ArtistTextDao(_textFilesPath + "artist.json");
    }

    public IGenreDao CreateGenreDao() {
        return new GenreTextDao(_textFilesPath + "genre.json");
    }

    public ITrackDao CreateTrackDao() {
        return new TrackTextDao(_textFilesPath + "track.json");
    }

    public IUserDao CreateUserDao() {
        return new UserTextDao(_textFilesPath + "user.json");
    }

    public void Dispose() {}

    public void BeginTransaction() {}
    public void Commit() {}
    public void Rollback() {}
}
