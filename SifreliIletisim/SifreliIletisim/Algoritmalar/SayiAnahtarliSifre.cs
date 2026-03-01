using System;
using System.Text;
using SifreliIletisimProjesi.Ortak;

namespace SifreliIletisim.Algoritmalar
{
    public class SayiAnahtarliSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int m = alfabe.Length;

            int anahtarUzunluk = anahtar.Length;
            int j = 0; 

            foreach (char c in metin)
            {
                int x = alfabe.IndexOf(c);
                if (x != -1)
                {
                    
                    int kaydirma = int.Parse(anahtar[j].ToString());

                    int yeniIndex = (x + kaydirma) % m;
                    sonuc.Append(alfabe[yeniIndex]);

                    j = (j + 1) % anahtarUzunluk; 
                }
            }
            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int m = alfabe.Length;

            int anahtarUzunluk = anahtar.Length;
            int j = 0;

            foreach (char c in sifreliMetin)
            {
                int y = alfabe.IndexOf(c);
                if (y != -1)
                {
                    int kaydirma = int.Parse(anahtar[j].ToString());

                    int yeniIndex = (y - kaydirma) % m;
                    if (yeniIndex < 0) yeniIndex += m; 

                    sonuc.Append(alfabe[yeniIndex]);

                    j = (j + 1) % anahtarUzunluk;
                }
            }
            return sonuc.ToString();
        }
    }
}