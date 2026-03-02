using System;
using System.Text;
using SifreliIletisimProjesi.Ortak;

namespace SifreliIletisim.Algoritmalar
{
    public class DogrusalSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            
            string[] parcalar = anahtar.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parcalar.Length != 2) throw new Exception("Doğrusal şifreleme için anahtar 'a,b' formatında olmalıdır (Örn: 5,7).");

            int a = int.Parse(parcalar[0].Trim());
            int b = int.Parse(parcalar[1].Trim());

            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int m = alfabe.Length; 

            foreach (char c in metin)
            {
                int x = alfabe.IndexOf(c);
                if (x != -1)
                {
                    int yeniIndex = ((a * x) + b) % m;
                    sonuc.Append(alfabe[yeniIndex]);
                }
            }
            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            string[] parcalar = anahtar.Split(',');
            int a = int.Parse(parcalar[0].Trim());
            int b = int.Parse(parcalar[1].Trim());

            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;
            int m = alfabe.Length;

           
            int aTers = 0;
            for (int i = 0; i < m; i++)
            {
                if ((a * i) % m == 1)
                {
                    aTers = i;
                    break;
                }
            }

            if (aTers == 0) throw new Exception("Seçilen 'a' çarpanının 29'a göre tersi yok! Başka bir sayı seçin.");

            foreach (char c in sifreliMetin)
            {
                int y = alfabe.IndexOf(c);
                if (y != -1)
                {
                    int yeniIndex = (aTers * (y - b)) % m;
                    if (yeniIndex < 0) yeniIndex += m; 

                    sonuc.Append(alfabe[yeniIndex]);
                }
            }
            return sonuc.ToString();
        }
    }
}