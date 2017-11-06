namespace Gimpies
{
    partial class VerkoopRegistreren
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.aantalTextbox = new System.Windows.Forms.TextBox();
            this.annuleerBtn = new System.Windows.Forms.Button();
            this.toevoegBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.idComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Artikel:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Aantal:";
            // 
            // aantalTextbox
            // 
            this.aantalTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aantalTextbox.Location = new System.Drawing.Point(58, 39);
            this.aantalTextbox.Name = "aantalTextbox";
            this.aantalTextbox.Size = new System.Drawing.Size(400, 20);
            this.aantalTextbox.TabIndex = 7;
            // 
            // annuleerBtn
            // 
            this.annuleerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.annuleerBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.annuleerBtn.Location = new System.Drawing.Point(12, 67);
            this.annuleerBtn.Name = "annuleerBtn";
            this.annuleerBtn.Size = new System.Drawing.Size(75, 23);
            this.annuleerBtn.TabIndex = 90;
            this.annuleerBtn.Text = "Annuleren";
            this.annuleerBtn.UseVisualStyleBackColor = true;
            this.annuleerBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // toevoegBtn
            // 
            this.toevoegBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.toevoegBtn.Location = new System.Drawing.Point(383, 67);
            this.toevoegBtn.Name = "toevoegBtn";
            this.toevoegBtn.Size = new System.Drawing.Size(75, 23);
            this.toevoegBtn.TabIndex = 8;
            this.toevoegBtn.Text = "Registreren";
            this.toevoegBtn.UseVisualStyleBackColor = true;
            this.toevoegBtn.Click += new System.EventHandler(this.toevoegBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(184, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(274, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // idComboBox
            // 
            this.idComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.idComboBox.FormattingEnabled = true;
            this.idComboBox.Location = new System.Drawing.Point(57, 12);
            this.idComboBox.Name = "idComboBox";
            this.idComboBox.Size = new System.Drawing.Size(121, 21);
            this.idComboBox.TabIndex = 91;
            this.idComboBox.SelectedIndexChanged += new System.EventHandler(this.idComboBox_SelectedIndexChanged);
            // 
            // VerkoopRegistreren
            // 
            this.AcceptButton = this.toevoegBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.annuleerBtn;
            this.ClientSize = new System.Drawing.Size(470, 102);
            this.Controls.Add(this.idComboBox);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.toevoegBtn);
            this.Controls.Add(this.annuleerBtn);
            this.Controls.Add(this.aantalTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "VerkoopRegistreren";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verkoop";
            this.Load += new System.EventHandler(this.ArtikelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox aantalTextbox;
        private System.Windows.Forms.Button annuleerBtn;
        private System.Windows.Forms.Button toevoegBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox idComboBox;
    }
}