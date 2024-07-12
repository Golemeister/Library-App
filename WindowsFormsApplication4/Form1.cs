using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        string CS = "Data Source=U1PC6\\SQLEXPRESS;Initial Catalog=Muzika;Integrated Security=True";
        Korisnik korisnik;
        string tip;
        bool privilegija;
        SqlConnection konekcija;
        

        public Form1()
        {
            InitializeComponent();
            korisnik = new Korisnik(tbUsername.Text, tbPassword.Text, tip);

        }

        private void prijaviSeToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (groupBox1.Visible)
                groupBox1.Visible = false;
            else
                groupBox1.Visible = true;
        }

        private void Prijavise_Click(object sender, EventArgs e)
        {
            
            konekcija = new SqlConnection(CS);
            konekcija.Open();
            SqlCommand upit = new SqlCommand("Select username,password,tip from Korisnici where Username = '" + tbUsername.Text + "'AND Password ='" + tbPassword.Text + "' AND tip ='" + tip +"'",konekcija);
            SqlDataReader reader = upit.ExecuteReader();
            
            
            
            if(reader.Read())
            {
                
                MessageBox.Show("Uspesno ste ulogovani");
                prijaviSeToolStripMenuItem.Visible = false;
                odjavaToolStripMenuItem.Visible = true;
                groupBox1.Visible = false;
                privi();
            }
            else
            {
                MessageBox.Show("Pogresan username ili password!");
            }
      
        }

        private void Admin_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        public void privi()
        {
            if (privilegija == true)
            {
                zanrToolStripMenuItem.Visible = true;
                pesmeKorisnikaToolStripMenuItem.Visible = true;
                storeToolStripMenuItem.Visible = false;
                mojePesmeToolStripMenuItem.Visible = false;

            }
            else
            {
                storeToolStripMenuItem.Visible = true;
                mojePesmeToolStripMenuItem.Visible = true;
                zanrToolStripMenuItem.Visible = false;
                pesmeKorisnikaToolStripMenuItem.Visible = false;
            }
        }
        
        public void odjava()
        {
            tbUsername.Clear();
            tbPassword.Clear();
            odjavaToolStripMenuItem.Visible = false;
            storeToolStripMenuItem.Visible = false;
            mojePesmeToolStripMenuItem.Visible = false;
            zanrToolStripMenuItem.Visible = false;
            pesmeKorisnikaToolStripMenuItem.Visible = false;
            prijaviSeToolStripMenuItem.Visible = true;
            odjavaToolStripMenuItem.Visible = false;
            listBox1.Visible = false;
            listBox2.Visible = false;
            label2.Visible = false;
            label3.Visible = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            odjavaToolStripMenuItem.Visible = false;
            storeToolStripMenuItem.Visible = false;
            mojePesmeToolStripMenuItem.Visible = false;
            zanrToolStripMenuItem.Visible = false;
            pesmeKorisnikaToolStripMenuItem.Visible = false;
            
            
        }

        private void odjavaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            odjava();
        }

        private void izlazToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
             tip = "admin";
             privilegija = true;
             
        }

        private void Korisnik_CheckedChanged(object sender, EventArgs e)
        {
            tip = "user";
            privilegija = false;
            
        }

        private void storeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Visible = true;
            listBox2.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            button1.Visible = true;
            listBox2.Items.Clear();
            
            konekcija = new SqlConnection(CS);
            konekcija.Open();
            SqlCommand mojepesme = new SqlCommand("Select Pesma.imePesme from Korisnici inner join owned on korisnici.ID=owned.IDKorisnika inner join Pesma on Pesma.ID=owned.IDPesme where korisnici.username='" + tbUsername.Text + "' and korisnici.password='" + tbPassword.Text + "'", konekcija);
            
            SqlDataReader citac = mojepesme.ExecuteReader();
            while (citac.Read())
            {
                for (int p = 0; p < citac.FieldCount; p++)
                {
                    listBox2.Items.Add(citac.GetValue(p));
                }

            }
            citac.Close();
            SqlCommand pesme = new SqlCommand("Select Pesma.imePesme from Pesma",konekcija);
            SqlDataReader citac2 = pesme.ExecuteReader();
            while (citac2.Read())
            {
                for (int p = 0; p < citac2.FieldCount; p++)
                {
                    listBox1.Items.Add(citac2.GetValue(p));
                }
            }
            citac2.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
            else
            {
                MessageBox.Show("Select items");
            }

        }

        private void mojePesmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Visible = false;
            label2.Visible = false;
            button1.Visible = false;
            label3.Visible = true;
            listBox2.Visible = true;
            konekcija = new SqlConnection(CS);
            konekcija.Open();
            SqlCommand mojepesme = new SqlCommand("Select Pesma.imePesme from Korisnici inner join owned on korisnici.ID=owned.IDKorisnika inner join Pesma on Pesma.ID=owned.IDPesme where korisnici.username='" + tbUsername.Text + "' and korisnici.password='" + tbPassword.Text + "'", konekcija);

            SqlDataReader citac = mojepesme.ExecuteReader();
            while (citac.Read())
            {
                for (int p = 0; p < citac.FieldCount; p++)
                {
                    listBox2.Items.Add(citac.GetValue(p));
                }

            }
        }
        

    }
}