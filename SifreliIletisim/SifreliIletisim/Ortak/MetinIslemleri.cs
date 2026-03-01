using System;
using System.Globalization;
using System.Text;

namespace SifreliIletisimProjesi.Ortak
{
    public static class MetinIslemleri
    {
        // Proje boyunca kullanacağımız standart 29 harfli Türkçe alfabe
        public const string Alfabe = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";

        public static string MetniTemizle(string hamMetin)
        {
            if (string.IsNullOrEmpty(hamMetin))
                return "";

            // 1. Önce tüm metni Türkçe karakter kurallarına göre büyük harfe çevir
            string buyukHarfliMetin = hamMetin.ToUpper(new CultureInfo("tr-TR"));

            // 2. Sadece alfabemizde olan harfleri yeni bir string'e ekle
            StringBuilder temizMetin = new StringBuilder();

            foreach (char karakter in buyukHarfliMetin)
            {
                // Eğer karakter bizim alfabemizin içinde varsa (index -1'den büyükse)
                if (Alfabe.IndexOf(karakter) >= 0)
                {
                    temizMetin.Append(karakter);
                }
            }

            return temizMetin.ToString();
        }
    }
}