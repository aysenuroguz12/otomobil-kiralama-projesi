using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Type;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
        FirebaseClient Client;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Client = new FireSharp.FirebaseClient(fc);
            }
            catch
            {
                MessageBox.Show("Veritabanına bağlantı hatası oluştu!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kullanıcıad = textBox1.Text;
            string sifre = textBox2.Text;

            string kullanıcıad1 = "otokiralama";
            string sifre1 = "1234";
            if (kullanıcıad == kullanıcıad1 && sifre == sifre1)
            {
                MessageBox.Show("Giriş başarılı");
                Form2 frm = new Form2();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tekrar deneyiniz");
                textBox2.Clear();

            }

            try
            {
                var data = new
                {
                    KullanıcıAd = textBox1.Text,
                    Sifre = textBox2.Text
                };

                var set = Client.Set("ototbl/" + textBox1.Text, data);

               
            }
            catch
            {
                MessageBox.Show("Firebase veri ekleme hatası!");
            }
        }

        

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var data = new
            {
               
            };

            try
            {
                SetResponse response = Client.Set("ototbl/" + textBox1.Text, data);
                MessageBox.Show("Veriler Firebase'e eklendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
        }
         
}
        
    

