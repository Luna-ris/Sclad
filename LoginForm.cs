using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Sclad
{
    public partial class LoginForm : Form
    {
        private SQLiteConnection conn;

        public LoginForm()
        {
            InitializeComponent();
            conn = new SQLiteConnection("Data Source=C:\\Users\\zenag\\Documents\\Warehouse.db;Version=3;");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWarehouse.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            conn.Open();
            string sql = "SELECT WarehouseID FROM Warehouses WHERE Name = @Name AND Password = @Password";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Name", txtWarehouse.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            object result = cmd.ExecuteScalar();
            conn.Close();

            if (result != null)
            {
                int warehouseId = Convert.ToInt32(result);
                MainForm mainForm = new MainForm(warehouseId);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неверное название склада или пароль!", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewWarehouse.Text) || string.IsNullOrWhiteSpace(txtNewPassword.Text) || string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            conn.Open();
            string checkSql = "SELECT COUNT(*) FROM Warehouses WHERE Name = @Name";
            SQLiteCommand checkCmd = new SQLiteCommand(checkSql, conn);
            checkCmd.Parameters.AddWithValue("@Name", txtNewWarehouse.Text);
            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count > 0)
            {
                MessageBox.Show("Склад с таким именем уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
                return;
            }

            string insertSql = "INSERT INTO Warehouses (Name, Password) VALUES (@Name, @Password)";
            SQLiteCommand insertCmd = new SQLiteCommand(insertSql, conn);
            insertCmd.Parameters.AddWithValue("@Name", txtNewWarehouse.Text);
            insertCmd.Parameters.AddWithValue("@Password", txtNewPassword.Text);
            insertCmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Регистрация успешна! Теперь вы можете войти в систему.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
