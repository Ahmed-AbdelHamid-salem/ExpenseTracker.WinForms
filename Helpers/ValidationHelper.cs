using DevExpress.XtraEditors.Filtering.Templates;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExpenseTracker.WinForms.Helpers
{
    public static class ValidationHelper
    {

        public static bool IsTextEmpty(Guna2TextBox txt, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show($"{fieldName} is required", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return true;
            }
            return false;
        }


        public static bool IsValidDecimal(Guna2TextBox txt, string fieldName)
        {
            if (!decimal.TryParse(txt.Text,NumberStyles.Any, 
                CultureInfo.InvariantCulture, out decimal value) || value <= 0)
            {
                MessageBox.Show($"{fieldName} must be a valid positive number",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            return true;
        }

    }
}
