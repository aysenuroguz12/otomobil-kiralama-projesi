using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
   

    public partial class Form4 : Form
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            // Form1 / Form3 ile AYNI secret olmalı - önceki kod kırpılmıştı
            AuthSecret = "7py1re49571VUbXGqPbrsyU1EWGIQFaLC9L8KZM0",
            BasePath = "https://otokiralama1-8a758-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        // Form3'ten gelen kiralama bilgileri
        private readonly string _marka;
        private readonly string _model;
        private readonly int _gun;
        private readonly int _fiyat;

        // Designer'ın kullanması için parametresiz constructor (tasarım zamanı)
        public Form4()
        {
            InitializeComponent();
        }

        // Form3'ten gerçek verilerle çağrılan constructor
        public Form4(string marka, string model, int gun, int fiyat) : this()
        {
            _marka = marka;
            _model = model;
            _gun = gun;
            _fiyat = fiyat;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firebase istemcisi başlatılırken hata oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Gelen bilgileri ekrana yansıt (kendi label/textbox adlarınla değiştir)
            var markaLabel = this.Controls.Find("labelMarka", true).FirstOrDefault() as Label;
            var modelLabel = this.Controls.Find("labelModel", true).FirstOrDefault() as Label;
            var gunLabel = this.Controls.Find("labelGun", true).FirstOrDefault() as Label;
            var fiyatBox = this.Controls.Find("textBox4", true).FirstOrDefault() as TextBox;

            if (markaLabel != null) markaLabel.Text = _marka;
            if (modelLabel != null) modelLabel.Text = _model;
            if (gunLabel != null) gunLabel.Text = _gun.ToString();
            if (fiyatBox != null)
            {
                fiyatBox.Text = _fiyat.ToString();
                fiyatBox.ReadOnly = true; // fiyatın kazayla değiştirilmesini engelle
            }
        }

        // Artık reflection ile ekrandan okumuyor, constructor'dan gelen değeri kullanıyor
        public int Fiyat => _fiyat;

        private Class3 VerileriTopla()
        {
            string musteriAd = GetText("txtMusteriAd");
            string musteriSoyad = GetText("txtMusteriSoyad");
            string musteriTelefon = GetText("txtMusteriTelefon");
            string musteriEposta = GetText("txtMusteriEposta");
            string musteriAdres = GetText("txtMusteriAdres");

            string kartAd = GetText("txtKartAd");
            string kartSoyad = GetText("txtKartSoyad");
            string kartNo = GetText("txtKartNumarasi");
            string cvv = GetText("txtCVV");
            string sonKullanma = GetText("txtSonKullanma"); // varsa gerçek alandan oku

            return new Class3
            {
                Musteri_Ad = musteriAd,
                Musteri_Soyad = musteriSoyad,
                Musteri_Telefon = musteriTelefon,
                Musteri_Eposta = musteriEposta,
                Musteri_Adres = musteriAdres,
                Kart_Ad = kartAd,
                Kart_Soyad = kartSoyad,
                Kart_Numarasi = kartNo,
                Kart_CVV = cvv,
                Kart_SonKullanma = string.IsNullOrEmpty(sonKullanma) ? "" : sonKullanma,
                Odeme_Tutari = _fiyat,
                Arac_Marka = _marka,
                Arac_Model = _model,
                Kiralanan_Gun = _gun
            };
        }

        private string GetText(string controlName)
        {
            return this.Controls.Find(controlName, true).FirstOrDefault() is TextBox t ? t.Text.Trim() : "";
        }

        private bool VeriGecerliMi(Class3 data)
        {
            if (string.IsNullOrEmpty(data.Musteri_Ad) || string.IsNullOrEmpty(data.Musteri_Soyad))
            {
                MessageBox.Show("Lütfen müşteri ad ve soyadını giriniz.", "Eksik Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(data.Musteri_Eposta) || !data.Musteri_Eposta.Contains("@"))
            {
                MessageBox.Show("Lütfen geçerli bir e-posta adresi giriniz.", "Eksik Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(data.Kart_Numarasi) || data.Kart_Numarasi.Length < 15 || data.Kart_Numarasi.Length > 16)
            {
                MessageBox.Show("Lütfen geçerli bir kart numarası giriniz.", "Eksik Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(data.Kart_CVV) || data.Kart_CVV.Length < 3)
            {
                MessageBox.Show("Lütfen geçerli bir CVV giriniz.", "Eksik Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private async void btnOdemeYap_Click(object sender, EventArgs e)
        {
            var data = VerileriTopla();

            if (client == null)
            {
                MessageBox.Show("Firebase bağlantısı yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!VeriGecerliMi(data)) return;

            try
            {
                PushResponse response = await client.PushAsync("BasariliOdemeler", data);

                if (response.Result != null && !string.IsNullOrEmpty(response.Result.name))
                {
                    MessageBox.Show("Ödeme başarıyla gerçekleştirildi. İşlem ID: " + response.Result.name,
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Form5 form5 = new Form5();
                    form5.ShowDialog();
                    this.Close(); // Hide değil Close - artık bu forma dönülmeyecek
                }
                else
                {
                    MessageBox.Show("Ödeme kaydedilirken bir hata oluştu.", "Hata",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme işlemi sırasında bir istisna oluştu: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Close();
        }

        // Aşağıdaki boş event handler'lar tasarımcıda kalabilir
        private void label7_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
    }
}