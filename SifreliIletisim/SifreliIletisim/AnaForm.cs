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

            // Yöntem değiştikçe eski yazan anahtarı temizleyelim ki kafa karışmasın
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
                    case "Kaydırmalı Şifreleme": sifreleyici = new KaydirmaliSifre(); break;
                    case "Doğrusal Şifreleme": sifreleyici = new DogrusalSifre(); break;
                    case "Yer Değiştirme Şifreleme": sifreleyici = new YerDegistirmeSifre(); break;
                    case "Sayı Anahtarlı Şifreleme": sifreleyici = new SayiAnahtarliSifre(); break;
                    case "Permütasyon Şifreleme": sifreleyici = new PermutasyonSifre(); break;
                    case "Rota Şifreleme": sifreleyici = new RotaSifre(); break;
                    case "Zigzag Şifreleme": sifreleyici = new ZigzagSifre(); break;

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

        private void btnEmailGonder_Click(object sender, EventArgs e)
        {
            // Arayüzdeki kutulardan verileri al
            string alici = txtAliciEmail.Text.Trim();
            string sifreliMetin = rtbGonderilecekMetin.Text.Trim();

            // Boş alan kontrolü
            if (string.IsNullOrEmpty(alici) || string.IsNullOrEmpty(sifreliMetin))
            {
                MessageBox.Show("Lütfen önce bir metin şifreleyin ve alıcı e-posta adresini girin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // İnternet işlemi birkaç saniye sürebileceği için kullanıcıya bilgi verelim
                btnEmailGonder.Enabled = false;
                btnEmailGonder.Text = "Gönderiliyor, Lütfen Bekleyin...";
                Cursor.Current = Cursors.WaitCursor;

                // E-posta gönderim işlemini başlat
                EpostaYoneticisi mailServisi = new EpostaYoneticisi();
                mailServisi.EpostaGonder(alici, sifreliMetin);

                // Başarılı olursa
                MessageBox.Show("Şifreli metin e-posta olarak başarıyla gönderildi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Hata olursa
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // İşlem bitince (başarılı veya hatalı) butonu eski haline getir
                btnEmailGonder.Enabled = true;
                btnEmailGonder.Text = "E-POSTA GÖNDER";
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnEmailIndir_Click(object sender, EventArgs e)
        {
            try
            {
                // Kullanıcıyı bekletirken butonu deaktif yapalım
                btnEmailIndir.Enabled = false;
                btnEmailIndir.Text = "İndiriliyor, Lütfen Bekleyin...";
                Cursor.Current = Cursors.WaitCursor;

                // Servisi çağır ve en son e-postayı getir
                EpostaYoneticisi mailServisi = new EpostaYoneticisi();
                string gelenSifreliMetin = mailServisi.EnSonEpostayiIndir();

                // Gelen metni arayüzdeki "alıcımetin" isimli RichTextBox'a yazdır
                // Trim() ile e-postanın sonundaki gereksiz boşlukları temizliyoruz
                alıcımetin.Text = gelenSifreliMetin.Trim();

                MessageBox.Show("E-postalar başarıyla kontrol edildi ve son mesaj indirildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Butonu eski haline getir
                btnEmailIndir.Enabled = true;
                btnEmailIndir.Text = "E-POSTALARI İNDİR";
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnSifreCoz_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Arayüzden gerekli verileri (şifreli metin, yöntem ve anahtar) al
                string sifreliMetin = alıcımetin.Text.Trim();
                string anahtar = txtAnahtarGiris.Text.Trim();

                // 2. Boş alan kontrolleri
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

                // 3. Seçilen yönteme göre doğru sınıfı oluştur (Gönderici ile aynı mantık)
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

                    default:
                        MessageBox.Show("Seçilen yöntem henüz çözme işlemine entegre edilmedi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                // 4. Şifreyi çözme fonksiyonunu çalıştır ve sonucu ekrana yazdır
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
