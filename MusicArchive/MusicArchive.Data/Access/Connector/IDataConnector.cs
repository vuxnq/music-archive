namespace MusicArchive.Data;

public interface IDataConnector : IDisposable
{
    public IReleaseDao CreateReleaseDao();
    public IArtistDao CreateArtistDao();
    public IGenreDao CreateGenreDao();
    public ITrackDao CreateTrackDao();

    public void BeginTransaction();
    public void Commit();
    public void Rollback();
}