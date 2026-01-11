using ExpenseTracker.WinForms.Models;
using System;
using System.Collections.Generic;
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

        public List<Expense> GetAll()
        {
            List<Expense> list = new List<Expense>();

            using (SQLiteConnection con = Database.GetConnection())
            {
                con.Open();

                string sql = "SELECT * FROM Expenses ORDER BY ExpenseDate DESC";

                using (SQLiteCommand cmd = new SQLiteCommand(sql,con))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            list.Add(new Expense()
                            {
                                Id = reader.GetInt32(0),
                                ExpenseDate = DateTime.Parse(reader.GetString(1)),
                                Title = reader.GetString(2),
                                Amount = reader.GetDecimal(3),
                                Notes = reader.GetString(4)
                            });
                        }
                    }
                }
            }

            return list;

        }

        public void Update(Expense expense)
        {
            using (SQLiteConnection con =  Database.GetConnection())
            {
                con.Open();

                string sql = @"UPDATE Expenses SET
                               ExpenseDate=@date,
                               Title=@title,
                               Amount=@amount,
                               Notes=@notes
                               WHERE Id=@id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql,con))
                {
                    cmd.Parameters.AddWithValue("@date", expense.ExpenseDate);
                    cmd.Parameters.AddWithValue("@title", expense.Title);
                    cmd.Parameters.AddWithValue("@amount", expense.Amount);
                    cmd.Parameters.AddWithValue("@notes", expense.Notes);
                    cmd.Parameters.AddWithValue("@id", expense.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var con = Database.GetConnection())
            {
                con.Open();
                string sql = "DELETE FROM Expenses WHERE Id = @id";

                using (SQLiteCommand cmd = new SQLiteCommand(sql,con))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
