using ExpenseTracker.WinForms.Data;
using ExpenseTracker.WinForms.Helpers;
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
            LoadTotalAmount();
            txtTitle.Select();
        }

        private void ClearForm()
        {
            txtTitle.Clear();
            txtAmount.Clear();
            txtNotes.Clear();
            txtTitle.Select();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
           try
            {

                if (ValidationHelper.IsTextEmpty(txtTitle, "Title")) return;
                if (!ValidationHelper.IsValidDecimal(txtAmount, "Amount")) return;

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
                    MessageBox.Show("Expense Updated successfully");
                }

                ClearForm();
                LoadExpenses();

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
                _selectedId = Convert.ToInt32(row.Cells["ID"].Value);

                dtpDate.Value = Convert.ToDateTime(row.Cells["ExpenseDate"].Value);
                txtTitle.Text = row.Cells["Title"].Value.ToString();
                txtAmount.Text = row.Cells["Amount"].Value.ToString();
                txtNotes.Text = row.Cells["Notes"].Value.ToString();
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (_selectedId == 0) return;

                _expenseRepo.Delete(_selectedId);
                LoadExpenses() ;
                ClearForm();
                _selectedId = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }
        }

        private void LoadTotalAmount()
        {
            decimal total = _expenseRepo.GetTotalAmount();
            lblTotalAmount.Text = $"Total Expenses: {total:N2}";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("خروج من البرنامج؟","تنبيه",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void BtnMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnClearInputs_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
