using System;
using System.Globalization;
using System.Text;

namespace SifreliIletisimProjesi.Ortak
{
    public static class MetinIslemleri
    {
      
        public const string Alfabe = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        public static string MetniTemizle(string hamMetin)
        {
            if (string.IsNullOrEmpty(hamMetin))
                return "";

           
            string buyukHarfliMetin = hamMetin.ToUpper(new CultureInfo("tr-TR"));

           
            StringBuilder temizMetin = new StringBuilder();

            foreach (char karakter in buyukHarfliMetin)
            {
               
                if (Alfabe.IndexOf(karakter) >= 0)
                {
                    temizMetin.Append(karakter);
                }
            }

            return temizMetin.ToString();
        }
    }
}