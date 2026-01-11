using System.Data.SQLite;
using System.IO;

namespace ExpenseTracker.WinForms.Data
{
    public static class Database
    {

        private readonly static string dbFile = "expenses.db";
        private readonly static string connectionString = $"Data Source = {dbFile}; Version = 3;";

        public static SQLiteConnection GetConnection()
        {
            if (!File.Exists(dbFile))
            {
                SQLiteConnection.CreateFile(dbFile);
                CreateTables();
            }

            return new SQLiteConnection(connectionString);
        }

        private static void CreateTables()
        {
            using (SQLiteConnection con = new SQLiteConnection(connectionString))
            {
                con.Open();

                string sql = @"CREATE TABLE Expenses (
                                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                                ExpenseDate TEXT,
                                Title TEXT,
                                Amount REAL,
                                Notes TEXT
                               );";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}