using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alperen_alp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class Dugum
        {
            public int kod;
            public int fiyat;
            public string isim;
            public Dugum sonraki;
            public Dugum onceki;
        }
        Dugum ilk = null;
        Dugum son = null;
        private void Listele_Click(object sender, EventArgs e)
        {
            Dugum isaretci = ilk;
            dataGridView1.Rows.Clear();
            if (ilk != son) // bir elemandan fazlaysa satır ekler
            {
                int satirSayisi = 0;
                while (isaretci.sonraki != null) // eklenecek satır miktarını bulur
                {
                    satirSayisi++;
                    isaretci = isaretci.sonraki;
                }
                dataGridView1.Rows.Add(satirSayisi);
            }

            int i = 0;
            int j = 0;
            isaretci = ilk;
            while (isaretci != null)
            {
                dataGridView1.Rows[i].Cells[j].Value = isaretci.kod;
                dataGridView1.Rows[i].Cells[j+1].Value = isaretci.isim;
                dataGridView1.Rows[i].Cells[j+2].Value = isaretci.fiyat;
                isaretci = isaretci.sonraki;
                i++;
            }
        }

        private void Ekle_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="" || textBox2.Text=="" || textBox3.Text=="")
            {
                MessageBox.Show("Bilgileri girmeniz gerekmektedir", "Kayıt Ekleme Uyarı",MessageBoxButtons.OK ,MessageBoxIcon.Warning);
            }
            else
            {
                Dugum yeni = new Dugum();
                yeni.kod = Convert.ToInt32(textBox1.Text);
                yeni.isim = textBox2.Text;
                yeni.fiyat = Convert.ToInt32(textBox3.Text);
                if (ilk == null) // eleman yoksa
                {
                    ilk = yeni;
                    son = yeni;
                }
                else
                {
                    Dugum isaretci1 = null;
                    Dugum isaretci2 = ilk;
                    while (isaretci2 != null)
                    {
                        if (yeni.kod == isaretci2.kod)
                        {
                            MessageBox.Show("Eklemek istediğiniz ürün mevcut", "Kayıt Ekleme Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        else
                        {
                            if (yeni.kod < isaretci2.kod && isaretci2 == ilk)  // başa ekler
                            {
                                ilk.onceki = yeni;
                                yeni.sonraki = ilk;
                                ilk = yeni;
                                ilk.onceki = null;
                                break;
                            }
                            if (yeni.kod < isaretci2.kod)  // araya ekler
                            {
                                isaretci1.sonraki = yeni;
                                yeni.onceki = isaretci1;
                                isaretci2.onceki = yeni;
                                yeni.sonraki = isaretci2;
                                break;
                            }
                            if (yeni.kod > isaretci2.kod && isaretci2 == son)  // sona ekler
                            {
                                son.sonraki = yeni;
                                yeni.onceki = son;
                                son = yeni;
                                son.sonraki = null;
                                break;
                            }
                        }
                        
                        isaretci1 = isaretci2;
                        isaretci2 = isaretci2.sonraki;
                    }
                }
            }
        }

        private void Bul1_Click(object sender, EventArgs e)
        {
            if (silmeTextBox1.Text=="")
            {
                MessageBox.Show("Ürün kodu boş bırakılamaz", "Kayıt Bulma Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int kodBul = Convert.ToInt32(silmeTextBox1.Text);
                Boolean durum = true;
                Dugum isaretci = ilk;
                while (isaretci != null)
                {
                    if (isaretci.kod == kodBul)
                    {
                        silmeTextBox2.Text = isaretci.isim;
                        silmeTextBox3.Text = Convert.ToString(isaretci.fiyat);
                        durum = false;
                    }
                    isaretci = isaretci.sonraki;
                }
                if (durum)
                {
                    silmeTextBox1.Text = "";
                    silmeTextBox2.Text = "";
                    silmeTextBox3.Text = "";
                    MessageBox.Show("Kayıt Bulunamadı", "Kayıt Bulma Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void Bul2_Click(object sender, EventArgs e)
        {
            if (guncelleTextBox1.Text=="")
            {
                MessageBox.Show("Ürün kodu boş bırakılamaz", "Kayıt Bulma Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int kodBul = Convert.ToInt32(guncelleTextBox1.Text);
                Boolean durum = true;
                Dugum isaretci = ilk;
                while (isaretci != null)
                {
                    if (isaretci.kod == kodBul)
                    {
                        guncelleTextBox2.Text = isaretci.isim;
                        guncelleTextBox3.Text = Convert.ToString(isaretci.fiyat);
                        durum = false;
                    }
                    isaretci = isaretci.sonraki;
                }
                if (durum)
                {
                    guncelleTextBox1.Text = "";
                    guncelleTextBox2.Text = "";
                    guncelleTextBox3.Text = "";
                    MessageBox.Show("Kayıt Bulunamadı", "Kayıt Bulma Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Sil_Click(object sender, EventArgs e)
        {
            if (silmeTextBox1.Text=="")
            {
                MessageBox.Show("Ürün kodu boş bırakılamaz", "Silme İşlemi Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (ilk == null)
                {
                    MessageBox.Show("Silinecek Kayıt Yok", "Silme İşlemi Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Dugum isaretci1 = null; // bir onceki dugumu bulmak icin gerekli
                    Dugum isaretci2 = ilk;
                    Dugum isaretci3 = ilk.sonraki;  // bir sonraki dugumu bulmak icin gerekli
                    int urunSil = Convert.ToInt32(silmeTextBox1.Text);
                    while (isaretci2 != null)
                    {

                        if (urunSil == isaretci2.kod && ilk == son)  // tek eleman varsa
                        {
                            ilk = null;
                            son = null;
                            break;
                        }
                        if (urunSil == isaretci2.kod && isaretci2 == ilk)  // baştan siler
                        {
                            ilk = ilk.sonraki;
                            ilk.onceki = null;
                            break;
                        }
                        if (urunSil == isaretci2.kod && isaretci2 == son)  // sondan siler
                        {
                            son = son.onceki;
                            son.sonraki = null;
                            break;
                        }
                        if (urunSil == isaretci2.kod && isaretci2 != ilk && isaretci2 != son)  // aradan siler
                        {
                            isaretci1.sonraki = isaretci3;
                            isaretci3.onceki = isaretci1;
                            break;
                        }
                        if (urunSil > isaretci2.kod && isaretci3 == null)
                        {
                            MessageBox.Show("Silinecek Kayıt Bulunamadı", "Silme İşlemi Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }
                        if (urunSil < isaretci2.kod && isaretci3 == null)
                        {
                            MessageBox.Show("Silinecek Kayıt Bulunamadı", "Silme İşlemi Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        isaretci1 = isaretci2;
                        isaretci2 = isaretci2.sonraki;
                        isaretci3 = isaretci3.sonraki;
                    }
                }
                

            }
            
        }

        private void Guncelle_Click(object sender, EventArgs e)
        {
            if (guncelleTextBox1.Text == "" || guncelleTextBox2.Text == "" || guncelleTextBox3.Text == "")
            {
                MessageBox.Show("Bilgileri girmeniz gerekmektedir", "Kayıt Ekleme Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Dugum yeni = new Dugum();
                yeni.kod = Convert.ToInt32(guncelleTextBox1.Text);
                yeni.isim = guncelleTextBox2.Text;
                yeni.fiyat = Convert.ToInt32(guncelleTextBox3.Text);
                Boolean durum = true;
                Dugum isaretci = ilk;
                while (isaretci != null)
                {
                    if (yeni.kod == isaretci.kod)
                    {
                        isaretci.isim = yeni.isim;
                        isaretci.fiyat = yeni.fiyat;
                        durum = false;
                    }
                    isaretci = isaretci.sonraki;
                }
                if (durum)
                {
                    guncelleTextBox1.Text = "";
                    guncelleTextBox2.Text = "";
                    guncelleTextBox3.Text = "";
                    MessageBox.Show("Kayıt Bulunamadı", "Kayıt Bulma Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilenAlan = dataGridView1.SelectedCells[0].RowIndex;
            silmeTextBox1.Text = dataGridView1.Rows[secilenAlan].Cells[0].Value.ToString();
            silmeTextBox2.Text = dataGridView1.Rows[secilenAlan].Cells[1].Value.ToString();
            silmeTextBox3.Text = dataGridView1.Rows[secilenAlan].Cells[2].Value.ToString();

            guncelleTextBox1.Text = dataGridView1.Rows[secilenAlan].Cells[0].Value.ToString();
            guncelleTextBox2.Text = dataGridView1.Rows[secilenAlan].Cells[1].Value.ToString();
            guncelleTextBox3.Text = dataGridView1.Rows[secilenAlan].Cells[2].Value.ToString();
        }
    }
}
