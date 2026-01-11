using ExpenseTracker.WinForms.Data;
using ExpenseTracker.WinForms.Models;
using System;
using System.Windows.Forms;

namespace ExpenseTracker.WinForms
{
    public partial class FrmExpenses : Form
    {

        private ExpenseRepository _expenseRepo;

        public FrmExpenses()
        {
            InitializeComponent();
            _expenseRepo = new ExpenseRepository();
        }

        private void LoadExpenses()
        {
            dgvExpenses.DataSource = _expenseRepo.GetAll();
        }


        private void ClearForm()
        {
            txtTitle.Clear();
            txtAmount.Clear();
            txtNotes.Clear();
        }


        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                Expense expense = new Expense()
                {
                    ExpenseDate = dtpDate.Value,
                    Title = txtTitle.Text,
                    Amount = decimal.Parse(txtAmount.Text),
                    Notes = txtNotes.Text
                };

                _expenseRepo.Add(expense);
                MessageBox.Show("Expense saved successfully");

                ClearForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }
        }

        private void FrmExpenses_Load(object sender, EventArgs e)
        {
            try
            {
                LoadExpenses();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }
        }
    }
}
