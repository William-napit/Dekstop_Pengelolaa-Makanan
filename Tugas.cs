using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pertemuan_5
{
    public partial class Tugas : Form
    {
        public Tugas()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        struct Pesanan
        {
            public string kategori, toppingorlevel, namamenu;
            public decimal harga, qty, Subtotal, HasilAkhir;
        }
        Pesanan[] P = new Pesanan[0];
        int harga, pajak = 5000;
        decimal Total, Subtotal, diskon, Akhir;

        private void nudqty_ValueChanged(object sender, EventArgs e)
        {
            HitungSubtotal();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (cboKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Kategori tidak boleh kosong !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
           
            else
            {
                Array.Resize(ref P, P.Count() + 1);
                P[P.GetUpperBound(0)].kategori = cboKategori.SelectedItem.ToString();
                P[P.GetUpperBound(0)].toppingorlevel = cboGanti.SelectedItem.ToString();
                P[P.GetUpperBound(0)].namamenu = lstMenu.SelectedItem.ToString();
                P[P.GetUpperBound(0)].qty = nudqty.Value;
                P[P.GetUpperBound(0)].harga = harga;
                P[P.GetUpperBound(0)].Subtotal = Subtotal;
                Tampil();


                MessageBox.Show("Pesanan Berhasil ditambahkan", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Clear();
        }
        private void Tampil()
        {
            lstTampil.Items.Clear();
            Total = 0;
            for (int i = 0; i <= P.GetUpperBound(0); i++)
            {
                lstTampil.Items.Add($"{P[i].kategori.PadRight(14)}{P[i].toppingorlevel.PadRight(20)}{P[i].namamenu.PadRight(30)}{P[i].qty.ToString().PadRight(7)}{P[i].harga.ToString("Rp #,##0.00").PadRight(15)}{P[i].Subtotal.ToString("Rp #,##0.00")}");
                Total += P[i].Subtotal;


            }

            lblTotal.Text = Total.ToString("Rp #,##0.00");
        }

        private void btnKonfirmasi_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah data yang anda masukkan sudah benar?", "Konfirmasi", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                tabPesanan.SelectedIndex = 3;
                string metodebayar, statuspesanan;
                decimal diskon;
                if (rdoCod.Checked == true)
                {
                    metodebayar = "Cod";
                }
                else if (rdoKartuKredit.Checked == true)
                {
                    metodebayar = $"Kartu Kredit (No. Kartu) : {txtKartuKredit.Text}";
                }
                else if (rdoTransfer.Checked == true)
                {
                    metodebayar = $"Kartu Kredit (No.karut) : {txtTransfer.Text}";
                }
                else
                {
                    metodebayar = "-";
                }


                if (rdoPulang.Checked == true)
                {
                    statuspesanan = "Bawa Pulang";
                }
                else if (rdoDitempat.Checked == true)
                {
                    statuspesanan = "Makan ditempat";
                }
                else
                {
                    statuspesanan = "-";
                }


                rtbTampil.Font = new Font("Consolas", 10);
                rtbTampil.ForeColor = Color.Navy;

                rtbTampil.Text += $"Nama Pelanggan       : { txtNama.Text}\n";
                rtbTampil.Text += $"Alamat               : { txtAlamat.Text}\n";
                rtbTampil.Text += $"{new string('=', 100)}\n";
                rtbTampil.Text += $"{"Kategori".PadRight(9)}{"Level or Topping".PadRight(25)}{"Nama Produk".PadRight(30)}{"Qty".PadRight(7)}{"Harga".PadRight(35)}{"SubTotal"}\n";
                rtbTampil.Text += $"{new string('=', 100)}\n";
                for (int i = 0; i <= P.GetUpperBound(0); i++)
                {
                    rtbTampil.Text += $"{lstTampil.Items[i]}\n";
                }


                rtbTampil.Text += $"{new string('=', 100)}\n";
                rtbTampil.Text += $"Diskon              : {lblDiskon.Text}\n";
                rtbTampil.Text += $"Pajak               : {lblPajak.Text}\n";
                rtbTampil.Text += $"Total               : {lblSemua.Text}\n";
                rtbTampil.Text += $"{new string('=', 100)}\n";
                rtbTampil.Text += $"Metode Pembayaran   : {metodebayar}\n";
                rtbTampil.Text += $"Status Pesanan      : {statuspesanan}\n";
                rtbTampil.Text += $"Kateogori Pelanggan : {cboKategoriPel.SelectedItem}\n";
                rtbTampil.SaveFile("Pesanan.rtf");

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("Pesanan.rtf");
        }

        private void cboKategoriPel_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (cboKategoriPel.SelectedIndex == 0)
            {
                lblDiskon.Text = "Rp.5000";
                Akhir = Total - 5000 + 5000;
            }
            else if (cboKategoriPel.SelectedIndex == 1)
            {
                lblDiskon.Text = "-";
                Akhir = Total + 5000;
            }
            else
            {
                lblDiskon.Text = "-";
                diskon = 0;
                

            }
            lblSemua.Text = Akhir.ToString("Rp#,##0.00");
        }

        private void cboGanti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGanti.SelectedItem != null)
            {
                lblTambahan.Text = cboGanti.SelectedItem.ToString();
            }
        }
        private void Clear()
        {
            cboKategori.SelectedIndex = -1;
            cboGanti.SelectedIndex = -1;
            lstMenu.Items.Clear();
            nudqty.Value = 0;
            lblSubTotal.Text = "-";
            lblHargaa.Text = "-";
            lblTambahan.Text = "-";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstMenu.SelectedItem == "Ayam Kecap")
            {
                harga = 45000;
                picGambar.Image = Image.FromFile("E:/Jual/Jangi/Pertemuan_5/Gambar/Ayam Kecap.png");
            }
            else if (lstMenu.SelectedItem == "Ayam Goreng Mentega")
            {
                harga = 43000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/Ayam Goreng Mentaga.png");
            }
            else if (lstMenu.SelectedItem == "Ayam Goreng Bawang Putih Manis")
            {
                harga = 47000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/Ayam goreng bawang putih pedas manis.png");
            }
            else if (lstMenu.SelectedItem == "Ayam Teriyaki")
            {
                harga = 50000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/Ayam teriyaki.png");
            }
            else if (lstMenu.SelectedItem == "Ayam Bakar Bumbu Padang")
            {
                harga = 54000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/Ayam bakar bumbu Padang.png");
            }
            else if (lstMenu.SelectedItem == "Boba milk tea")
            {
                harga = 15000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/milktea.jpg");
            }
            else if (lstMenu.SelectedItem == "Cheesy milky")
            {
                harga = 20000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/chess.jpeg");
            }
            else if (lstMenu.SelectedItem == "Banana milk")
            {
                harga = 17000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/banana.jpg");
            }
            else if (lstMenu.SelectedItem == "Strawberry macchiato")
            {
                harga = 15000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/stroberi.jpg");
            }
            else if (lstMenu.SelectedItem == "Brown sugar")
            {
                harga = 16000;
                picGambar.Image = Image.FromFile("E:/Jual/Tugas/Gambar/brown.jpg");
            }

            lblHargaa.Text = harga.ToString("Rp #,##0.00");
        }

        private void Tugas_Load(object sender, EventArgs e)
        {
            cboKategori.Items.Add("Makanan");
            cboKategori.Items.Add("Minuman");
            cboKategori.DropDownStyle = ComboBoxStyle.DropDownList;

            cboKategoriPel.Items.Add("Member");
            cboKategoriPel.Items.Add("Non - Member");
            cboKategoriPel.DropDownStyle = ComboBoxStyle.DropDownList;
            lblPajak.Text = pajak.ToString("Rp #,##0.00");
        }
        private void HitungSubtotal()
        {
            Subtotal = harga * nudqty.Value;
            lblSubTotal.Text = Subtotal.ToString("Rp #,##0.00");
        }
        private void cboKategori_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cboKategori.SelectedIndex == 0)
            {
                lstMenu.Items.Clear();

                lstMenu.Items.Add("Ayam Kecap");
                lstMenu.Items.Add("Ayam Goreng Mentega");
                lstMenu.Items.Add("Ayam Goreng Bawang Putih Manis");
                lstMenu.Items.Add("Ayam Teriyaki");
                lstMenu.Items.Add("Ayam Bakar Bumbu Padang");
                lblGanti.Text = "Level Kepedasan";

                cboGanti.Items.Clear();
                cboGanti.Items.Add("1");
                cboGanti.Items.Add("2");
                cboGanti.Items.Add("3");
                cboGanti.Items.Add("4");
                cboGanti.Items.Add("5");
                cboGanti.DropDownStyle = ComboBoxStyle.DropDownList;



            }
            else if (cboKategori.SelectedIndex == 1)
            {
                lstMenu.Items.Clear();

                lstMenu.Items.Add("Boba milk tea");
                lstMenu.Items.Add("Cheesy milky");
                lstMenu.Items.Add("Banana milk");
                lstMenu.Items.Add("Strawberry macchiato");
                lstMenu.Items.Add("Brown sugar");
                lblGanti.Text = "Topping";

                cboGanti.Items.Clear();
                cboGanti.Items.Add("Boba milk tea");
                cboGanti.Items.Add("Cheesy milky");
                cboGanti.Items.Add("Banana milk");
                cboGanti.Items.Add("Strawberry macchiato");
                cboGanti.Items.Add("Brown sugar");
                cboGanti.DropDownStyle = ComboBoxStyle.DropDownList;


            }

        }
    }
}
