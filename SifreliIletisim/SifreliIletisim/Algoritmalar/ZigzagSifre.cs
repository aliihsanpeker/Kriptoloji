using System;
using System.Text;
using SifreliIletisimProjesi.Ortak;

namespace SifreliIletisim.Algoritmalar
{
    public class ZigzagSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            int derinlik = int.Parse(anahtar);
            if (derinlik <= 1 || metin.Length <= derinlik) return metin;

            string[] raylar = new string[derinlik];
            for (int i = 0; i < derinlik; i++) raylar[i] = "";

            int satir = 0;
            int yon = 1;

            foreach (char c in metin)
            {
                raylar[satir] += c;
                satir += yon;

                if (satir == 0 || satir == derinlik - 1) yon *= -1; 
            }

            return string.Join("", raylar);
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            int derinlik = int.Parse(anahtar);
            if (derinlik <= 1 || sifreliMetin.Length <= derinlik) return sifreliMetin;

            int[] uzunluklar = new int[derinlik];
            int satir = 0, yon = 1;

           
            foreach (char c in sifreliMetin)
            {
                uzunluklar[satir]++;
                satir += yon;
                if (satir == 0 || satir == derinlik - 1) yon *= -1;
            }

           
            string[] raylar = new string[derinlik];
            int index = 0;
            for (int i = 0; i < derinlik; i++)
            {
                raylar[i] = sifreliMetin.Substring(index, uzunluklar[i]);
                index += uzunluklar[i];
            }


            StringBuilder sonuc = new StringBuilder();
            int[] pointerlar = new int[derinlik];
            satir = 0; yon = 1;

            for (int i = 0; i < sifreliMetin.Length; i++)
            {
                sonuc.Append(raylar[satir][pointerlar[satir]]);
                pointerlar[satir]++;
                satir += yon;
                if (satir == 0 || satir == derinlik - 1) yon *= -1;
            }

            return sonuc.ToString();
        }
    }
}