using System;
using System.Text;
using SifreliIletisimProjesi.Ortak;

namespace SifreliIletisim.Algoritmalar
{
    public class RotaSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            int sutunSayisi = int.Parse(anahtar);
            int satirSayisi = (int)Math.Ceiling((double)metin.Length / sutunSayisi);

            char[,] tablo = new char[satirSayisi, sutunSayisi];
            int index = 0;

           
            for (int r = 0; r < satirSayisi; r++)
            {
                for (int c = 0; c < sutunSayisi; c++)
                {
                    tablo[r, c] = index < metin.Length ? metin[index++] : 'X';
                }
            }

            StringBuilder sonuc = new StringBuilder();
           
            for (int c = 0; c < sutunSayisi; c++)
            {
                if (c % 2 == 0)
                {
                    for (int r = 0; r < satirSayisi; r++) sonuc.Append(tablo[r, c]);
                }
                else
                {
                    for (int r = satirSayisi - 1; r >= 0; r--) sonuc.Append(tablo[r, c]);
                }
            }
            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            int sutunSayisi = int.Parse(anahtar);
            int satirSayisi = sifreliMetin.Length / sutunSayisi;
            char[,] tablo = new char[satirSayisi, sutunSayisi];
            int index = 0;

            
            for (int c = 0; c < sutunSayisi; c++)
            {
                if (c % 2 == 0)
                {
                    for (int r = 0; r < satirSayisi; r++) tablo[r, c] = sifreliMetin[index++];
                }
                else
                {
                    for (int r = satirSayisi - 1; r >= 0; r--) tablo[r, c] = sifreliMetin[index++];
                }
            }

            StringBuilder sonuc = new StringBuilder();
           
            for (int r = 0; r < satirSayisi; r++)
            {
                for (int c = 0; c < sutunSayisi; c++) sonuc.Append(tablo[r, c]);
            }
            return sonuc.ToString().TrimEnd('X');
        }
    }
}