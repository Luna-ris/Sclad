﻿using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using System.IO;
using System.Globalization;

namespace Sclad
{
    public partial class SuppliesForm : Form
    {
        private SQLiteConnection conn;
        private int warehouseId;

        public SuppliesForm(int warehouseId)
        {
            InitializeComponent();
            this.warehouseId = warehouseId;
            conn = new SQLiteConnection("Data Source=C:\\Users\\zenag\\Documents\\Warehouse.db;Version=3;");
            LoadData();
            LoadSuppliers();
            LoadProducts();
        }

        private void LoadData()
        {
            conn.Open();
            string sql = "SELECT * FROM Supplies WHERE WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void LoadSuppliers()
        {
            conn.Open();
            string sql = "SELECT SupplierID, Name FROM Suppliers WHERE WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboSupplier.DataSource = dt;
            comboSupplier.DisplayMember = "Name";
            comboSupplier.ValueMember = "SupplierID";
            conn.Close();
        }

        private void LoadProducts()
        {
            conn.Open();
            string sql = "SELECT ProductID, Name FROM Products WHERE WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboProduct.DataSource = dt;
            comboProduct.DisplayMember = "Name";
            comboProduct.ValueMember = "ProductID";
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSupplyDate.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                conn.Open();
                string sql;
                SQLiteCommand cmd;
                if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Cells["SupplyID"].Value != null)
                {
                    sql = "UPDATE Supplies SET SupplierID = @SupplierID, ProductID = @ProductID, SupplyDate = @SupplyDate WHERE SupplyID = @ID AND WarehouseID = @WarehouseID";
                    cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["SupplyID"].Value);
                }
                else
                {
                    sql = "INSERT INTO Supplies (SupplierID, ProductID, SupplyDate, WarehouseID) VALUES (@SupplierID, @ProductID, @SupplyDate, @WarehouseID)";
                    cmd = new SQLiteCommand(sql, conn);
                }
                cmd.Parameters.AddWithValue("@SupplierID", comboSupplier.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductID", comboProduct.SelectedValue);
                cmd.Parameters.AddWithValue("@SupplyDate", txtSupplyDate.Text);
                cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
                cmd.ExecuteNonQuery();
                conn.Close();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранную поставку?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    conn.Open();
                    string sql = "DELETE FROM Supplies WHERE SupplyID = @ID AND WarehouseID = @WarehouseID";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["SupplyID"].Value);
                    cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                comboSupplier.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["SupplierID"].Value;
                comboProduct.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value;
                txtSupplyDate.Text = dataGridView1.Rows[e.RowIndex].Cells["SupplyDate"].Value.ToString();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM Supplies WHERE SupplyDate LIKE @Search AND WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Search", "%" + txtSearch.Text + "%");
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM Supplies WHERE WarehouseID = @WarehouseID";
            if (comboFilter.SelectedIndex == 1) sql += " AND SupplyDate BETWEEN '2025-04-01' AND '2025-04-11'";
            else if (comboFilter.SelectedIndex == 2) sql += " AND SupplyDate BETWEEN '2025-05-01' AND '2025-05-11'";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT s.SupplyDate, sup.Name AS Supplier, p.Name AS Product " +
                         "FROM Supplies s " +
                         "JOIN Suppliers sup ON s.SupplierID = sup.SupplierID " +
                         "JOIN Products p ON s.ProductID = p.ProductID " +
                         "WHERE s.SupplyDate BETWEEN '2025-04-01' AND '2025-04-11' AND s.WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf|Excel Files|*.xlsx";
            saveFileDialog.Title = "Экспорт";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string fileExtension = Path.GetExtension(filePath).ToLower();

                if (fileExtension == ".pdf")
                {
                    ExportToPDF(filePath);
                }
                else if (fileExtension == ".xlsx")
                {
                    ExportToXLSX(filePath);
                }
            }
        }

        private void ExportToPDF(string filePath)
        {
            Document doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();

            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                table.AddCell(cell);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    table.AddCell(cell.Value?.ToString());
                }
            }

            doc.Add(table);
            doc.Close();
        }

        private void ExportToXLSX(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Supplies");
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataGridView1.Columns[i].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].IsNewRow) continue;
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                workbook.SaveAs(filePath);
            }
        }

        private void SuppliesForm_Load(object sender, EventArgs e)
        {

        }
    }
}
