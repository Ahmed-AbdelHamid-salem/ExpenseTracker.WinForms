using ExpenseTracker.WinForms.Models;
using System;
using System.Data.SQLite;

namespace ExpenseTracker.WinForms.Data
{
    internal class ExpenseRepository
    {

        public void Add(Expense expense)
        {
            using (SQLiteConnection con = Database.GetConnection())
            {
                con.Open();

                string sql = @"INSERT INTO Expenses 
                               (ExpenseDate, Title, Amount, Notes)
                               VALUES (@date, @title, @amount, @notes)";

                using (SQLiteCommand cmd = new SQLiteCommand(sql,con))
                {
                    cmd.Parameters.AddWithValue("@date", expense.ExpenseDate);
                    cmd.Parameters.AddWithValue("@title", expense.Title);
                    cmd.Parameters.AddWithValue("@amount", expense.Amount);
                    cmd.Parameters.AddWithValue("@notes", expense.Notes);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
