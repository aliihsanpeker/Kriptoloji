using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit;
using MimeKit;

namespace SifreliIletisim.Servisler
{
    public class EpostaYoneticisi
    {
        // Kayıtlı hesaplar: e-posta → uygulama şifresi
        private readonly Dictionary<string, string> _hesaplar = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "aliihsanpekerpo@gmail.com", "zrdz fbbz xkzh abrs" },
            { "batuhaneren1526@gmail.com", "kkka egrb wkly ejmz" }
        };

        // Varsayılan gönderici hesap
        private readonly string _varsayilanGonderici = "aliihsanpekerpo@gmail.com";

        /// <summary>
        /// Varsayılan gönderici hesabından alıcıya şifreli metin gönderir.
        /// </summary>
        public void EpostaGonder(string aliciEmail, string sifreliMetin)
        {
            try
            {
                string gondericiSifre = _hesaplar[_varsayilanGonderici];

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(_varsayilanGonderici, "Şifreli İletişim Botu");
                mail.To.Add(aliciEmail);
                mail.Subject = "Güvenli Gelen Mesaj";
                mail.Body = sifreliMetin;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(_varsayilanGonderici, gondericiSifre);
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("E-posta gönderilirken hata oluştu:\n" + ex.Message);
            }
        }

        /// <summary>
        /// Belirtilen e-posta adresinin gelen kutusundan en son mesajı indirir.
        /// </summary>
        public string EnSonEpostayiIndir(string hedefEmail)
        {
            // Girilen e-posta kayıtlı mı kontrol et
            if (!_hesaplar.ContainsKey(hedefEmail))
            {
                throw new Exception(
                    "Girilen e-posta adresi sistemde kayıtlı değil!\n" +
                    "Kayıtlı adresler: " + string.Join(", ", _hesaplar.Keys));
            }

            string sifre = _hesaplar[hedefEmail];

            try
            {
                using (var client = new ImapClient())
                {
                    client.Connect("imap.gmail.com", 993, true);
                    client.Authenticate(hedefEmail, sifre);
                    client.Inbox.Open(FolderAccess.ReadOnly);

                    if (client.Inbox.Count == 0)
                        return "Gelen kutusunda hiç e-posta yok.";

                    var mesaj = client.Inbox.GetMessage(client.Inbox.Count - 1);
                    client.Disconnect(true);

                    return mesaj.TextBody ?? mesaj.HtmlBody ?? "E-posta içeriği okunamadı.";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("E-posta indirilirken hata oluştu:\n" + ex.Message);
            }
        }
    }
}