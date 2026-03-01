using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Ortak; 
using System;
using System.Text;

namespace SifreliIletisimProjesi.Algoritmalar
{
    public class KaydirmaliSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            
            int k = int.Parse(anahtar);
            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int alfabeUzunluk = alfabe.Length; 

            foreach (char c in metin)
            {
                int index = alfabe.IndexOf(c);
                if (index != -1) 
                {
                 
                    int yeniIndex = (index + k) % alfabeUzunluk;
                    sonuc.Append(alfabe[yeniIndex]);
                }
            }
            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            int k = int.Parse(anahtar);
            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int alfabeUzunluk = alfabe.Length;

            foreach (char c in sifreliMetin)
            {
                int index = alfabe.IndexOf(c);
                if (index != -1)
                {
                   
                    int yeniIndex = (index - k) % alfabeUzunluk;

                    
                    if (yeniIndex < 0)
                    {
                        yeniIndex += alfabeUzunluk;
                    }
                    sonuc.Append(alfabe[yeniIndex]);
                }
            }
            return sonuc.ToString();
        }
    }
}