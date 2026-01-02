using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Globalization;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private string secilenFotografYolu = null;
        private SQLiteConnection connection;
        private string dbFileName = "Kitaplar.sqlite";

        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.DataError += DataGridView1_DataError;
            
            this.Kitapad.TextChanged += new System.EventHandler(this.Kitapad_TextChanged);
        }

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false; 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = $"Data Source={dbFileName};Version=3;";
            if (!File.Exists(dbFileName))
            {
                SQLiteConnection.CreateFile(dbFileName);
            }

            connection = new SQLiteConnection(connectionString);
            try
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS kitaplar (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        kitap_adi TEXT NOT NULL,
                        yazar TEXT,
                        sayfa_sayisi INTEGER,
                        baslangic_tarihi TEXT,
                        bitis_tarihi TEXT,
                        ortalama_sayfa REAL,
                        kapak_fotografi_yolu TEXT
                    );";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
                LoadBooksFromDatabase(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void LoadBooksFromDatabase(string searchTerm = null)
        {
            try
            {
                string selectQuery = "SELECT id, kitap_adi, yazar, sayfa_sayisi, baslangic_tarihi, bitis_tarihi, ortalama_sayfa, kapak_fotografi_yolu FROM kitaplar";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    selectQuery += " WHERE kitap_adi LIKE @searchTerm";
                }

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        command.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                    }

                    DataTable dataTable = new DataTable();
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                    dataAdapter.Fill(dataTable);

                    dataGridView1.Columns.Clear();
                    dataGridView1.AutoGenerateColumns = false;

                    dataGridView1.Columns.Add("id", "ID");
                    dataGridView1.Columns["id"].DataPropertyName = "id";

                    dataGridView1.Columns.Add("kitap_adi", "Kitap Adı");
                    dataGridView1.Columns["kitap_adi"].DataPropertyName = "kitap_adi";

                    dataGridView1.Columns.Add("yazar", "Yazar");
                    dataGridView1.Columns["yazar"].DataPropertyName = "yazar";

                    dataGridView1.Columns.Add("sayfa_sayisi", "Sayfa Sayısı");
                    dataGridView1.Columns["sayfa_sayisi"].DataPropertyName = "sayfa_sayisi";

                    dataGridView1.Columns.Add("baslangic_tarihi", "Başlangıç Tarihi");
                    dataGridView1.Columns["baslangic_tarihi"].DataPropertyName = "baslangic_tarihi";

                    dataGridView1.Columns.Add("bitis_tarihi", "Bitiş Tarihi");
                    dataGridView1.Columns["bitis_tarihi"].DataPropertyName = "bitis_tarihi";

                    dataGridView1.Columns.Add("ortalama_sayfa", "Ortalama Sayfa");
                    dataGridView1.Columns["ortalama_sayfa"].DataPropertyName = "ortalama_sayfa";

                    dataGridView1.Columns.Add("kapak_fotografi_yolu", "Kapak Fotoğrafı Yolu");
                    dataGridView1.Columns["kapak_fotografi_yolu"].DataPropertyName = "kapak_fotografi_yolu";
                    dataGridView1.Columns["kapak_fotografi_yolu"].Visible = false;

                    DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                    imgCol.Name = "KapakFoto";
                    imgCol.HeaderText = "Kapak Fotoğrafı";
                    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    dataGridView1.Columns.Add(imgCol);

                    dataGridView1.DataSource = dataTable;
                    dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void Kitapad_TextChanged(object sender, EventArgs e)
        {
            LoadBooksFromDatabase(Kitapad.Text);
        }
        
        private double CalculateAveragePages(DateTime baslangicTarihi, DateTime? bitisTarihi, int sayfaSayisi)
        {
            if (!bitisTarihi.HasValue)
            {
                return 0; 
                }

            TimeSpan fark = bitisTarihi.Value - baslangicTarihi;

            int gunSayisi = (int)fark.TotalDays + 1;

            if (gunSayisi <= 0)
            {
                gunSayisi = 1;
            }
            return (double)sayfaSayisi / gunSayisi;
        }

        private void kitap_ekleme_Click(object sender, EventArgs e)
        {
            try
            {
                string kitapAdi = Kitapad.Text;
                string yazar = Yazar2.Text;
                string sayfaSayisiText = sayfa.Text;
                string baslangicTarihiText = bastarih.Text;
                string bitisTarihiText = bitarih.Text;

                if (string.IsNullOrWhiteSpace(kitapAdi) || string.IsNullOrWhiteSpace(sayfaSayisiText) || string.IsNullOrWhiteSpace(baslangicTarihiText))
                {
                    MessageBox.Show("Kitap Adı, Sayfa Sayısı ve Başlangıç Tarihi boş bırakılamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(sayfaSayisiText, out int sayfaSayisi))
                {
                    MessageBox.Show("Sayfa Sayısı sadece rakamlardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!DateTime.TryParse(baslangicTarihiText, out DateTime baslangicTarihi))
                {
                    MessageBox.Show("Başlangıç Tarihi geçerli bir formatta değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime? bitisTarihi = null;
                if (!string.IsNullOrWhiteSpace(bitisTarihiText))
                {
                    if (DateTime.TryParse(bitisTarihiText, out DateTime tempBitis))
                    {
                        bitisTarihi = tempBitis;
                    }
                }

                double ortalamaSayfa = CalculateAveragePages(baslangicTarihi, bitisTarihi, sayfaSayisi);

                string insertQuery = @"
                    INSERT INTO kitaplar (kitap_adi, yazar, sayfa_sayisi, baslangic_tarihi, bitis_tarihi, ortalama_sayfa, kapak_fotografi_yolu)
                    VALUES (@kitapAdi, @yazar, @sayfaSayisi, @baslangicTarihi, @bitisTarihi, @ortalamaSayfa, @kapakFotografiYolu);";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                    command.Parameters.AddWithValue("@yazar", yazar);
                    command.Parameters.AddWithValue("@sayfaSayisi", sayfaSayisi);
                    command.Parameters.AddWithValue("@baslangicTarihi", baslangicTarihi.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@bitisTarihi", bitisTarihi.HasValue ? bitisTarihi.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ortalamaSayfa", ortalamaSayfa);
                    command.Parameters.AddWithValue("@kapakFotografiYolu", string.IsNullOrEmpty(secilenFotografYolu) ? (object)DBNull.Value : secilenFotografYolu);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Kitap başarıyla eklendi!", "Başarılı");
                LoadBooksFromDatabase(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kitap_yükle_Click(object sender, EventArgs e)
        {
            LoadBooksFromDatabase();
        }

        private void Kitap_güncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellemek için bir kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int kitapId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                string kitapAdi = Kitapad.Text;
                string yazar = Yazar2.Text;

                if (!int.TryParse(sayfa.Text, out int sayfaSayisi))
                {
                    MessageBox.Show("Sayfa Sayısı sadece rakamlardan oluşmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!DateTime.TryParse(bastarih.Text, out DateTime baslangicTarihi))
                {
                    MessageBox.Show("Başlangıç Tarihi geçerli bir formatta değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime? bitisTarihi = null;
                if (!string.IsNullOrWhiteSpace(bitarih.Text))
                {
                    if (DateTime.TryParse(bitarih.Text, out DateTime tempBitis))
                    {
                        bitisTarihi = tempBitis;
                    }
                }

                double ortalamaSayfa = CalculateAveragePages(baslangicTarihi, bitisTarihi, sayfaSayisi);

                string updateQuery = @"
                    UPDATE kitaplar
                    SET kitap_adi = @kitapAdi, yazar = @yazar, sayfa_sayisi = @sayfaSayisi,
                        baslangic_tarihi = @baslangicTarihi, bitis_tarihi = @bitisTarihi,
                        ortalama_sayfa = @ortalamaSayfa
                    WHERE id = @kitapId;";

                using (var command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@kitapAdi", kitapAdi);
                    command.Parameters.AddWithValue("@yazar", yazar);
                    command.Parameters.AddWithValue("@sayfaSayisi", sayfaSayisi);
                    command.Parameters.AddWithValue("@baslangicTarihi", baslangicTarihi.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@bitisTarihi", bitisTarihi.HasValue ? bitisTarihi.Value.ToString("yyyy-MM-dd") : (object)DBNull.Value);
                    command.Parameters.AddWithValue("@ortalamaSayfa", ortalamaSayfa);
                    command.Parameters.AddWithValue("@kitapId", kitapId);
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Kitap başarıyla güncellendi!", "Başarılı");
                LoadBooksFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void RecalculateAllAverages()
        {
            try
            {
                string selectAllQuery = "SELECT id, sayfa_sayisi, baslangic_tarihi, bitis_tarihi FROM kitaplar";
                DataTable allBooksTable = new DataTable();
                using (var command = new SQLiteCommand(selectAllQuery, connection))
                {
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);
                    dataAdapter.Fill(allBooksTable);
                }

                using (var transaction = connection.BeginTransaction())
                {
                    string updateQuery = "UPDATE kitaplar SET ortalama_sayfa = @ortalamaSayfa WHERE id = @kitapId";
                    using (var command = new SQLiteCommand(updateQuery, connection, transaction))
                    {
                        foreach (DataRow row in allBooksTable.Rows)
                        {
                            int kitapId = Convert.ToInt32(row["id"]);
                            int sayfaSayisi = Convert.ToInt32(row["sayfa_sayisi"]);
                            DateTime baslangicTarihi = DateTime.Parse(row["baslangic_tarihi"].ToString());

                            DateTime? bitisTarihi = null;
                            if (row["bitis_tarihi"] != DBNull.Value)
                            {
                                bitisTarihi = DateTime.Parse(row["bitis_tarihi"].ToString());
                            }

                            double ortalamaSayfa = CalculateAveragePages(baslangicTarihi, bitisTarihi, sayfaSayisi);

                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@ortalamaSayfa", ortalamaSayfa);
                            command.Parameters.AddWithValue("@kitapId", kitapId);
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                MessageBox.Show("Tüm kitapların ortalama sayfa sayıları başarıyla yeniden hesaplandı!", "Başarılı");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tüm kitaplar güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kitap_fotograf_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tüm Dosyalar|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    secilenFotografYolu = openFileDialog.FileName;
                    pbKapakFotografi.ImageLocation = secilenFotografYolu;
                    pbKapakFotografi.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Resim yüklenirken bir hata oluştu: " + ex.Message);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == dataGridView1.Columns["KapakFoto"].Index)
            {
                int kitapId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tüm Dosyalar|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string yeniFotografYolu = openFileDialog.FileName;

                        string updateQuery = @"
                            UPDATE kitaplar
                            SET kapak_fotografi_yolu = @yeniYol
                            WHERE id = @kitapId;";

                        using (var command = new SQLiteCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@yeniYol", yeniFotografYolu);
                            command.Parameters.AddWithValue("@kitapId", kitapId);
                            command.ExecuteNonQuery();
                        }

                        MessageBox.Show("Kapak fotoğrafı başarıyla güncellendi!", "Başarılı");
                        LoadBooksFromDatabase();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resim güncellenirken bir hata oluştu: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                string kapakFotografiYolu = row.Cells["kapak_fotografi_yolu"].Value?.ToString();
                pbKapakFotografi.SizeMode = PictureBoxSizeMode.Zoom;

                if (!string.IsNullOrEmpty(kapakFotografiYolu) && File.Exists(kapakFotografiYolu))
                {
                    try
                    {
                        using (Image img = Image.FromFile(kapakFotografiYolu))
                        {
                            pbKapakFotografi.Image = new Bitmap(img);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Resim yüklenirken bir hata oluştu: " + ex.Message);
                        pbKapakFotografi.Image = null;
                    }
                }
                else
                {
                    pbKapakFotografi.Image = null;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns.Contains("KapakFoto") && e.ColumnIndex == dataGridView1.Columns["KapakFoto"].Index)
            {
                try
                {
                    string filePath = dataGridView1.Rows[e.RowIndex].Cells["kapak_fotografi_yolu"].Value?.ToString();

                    if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                    {
                        e.Value = null;
                        e.FormattingApplied = true;
                    }
                    else
                    {
                        using (Image img = Image.FromFile(filePath))
                        {
                            e.Value = new Bitmap(img);
                        }
                        e.FormattingApplied = true;
                    }
                }
                catch (Exception)
                {
                    e.Value = null;
                    e.FormattingApplied = true;
                }
            }
            if (dataGridView1.Columns.Contains("ortalama_sayfa") && e.ColumnIndex == dataGridView1.Columns["ortalama_sayfa"].Index)
            {
                if (e.Value != null && e.Value is double)
                {
                    e.Value = ((double)e.Value).ToString("N2", CultureInfo.CurrentCulture);
                    e.FormattingApplied = true;
                }
            }
        }

        private void Kitap_sil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int kitapId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                DialogResult dialogResult = MessageBox.Show(
                    "Seçili kitabı silmek istediğine emin misin?",
                    "Kitap Silme",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        using (var transaction = connection.BeginTransaction())
                        {
                            string deleteQuery = "DELETE FROM kitaplar WHERE id = @kitapId";
                            using (var command = new SQLiteCommand(deleteQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@kitapId", kitapId);
                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    transaction.Commit();
                                    MessageBox.Show("Kitap veritabanından silindi.");
                                }
                                else
                                {
                                    transaction.Rollback();
                                    MessageBox.Show("Silme başarısız: id bulunamadı.");
                                }
                            }
                        }
                        LoadBooksFromDatabase(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Kitap silinirken hata: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğin kitabı seç.");
            }
        }

        private void Kitapara_Click(object sender, EventArgs e)
        {
            LoadBooksFromDatabase(Kitapad.Text);
        }
        
        private void btnTümunuHesapla_Click(object sender, EventArgs e)
        {
            RecalculateAllAverages();
            LoadBooksFromDatabase();
        }

        private void KitapSayfaHesap_Click(object sender, EventArgs e)
        {
            RecalculateAllAverages();
            LoadBooksFromDatabase();
        }
    }
}
