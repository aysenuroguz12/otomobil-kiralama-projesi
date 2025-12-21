using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Type;
using Newtonsoft.Json; 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
  
    public partial class Form4 : Form
    {
       
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "7py1re49571VUbXGQFaLC9L8KZM0",
            BasePath = "https://otokiralama1-8a758-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firebase istemcisi başlatılırken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Class3 VerileriTopla()
        {

    
    string musteriAd = this.Controls.Find("txtMusteriAd", true).FirstOrDefault() is TextBox t1 ? t1.Text : "";
    string musteriSoyad = this.Controls.Find("txtMusteriSoyad", true).FirstOrDefault() is TextBox t2 ? t2.Text : "";
    string musteriTelefon = this.Controls.Find("txtMusteriTelefon", true).FirstOrDefault() is TextBox t3 ? t3.Text : "";
    string musteriEposta = this.Controls.Find("txtMusteriEposta", true).FirstOrDefault() is TextBox t4 ? t4.Text : "";
    string musteriAdres = this.Controls.Find("txtMusteriAdres", true).FirstOrDefault() is TextBox t5 ? t5.Text : "";

    
    string kartAd = this.Controls.Find("txtKartAd", true).FirstOrDefault() is TextBox t6 ? t6.Text : "";
    string kartSoyad = this.Controls.Find("txtKartSoyad", true).FirstOrDefault() is TextBox t7 ? t7.Text : "";
    string kartNo = this.Controls.Find("txtKartNumarasi", true).FirstOrDefault() is TextBox t8 ? t8.Text : "";
    string cvv = this.Controls.Find("txtCVV", true).FirstOrDefault() is TextBox t9 ? t9.Text : "";
    string sonKullanma = "12/2025";

  

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
        Kart_SonKullanma = sonKullanma,
        Odeme_Tutari = (decimal)this.Fiyat 
            };
        }


        
        private async void btnKaydet_Click(object sender, EventArgs e)
        {
            var data = VerileriTopla();

            if (data == null || client == null)
            {
                if (client == null) MessageBox.Show("Firebase bağlantısı yok.", "Hata");
                return;
            }

            try
            {
                string path = $"MusteriKayitlari/{data.Musteri_Eposta.Replace(".", "_")}";
                SetResponse response = await client.SetAsync(path, data);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Müşteri Bilgileri başarıyla kaydedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kaydetme işlemi başarısız oldu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void btnOdemeYap_Click(object sender, EventArgs e)
        {
            var data = VerileriTopla();

            if (data == null || client == null)
            {
                if (client == null) MessageBox.Show("Firebase bağlantısı yok.", "Hata");
                return;
            }

            try
            {
                PushResponse response = await client.PushAsync("BasariliOdemeler", data);

                if (response.Result != null && !string.IsNullOrEmpty(response.Result.name))
                {
                    MessageBox.Show("ÖDEME BAŞARILI BİR ŞEKİLDE GERÇEKLEŞTİRİLDİ. İşlem ID: " + response.Result.name, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form5 form5 = new Form5();
                    form5.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Ödeme kaydedilirken bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ödeme işlemi sırasında bir istisna oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ödeme başarıyla gerçekleşti.");
        }

        private async void button2_Click(object sender, EventArgs e)
        {
           
            var data = VerileriTopla();

          
            if (data == null || client == null)
            {
                if (client == null)
                {
                    MessageBox.Show("Firebase bağlantısı yok.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            try
            {
                
                PushResponse response = await client.PushAsync("KiralamaBilgileri", data);

                if (response.Result != null && !string.IsNullOrEmpty(response.Result.name))
                {
                    MessageBox.Show("Kiralama bilgileri başarıyla kaydedildi! ID: " + response.Result.name, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    
                    MessageBox.Show($"Veri kaydedilirken bir hata oluştu. HTTP Durum Kodu: {response.StatusCode}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri kaydı sırasında bir istisna oluştu: " + ex.Message, "İstisna", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void button4_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.ShowDialog();
            this.Hide();
        }

      
        private void button5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
            this.Hide();
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
        public int Fiyat
        {
            get
            {
                

                TextBox fiyatTextBox = this.Controls.Find("textBox4", true).FirstOrDefault() as TextBox;

                if (fiyatTextBox != null && int.TryParse(fiyatTextBox.Text, out int fiyatDegeri))
                {
                    
                    return fiyatDegeri;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                
            }
        }
       
        private void label7_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void groupBox2_Enter(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }

        
        public string textBox;
    } 
}