namespace User
{
    partial class BankEmployeePage
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
            listBox1 = new ListBox();
            listBox2 = new ListBox();
            label2 = new Label();
            button1 = new Button();
            button2 = new Button();
            listBox3 = new ListBox();
            label3 = new Label();
            splitContainer1 = new SplitContainer();
            textBox1 = new TextBox();
            label4 = new Label();
            textBox3 = new TextBox();
            label6 = new Label();
            textBox5 = new TextBox();
            textBox6 = new TextBox();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(98, 25);
            label1.TabIndex = 0;
            label1.Text = "Переводы";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 25;
            listBox1.Location = new Point(12, 37);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(244, 254);
            listBox1.TabIndex = 1;
            // 
            // listBox2
            // 
            listBox2.FormattingEnabled = true;
            listBox2.ItemHeight = 25;
            listBox2.Location = new Point(273, 37);
            listBox2.Name = "listBox2";
            listBox2.Size = new Size(244, 254);
            listBox2.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(273, 9);
            label2.Name = "label2";
            label2.Size = new Size(132, 25);
            label2.TabIndex = 2;
            label2.Text = "Заросы отчета";
            // 
            // button1
            // 
            button1.Location = new Point(273, 311);
            button1.Name = "button1";
            button1.Size = new Size(506, 99);
            button1.TabIndex = 4;
            button1.Text = "Создать профиль пользователю";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 311);
            button2.Name = "button2";
            button2.Size = new Size(244, 99);
            button2.TabIndex = 5;
            button2.Text = "Создать отчет о переводе";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // listBox3
            // 
            listBox3.FormattingEnabled = true;
            listBox3.ItemHeight = 25;
            listBox3.Location = new Point(535, 37);
            listBox3.Name = "listBox3";
            listBox3.Size = new Size(244, 254);
            listBox3.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(535, 9);
            label3.Name = "label3";
            label3.Size = new Size(255, 25);
            label3.TabIndex = 6;
            label3.Text = "Заросы на создание профиля";
            // 
            // splitContainer1
            // 
            splitContainer1.Location = new Point(12, 475);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(textBox1);
            splitContainer1.Panel1.Controls.Add(label4);
            splitContainer1.Panel1.Controls.Add(textBox3);
            splitContainer1.Panel1.Controls.Add(label6);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(textBox5);
            splitContainer1.Panel2.Controls.Add(textBox6);
            splitContainer1.Panel2.Controls.Add(label8);
            splitContainer1.Panel2.Controls.Add(label9);
            splitContainer1.Size = new Size(767, 193);
            splitContainer1.SplitterDistance = 386;
            splitContainer1.TabIndex = 8;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(20, 55);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(244, 31);
            textBox1.TabIndex = 17;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 15);
            label4.Name = "label4";
            label4.Size = new Size(207, 25);
            label4.TabIndex = 16;
            label4.Text = "Username пользователя";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(20, 122);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(244, 31);
            textBox3.TabIndex = 21;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(20, 94);
            label6.Name = "label6";
            label6.Size = new Size(221, 25);
            label6.TabIndex = 20;
            label6.Text = "Информация о переводе";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(5, 122);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(244, 31);
            textBox5.TabIndex = 27;
            // 
            // textBox6
            // 
            textBox6.Location = new Point(5, 55);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(244, 31);
            textBox6.TabIndex = 25;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(5, 94);
            label8.Name = "label8";
            label8.Size = new Size(190, 25);
            label8.TabIndex = 26;
            label8.Text = "Пароль пользователя";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(3, 15);
            label9.Name = "label9";
            label9.Size = new Size(207, 25);
            label9.TabIndex = 24;
            label9.Text = "Username пользователя";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(12, 437);
            label10.Name = "label10";
            label10.Size = new Size(248, 25);
            label10.TabIndex = 9;
            label10.Text = "Создание отчета о переводе";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(407, 437);
            label11.Name = "label11";
            label11.Size = new Size(289, 25);
            label11.TabIndex = 10;
            label11.Text = "Создание профила пользователю";
            // 
            // BankEmployeePage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(809, 676);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(splitContainer1);
            Controls.Add(listBox3);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(listBox2);
            Controls.Add(label2);
            Controls.Add(listBox1);
            Controls.Add(label1);
            Name = "BankEmployeePage";
            Text = "BankEmployeePage";
            Load += BankEmployeePage_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ListBox listBox1;
        private ListBox listBox2;
        private Label label2;
        private Button button1;
        private Button button2;
        private ListBox listBox3;
        private Label label3;
        private SplitContainer splitContainer1;
        private TextBox textBox1;
        private Label label4;
        private TextBox textBox3;
        private Label label6;
        private TextBox textBox5;
        private TextBox textBox6;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
    }
}