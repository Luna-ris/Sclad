using System.Windows.Forms;

namespace Sclad
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnSuppliers = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnRecipients = new System.Windows.Forms.Button();
            this.btnWarehousemen = new System.Windows.Forms.Button();
            this.btnSupplies = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.btnSuppliers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuppliers.Font = new System.Drawing.Font("Arial", 12F);
            this.btnSuppliers.Location = new System.Drawing.Point(150, 120);
            this.btnSuppliers.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(200, 48);
            this.btnSuppliers.TabIndex = 2;
            this.btnSuppliers.Text = "Поставщики";
            this.btnSuppliers.UseVisualStyleBackColor = false;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.Font = new System.Drawing.Font("Arial", 12F);
            this.btnProducts.Location = new System.Drawing.Point(400, 120);
            this.btnProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(200, 48);
            this.btnProducts.TabIndex = 3;
            this.btnProducts.Text = "Товары";
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnRecipients
            // 
            this.btnRecipients.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.btnRecipients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecipients.Font = new System.Drawing.Font("Arial", 12F);
            this.btnRecipients.Location = new System.Drawing.Point(650, 120);
            this.btnRecipients.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRecipients.Name = "btnRecipients";
            this.btnRecipients.Size = new System.Drawing.Size(200, 48);
            this.btnRecipients.TabIndex = 4;
            this.btnRecipients.Text = "Получатели";
            this.btnRecipients.UseVisualStyleBackColor = false;
            this.btnRecipients.Click += new System.EventHandler(this.btnRecipients_Click);
            // 
            // btnWarehousemen
            // 
            this.btnWarehousemen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.btnWarehousemen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWarehousemen.Font = new System.Drawing.Font("Arial", 12F);
            this.btnWarehousemen.Location = new System.Drawing.Point(150, 200);
            this.btnWarehousemen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnWarehousemen.Name = "btnWarehousemen";
            this.btnWarehousemen.Size = new System.Drawing.Size(200, 48);
            this.btnWarehousemen.TabIndex = 5;
            this.btnWarehousemen.Text = "Кладовщики";
            this.btnWarehousemen.UseVisualStyleBackColor = false;
            this.btnWarehousemen.Click += new System.EventHandler(this.btnWarehousemen_Click);
            // 
            // btnSupplies
            // 
            this.btnSupplies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.btnSupplies.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupplies.Font = new System.Drawing.Font("Arial", 12F);
            this.btnSupplies.Location = new System.Drawing.Point(400, 200);
            this.btnSupplies.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSupplies.Name = "btnSupplies";
            this.btnSupplies.Size = new System.Drawing.Size(200, 48);
            this.btnSupplies.TabIndex = 6;
            this.btnSupplies.Text = "Поставки";
            this.btnSupplies.UseVisualStyleBackColor = false;
            this.btnSupplies.Click += new System.EventHandler(this.btnSupplies_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(206)))), ((int)(((byte)(235)))));
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Arial", 12F);
            this.btnTransactions.Location = new System.Drawing.Point(650, 200);
            this.btnTransactions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(200, 48);
            this.btnTransactions.TabIndex = 7;
            this.btnTransactions.Text = "Транзакции";
            this.btnTransactions.UseVisualStyleBackColor = false;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(350, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(377, 40);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Управление складом";
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 64);
            this.panelHeader.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1000, 480);
            this.Controls.Add(this.btnTransactions);
            this.Controls.Add(this.btnSupplies);
            this.Controls.Add(this.btnWarehousemen);
            this.Controls.Add(this.btnRecipients);
            this.Controls.Add(this.btnProducts);
            this.Controls.Add(this.btnSuppliers);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.panelHeader);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "Склад";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Button btnSuppliers;
        private Button btnProducts;
        private Button btnRecipients;
        private Button btnWarehousemen;
        private Button btnSupplies;
        private Button btnTransactions;
        private Label lblTitle;
        private Panel panelHeader;
    }
}
