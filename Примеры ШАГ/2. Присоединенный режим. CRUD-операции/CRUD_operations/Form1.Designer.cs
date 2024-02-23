namespace CRUD_operations
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            menuStrip1 = new MenuStrip();
            cRUDoperationToolStripMenuItem = new ToolStripMenuItem();
            выборкаКнигToolStripMenuItem = new ToolStripMenuItem();
            добавлениеНовойКнигиToolStripMenuItem = new ToolStripMenuItem();
            редактированиеКнигиToolStripMenuItem = new ToolStripMenuItem();
            удалениеКнигиToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 28);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(1221, 594);
            dataGridView1.TabIndex = 2;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { cRUDoperationToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1221, 28);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            // 
            // cRUDoperationToolStripMenuItem
            // 
            cRUDoperationToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { выборкаКнигToolStripMenuItem, добавлениеНовойКнигиToolStripMenuItem, редактированиеКнигиToolStripMenuItem, удалениеКнигиToolStripMenuItem });
            cRUDoperationToolStripMenuItem.Name = "cRUDoperationToolStripMenuItem";
            cRUDoperationToolStripMenuItem.Size = new Size(138, 24);
            cRUDoperationToolStripMenuItem.Text = "CRUD-операции";
            // 
            // выборкаКнигToolStripMenuItem
            // 
            выборкаКнигToolStripMenuItem.Name = "выборкаКнигToolStripMenuItem";
            выборкаКнигToolStripMenuItem.Size = new Size(283, 26);
            выборкаКнигToolStripMenuItem.Text = "Выборка книг";
            выборкаКнигToolStripMenuItem.Click += Select_books;
            // 
            // добавлениеНовойКнигиToolStripMenuItem
            // 
            добавлениеНовойКнигиToolStripMenuItem.Name = "добавлениеНовойКнигиToolStripMenuItem";
            добавлениеНовойКнигиToolStripMenuItem.Size = new Size(283, 26);
            добавлениеНовойКнигиToolStripMenuItem.Text = "Добавление новой книги ...";
            добавлениеНовойКнигиToolStripMenuItem.Click += AddBook;
            // 
            // редактированиеКнигиToolStripMenuItem
            // 
            редактированиеКнигиToolStripMenuItem.Name = "редактированиеКнигиToolStripMenuItem";
            редактированиеКнигиToolStripMenuItem.Size = new Size(283, 26);
            редактированиеКнигиToolStripMenuItem.Text = "Редактирование книги ...";
            редактированиеКнигиToolStripMenuItem.Click += EditBook;
            // 
            // удалениеКнигиToolStripMenuItem
            // 
            удалениеКнигиToolStripMenuItem.Name = "удалениеКнигиToolStripMenuItem";
            удалениеКнигиToolStripMenuItem.Size = new Size(283, 26);
            удалениеКнигиToolStripMenuItem.Text = "Удаление книги";
            удалениеКнигиToolStripMenuItem.Click += DeleteBook;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1221, 622);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Книги";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridView1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem cRUDoperationToolStripMenuItem;
        private ToolStripMenuItem выборкаКнигToolStripMenuItem;
        private ToolStripMenuItem добавлениеНовойКнигиToolStripMenuItem;
        private ToolStripMenuItem редактированиеКнигиToolStripMenuItem;
        private ToolStripMenuItem удалениеКнигиToolStripMenuItem;
    }
}