using System;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Imap;
using MailKit;

namespace SifreliIletisim.Servisler
{
    public class EpostaYoneticisi
    {
       
        private string AEmail = "aliihsanpekerpo@gmail.com";
        private string AuygulamaSifresi = "zrdz fbbz xkzh abrs";
        /* * private string BEmail = "batuhaneren34@gmail.com";
           * private string BuygulamaSifresi = "16_haneli_uygulama_sifresi";*/

        public void EpostaGonder(string aliciEmail, string sifreliMetin)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(AEmail, "Şifreli İletişim Botu");
                mail.To.Add(aliciEmail);

              
                mail.Subject = "Güvenli Gelen Mesaj";

              
                mail.Body = sifreliMetin;

            
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(AEmail, AuygulamaSifresi);
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

                    
                    client.Authenticate(AEmail, AuygulamaSifresi);
                    /* * client.Authenticate(aliciHesap, aliciSifre);*/

                   



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