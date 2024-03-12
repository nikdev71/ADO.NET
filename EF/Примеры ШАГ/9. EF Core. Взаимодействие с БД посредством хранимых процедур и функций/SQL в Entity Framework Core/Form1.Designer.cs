namespace SQL_в_Entity_Framework_Core
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
            button1 = new Button();
            label2 = new Label();
            label1 = new Label();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button7 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            button13 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(-1, 2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(967, 227);
            dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(-1, 235);
            button1.Name = "button1";
            button1.Size = new Size(967, 29);
            button1.TabIndex = 1;
            button1.Text = "Все объекты из таблицы Books";
            button1.UseVisualStyleBackColor = true;
            button1.Click += All_Objects_From_Books;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(838, 282);
            label2.Name = "label2";
            label2.Size = new Size(31, 20);
            label2.TabIndex = 25;
            label2.Text = "До:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(708, 280);
            label1.Name = "label1";
            label1.Size = new Size(29, 20);
            label1.TabIndex = 24;
            label1.Text = "От:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(876, 279);
            textBox2.Margin = new Padding(4, 5, 4, 5);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(90, 27);
            textBox2.TabIndex = 23;
            textBox2.Text = "400";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(744, 279);
            textBox1.Margin = new Padding(4, 5, 4, 5);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(87, 27);
            textBox1.TabIndex = 22;
            textBox1.Text = "100";
            // 
            // button7
            // 
            button7.Location = new Point(-1, 272);
            button7.Margin = new Padding(4, 5, 4, 5);
            button7.Name = "button7";
            button7.Size = new Size(702, 35);
            button7.TabIndex = 21;
            button7.Text = "Параметризированный запрос";
            button7.UseVisualStyleBackColor = true;
            button7.Click += Parameterized_Query;
            // 
            // button2
            // 
            button2.Location = new Point(-1, 350);
            button2.Name = "button2";
            button2.Size = new Size(967, 29);
            button2.TabIndex = 26;
            button2.Text = "Запрос на вставку записи в таблицу Books";
            button2.UseVisualStyleBackColor = true;
            button2.Click += QueryToInsert;
            // 
            // button3
            // 
            button3.Location = new Point(-1, 385);
            button3.Name = "button3";
            button3.Size = new Size(967, 29);
            button3.TabIndex = 27;
            button3.Text = "Запрос на удаление записи из таблицы Books";
            button3.UseVisualStyleBackColor = true;
            button3.Click += QueryToDelete;
            // 
            // button4
            // 
            button4.Location = new Point(-1, 315);
            button4.Name = "button4";
            button4.Size = new Size(967, 29);
            button4.TabIndex = 28;
            button4.Text = "Интерполяция строк для передачи параметров";
            button4.UseVisualStyleBackColor = true;
            button4.Click += SqlInterpolated;
            // 
            // button5
            // 
            button5.Location = new Point(-1, 420);
            button5.Name = "button5";
            button5.Size = new Size(967, 29);
            button5.TabIndex = 29;
            button5.Text = "Запрос на обновление записей в таблице Books";
            button5.UseVisualStyleBackColor = true;
            button5.Click += QueryToUpdate;
            // 
            // button6
            // 
            button6.Location = new Point(-1, 455);
            button6.Name = "button6";
            button6.Size = new Size(967, 29);
            button6.TabIndex = 30;
            button6.Text = "Выполнение функции, возвращающей таблицу";
            button6.UseVisualStyleBackColor = true;
            button6.Click += ExecuteTableFunction;
            // 
            // button9
            // 
            button9.Location = new Point(-1, 490);
            button9.Name = "button9";
            button9.Size = new Size(967, 29);
            button9.TabIndex = 32;
            button9.Text = "Вызов хранимой процедуры, возвращающей выборку";
            button9.UseVisualStyleBackColor = true;
            button9.Click += ShowBooksByThemes;
            // 
            // button10
            // 
            button10.Location = new Point(-1, 525);
            button10.Name = "button10";
            button10.Size = new Size(967, 29);
            button10.TabIndex = 33;
            button10.Text = "Передача выходного параметра в хранимую процедуру";
            button10.UseVisualStyleBackColor = true;
            button10.Click += How_many_books;
            // 
            // button11
            // 
            button11.Location = new Point(-1, 560);
            button11.Name = "button11";
            button11.Size = new Size(967, 29);
            button11.TabIndex = 34;
            button11.Text = " Передача параметра в хранимую процедуру для удаления записи";
            button11.UseVisualStyleBackColor = true;
            button11.Click += Delete_Book;
            // 
            // button12
            // 
            button12.Location = new Point(-1, 595);
            button12.Name = "button12";
            button12.Size = new Size(967, 29);
            button12.TabIndex = 35;
            button12.Text = "Выполнение хранимой процедуры, возвращающей значение";
            button12.UseVisualStyleBackColor = true;
            button12.Click += MaxPages;
            // 
            // button13
            // 
            button13.Location = new Point(-1, 630);
            button13.Name = "button13";
            button13.Size = new Size(967, 29);
            button13.TabIndex = 36;
            button13.Text = "Вызов хранимой процедуры для добавления книги в таблицу";
            button13.UseVisualStyleBackColor = true;
            button13.Click += Add_Book;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(970, 662);
            Controls.Add(button13);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button7);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "SQL в Entity Framework Core";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private Label label2;
        private Label label1;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button7;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;
        private Button button13;
    }
}
