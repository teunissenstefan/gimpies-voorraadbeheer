namespace Gimpies
{
    partial class WerknemerForm
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gebruikerTextbox = new System.Windows.Forms.TextBox();
            this.annuleerBtn = new System.Windows.Forms.Button();
            this.toevoegBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.wachtwoordTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rang:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Gebruikersnaam: ";
            // 
            // gebruikerTextbox
            // 
            this.gebruikerTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gebruikerTextbox.Location = new System.Drawing.Point(108, 39);
            this.gebruikerTextbox.Name = "gebruikerTextbox";
            this.gebruikerTextbox.Size = new System.Drawing.Size(350, 20);
            this.gebruikerTextbox.TabIndex = 2;
            // 
            // annuleerBtn
            // 
            this.annuleerBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.annuleerBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.annuleerBtn.Location = new System.Drawing.Point(12, 95);
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
            this.toevoegBtn.Location = new System.Drawing.Point(383, 95);
            this.toevoegBtn.Name = "toevoegBtn";
            this.toevoegBtn.Size = new System.Drawing.Size(75, 23);
            this.toevoegBtn.TabIndex = 4;
            this.toevoegBtn.Text = "Registreren";
            this.toevoegBtn.UseVisualStyleBackColor = true;
            this.toevoegBtn.Click += new System.EventHandler(this.toevoegBtn_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(108, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(350, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // wachtwoordTextbox
            // 
            this.wachtwoordTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wachtwoordTextbox.Location = new System.Drawing.Point(108, 65);
            this.wachtwoordTextbox.Name = "wachtwoordTextbox";
            this.wachtwoordTextbox.Size = new System.Drawing.Size(350, 20);
            this.wachtwoordTextbox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 92;
            this.label3.Text = "Wachtwoord:";
            // 
            // WerknemerForm
            // 
            this.AcceptButton = this.toevoegBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.annuleerBtn;
            this.ClientSize = new System.Drawing.Size(470, 130);
            this.Controls.Add(this.wachtwoordTextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.toevoegBtn);
            this.Controls.Add(this.annuleerBtn);
            this.Controls.Add(this.gebruikerTextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "WerknemerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Werknemer";
            this.Load += new System.EventHandler(this.ArtikelForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gebruikerTextbox;
        private System.Windows.Forms.Button annuleerBtn;
        private System.Windows.Forms.Button toevoegBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TextBox wachtwoordTextbox;
        private System.Windows.Forms.Label label3;
    }
}