using Microsoft.Data.Sqlite;

namespace MusicArchive.Data;

public enum GlobalConnectorDataSource {
    Sqlite,
    Text
}

public static class GlobalConnector {
    private static GlobalConnectorDataSource _dataSource = GlobalConnectorDataSource.Sqlite;

    public static IDataConnector CreateConnection() {
        if (_dataSource == GlobalConnectorDataSource.Sqlite) {
            return new SqlConnector();
        }
        return new TextConnector();
    }

    public static void SetDataSource(GlobalConnectorDataSource dataSource) {
        _dataSource = dataSource;
    }

    public static string GetConnectionString() {
        var solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
        var builder = new SqliteConnectionStringBuilder();
        builder.DataSource = Path.Combine(solutionPath, "musicarchive.db");
        builder.Mode = SqliteOpenMode.ReadWriteCreate;
        return builder.ConnectionString;
    }

    public static string GetFilesPath() {
        var solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", ".."));
        return Path.Combine(solutionPath, "MusicArchive.TextFiles/");
    }
}