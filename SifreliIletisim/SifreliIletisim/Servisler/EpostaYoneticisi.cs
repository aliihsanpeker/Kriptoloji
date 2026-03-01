using System;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit;

namespace SifreliIletisim.Servisler
{
    public class EpostaYoneticisi
    {
        // Kendi aldığın proje mailini ve 16 haneli uygulama şifresini (boşluksuz) buraya yaz
        private string gondericiEmail = "aliihsanpekerpo@gmail.com";
        private string uygulamaSifresi = "zrdz fbbz xkzh abrs";

        public void EpostaGonder(string aliciEmail, string sifreliMetin)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(gondericiEmail, "Şifreli İletişim Botu");
                mail.To.Add(aliciEmail);

                // Konu başlığı standart olabilir
                mail.Subject = "Güvenli Gelen Mesaj";

                // HOCANIN KURALI: Sadece şifreli metin gidiyor, hiçbir ipucu yok!
                mail.Body = sifreliMetin;

                // Gmail'in SMTP sunucu ayarları
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(gondericiEmail, uygulamaSifresi);
                smtp.EnableSsl = true;

                // E-postayı gönder
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // Bir hata çıkarsa form ekranına yansıtmak için hatayı fırlatıyoruz
                throw new Exception("E-posta gönderilirken hata oluştu:\n" + ex.Message);
            }
        }

        // ... (Önceki EpostaGonder metodu burada duruyor)

        public string EnSonEpostayiIndir()
        {
            try
            {
                using (var client = new ImapClient())
                {
                    // Gmail'in IMAP sunucusuna bağlanıyoruz
                    client.Connect("imap.gmail.com", 993, true);

                    // Koda gömdüğün aynı e-posta ve 16 haneli şifre ile giriş yapıyoruz
                    client.Authenticate(gondericiEmail, uygulamaSifresi);

                    // Gelen kutusunu açıyoruz (Sadece okuma modunda)
                    client.Inbox.Open(FolderAccess.ReadOnly);

                    // Eğer kutu boşsa uyarı ver
                    if (client.Inbox.Count == 0)
                    {
                        return "Gelen kutusunda hiç e-posta yok.";
                    }

                    // En son gelen e-postayı al (İndeks 0'dan başladığı için Count - 1)
                    var sonMesaj = client.Inbox.GetMessage(client.Inbox.Count - 1);

                    // Sunucuyla bağlantıyı kes
                    client.Disconnect(true);

                    // Sadece e-postanın metin gövdesini döndür (Html yoksa düz metni alır)
                    return sonMesaj.TextBody ?? sonMesaj.HtmlBody ?? "E-posta içeriği okunamadı.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("E-postalar indirilirken bir hata oluştu:\n" + ex.Message);
            }
        }
    }
}