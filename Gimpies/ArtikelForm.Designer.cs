namespace Gimpies
{
    partial class ArtikelForm
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
            this.beschrijvingTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.aantalTextbox = new System.Windows.Forms.TextBox();
            this.prijsTextbox = new System.Windows.Forms.TextBox();
            this.maatTextbox = new System.Windows.Forms.TextBox();
            this.annuleerBtn = new System.Windows.Forms.Button();
            this.toevoegBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Beschrijving:";
            // 
            // beschrijvingTextbox
            // 
            this.beschrijvingTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.beschrijvingTextbox.Location = new System.Drawing.Point(85, 12);
            this.beschrijvingTextbox.Name = "beschrijvingTextbox";
            this.beschrijvingTextbox.Size = new System.Drawing.Size(554, 20);
            this.beschrijvingTextbox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Aantal:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Prijs:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Maat:";
            // 
            // aantalTextbox
            // 
            this.aantalTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.aantalTextbox.Location = new System.Drawing.Point(85, 38);
            this.aantalTextbox.Name = "aantalTextbox";
            this.aantalTextbox.Size = new System.Drawing.Size(554, 20);
            this.aantalTextbox.TabIndex = 5;
            // 
            // prijsTextbox
            // 
            this.prijsTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prijsTextbox.Location = new System.Drawing.Point(85, 64);
            this.prijsTextbox.Name = "prijsTextbox";
            this.prijsTextbox.Size = new System.Drawing.Size(554, 20);
            this.prijsTextbox.TabIndex = 6;
            // 
            // maatTextbox
            // 
            this.maatTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.maatTextbox.Location = new System.Drawing.Point(85, 90);
            this.maatTextbox.Name = "maatTextbox";
            this.maatTextbox.Size = new System.Drawing.Size(554, 20);
            this.maatTextbox.TabIndex = 7;
            // 
            // annuleerBtn
            // 
            this.annuleerBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.annuleerBtn.Location = new System.Drawing.Point(12, 116);
            this.annuleerBtn.Name = "annuleerBtn";
            this.annuleerBtn.Size = new System.Drawing.Size(75, 23);
            this.annuleerBtn.TabIndex = 10;
            this.annuleerBtn.Text = "Annuleren";
            this.annuleerBtn.UseVisualStyleBackColor = true;
            this.annuleerBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // toevoegBtn
            // 
            this.toevoegBtn.Location = new System.Drawing.Point(564, 116);
            this.toevoegBtn.Name = "toevoegBtn";
            this.toevoegBtn.Size = new System.Drawing.Size(75, 23);
            this.toevoegBtn.TabIndex = 9;
            this.toevoegBtn.Text = "Toevoegen";
            this.toevoegBtn.UseVisualStyleBackColor = true;
            this.toevoegBtn.Click += new System.EventHandler(this.toevoegBtn_Click);
            // 
            // ArtikelForm
            // 
            this.AcceptButton = this.toevoegBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.annuleerBtn;
            this.ClientSize = new System.Drawing.Size(651, 151);
            this.Controls.Add(this.toevoegBtn);
            this.Controls.Add(this.annuleerBtn);
            this.Controls.Add(this.maatTextbox);
            this.Controls.Add(this.prijsTextbox);
            this.Controls.Add(this.aantalTextbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.beschrijvingTextbox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ArtikelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Artikel";
            this.Load += new System.EventHandler(this.ArtikelForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox beschrijvingTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox aantalTextbox;
        private System.Windows.Forms.TextBox prijsTextbox;
        private System.Windows.Forms.TextBox maatTextbox;
        private System.Windows.Forms.Button annuleerBtn;
        private System.Windows.Forms.Button toevoegBtn;
    }
}