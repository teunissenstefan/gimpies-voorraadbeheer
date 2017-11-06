namespace Gimpies
{
    partial class Login
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
            this.CancelBtn = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.gebruikersnaamTxtBox = new System.Windows.Forms.TextBox();
            this.wachtwoordTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logo = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelBtn
            // 
            this.CancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(12, 222);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 100;
            this.CancelBtn.Text = "Afsluiten";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.LoginBtn.Location = new System.Drawing.Point(197, 222);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(75, 23);
            this.LoginBtn.TabIndex = 2;
            this.LoginBtn.Text = "Inloggen";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // gebruikersnaamTxtBox
            // 
            this.gebruikersnaamTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gebruikersnaamTxtBox.Location = new System.Drawing.Point(105, 170);
            this.gebruikersnaamTxtBox.Name = "gebruikersnaamTxtBox";
            this.gebruikersnaamTxtBox.Size = new System.Drawing.Size(167, 20);
            this.gebruikersnaamTxtBox.TabIndex = 0;
            this.gebruikersnaamTxtBox.TextChanged += new System.EventHandler(this.gebruikersnaamTxtBox_TextChanged);
            // 
            // wachtwoordTxtBox
            // 
            this.wachtwoordTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wachtwoordTxtBox.Location = new System.Drawing.Point(105, 196);
            this.wachtwoordTxtBox.Name = "wachtwoordTxtBox";
            this.wachtwoordTxtBox.PasswordChar = '*';
            this.wachtwoordTxtBox.Size = new System.Drawing.Size(167, 20);
            this.wachtwoordTxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 0;
            // 
            // logo
            // 
            this.logo.Location = new System.Drawing.Point(15, 12);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(257, 152);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logo.TabIndex = 101;
            this.logo.TabStop = false;
            this.logo.Click += new System.EventHandler(this.logo_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 102;
            this.label2.Text = "Gebruikersnaam:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 199);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 103;
            this.label3.Text = "Wachtwoord:";
            // 
            // Login
            // 
            this.AcceptButton = this.LoginBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(284, 256);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.wachtwoordTxtBox);
            this.Controls.Add(this.gebruikersnaamTxtBox);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.CancelBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.TextBox gebruikersnaamTxtBox;
        private System.Windows.Forms.TextBox wachtwoordTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}