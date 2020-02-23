using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace PersonelTakip
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=BILGISAYAR; Initial Catalog=PersonelTakip; Integrated Security = True");


        private void personelleri_goster()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter personelleri_listele = new SqlDataAdapter("select tc_no as[TC KİMLİK NO],ad as[AD],soyad as[SOYAD],cinsiyet as[CİNSİYET],mezuniyet as[MEZUNiYET],gorevi as[GÖREVİ],gorev_yeri as[GÖREV YERİ],dogum_tarihi as[DOĞUM TARİHİ] from personeller Order By ad ASC", baglanti);
                DataSet dsHafiza = new DataSet();
                personelleri_listele.Fill(dsHafiza);
                dataGridView1.DataSource = dsHafiza.Tables[0];
                baglanti.Close();

            }
            catch (Exception hatamsj)
            {
                MessageBox.Show(hatamsj.Message, "SKY Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();

                
            }
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }


        private void Form3_Load(object sender, EventArgs e)
        {
            personelleri_goster();
            this.Text = "KULLANICI İŞLEMLERİ";
            label19.Text = Form1.adi + " " + Form1.soyadi;
            pictureBox1.Height = 120;
            pictureBox1.Width = 120;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Height = 120;
            pictureBox2.Width = 120;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;

            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\" + Form1.tcno + ".jpg");

            }
            catch
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + "\\kullaniciresimler\\resimyok");

            }
            maskedTextBox1.Mask = "00000000000";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool kayit_arama_durumu = false;
            if(maskedTextBox1.Text.Length==11)
            {
                baglanti.Open();
                SqlCommand selectsorgu = new SqlCommand("select*from personeller where tc_no='" + maskedTextBox1.Text + "'", baglanti);
                SqlDataReader kayitokuma = selectsorgu.ExecuteReader();
                while(kayitokuma.Read())
                {
                    kayit_arama_durumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\" + kayitokuma.GetValue(0) + ".jpg");

                    }
                    catch 
                    {

                       pictureBox1.Image = Image.FromFile(Application.StartupPath + "\\personelresimler\\resimyok.png");

                    }
                    label10.Text = kayitokuma.GetValue(1).ToString();
                    label11.Text = kayitokuma.GetValue(2).ToString();
                    if (kayitokuma.GetValue(3).ToString() == "Bay")
                        label12.Text = "Bay";
                    else
                        label12.Text = "Bayan";
                    label13.Text = kayitokuma.GetValue(4).ToString();
                    label14.Text = kayitokuma.GetValue(5).ToString();
                    label15.Text = kayitokuma.GetValue(6).ToString();
                    label16.Text = kayitokuma.GetValue(7).ToString();
                    
                    break;

                    }
                    if (kayit_arama_durumu == false)
                        MessageBox.Show("Aranan Kayıt Bulunamadı!");
                    baglanti.Close();
            }
                else
                MessageBox.Show("11 haneli tc kimlik no giriniz!");
        }
    }
}
