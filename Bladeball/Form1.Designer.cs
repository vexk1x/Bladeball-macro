namespace Bladeball
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
            button1 = new Button();
            main = new Panel();
            ExitButton = new Button();
            textBox1 = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            StartKey = new Button();
            BlockKey2 = new Button();
            BlockKey1 = new Button();
            label6 = new Label();
            main.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // main
            // 
            main.BackColor = Color.FromArgb(17, 19, 24);
            main.Controls.Add(label6);
            main.Controls.Add(ExitButton);
            main.Controls.Add(textBox1);
            main.Controls.Add(label5);
            main.Controls.Add(label4);
            main.Controls.Add(label3);
            main.Controls.Add(label2);
            main.Controls.Add(label1);
            main.Controls.Add(StartKey);
            main.Controls.Add(BlockKey2);
            main.Controls.Add(BlockKey1);
            main.Dock = DockStyle.Fill;
            main.Location = new Point(0, 0);
            main.Name = "main";
            main.Size = new Size(484, 245);
            main.TabIndex = 0;
            // 
            // ExitButton
            // 
            ExitButton.Location = new Point(367, 184);
            ExitButton.Name = "ExitButton";
            ExitButton.Size = new Size(75, 23);
            ExitButton.TabIndex = 1;
            ExitButton.UseVisualStyleBackColor = true;
            ExitButton.Click += ExitButton_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(34, 38, 48);
            textBox1.ForeColor = Color.White;
            textBox1.Location = new Point(323, 42);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(149, 23);
            textBox1.TabIndex = 9;
            textBox1.TextChanged += CpsChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = SystemColors.AppWorkspace;
            label5.Location = new Point(283, 46);
            label5.Name = "label5";
            label5.Size = new Size(34, 15);
            label5.TabIndex = 8;
            label5.Text = "CPS: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.ForeColor = SystemColors.AppWorkspace;
            label4.Location = new Point(134, 184);
            label4.Name = "label4";
            label4.Size = new Size(96, 15);
            label4.TabIndex = 7;
            label4.Text = "(Start / Stop Key)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.ForeColor = SystemColors.AppWorkspace;
            label3.Location = new Point(134, 99);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 6;
            label3.Text = "(Block Key 2)";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.ForeColor = SystemColors.AppWorkspace;
            label2.Location = new Point(134, 46);
            label2.Name = "label2";
            label2.Size = new Size(75, 15);
            label2.TabIndex = 5;
            label2.Text = "(Block Key 1)";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ButtonFace;
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(123, 15);
            label1.TabIndex = 4;
            label1.Text = "Hotkeys (ESC to reset)";
            // 
            // StartKey
            // 
            StartKey.Location = new Point(26, 180);
            StartKey.Name = "StartKey";
            StartKey.Size = new Size(75, 23);
            StartKey.TabIndex = 3;
            StartKey.Text = "button2";
            StartKey.UseVisualStyleBackColor = true;
            StartKey.Click += StartKey_Click;
            // 
            // BlockKey2
            // 
            BlockKey2.Location = new Point(26, 95);
            BlockKey2.Name = "BlockKey2";
            BlockKey2.Size = new Size(75, 23);
            BlockKey2.TabIndex = 2;
            BlockKey2.Text = "button2";
            BlockKey2.UseVisualStyleBackColor = true;
            BlockKey2.Click += BlockKey2_Click;
            // 
            // BlockKey1
            // 
            BlockKey1.Location = new Point(26, 42);
            BlockKey1.Name = "BlockKey1";
            BlockKey1.Size = new Size(75, 23);
            BlockKey1.TabIndex = 1;
            BlockKey1.Text = "button2";
            BlockKey1.UseVisualStyleBackColor = true;
            BlockKey1.Click += BlockKey1_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = SystemColors.ButtonFace;
            label6.Location = new Point(323, 9);
            label6.Name = "label6";
            label6.Size = new Size(126, 15);
            label6.TabIndex = 1;
            label6.Text = "BladeBall Macro V1.1.0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 245);
            Controls.Add(main);
            Name = "Form1";
            Text = "Form1";
            main.ResumeLayout(false);
            main.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Panel main;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button StartKey;
        private Button BlockKey2;
        private Button BlockKey1;
        private TextBox textBox1;
        private Label label5;
        private Button ExitButton;
        private Label label6;
    }
}
