using Microsoft.Data.Sqlite;

namespace MusicArchive.Data;

public class SqlConnector : IDataConnector {
    private string connectionString;
    private SqliteConnection connection;
    private bool disposed = false;

    public SqlConnector(string connectionString) {
        this.connectionString = connectionString;
        SQLitePCL.Batteries.Init();
        connection = new SqliteConnection(connectionString);
        connection.Open();
    }

    public IReleaseDao CreateReleaseDao() {
        return new ReleaseSqlDao(connection);
    }

    public IArtistDao CreateArtistDao() {
        return new ArtistSqlDao(connection);
    }

    public IGenreDao CreateGenreDao() {
        return new GenreSqlDao(connection);
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (disposed) return;
        if (disposing) {
            connection?.Dispose();
        }
        disposed = true;
    }
}