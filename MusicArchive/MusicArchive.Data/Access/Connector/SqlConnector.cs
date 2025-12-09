using Microsoft.Data.Sqlite;

namespace MusicArchive.Data;

public class SqlConnector : IDataConnector {
    private readonly SqliteConnection _connection;
    private SqliteTransaction? _transaction;
    private bool _disposed = false;

    public SqlConnector() {
        var connectionString = GlobalConnector.GetConnectionString();
        SQLitePCL.Batteries.Init();
        _connection = new SqliteConnection(connectionString);
        _connection.Open();
    }

    public IReleaseDao CreateReleaseDao() {
        return new ReleaseSqlDao(_connection);
    }

    public IArtistDao CreateArtistDao() {
        return new ArtistSqlDao(_connection);
    }

    public IGenreDao CreateGenreDao() {
        return new GenreSqlDao(_connection);
    }

    public ITrackDao CreateTrackDao() {
        return new TrackSqlDao(_connection);
    }

    public IUserDao CreateUserDao() {
        return new UserSqlDao(_connection);
    }

    public void Dispose() {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) {
        if (_disposed) return;
        if (disposing) {
            try {
                _transaction?.Dispose();
            } catch {}
            _connection?.Dispose();
        }
        _disposed = true;
    }

    public void BeginTransaction() {
        if (_transaction != null) return;
        _transaction = _connection.BeginTransaction();
    }

    public void Commit() {
        if (_transaction == null) return;
        _transaction.Commit();
        _transaction.Dispose();
        _transaction = null;
    }

    public void Rollback() {
        if (_transaction == null) return;
        _transaction.Rollback();
        _transaction.Dispose();
        _transaction = null;
    }
}
