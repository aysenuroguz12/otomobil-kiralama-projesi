using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    
    public class Class3
    {
      
       
        public string Musteri_Ad { get; set; }
        public string Musteri_Soyad { get; set; }
        public string Musteri_Telefon { get; set; }
        public string Musteri_Eposta { get; set; }
        public string Musteri_Adres { get; set; }

       
        public string Kart_Ad { get; set; }
        public string Kart_Soyad { get; set; }
        public string Kart_Numarasi { get; set; }
        public string Kart_CVV { get; set; }
        public string Kart_SonKullanma { get; set; } 

       
        public string Arac_Marka { get; set; }
        public string Arac_Model { get; set; }
        public int Kiralanan_Gun { get; set; }
        public decimal Odeme_Tutari { get; set; } 

       
        public string Isleme_Tarihi { get; set; } = DateTime.Now.ToString("dd.MM.yyyy HH:mm");


       
        public override string ToString()
        {
            return $"{Musteri_Ad} {Musteri_Soyad} - {Odeme_Tutari} TL Ödeme ({Arac_Marka} {Arac_Model} için)";
        }
    }
}