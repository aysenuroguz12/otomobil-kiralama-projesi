using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        IFirebaseConfig fc = new FirebaseConfig
        {
            AuthSecret = "7py1re49571VUbXGqPbrsyU1EWGIQFaLC9L8KZM0",
            BasePath = "https://otokiralama1-8a758-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient Client;

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Client = new FireSharp.FirebaseClient(fc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanına bağlantı hatası oluştu! Detay: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Şifre karakteri gizleme burada bir kere ayarlanır
            textBox2.UseSystemPasswordChar = true;
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private async void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAd = textBox1.Text.Trim();
            string sifre = textBox2.Text;

            if (string.IsNullOrEmpty(kullaniciAd) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string kullaniciAd1 = "otokiralama";
            const string sifre1 = "1234";

            if (kullaniciAd == kullaniciAd1 && sifre == sifre1)
            {
                MessageBox.Show("Giriş başarılı", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Form2 frm = new Form2();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tekrar deneyiniz", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox2.Clear();
                return; // Yanlış girişte Firebase'e yazmaya gerek yok
            }

            // Giriş bilgisini logla / kaydet (opsiyonel)
            if (Client != null)
            {
                try
                {
                    var data = new
                    {
                        KullaniciAd = kullaniciAd,
                        SonGiris = DateTime.Now.ToString()
                    };

                    await Client.SetAsync("ototbl/" + kullaniciAd, data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Firebase veri ekleme hatası! Detay: " + ex.Message,
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Bu butonun ne kaydetmesi gerektiğini netleştirince
            // buraya gerçek alanlarla dolu bir "data" objesi ekleyeceğiz.
            MessageBox.Show("Bu butonun işlevini birlikte tanımlayalım.",
                "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}