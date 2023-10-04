namespace OS2_Projekt_Tvrdy
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
            this.components = new System.ComponentModel.Container();
            this.btnOdaberi = new System.Windows.Forms.Button();
            this.btnKriptiraj = new System.Windows.Forms.Button();
            this.btnDekriptiraj = new System.Windows.Forms.Button();
            this.btnPotpisi = new System.Windows.Forms.Button();
            this.btnProvjeriPotpis = new System.Windows.Forms.Button();
            this.cbAlgoritam = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SuspendLayout();
            // 
            // btnOdaberi
            // 
            this.btnOdaberi.Location = new System.Drawing.Point(301, 48);
            this.btnOdaberi.Name = "btnOdaberi";
            this.btnOdaberi.Size = new System.Drawing.Size(113, 29);
            this.btnOdaberi.TabIndex = 0;
            this.btnOdaberi.Text = "Odaberi";
            this.btnOdaberi.UseVisualStyleBackColor = true;
            this.btnOdaberi.Click += new System.EventHandler(this.btnOdaberi_Click);
            // 
            // btnKriptiraj
            // 
            this.btnKriptiraj.Location = new System.Drawing.Point(30, 111);
            this.btnKriptiraj.Name = "btnKriptiraj";
            this.btnKriptiraj.Size = new System.Drawing.Size(113, 29);
            this.btnKriptiraj.TabIndex = 1;
            this.btnKriptiraj.Text = "Kriptiraj";
            this.btnKriptiraj.UseVisualStyleBackColor = true;
            this.btnKriptiraj.Click += new System.EventHandler(this.btnKriptiraj_Click);
            // 
            // btnDekriptiraj
            // 
            this.btnDekriptiraj.Location = new System.Drawing.Point(165, 111);
            this.btnDekriptiraj.Name = "btnDekriptiraj";
            this.btnDekriptiraj.Size = new System.Drawing.Size(113, 29);
            this.btnDekriptiraj.TabIndex = 2;
            this.btnDekriptiraj.Text = "Dekriptiraj";
            this.btnDekriptiraj.UseVisualStyleBackColor = true;
            this.btnDekriptiraj.Click += new System.EventHandler(this.btnDekriptiraj_Click);
            // 
            // btnPotpisi
            // 
            this.btnPotpisi.Location = new System.Drawing.Point(30, 160);
            this.btnPotpisi.Name = "btnPotpisi";
            this.btnPotpisi.Size = new System.Drawing.Size(113, 29);
            this.btnPotpisi.TabIndex = 3;
            this.btnPotpisi.Text = "Potpiši";
            this.btnPotpisi.UseVisualStyleBackColor = true;
            this.btnPotpisi.Click += new System.EventHandler(this.btnPotpisi_Click);
            // 
            // btnProvjeriPotpis
            // 
            this.btnProvjeriPotpis.Location = new System.Drawing.Point(165, 160);
            this.btnProvjeriPotpis.Name = "btnProvjeriPotpis";
            this.btnProvjeriPotpis.Size = new System.Drawing.Size(113, 29);
            this.btnProvjeriPotpis.TabIndex = 4;
            this.btnProvjeriPotpis.Text = "Provjeri potpis";
            this.btnProvjeriPotpis.UseVisualStyleBackColor = true;
            this.btnProvjeriPotpis.Click += new System.EventHandler(this.btnProvjeriPotpis_Click);
            // 
            // cbAlgoritam
            // 
            this.cbAlgoritam.FormattingEnabled = true;
            this.cbAlgoritam.Location = new System.Drawing.Point(30, 48);
            this.cbAlgoritam.Name = "cbAlgoritam";
            this.cbAlgoritam.Size = new System.Drawing.Size(248, 28);
            this.cbAlgoritam.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(238, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Odaberite algoritam za kriptiranje:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 234);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAlgoritam);
            this.Controls.Add(this.btnProvjeriPotpis);
            this.Controls.Add(this.btnPotpisi);
            this.Controls.Add(this.btnDekriptiraj);
            this.Controls.Add(this.btnKriptiraj);
            this.Controls.Add(this.btnOdaberi);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnOdaberi;
        private Button btnKriptiraj;
        private Button btnDekriptiraj;
        private Button btnPotpisi;
        private Button btnProvjeriPotpis;
        private ComboBox cbAlgoritam;
        private Label label1;
        private ContextMenuStrip contextMenuStrip1;
    }
}