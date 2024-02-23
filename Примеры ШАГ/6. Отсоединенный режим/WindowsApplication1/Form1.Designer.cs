namespace WindowsApplication1
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
            connection.Close();
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
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            listBox1 = new System.Windows.Forms.ListBox();
            button3 = new System.Windows.Forms.Button();
            textBox1 = new System.Windows.Forms.TextBox();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            dataGridView2 = new System.Windows.Forms.DataGridView();
            button4 = new System.Windows.Forms.Button();
            textBox2 = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            dataGridView3 = new System.Windows.Forms.DataGridView();
            label2 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(13, 10);
            button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(280, 55);
            button1.TabIndex = 0;
            button1.Text = "Создание таблиц";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Create_Tables_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(614, 10);
            button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(280, 55);
            button2.TabIndex = 1;
            button2.Text = "Выборка данных";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Select_Data_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 20;
            listBox1.Location = new System.Drawing.Point(606, 393);
            listBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(288, 144);
            listBox1.TabIndex = 2;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(13, 393);
            button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(347, 61);
            button3.TabIndex = 3;
            button3.Text = "Удаление записи";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Delete_Row_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(526, 410);
            textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(66, 27);
            textBox1.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(13, 75);
            dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new System.Drawing.Size(433, 149);
            dataGridView1.TabIndex = 5;
            // 
            // dataGridView2
            // 
            dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new System.Drawing.Point(461, 75);
            dataGridView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 51;
            dataGridView2.Size = new System.Drawing.Size(433, 149);
            dataGridView2.TabIndex = 6;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(13, 464);
            button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(347, 67);
            button4.TabIndex = 7;
            button4.Text = "Обновление записи";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Update_Row_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(384, 504);
            textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(208, 27);
            textBox2.TabIndex = 8;
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(387, 412);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(122, 28);
            label1.TabIndex = 9;
            label1.Text = "Индекс записи: ";
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new System.Drawing.Point(13, 234);
            dataGridView3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new System.Drawing.Size(881, 149);
            dataGridView3.TabIndex = 11;
            // 
            // label2
            // 
            label2.Location = new System.Drawing.Point(384, 466);
            label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(125, 28);
            label2.TabIndex = 18;
            label2.Text = "Идентификатор: ";
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(526, 464);
            textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(66, 27);
            textBox3.TabIndex = 17;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(312, 10);
            button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(280, 55);
            button5.TabIndex = 19;
            button5.Text = "Добавление записей";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Insert_Rows_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(904, 546);
            Controls.Add(button5);
            Controls.Add(label2);
            Controls.Add(textBox3);
            Controls.Add(dataGridView3);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(button4);
            Controls.Add(dataGridView2);
            Controls.Add(dataGridView1);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(listBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MaximizeBox = false;
            Name = "Form1";
            Text = "Отсоединенная модель работы с данными";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button5;
    }
}

