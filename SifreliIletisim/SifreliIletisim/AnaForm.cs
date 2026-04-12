using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Algoritmalar;
using SifreliIletisimProjesi.Ortak;
using SifreliIletisim.Servisler;
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
            if (cmbYontemSecimi.SelectedItem == null) return;

            string secilenYontem = cmbYontemSecimi.SelectedItem.ToString();

          
            txtAnahtarGiris.Clear();

            switch (secilenYontem)
            {
                case "Kaydırmalı Şifreleme":
                    label3.Text = "Kaydırma Sayısı (Örn: 5):";
                    break;

                case "Doğrusal Şifreleme":
                    label3.Text = "Çarpan, Eklenti (Örn: 5,7):";
                    break;

                case "Yer Değiştirme Şifreleme":
                    label3.Text = "Karmaşık Alfabe (29 Harf):";
                    break;

                case "Sayı Anahtarlı Şifreleme":
                    label3.Text = "Sayı Dizisi (Örn: 314):";
                    break;

                default:
                    label3.Text = "Anahtar / Parametre:";
                    break;
              
                case "Permütasyon Şifreleme":
                    label3.Text = "Okuma Sırası (Örn: 3,1,4,2):";
                    break;

                case "Rota Şifreleme":
                    label3.Text = "Sütun Sayısı (Örn: 4):";
                    break;

                case "Zigzag Şifreleme":
                    label3.Text = "Derinlik Sayısı (Örn: 3):";
                    break;

                case "Vigenere Şifreleme":
                    label3.Text = "Anahtar Kelime (Örn: KALEM):";
                    break;

                case "4 Kare Şifreleme":
                    label3.Text = "Çift Anahtar (Harf Bazlı, Örn: BİLGİ,HIZ):";
                    break;
            }
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

          
                string temizMetin = (secilenYontem == "4 Kare Şifreleme") 
                    ? MetinIslemleri.MetniTemizle36(hamMetin) 
                    : MetinIslemleri.MetniTemizle(hamMetin);

                ISifreleme sifreleyici = null;

                switch (secilenYontem)
                {
                    case "Kaydırmalı Şifreleme": sifreleyici = new KaydirmaliSifre(); break;
                    case "Doğrusal Şifreleme": sifreleyici = new DogrusalSifre(); break;
                    case "Yer Değiştirme Şifreleme": sifreleyici = new YerDegistirmeSifre(); break;
                    case "Sayı Anahtarlı Şifreleme": sifreleyici = new SayiAnahtarliSifre(); break;
                    case "Permütasyon Şifreleme": sifreleyici = new PermutasyonSifre(); break;
                    case "Rota Şifreleme": sifreleyici = new RotaSifre(); break;
                    case "Zigzag Şifreleme": sifreleyici = new ZigzagSifre(); break;
                    case "Vigenere Şifreleme": sifreleyici = new VigenereSifre(); break;
                    case "4 Kare Şifreleme": sifreleyici = new FourSquareSifre(); break;

                    default:
                        MessageBox.Show("Seçilen yöntem henüz koda entegre edilmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                string sifreliSonuc = sifreleyici.Sifrele(temizMetin, anahtar);
                rtbGonderilecekMetin.Text = sifreliSonuc;
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu. Anahtar formatını kontrol edin.\nDetay: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEmailGonder_Click(object sender, EventArgs e)
        {
           
            string alici = txtAliciEmail.Text.Trim();
            string sifreliMetin = rtbGonderilecekMetin.Text.Trim();

           
            if (string.IsNullOrEmpty(alici) || string.IsNullOrEmpty(sifreliMetin))
            {
                MessageBox.Show("Lütfen önce bir metin şifreleyin ve alıcı e-posta adresini girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
              
                btnEmailGonder.Enabled = false;
                btnEmailGonder.Text = "Gönderiliyor, Lütfen Bekleyin...";
                Cursor.Current = Cursors.WaitCursor;

             
                EpostaYoneticisi mailServisi = new EpostaYoneticisi();
                mailServisi.EpostaGonder(alici, sifreliMetin);

              
                MessageBox.Show("Şifreli metin e-posta olarak başarıyla gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
         
                btnEmailGonder.Enabled = true;
                btnEmailGonder.Text = "E-POSTA GÖNDER";
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnEmailIndir_Click(object sender, EventArgs e)
        {
            try
            {
            
                btnEmailIndir.Enabled = false;
                btnEmailIndir.Text = "İndiriliyor, Lütfen Bekleyin...";
                Cursor.Current = Cursors.WaitCursor;

             
                EpostaYoneticisi mailServisi = new EpostaYoneticisi();
                string gelenSifreliMetin = mailServisi.EnSonEpostayiIndir();

               
                alıcımetin.Text = gelenSifreliMetin.Trim();

                MessageBox.Show("E-postalar başarıyla kontrol edildi ve son mesaj indirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
         
                btnEmailIndir.Enabled = true;
                btnEmailIndir.Text = "E-POSTALARI İNDİR";
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSifreCoz_Click(object sender, EventArgs e)
        {
            try
            {
              
                string sifreliMetin = alıcımetin.Text.Trim();
                string anahtar = txtAnahtarGiris.Text.Trim();

    
                if (string.IsNullOrEmpty(sifreliMetin))
                {
                    MessageBox.Show("Çözülecek şifreli metin bulunamadı! Lütfen önce e-postaları indirin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbYontemSecimi.SelectedItem == null)
                {
                    MessageBox.Show("Lütfen şifreyi çözmek için üst taraftan bir yöntem seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(anahtar))
                {
                    MessageBox.Show("Şifreyi çözmek için anahtar girmelisiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string secilenYontem = cmbYontemSecimi.SelectedItem.ToString();

               
                ISifreleme sifreleyici = null;

                switch (secilenYontem)
                {
                    case "Kaydırmalı Şifreleme": sifreleyici = new KaydirmaliSifre(); break;
                    case "Doğrusal Şifreleme": sifreleyici = new DogrusalSifre(); break;
                    case "Yer Değiştirme Şifreleme": sifreleyici = new YerDegistirmeSifre(); break;
                    case "Sayı Anahtarlı Şifreleme": sifreleyici = new SayiAnahtarliSifre(); break;
                    case "Permütasyon Şifreleme": sifreleyici = new PermutasyonSifre(); break;
                    case "Rota Şifreleme": sifreleyici = new RotaSifre(); break;
                    case "Zigzag Şifreleme": sifreleyici = new ZigzagSifre(); break;
                    case "Vigenere Şifreleme": sifreleyici = new VigenereSifre(); break;
                    case "4 Kare Şifreleme": sifreleyici = new FourSquareSifre(); break;

                    default:
                        MessageBox.Show("Seçilen yöntem henüz çözme işlemine entegre edilmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

             
                string cozulmusSonuc = sifreleyici.Coz(sifreliMetin, anahtar);
                rtbCozulmusMetin.Text = cozulmusSonuc;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Şifre çözülürken bir hata oluştu. Girdiğiniz anahtarın doğruluğunu kontrol edin.\nDetay: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtAnahtarGiris_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void rtbCozulmusMetin_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
