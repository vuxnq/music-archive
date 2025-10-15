namespace MusicArchive.Data;

public interface IDataConnector
{
    public IReleaseDao CreateReleaseDao();
    public IArtistDao CreateArtistDao();
    public IGenreDao CreateGenreDao();
}