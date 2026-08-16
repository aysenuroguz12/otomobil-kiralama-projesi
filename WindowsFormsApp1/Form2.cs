using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // ComboBox'lar burada, sadece bir kez dolduruluyor
            comboBox1.Items.Clear();
            comboBox1.Items.Add(3);

            comboBox2.Items.Clear();
            comboBox2.Items.Add(200);

            // TODO: toplam araç / kirada araç sayısını Firebase'den çekip
            // label2.Text ve label4.Text'e yazmak istiyorsan söyle, ekleyelim.
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void pictureBox2_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçime göre yapılacak iş buraya (liste doldurma DEĞİL)
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Seçime göre yapılacak iş buraya (liste doldurma DEĞİL)
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarForm f3 = new CarForm();
            f3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RentalForm form4 = new RentalForm();
            form4.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoginForm f1 = new LoginForm();
            f1.Show();
            this.Hide();
        }
    }
}