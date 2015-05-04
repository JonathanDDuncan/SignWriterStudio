using System.Data.SQLite;

namespace SQLiteRepository.SQLiteQuery
{
    public interface ISqliteQuery
    {
        string Path { get; set; }
        IQueryResult Execute(SQLiteConnection conn, SQLiteTransaction tr);
    }
}