using ExpenseTracker.WinForms.Models;
using System;
using System.Data;
using System.Data.SQLite;

namespace ExpenseTracker.WinForms.Data
{
    internal class ExpenseRepository : BaseRepository
    {
        public DataTable GetAll()
        {
            return ExecuteQuery("SELECT * FROM Expenses ORDER BY ExpenseDate DESC");
        }

        public void Add(Expense expense)
        {
            ExecuteNonQuery(
                "INSERT INTO Expenses (Title, Amount, ExpenseDate, Notes) VALUES (@Title, @Amount, @Date, @Note)",
                new SQLiteParameter("@Title", expense.Title),
                new SQLiteParameter("@Amount", expense.Amount),
                new SQLiteParameter("@Date", expense.ExpenseDate),
                new SQLiteParameter("@Note", expense.Notes)
            );
        }

        public void Update(Expense expense)
        {
            ExecuteNonQuery(
                @"UPDATE Expenses 
                  SET Title = @Title, Amount = @Amount, ExpenseDate = @Date, Notes = @Note 
                  WHERE Id = @id",
                new SQLiteParameter("@Title", expense.Title),
                new SQLiteParameter("@Amount", expense.Amount),
                new SQLiteParameter("@Date", expense.ExpenseDate),
                new SQLiteParameter("@Note", expense.Notes),
                new SQLiteParameter("@id", expense.Id)
            );
        }

        public void Delete(int id)
        {
            ExecuteNonQuery(
                "DELETE FROM Expenses WHERE Id = @id",
                new SQLiteParameter("@id", id)
            );
        }

        public decimal GetTotalAmount()
        {
            return Convert.ToDecimal(
                ExecuteScalar("SELECT IFNULL(SUM(Amount),0) FROM Expenses")
            );
        }
    }
}
