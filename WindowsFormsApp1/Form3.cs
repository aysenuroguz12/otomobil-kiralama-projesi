using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public class KiralamaKaydi
        {
            public string Model { get; set; }
            public string Marka { get; set; }
            public int Gun { get; set; }
            public int Fiyat { get; set; }
        }

        public Form3()
        {
            InitializeComponent();
        }

        IFirebaseConfig fc = new FirebaseConfig
        {
            AuthSecret = "7py1re49571VUbXGqPbrsyU1EWGIQFaLC9L8KZM0",
            BasePath = "https://otokiralama1-8a758-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient Client;

        private void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                Client = new FireSharp.FirebaseClient(fc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Firebase bağlantı sorunu! Detay: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            comboBox1.Items.Clear();
            comboBox1.Items.Add("BMW");
            comboBox1.Items.Add("Audi");
            comboBox1.Items.Add("Mercedes");

            comboBox3.Items.Clear();
            comboBox3.Items.Add("25");
            comboBox3.Items.Add("30");
            comboBox3.Items.Add("35");

            pictureBox1.Visible = true;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SafeLoadImage(pictureBox1, @"C:\Users\Casper\Downloads\ARC4.jpg");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            SafeLoadImage(pictureBox2, @"C:\Users\Casper\Downloads\arc5.jpg.jpg");
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            SafeLoadImage(pictureBox3, @"C:\Users\Casper\Downloads\arac1.jpg");
        }

        private void SafeLoadImage(PictureBox box, string path)
        {
            try
            {
                box.Image = Image.FromFile(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Resim yüklenemedi: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateVehicleDisplay(string selectedMarka)
        {
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;

            checkBox1.Visible = false;
            checkBox2.Visible = false;
            checkBox3.Visible = false;
            checkBox4.Visible = false;

            comboBox2.Items.Clear();
            comboBox2.Text = "";

            if (selectedMarka == "BMW")
            {
                pictureBox1.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;

                comboBox2.Items.Add("X1");
                comboBox2.Items.Add("X3");
                comboBox2.Items.Add("X5");
            }
            else if (selectedMarka == "Audi")
            {
                pictureBox2.Visible = true;
                checkBox1.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;

                comboBox2.Items.Add("A4");
                comboBox2.Items.Add("A6");
                comboBox2.Items.Add("Q5");
            }
            else if (selectedMarka == "Mercedes")
            {
                pictureBox3.Visible = true;
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;

                comboBox2.Items.Add("C-Class");
                comboBox2.Items.Add("E-Class");
                comboBox2.Items.Add("GLA");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                UpdateVehicleDisplay("");
                return;
            }

            string marka = comboBox1.SelectedItem.ToString();
            UpdateVehicleDisplay(marka);
        }

        private int HesaplaFiyat(int gun)
        {
            int fiyat = gun * 500;
            if (checkBox1.Checked) fiyat += 300;
            if (checkBox2.Checked) fiyat += 500;
            if (checkBox3.Checked) fiyat += 100;
            if (checkBox4.Checked) fiyat += 150;
            return fiyat;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem == null)
            {
                comboBox4.Text = "";
                return;
            }

            int gun;
            if (!int.TryParse(comboBox3.SelectedItem.ToString(), out gun))
            {
                MessageBox.Show("Seçilen gün sayısı geçerli bir sayı değil.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBox4.Text = "";
                return;
            }

            comboBox4.Text = HesaplaFiyat(gun).ToString() + " TL";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) => comboBox3_SelectedIndexChanged(sender, e);
        private void checkBox2_CheckedChanged(object sender, EventArgs e) => comboBox3_SelectedIndexChanged(sender, e);
        private void checkBox3_CheckedChanged(object sender, EventArgs e) => comboBox3_SelectedIndexChanged(sender, e);
        private void checkBox4_CheckedChanged(object sender, EventArgs e) => comboBox3_SelectedIndexChanged(sender, e);

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 ||
                comboBox2.SelectedIndex == -1 ||
                comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm seçimleri yapın!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string marka = comboBox1.SelectedItem.ToString();
            string model = comboBox2.SelectedItem.ToString();
            int gun = int.Parse(comboBox3.SelectedItem.ToString());
            int fiyat = HesaplaFiyat(gun);

            // Artık veriler Form4'e gerçekten aktarılıyor
            Form4 f4 = new Form4(marka, model, gun, fiyat);
            f4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (Client == null)
            {
                MessageBox.Show("Firebase istemcisi başlatılamadı. İnternet bağlantınızı kontrol edin.",
                    "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string marka = comboBox1.Text;
            string model = comboBox2.Text;

            int gun;
            if (!int.TryParse(comboBox3.Text, out gun))
            {
                MessageBox.Show("Lütfen 'Gün' alanı için geçerli bir sayı giriniz.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fiyatText = comboBox4.Text.Replace(" TL", "").Trim();
            int fiyat;
            if (!int.TryParse(fiyatText, out fiyat))
            {
                MessageBox.Show("Lütfen 'Fiyat' alanı için geçerli bir sayı giriniz.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(marka) || string.IsNullOrEmpty(model) || gun <= 0 || fiyat <= 0)
            {
                MessageBox.Show("Lütfen tüm kiralama bilgilerini eksiksiz ve doğru giriniz.",
                    "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var data = new KiralamaKaydi
            {
                Marka = marka,
                Model = model,
                Gun = gun,
                Fiyat = fiyat
            };

            try
            {
                PushResponse response = await Client.PushAsync("Kiralamalar/", data);

                if (response.Result != null && !string.IsNullOrEmpty(response.Result.name))
                {
                    MessageBox.Show("Kiralama bilgileri başarıyla kaydedildi! ID: " + response.Result.name,
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Veri kaydedilirken bir hata oluştu: " + response.StatusCode,
                        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri kaydı sırasında bir istisna oluştu: " + ex.Message,
                    "İstisna", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}