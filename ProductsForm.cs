using System;
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
    public partial class ProductsForm : Form
    {
        private SQLiteConnection conn;
        private int warehouseId;

        public ProductsForm(int warehouseId)
        {
            InitializeComponent();
            this.warehouseId = warehouseId;
            conn = new SQLiteConnection("Data Source=C:\\Users\\zenag\\Documents\\Warehouse.db;Version=3;");
            LoadData();
            LoadWarehousemen();
        }

        private void LoadData()
        {
            conn.Open();
            string sql = "SELECT * FROM Products WHERE WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void LoadWarehousemen()
        {
            conn.Open();
            string sql = "SELECT WarehousemanID, Name FROM Warehousemen WHERE WarehouseID = @WarehouseID";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboWarehouseman.DataSource = dt;
            comboWarehouseman.DisplayMember = "Name";
            comboWarehouseman.ValueMember = "WarehousemanID";
            conn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out _))
                {
                    MessageBox.Show("Количество должно быть числом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                conn.Open();
                string sql;
                SQLiteCommand cmd;
                if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows[0].Cells["ProductID"].Value != null)
                {
                    sql = "UPDATE Products SET Name = @Name, Quantity = @Quantity, WarehousemanID = @WarehousemanID WHERE ProductID = @ID AND WarehouseID = @WarehouseID";
                    cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ProductID"].Value);
                }
                else
                {
                    sql = "INSERT INTO Products (Name, Quantity, WarehousemanID, WarehouseID) VALUES (@Name, @Quantity, @WarehousemanID, @WarehouseID)";
                    cmd = new SQLiteCommand(sql, conn);
                }
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Quantity", txtQuantity.Text);
                cmd.Parameters.AddWithValue("@WarehousemanID", comboWarehouseman.SelectedValue);
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
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранный товар?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    conn.Open();
                    string sql = "DELETE FROM Products WHERE ProductID = @ID AND WarehouseID = @WarehouseID";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", dataGridView1.SelectedRows[0].Cells["ProductID"].Value);
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
                txtName.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                txtQuantity.Text = dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString();
                comboWarehouseman.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["WarehousemanID"].Value;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT * FROM Products WHERE Name LIKE @Search AND WarehouseID = @WarehouseID";
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
            string sql = "SELECT * FROM Products WHERE WarehouseID = @WarehouseID";
            if (comboFilter.SelectedIndex == 1) sql += " AND Quantity < 50";
            else if (comboFilter.SelectedIndex == 2) sql += " AND Quantity > 50";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@WarehouseID", warehouseId);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sql = "SELECT p.Name, p.Quantity FROM Products p WHERE p.Quantity > 0 AND p.WarehouseID = @WarehouseID";
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
                var worksheet = workbook.Worksheets.Add("Products");
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

        private void ProductsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
