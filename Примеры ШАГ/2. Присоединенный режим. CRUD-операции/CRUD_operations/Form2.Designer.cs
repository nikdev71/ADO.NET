namespace CRUD_operations
{
    partial class Form2
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            NameText = new TextBox();
            PagesText = new TextBox();
            CommentText = new TextBox();
            ThemesText = new TextBox();
            PressText = new TextBox();
            YearPressText = new TextBox();
            AuthorText = new TextBox();
            QuantityText = new TextBox();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(117, 13);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 0;
            label1.Text = "Название:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(87, 229);
            label2.Name = "label2";
            label2.Size = new Size(110, 20);
            label2.TabIndex = 1;
            label2.Text = "Комментарий:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(120, 154);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 2;
            label3.Text = "Тематика:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(91, 117);
            label4.Name = "label4";
            label4.Size = new Size(106, 20);
            label4.TabIndex = 3;
            label4.Text = "Издательство:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(99, 83);
            label5.Name = "label5";
            label5.Size = new Size(98, 20);
            label5.TabIndex = 4;
            label5.Text = "Год издания:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(143, 48);
            label6.Name = "label6";
            label6.Size = new Size(54, 20);
            label6.TabIndex = 5;
            label6.Text = "Автор:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(9, 267);
            label7.Name = "label7";
            label7.Size = new Size(188, 20);
            label7.TabIndex = 6;
            label7.Text = "Количество экземпляров:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(43, 192);
            label8.Name = "label8";
            label8.Size = new Size(154, 20);
            label8.TabIndex = 7;
            label8.Text = "Количество страниц:";
            // 
            // NameText
            // 
            NameText.Location = new Point(211, 9);
            NameText.Name = "NameText";
            NameText.Size = new Size(363, 27);
            NameText.TabIndex = 1;
            NameText.Text = "SQL";
            // 
            // PagesText
            // 
            PagesText.Location = new Point(211, 188);
            PagesText.Name = "PagesText";
            PagesText.Size = new Size(363, 27);
            PagesText.TabIndex = 6;
            PagesText.Text = "1000";
            // 
            // CommentText
            // 
            CommentText.Location = new Point(211, 225);
            CommentText.Name = "CommentText";
            CommentText.Size = new Size(363, 27);
            CommentText.TabIndex = 7;
            CommentText.Text = "Карманный справочник";
            // 
            // ThemesText
            // 
            ThemesText.Location = new Point(211, 151);
            ThemesText.Name = "ThemesText";
            ThemesText.Size = new Size(363, 27);
            ThemesText.TabIndex = 5;
            ThemesText.Text = "Базы данных";
            // 
            // PressText
            // 
            PressText.Location = new Point(211, 112);
            PressText.Name = "PressText";
            PressText.Size = new Size(363, 27);
            PressText.TabIndex = 4;
            PressText.Text = "Питер";
            // 
            // YearPressText
            // 
            YearPressText.Location = new Point(211, 78);
            YearPressText.Name = "YearPressText";
            YearPressText.Size = new Size(363, 27);
            YearPressText.TabIndex = 3;
            YearPressText.Text = "2010";
            // 
            // AuthorText
            // 
            AuthorText.Location = new Point(211, 45);
            AuthorText.Name = "AuthorText";
            AuthorText.Size = new Size(363, 27);
            AuthorText.TabIndex = 2;
            AuthorText.Text = "Генник";
            // 
            // QuantityText
            // 
            QuantityText.Location = new Point(211, 262);
            QuantityText.Name = "QuantityText";
            QuantityText.Size = new Size(363, 27);
            QuantityText.TabIndex = 8;
            QuantityText.Text = "10";
            // 
            // button1
            // 
            button1.Location = new Point(56, 317);
            button1.Name = "button1";
            button1.Size = new Size(204, 29);
            button1.TabIndex = 9;
            button1.Text = "Добавить";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Add_book;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(314, 317);
            button2.Name = "button2";
            button2.Size = new Size(204, 29);
            button2.TabIndex = 10;
            button2.Text = "Отмена";
            button2.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(586, 363);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(QuantityText);
            Controls.Add(AuthorText);
            Controls.Add(YearPressText);
            Controls.Add(PressText);
            Controls.Add(ThemesText);
            Controls.Add(CommentText);
            Controls.Add(PagesText);
            Controls.Add(NameText);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private TextBox NameText;
        private TextBox PagesText;
        private TextBox CommentText;
        private TextBox ThemesText;
        private TextBox PressText;
        private TextBox YearPressText;
        private TextBox AuthorText;
        private TextBox QuantityText;
        private Button button1;
        private Button button2;
    }
}