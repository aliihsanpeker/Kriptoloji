using System;
using System.Text;
using SifreliIletisimProjesi.Ortak;

namespace SifreliIletisim.Algoritmalar
{
    public class YerDegistirmeSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            anahtar = anahtar.ToUpper();
            if (anahtar.Length != 29) throw new Exception("Yer değiştirme anahtarı tam olarak 29 karakterlik karışık bir alfabe olmalıdır.");

            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;

            foreach (char c in metin)
            {
                int index = alfabe.IndexOf(c);
                if (index != -1)
                {
                    sonuc.Append(anahtar[index]); 
                }
            }
            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            anahtar = anahtar.ToUpper();
            StringBuilder sonuc = new StringBuilder();
            string alfabe = MetinIslemleri.Alfabe;

            foreach (char c in sifreliMetin)
            {
                int index = anahtar.IndexOf(c); 
                if (index != -1)
                {
                    sonuc.Append(alfabe[index]); 
                }
            }
            return sonuc.ToString();
        }
    }
}