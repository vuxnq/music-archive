namespace MusicArchive.Data;

public class SqlConnector : IDataConnector {
    private string connectionString;

    public SqlConnector(string connectionString) {
        this.connectionString = connectionString;
        SQLitePCL.Batteries.Init();
    }
    
    public IReleaseDao CreateReleaseDao() {
        return new ReleaseSqlDao(connectionString);
    }

    public IArtistDao CreateArtistDao() {
        return new ArtistSqlDao(connectionString);
    }

    public IGenreDao CreateGenreDao() {
        return new GenreSqlDao(connectionString);
    }
}