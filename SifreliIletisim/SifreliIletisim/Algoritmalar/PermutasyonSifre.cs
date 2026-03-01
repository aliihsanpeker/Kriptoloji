using System;
using System.Text;
using SifreliIletisimProjesi.Ortak;

namespace SifreliIletisim.Algoritmalar
{
    public class PermutasyonSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            string[] parcalar = anahtar.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] key = new int[parcalar.Length];
            for (int i = 0; i < parcalar.Length; i++)
                key[i] = int.Parse(parcalar[i].Trim()) - 1; 

            int b = key.Length; 

           
            while (metin.Length % b != 0)
                metin += "X";

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < metin.Length; i += b)
            {
                for (int j = 0; j < b; j++)
                {
                    sonuc.Append(metin[i + key[j]]);
                }
            }
            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            string[] parcalar = anahtar.Split(',');
            int[] key = new int[parcalar.Length];
            for (int i = 0; i < parcalar.Length; i++)
                key[i] = int.Parse(parcalar[i].Trim()) - 1;

            int b = key.Length;
            int[] tersKey = new int[b];

           
            for (int i = 0; i < b; i++)
                tersKey[key[i]] = i;

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < sifreliMetin.Length; i += b)
            {
                for (int j = 0; j < b; j++)
                {
                    sonuc.Append(sifreliMetin[i + tersKey[j]]);
                }
            }

           
            return sonuc.ToString().TrimEnd('X');
        }
    }
}