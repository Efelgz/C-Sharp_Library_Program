namespace WindowsFormsApp2
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
            this.kitap_ekleme = new DevExpress.XtraEditors.SimpleButton();
            this.Kitap_sil = new DevExpress.XtraEditors.SimpleButton();
            this.Kitap_yükle = new DevExpress.XtraEditors.SimpleButton();
            this.Kitap_fotograf = new DevExpress.XtraEditors.SimpleButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Kitapad = new DevExpress.XtraEditors.TextEdit();
            this.ıd = new DevExpress.XtraEditors.TextEdit();
            this.Yazar2 = new DevExpress.XtraEditors.TextEdit();
            this.bitarih = new DevExpress.XtraEditors.TextEdit();
            this.bastarih = new DevExpress.XtraEditors.TextEdit();
            this.sayfa = new DevExpress.XtraEditors.TextEdit();
            this.pbKapakFotografi = new System.Windows.Forms.PictureBox();
            this.KitapSayfaHesap = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kitapad.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ıd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Yazar2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bastarih.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sayfa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKapakFotografi)).BeginInit();
            this.SuspendLayout();
            // 
            // kitap_ekleme
            // 
            this.kitap_ekleme.Location = new System.Drawing.Point(13, 13);
            this.kitap_ekleme.Name = "kitap_ekleme";
            this.kitap_ekleme.Size = new System.Drawing.Size(161, 51);
            this.kitap_ekleme.TabIndex = 0;
            this.kitap_ekleme.Text = "Kitap ekle";
            this.kitap_ekleme.Click += new System.EventHandler(this.kitap_ekleme_Click);
            // 
            // Kitap_sil
            // 
            this.Kitap_sil.Location = new System.Drawing.Point(12, 70);
            this.Kitap_sil.Name = "Kitap_sil";
            this.Kitap_sil.Size = new System.Drawing.Size(161, 51);
            this.Kitap_sil.TabIndex = 3;
            this.Kitap_sil.Text = "Kitap sil";
            this.Kitap_sil.Click += new System.EventHandler(this.Kitap_sil_Click);
            // 
            // Kitap_yükle
            // 
            this.Kitap_yükle.Location = new System.Drawing.Point(12, 127);
            this.Kitap_yükle.Name = "Kitap_yükle";
            this.Kitap_yükle.Size = new System.Drawing.Size(161, 51);
            this.Kitap_yükle.TabIndex = 5;
            this.Kitap_yükle.Text = "Kitapları yükle";
            this.Kitap_yükle.Click += new System.EventHandler(this.Kitap_yükle_Click);
            // 
            // Kitap_fotograf
            // 
            this.Kitap_fotograf.Location = new System.Drawing.Point(12, 184);
            this.Kitap_fotograf.Name = "Kitap_fotograf";
            this.Kitap_fotograf.Size = new System.Drawing.Size(161, 51);
            this.Kitap_fotograf.TabIndex = 6;
            this.Kitap_fotograf.Text = "Kitap fotoğrafı ekle";
            this.Kitap_fotograf.Click += new System.EventHandler(this.Kitap_fotograf_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(179, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(825, 498);
            this.dataGridView1.TabIndex = 7;
            // 
            // Kitapad
            // 
            this.Kitapad.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Kitapad.Location = new System.Drawing.Point(290, 516);
            this.Kitapad.Name = "Kitapad";
            this.Kitapad.Size = new System.Drawing.Size(105, 20);
            this.Kitapad.TabIndex = 8;
            // 
            // ıd
            // 
            this.ıd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.ıd.Location = new System.Drawing.Point(179, 516);
            this.ıd.Name = "ıd";
            this.ıd.Size = new System.Drawing.Size(105, 20);
            this.ıd.TabIndex = 9;
            // 
            // Yazar2
            // 
            this.Yazar2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Yazar2.Location = new System.Drawing.Point(401, 516);
            this.Yazar2.Name = "Yazar2";
            this.Yazar2.Size = new System.Drawing.Size(105, 20);
            this.Yazar2.TabIndex = 11;
            // 
            // bitarih
            // 
            this.bitarih.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bitarih.Location = new System.Drawing.Point(734, 516);
            this.bitarih.Name = "bitarih";
            this.bitarih.Size = new System.Drawing.Size(105, 20);
            this.bitarih.TabIndex = 14;
            // 
            // bastarih
            // 
            this.bastarih.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.bastarih.Location = new System.Drawing.Point(623, 516);
            this.bastarih.Name = "bastarih";
            this.bastarih.Size = new System.Drawing.Size(105, 20);
            this.bastarih.TabIndex = 15;
            // 
            // sayfa
            // 
            this.sayfa.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.sayfa.Location = new System.Drawing.Point(512, 516);
            this.sayfa.Name = "sayfa";
            this.sayfa.Size = new System.Drawing.Size(105, 20);
            this.sayfa.TabIndex = 16;
            // 
            // pbKapakFotografi
            // 
            this.pbKapakFotografi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbKapakFotografi.Location = new System.Drawing.Point(1027, 13);
            this.pbKapakFotografi.Name = "pbKapakFotografi";
            this.pbKapakFotografi.Size = new System.Drawing.Size(272, 425);
            this.pbKapakFotografi.TabIndex = 17;
            this.pbKapakFotografi.TabStop = false;
            // 
            // KitapSayfaHesap
            // 
            this.KitapSayfaHesap.Location = new System.Drawing.Point(13, 241);
            this.KitapSayfaHesap.Name = "KitapSayfaHesap";
            this.KitapSayfaHesap.Size = new System.Drawing.Size(161, 51);
            this.KitapSayfaHesap.TabIndex = 18;
            this.KitapSayfaHesap.Text = "Sayfaları tekrardan hesapla";
            this.KitapSayfaHesap.Click += new System.EventHandler(this.KitapSayfaHesap_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1427, 617);
            this.Controls.Add(this.KitapSayfaHesap);
            this.Controls.Add(this.pbKapakFotografi);
            this.Controls.Add(this.sayfa);
            this.Controls.Add(this.bastarih);
            this.Controls.Add(this.bitarih);
            this.Controls.Add(this.Yazar2);
            this.Controls.Add(this.ıd);
            this.Controls.Add(this.Kitapad);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.Kitap_fotograf);
            this.Controls.Add(this.Kitap_yükle);
            this.Controls.Add(this.Kitap_sil);
            this.Controls.Add(this.kitap_ekleme);
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Kitapad.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ıd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Yazar2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bitarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bastarih.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sayfa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbKapakFotografi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton kitap_ekleme;
        private DevExpress.XtraEditors.SimpleButton Kitap_sil;
        private DevExpress.XtraEditors.SimpleButton Kitap_yükle;
        private DevExpress.XtraEditors.SimpleButton Kitap_fotograf;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraEditors.TextEdit Kitapad;
        private DevExpress.XtraEditors.TextEdit ıd;
        private DevExpress.XtraEditors.TextEdit Yazar2;
        private DevExpress.XtraEditors.TextEdit bitarih;
        private DevExpress.XtraEditors.TextEdit bastarih;
        private DevExpress.XtraEditors.TextEdit sayfa;
        private System.Windows.Forms.PictureBox pbKapakFotografi;
        private DevExpress.XtraEditors.SimpleButton KitapSayfaHesap;
    }
}

