using System;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit;

namespace SifreliIletisim.Servisler
{
    public class EpostaYoneticisi
    {
       
        private string gondericiEmail = "aliihsanpekerpo@gmail.com";
        private string uygulamaSifresi = "zrdz fbbz xkzh abrs";

        public void EpostaGonder(string aliciEmail, string sifreliMetin)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(gondericiEmail, "Şifreli İletişim Botu");
                mail.To.Add(aliciEmail);

              
                mail.Subject = "Güvenli Gelen Mesaj";

              
                mail.Body = sifreliMetin;

            
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(gondericiEmail, uygulamaSifresi);
                smtp.EnableSsl = true;

             
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
               
                throw new Exception("E-posta gönderilirken hata oluştu:\n" + ex.Message);
            }
        }


        public string EnSonEpostayiIndir()
        {
            try
            {
                using (var client = new ImapClient())
                {
                  
                    client.Connect("imap.gmail.com", 993, true);

                    
                    client.Authenticate(gondericiEmail, uygulamaSifresi);
                  /* * Gerçek senaryoda bu kısım 'gondericiEmail' yerine 'aliciEmail' (Arkadaşın) 
                     * ve o hesaba ait 'uygulamaSifresi' ile giriş yapmalıdır. 
                     * * Örnek:
                     * string aliciHesap = "arkadasin_adresi@gmail.com";
                     * string aliciSifre = "arkadasinin_16_haneli_uygulama_sifresi";
                     * client.Authenticate(aliciHesap, aliciSifre);
                     */

                    
                    client.Inbox.Open(FolderAccess.ReadOnly);

                  
                    if (client.Inbox.Count == 0)
                    {
                        return "Gelen kutusunda hiç e-posta yok.";
                    }

                   
                    var sonMesaj = client.Inbox.GetMessage(client.Inbox.Count - 1);

                
                    client.Disconnect(true);

                   
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