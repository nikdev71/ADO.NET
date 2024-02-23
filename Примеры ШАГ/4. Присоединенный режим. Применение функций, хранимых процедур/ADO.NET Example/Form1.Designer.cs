namespace ADO.NET_Example
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button7 = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            button8 = new System.Windows.Forms.Button();
            button9 = new System.Windows.Forms.Button();
            button10 = new System.Windows.Forms.Button();
            button11 = new System.Windows.Forms.Button();
            button13 = new System.Windows.Forms.Button();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button7
            // 
            button7.Location = new System.Drawing.Point(11, 456);
            button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(702, 35);
            button7.TabIndex = 9;
            button7.Text = "Параметризированный запрос";
            button7.UseVisualStyleBackColor = true;
            button7.Click += Select_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(756, 463);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(87, 27);
            textBox1.TabIndex = 10;
            textBox1.Text = "100";
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(888, 463);
            textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(90, 27);
            textBox2.TabIndex = 11;
            textBox2.Text = "400";
            // 
            // button8
            // 
            button8.Location = new System.Drawing.Point(10, 500);
            button8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(967, 35);
            button8.TabIndex = 12;
            button8.Text = " Передача параметра в хранимую процедуру для удаления записи";
            button8.UseVisualStyleBackColor = true;
            button8.Click += Delete_Book_Click;
            // 
            // button9
            // 
            button9.Location = new System.Drawing.Point(11, 545);
            button9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(966, 35);
            button9.TabIndex = 13;
            button9.Text = "Передача выходного параметра в хранимую процедуру";
            button9.UseVisualStyleBackColor = true;
            button9.Click += How_many_books_Click;
            // 
            // button10
            // 
            button10.Location = new System.Drawing.Point(11, 590);
            button10.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button10.Name = "button10";
            button10.Size = new System.Drawing.Size(966, 35);
            button10.TabIndex = 14;
            button10.Text = "Выполнение хранимой процедуры, возвращающей значение";
            button10.UseVisualStyleBackColor = true;
            button10.Click += MaxPages_Click;
            // 
            // button11
            // 
            button11.Location = new System.Drawing.Point(12, 635);
            button11.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(966, 35);
            button11.TabIndex = 15;
            button11.Text = "Выполнение функции, возвращающей таблицу";
            button11.UseVisualStyleBackColor = true;
            button11.Click += BooksList_Click;
            // 
            // button13
            // 
            button13.Location = new System.Drawing.Point(13, 680);
            button13.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button13.Name = "button13";
            button13.Size = new System.Drawing.Size(966, 35);
            button13.TabIndex = 17;
            button13.Text = "Вызов хранимой процедуры, возвращающей выборку";
            button13.UseVisualStyleBackColor = true;
            button13.Click += ShowBooksByThemes_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(11, 12);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new System.Drawing.Size(968, 436);
            dataGridView1.TabIndex = 18;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(720, 464);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(29, 20);
            label1.TabIndex = 19;
            label1.Text = "От:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(850, 466);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(31, 20);
            label2.TabIndex = 20;
            label2.Text = "До:";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(11, 725);
            button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(966, 35);
            button1.TabIndex = 21;
            button1.Text = "Вызов хранимой процедуры для добавления книги в таблицу";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Add_Book_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(991, 767);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(button13);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button7);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            Text = "База данных Книги";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

