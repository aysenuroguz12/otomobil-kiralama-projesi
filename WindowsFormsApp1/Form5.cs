using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp1.PaymentForm;

namespace WindowsFormsApp1
{
    public partial class PaymentForm : Form
    {
        public PaymentForm()
        {
            InitializeComponent();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        { 
        }


             public string comboBax2 { get; set; }
        public string comboBox1 { get; set; }
        public int comboBox3 { get; set; }
             public override string ToString()
        {
            return $"{comboBax2} - {comboBox1} - {comboBox3} TL";
        }








        private void button1_Click(object sender, EventArgs e)
        { }
            // Araç bilgisi
public class Arac
        {
           
        }

       
                     
        }

    }


