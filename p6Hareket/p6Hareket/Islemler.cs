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

namespace p6Hareket
{
    public partial class Islemler : Form
    {
        public Islemler()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-23T2RIK\\SQLEXPRESS;Initial Catalog=p6Hareket;Integrated Security=True");

        void kayit()
        {
            //PROSEDÜR
            SqlDataAdapter da = new SqlDataAdapter("Execute p6", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Islemler_Load(object sender, EventArgs e)
        {
            
           
            SqlCommand komut1 = new SqlCommand("select * from Table_Urunler", conn);
            SqlDataAdapter da = new SqlDataAdapter(komut1);
            DataTable dt = new DataTable();
            da.Fill(dt);
            SqlCommand komut2 = new SqlCommand("select * from Table_Musteriler", conn);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            SqlCommand komut3 = new SqlCommand("select * from Table_Personeller", conn);
            SqlDataAdapter da3 = new SqlDataAdapter(komut3);
            DataTable dt3 = new DataTable();
            da3.Fill(dt3);



            conn.Open();
            SqlCommand cmd = new SqlCommand("select AD from Table_Urunler", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                cmbUrunAd.Items.Add(dr[0]);
            }
            conn.Close();

            conn.Open();
            SqlCommand cmd2 = new SqlCommand("select ADSOYAD from Table_Musteriler", conn);
            SqlDataReader dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                cmbUrunAd.Items.Add(dr2[0]);
            }
            conn.Close();

            conn.Open();
            SqlCommand cmd3 = new SqlCommand("select AD from Table_Personeller", conn);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                cmbPersonelAd.Items.Add(dr3[0]);
            }
            conn.Close();

            cmbUrunAd.DisplayMember = "AD";
            cmbUrunAd.ValueMember = "ID";
            cmbUrunAd.DataSource = dt;

            cmbMusteri.DisplayMember = "ADSOYAD";
            cmbMusteri.ValueMember = "ID";
            cmbMusteri.DataSource = dt2;

            cmbPersonelAd.DisplayMember = "AD";
            cmbPersonelAd.ValueMember="ID";
            cmbPersonelAd.DataSource = dt3;
            //COMBOBOXLAR FORM AÇILDIĞINDA BOŞ GELSİN DİYE YAPILAN İŞLEM:
            cmbMusteri.Text = "";
            cmbUrunAd.Text = "";
            cmbPersonelAd.Text = "";
            kayit();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd2 = new SqlCommand("insert into Table_Hareketler (URUNAD, MUSTERI, PERSONEL, FIYAT) values (@p1,@p2,@p3,@p4)", conn);
            cmd2.Parameters.AddWithValue("@p1",byte.Parse(cmbUrunAd.SelectedValue.ToString()));
            cmd2.Parameters.AddWithValue("@p2", byte.Parse(cmbMusteri.SelectedValue.ToString()));
            cmd2.Parameters.AddWithValue("@p3", byte.Parse(cmbPersonelAd.SelectedValue.ToString()));
            cmd2.Parameters.AddWithValue("@p4", txtFiyat.Text);
            cmd2.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Sipariş Başarıyla Verildi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            kayit();

        }

        private void btnUrunKayit_Click(object sender, EventArgs e)
        {

            conn.Open();
            SqlCommand komut = new SqlCommand("insert into Table_Urunler (AD,STOK,ALISFIYAT,SATISFIYAT) values (@p1,@p2,@p3,@p4)", conn);
            komut.Parameters.AddWithValue("@p1", txtEkleUrunAd.Text);
            komut.Parameters.AddWithValue("@p2", txtEkleUrunStok.Text);
            komut.Parameters.AddWithValue("@p3", txtEkleUrunAlis.Text);
            komut.Parameters.AddWithValue("@p4", txtEkleUrunSatis.Text);
            komut.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ürün Başarıyla Kayıt Edildi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnMusteriKayit_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand komut4 = new SqlCommand("insert into Table_Musteriler (ADSOYAD) values (@p1)", conn);
            komut4.Parameters.AddWithValue("@p1", txtMusteriKayit.Text);
            komut4.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Müşteri Başarıyla Kayıt Edildi!", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
