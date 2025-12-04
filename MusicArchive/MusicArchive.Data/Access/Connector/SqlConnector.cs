using Microsoft.Data.Sqlite;

namespace MusicArchive.Data;

public class SqlConnector : IDataConnector {
    private string connectionString;
    private SqliteConnection connection;
    private SqliteTransaction? transaction;
    private bool disposed = false;

    public SqlConnector() {
        this.connectionString = GlobalConnector.GetConnectionString();
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

    public ITrackDao CreateTrackDao() {
        return new TrackSqlDao(connection);
    }

    public IUserDao CreateUserDao() {
        return new UserSqlDao(connection);
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (disposed) return;
        if (disposing) {
            try {
                transaction?.Dispose();
            } catch {}
            connection?.Dispose();
        }
        disposed = true;
    }

    public void BeginTransaction() {
        if (transaction != null) return;
        transaction = connection.BeginTransaction();
    }

    public void Commit() {
        if (transaction == null) return;
        transaction.Commit();
        transaction.Dispose();
        transaction = null;
    }

    public void Rollback() {
        if (transaction == null) return;
        transaction.Rollback();
        transaction.Dispose();
        transaction = null;
    }
}