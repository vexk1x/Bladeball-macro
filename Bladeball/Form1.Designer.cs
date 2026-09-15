namespace Bladeball
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
            this.main = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.StartKey = new System.Windows.Forms.Button();
            this.BlockKey2 = new System.Windows.Forms.Button();
            this.BlockKey1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.main.SuspendLayout();
            this.SuspendLayout();
            // 
            // main
            // 
            this.main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(19)))), ((int)(((byte)(24)))));
            this.main.Controls.Add(this.label6);
            this.main.Controls.Add(this.label3);
            this.main.Controls.Add(this.label5);
            this.main.Controls.Add(this.label4);
            this.main.Controls.Add(this.label2);
            this.main.Controls.Add(this.label1);
            this.main.Controls.Add(this.textBox1);
            this.main.Controls.Add(this.ExitButton);
            this.main.Controls.Add(this.StartKey);
            this.main.Controls.Add(this.BlockKey2);
            this.main.Controls.Add(this.BlockKey1);
            this.main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.main.Location = new System.Drawing.Point(0, 0);
            this.main.Name = "main";
            this.main.Size = new System.Drawing.Size(470, 225);
            this.main.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(271, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "CPS: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Hotkeys: (ESC to reset)";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(48)))));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(311, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(147, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.CpsChanged);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(358, 168);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 3;
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // StartKey
            // 
            this.StartKey.Location = new System.Drawing.Point(28, 168);
            this.StartKey.Name = "StartKey";
            this.StartKey.Size = new System.Drawing.Size(75, 23);
            this.StartKey.TabIndex = 2;
            this.StartKey.UseVisualStyleBackColor = true;
            this.StartKey.Click += new System.EventHandler(this.StartKey_Click);
            // 
            // BlockKey2
            // 
            this.BlockKey2.Location = new System.Drawing.Point(28, 77);
            this.BlockKey2.Name = "BlockKey2";
            this.BlockKey2.Size = new System.Drawing.Size(75, 23);
            this.BlockKey2.TabIndex = 1;
            this.BlockKey2.UseVisualStyleBackColor = true;
            this.BlockKey2.Click += new System.EventHandler(this.BlockKey2_Click);
            // 
            // BlockKey1
            // 
            this.BlockKey1.Location = new System.Drawing.Point(28, 32);
            this.BlockKey1.Name = "BlockKey1";
            this.BlockKey1.Size = new System.Drawing.Size(75, 23);
            this.BlockKey1.TabIndex = 0;
            this.BlockKey1.UseVisualStyleBackColor = true;
            this.BlockKey1.Click += new System.EventHandler(this.BlockKey1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(134, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "(Block Hotkey 2)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(134, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "(Start/Stop Hotkey)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(134, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "(Block Hotkey 1)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(332, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(117, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Bladeball Macro V1.1.1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 225);
            this.Controls.Add(this.main);
            this.Name = "Form1";
            this.Text = "Form1";
            this.main.ResumeLayout(false);
            this.main.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel main;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button StartKey;
        private System.Windows.Forms.Button BlockKey2;
        private System.Windows.Forms.Button BlockKey1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
    }
}

