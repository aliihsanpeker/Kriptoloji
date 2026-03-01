using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Algoritmalar;
using SifreliIletisimProjesi.Ortak;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SifreliIletisim
{
    public partial class AnaForm : Form
    {
        public AnaForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void sifrebelirleme_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnSifrele_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Gerekli alanların boş olup olmadığını kontrol et
                if (cmbYontemSecimi.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen önce bir şifreleme yöntemi seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string secilenYontem = cmbYontemSecimi.SelectedItem.ToString();
                string hamMetin = rtbOrijinalMetin.Text;
                string anahtar = txtAnahtarGiris.Text;

                if (string.IsNullOrWhiteSpace(hamMetin) || string.IsNullOrWhiteSpace(anahtar))
                {
                    MessageBox.Show("Metin ve anahtar alanları boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Metni temizle
                string temizMetin = MetinIslemleri.MetniTemizle(hamMetin);

                // 3. Seçilen yönteme göre doğru şifreleme sınıfını oluştur
                ISifreleme sifreleyici = null;

                switch (secilenYontem)
                {
                    case "Kaydırmalı Şifreleme":
                        sifreleyici = new KaydirmaliSifre();
                        break;

                    // Arkadaşın diğer algoritmaları yazdıkça buraya eklenecek. Örnek:
                    // case "Doğrusal Şifreleme":
                    //     sifreleyici = new DogrusalSifre();
                    //     break;

                    default:
                        MessageBox.Show("Seçilen yöntem henüz koda entegre edilmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                // 4. Şifrelemeyi yap ve ekrana yazdır
                string sifreliSonuc = sifreleyici.Sifrele(temizMetin, anahtar);
                rtbGonderilecekMetin.Text = sifreliSonuc;
                // Not: RichTextBox adını hangisi yaptıysan onu kullan (örn: rtbSifreliMetinGoster)
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu. Anahtar formatını kontrol edin.\nDetay: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
