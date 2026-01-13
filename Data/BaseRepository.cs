using System.Data;
using System.Data.SQLite;

namespace ExpenseTracker.WinForms.Data
{
    public abstract class BaseRepository
    {
        protected SQLiteConnection GetConnection()
        {
            return Database.GetConnection();
        }

        protected SQLiteCommand CreateCommand(string sql,
                                                SQLiteConnection conn, 
                                                params SQLiteParameter[] parameters)
        {
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        protected int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                using (var cmd = CreateCommand(sql,conn,parameters))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        protected object ExecuteScalar(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    return cmd.ExecuteScalar();
                }
            }
        }

        protected DataTable ExecuteQuery(string sql, params SQLiteParameter[] parameters)
        {
            using (SQLiteConnection conn = GetConnection())
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {

                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        return table;
                    }
                }
            }
        }

    }
}
