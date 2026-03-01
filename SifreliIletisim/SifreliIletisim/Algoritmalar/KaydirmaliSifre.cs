using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Ortak; // MetinIslemleri sınıfına ulaşmak için
using System;
using System.Text;

namespace SifreliIletisimProjesi.Algoritmalar
{
    public class KaydirmaliSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            // Kaydırmalı şifrede anahtar bir sayıdır (Örn: 3, 5, 12). Sayıya çeviriyoruz.
            int k = int.Parse(anahtar);
            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int alfabeUzunluk = alfabe.Length; // 29

            foreach (char c in metin)
            {
                int index = alfabe.IndexOf(c);
                if (index != -1) // Karakter alfabemizde varsa
                {
                    // Yeni indeksi bul ve 29'a göre modunu al
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
                    // Çözerken geriye gidiyoruz (index - k)
                    int yeniIndex = (index - k) % alfabeUzunluk;

                    // C#'ta negatif mod düzeltmesi: Eğer sonuç eksiyse, alfabe uzunluğunu ekle.
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