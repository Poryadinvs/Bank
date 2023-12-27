namespace User
{
    partial class UserPage
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
            textBox1 = new TextBox();
            button1 = new Button();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 105);
            label1.Name = "label1";
            label1.Size = new Size(177, 25);
            label1.TabIndex = 0;
            label1.Text = "Сумма отправления";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(195, 105);
            label2.Name = "label2";
            label2.Size = new Size(188, 25);
            label2.TabIndex = 1;
            label2.Text = "Username получателя";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 157);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(156, 31);
            textBox1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(12, 213);
            button1.Name = "button1";
            button1.Size = new Size(371, 31);
            button1.TabIndex = 3;
            button1.Text = "Отправить денег";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(219, 157);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(164, 31);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 71);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(371, 31);
            textBox3.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 23);
            label3.Name = "label3";
            label3.Size = new Size(67, 25);
            label3.TabIndex = 8;
            label3.Text = "Баланс";
            // 
            // UserPage
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 278);
            Controls.Add(label3);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "UserPage";
            Text = "UserPage";
            Load += UserPage_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private Button button1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label3;
    }
}