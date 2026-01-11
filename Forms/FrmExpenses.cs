using ExpenseTracker.WinForms.Data;
using ExpenseTracker.WinForms.Models;
using System;
using System.Windows.Forms;

namespace ExpenseTracker.WinForms
{
    public partial class FrmExpenses : Form
    {

        private ExpenseRepository _expenseRepo;
        private int _selectedId = 0;

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

                if (_selectedId == 0)
                {
                    _expenseRepo.Add(expense);
                    MessageBox.Show("Expense saved successfully");
                }
                else
                {
                    expense.Id = _selectedId;
                    _expenseRepo.Update(expense);
                }

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

        private void DgvExpenses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvExpenses.Rows[e.RowIndex];
                _selectedId = (int)row.Cells["Id"].Value;

                dtpDate.Value = (DateTime)row.Cells["ExpenseDate"].Value;
                txtTitle.Text = row.Cells["Title"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                txtNotes.Text = row.Cells["Notes"].Value.ToString();

            }
        }
    }
}
