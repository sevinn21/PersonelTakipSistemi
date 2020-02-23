using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//kütüphanenin eklenmesi
using System.Data.SqlClient;


namespace PersonelTakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //veri tabanı dosya yolu ve provider nesnelerinin belirlenmesi
        SqlConnection baglanti = new SqlConnection("Data Source=BILGISAYAR;Initial Catalog=PersonelTakip;Integrated Security=True");

        public static string tcno, adi, soyadi, yetki;

        //yerel yani bu formda geçerli olacak değişkenler
        int hak = 3;
        bool durum = false;

        private void button1_Click(object sender, EventArgs e)
        {

            if (hak != 0)
            {
                baglanti.Open();

                SqlCommand sorgu = new SqlCommand("select * from [user] where kullanici_adi = @kullanici_adi and parola=@parola", baglanti);
                sorgu.Parameters.AddWithValue("@kullanici_adi", textBox1.Text);
                sorgu.Parameters.AddWithValue("@parola", textBox2.Text);
                SqlDataReader kayitokuma = sorgu.ExecuteReader();

                while (kayitokuma.Read())
                {
                    if (radioButton1.Checked == true)
                    {
                        if (kayitokuma["yetki"].ToString() == "yonetici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();

                            Form2 frm2 = new Form2();
                            frm2.Show();
                            this.Hide();

                        }

                    }

                    if (radioButton2.Checked == true)
                    {
                        if (kayitokuma["yetki"].ToString() == "kullanici")
                        {
                            durum = true;
                            tcno = kayitokuma.GetValue(0).ToString();
                            adi = kayitokuma.GetValue(1).ToString();
                            soyadi = kayitokuma.GetValue(2).ToString();
                            yetki = kayitokuma.GetValue(3).ToString();

                            Form3 frm3 = new Form3();
                            frm3.Show();
                            this.Hide();


                        }
                    }
                }
                if (durum == false)
                    hak--;

                baglanti.Close();

            }
            label4.Text = Convert.ToString(hak);
            if (hak == 0)
            {
                button1.Enabled = false;
                MessageBox.Show("Giriş Hakkı Kalmadı", "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {

            this.Text = "Kullanıcı Girişi...";
            this.AcceptButton = button1; this.CancelButton = button2;
            label4.Text = Convert.ToString(hak);
            radioButton1.Checked = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        }

      

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
