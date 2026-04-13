using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Ortak;
using System;
using System.Text;
using System.Globalization;

namespace SifreliIletisimProjesi.Algoritmalar
{
    public class VigenereSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            if (string.IsNullOrEmpty(metin)) return "";
            if (string.IsNullOrEmpty(anahtar)) return metin;

            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int n = alfabe.Length;

          
            string temizAnahtar = MetinIslemleri.MetniTemizle(anahtar);
            if (string.IsNullOrEmpty(temizAnahtar)) return metin;

            for (int i = 0; i < metin.Length; i++)
            {
                int pIndex = alfabe.IndexOf(metin[i]);
                if (pIndex != -1)
                {
                    
                    int kIndex = alfabe.IndexOf(temizAnahtar[i % temizAnahtar.Length]);
                    
                 
                    int yeniIndex = (pIndex + kIndex) % n;
                    sonuc.Append(alfabe[yeniIndex]);
                }
                else
                {
                    sonuc.Append(metin[i]); 
                }
            }

            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            if (string.IsNullOrEmpty(sifreliMetin)) return "";
            if (string.IsNullOrEmpty(anahtar)) return sifreliMetin;

            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int n = alfabe.Length;

            string temizAnahtar = MetinIslemleri.MetniTemizle(anahtar);
            if (string.IsNullOrEmpty(temizAnahtar)) return sifreliMetin;

            for (int i = 0; i < sifreliMetin.Length; i++)
            {
                int cIndex = alfabe.IndexOf(sifreliMetin[i]);
                if (cIndex != -1)
                {
                    int kIndex = alfabe.IndexOf(temizAnahtar[i % temizAnahtar.Length]);
                    
                
                    int yeniIndex = (cIndex - kIndex) % n;
                    if (yeniIndex < 0) yeniIndex += n;
                    
                    sonuc.Append(alfabe[yeniIndex]);
                }
                else
                {
                    sonuc.Append(sifreliMetin[i]);
                }
            }

            return sonuc.ToString();
        }
    }
}
