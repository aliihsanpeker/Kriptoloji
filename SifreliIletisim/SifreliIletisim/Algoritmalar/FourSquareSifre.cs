using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Ortak;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SifreliIletisimProjesi.Algoritmalar
{
    public class FourSquareSifre : ISifreleme
    {
        private char[,] q1 = new char[6, 6];
        private char[,] q2 = new char[6, 6];
        private char[,] q3 = new char[6, 6];
        private char[,] q4 = new char[6, 6];

        private void MatrisleriHazirla(string anahtar1, string anahtar2)
        {
            string alfabe = MetinIslemleri.Alfabe36;

            // Q1 ve Q4 standart alfabetik matrisler
            for (int i = 0; i < 36; i++)
            {
                q1[i / 6, i % 6] = alfabe[i];
                q4[i / 6, i % 6] = alfabe[i];
            }

            // Q2 (Sağ-Üst) - 1. Anahtara göre
            string q2Dizi = AnahtardanDiziOlustur(anahtar1, alfabe);
            for (int i = 0; i < 36; i++) q2[i / 6, i % 6] = q2Dizi[i];

            // Q3 (Sol-Alt) - 2. Anahtara göre
            string q3Dizi = AnahtardanDiziOlustur(anahtar2, alfabe);
            for (int i = 0; i < 36; i++) q3[i / 6, i % 6] = q3Dizi[i];
        }

        private string AnahtardanDiziOlustur(string anahtar, string alfabe)
        {
            string temizAnahtar = MetinIslemleri.MetniTemizle36(anahtar);
            HashSet<char> kullanilanlar = new HashSet<char>();
            StringBuilder sonuc = new StringBuilder();

            foreach (char c in temizAnahtar)
            {
                if (!kullanilanlar.Contains(c))
                {
                    sonuc.Append(c);
                    kullanilanlar.Add(c);
                }
            }

            foreach (char c in alfabe)
            {
                if (!kullanilanlar.Contains(c))
                {
                    sonuc.Append(c);
                    kullanilanlar.Add(c);
                }
            }

            return sonuc.ToString();
        }

        public string Sifrele(string metin, string anahtar)
        {
            string[] anahtarlar = AnahtarCiftiniAyir(anahtar);
            MatrisleriHazirla(anahtarlar[0], anahtarlar[1]);

            string temizMetin = MetinIslemleri.MetniTemizle36(metin);
            if (temizMetin.Length % 2 != 0) temizMetin += "X"; // Tekse X ekle

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < temizMetin.Length; i += 2)
            {
                char p1 = temizMetin[i];
                char p2 = temizMetin[i + 1];

                var pos1 = KarakterBul(q1, p1);
                var pos2 = KarakterBul(q4, p2);

                sonuc.Append(q2[pos1.Item1, pos2.Item2]);
                sonuc.Append(q3[pos2.Item1, pos1.Item2]);
            }

            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            string[] anahtarlar = AnahtarCiftiniAyir(anahtar);
            MatrisleriHazirla(anahtarlar[0], anahtarlar[1]);

            if (sifreliMetin.Length % 2 != 0) return "HATA: Şifreli metin uzunluğu çift olmalıdır.";

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < sifreliMetin.Length; i += 2)
            {
                char c1 = sifreliMetin[i];
                char c2 = sifreliMetin[i + 1];

                var pos1 = KarakterBul(q2, c1);
                var pos2 = KarakterBul(q3, c2);

                sonuc.Append(q1[pos1.Item1, pos2.Item2]);
                sonuc.Append(q4[pos2.Item1, pos1.Item2]);
            }

            return sonuc.ToString();
        }

        private string[] AnahtarCiftiniAyir(string anahtar)
        {
            string[] parcalar = anahtar.Split(',');
            if (parcalar.Length >= 2) return new string[] { parcalar[0].Trim(), parcalar[1].Trim() };
            if (parcalar.Length == 1) return new string[] { parcalar[0].Trim(), parcalar[0].Trim() };
            return new string[] { "", "" };
        }

        private Tuple<int, int> KarakterBul(char[,] matris, char c)
        {
            for (int r = 0; r < 6; r++)
            {
                for (int col = 0; col < 6; col++)
                {
                    if (matris[r, col] == c) return Tuple.Create(r, col);
                }
            }
            return Tuple.Create(0, 0);
        }
    }
}
