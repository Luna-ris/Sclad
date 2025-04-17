using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Sclad
{
    public partial class MainForm : Form
    {
        private int warehouseId;

        public MainForm(int warehouseId)
        {
            InitializeComponent();
            this.warehouseId = warehouseId;
            this.KeyPreview = true;
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SuppliersForm suppliersForm = new SuppliersForm(warehouseId);
            suppliersForm.ShowDialog();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            ProductsForm productsForm = new ProductsForm(warehouseId);
            productsForm.ShowDialog();
        }

        private void btnRecipients_Click(object sender, EventArgs e)
        {
            RecipientsForm recipientsForm = new RecipientsForm(warehouseId);
            recipientsForm.ShowDialog();
        }

        private void btnWarehousemen_Click(object sender, EventArgs e)
        {
            WarehousemenForm warehousemenForm = new WarehousemenForm(warehouseId);
            warehousemenForm.ShowDialog();
        }

        private void btnSupplies_Click(object sender, EventArgs e)
        {
            SuppliesForm suppliesForm = new SuppliesForm(warehouseId);
            suppliesForm.ShowDialog();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            TransactionsForm transactionsForm = new TransactionsForm(warehouseId);
            transactionsForm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1)
            {
                string filePath = @"C:\Users\zenag\Руководство пользователя.chm";
                if (File.Exists(filePath))
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
                else
                {
                    MessageBox.Show("Файл не найден: " + filePath);
                }
            }
        }
    }
}
