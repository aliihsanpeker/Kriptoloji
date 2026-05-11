using SifreliIletisim.Algoritmalar;
using SifreliIletisimProjesi.Ortak;
using System;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace SifreliIletisimProjesi.Algoritmalar
{
    /// <summary>
    
    /// </summary>
    public class RsaSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            // Diğer algoritmalarla aynı standart: büyük harf + özel karakter/boşluk temizleme
            string temizMetin = MetinIslemleri.MetniTemizle(metin);

            var asallar = AnahtariAyristir(anahtar);
            BigInteger p = asallar.Item1;
            BigInteger q = asallar.Item2;

            AsalKontrol(p, q);

            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = PublikUstelBul(phi);

            StringBuilder sonuc = new StringBuilder();

            for (int i = 0; i < temizMetin.Length; i++)
            {
                BigInteger m = (BigInteger)temizMetin[i];

                if (m >= n)
                    throw new Exception("Karakter değeri n'den büyük. Daha büyük asal sayılar kullanın.");

                BigInteger c = BigInteger.ModPow(m, e, n);

                if (i > 0) sonuc.Append(" ");
                sonuc.Append(c.ToString());
            }

            return sonuc.ToString();
        }

        public string Coz(string sifreliMetin, string anahtar)
        {
            var asallar = AnahtariAyristir(anahtar);
            BigInteger p = asallar.Item1;
            BigInteger q = asallar.Item2;

            AsalKontrol(p, q);

            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = PublikUstelBul(phi);
            BigInteger d = ModulerTers(e, phi);

            StringBuilder sonuc = new StringBuilder();

            string[] parcalar = sifreliMetin.Trim().Split(' ');

            foreach (string parca in parcalar)
            {
                if (string.IsNullOrWhiteSpace(parca)) continue;

                BigInteger c = BigInteger.Parse(parca);

                BigInteger m = BigInteger.ModPow(c, d, n);

                sonuc.Append((char)(int)m);
            }

            // Çözülen metni büyük harfe çevir (standart gereği)
            return sonuc.ToString().ToUpper(new CultureInfo("tr-TR"));
        }

        // ---------- Yardımcı Metotlar ----------

        /// <summary>
        
        /// </summary>
        private Tuple<BigInteger, BigInteger> AnahtariAyristir(string anahtar)
        {
            string[] parcalar = anahtar.Split(',');
            if (parcalar.Length != 2)
                throw new Exception("RSA anahtarı 'p,q' formatında olmalıdır. Örn: 61,53");

            BigInteger p = BigInteger.Parse(parcalar[0].Trim());
            BigInteger q = BigInteger.Parse(parcalar[1].Trim());

            return Tuple.Create(p, q);
        }

        /// <summary>
       
        /// </summary>
        private void AsalKontrol(BigInteger p, BigInteger q)
        {
            if (!AsalMi(p))
                throw new Exception($"{p} asal sayı değildir!");
            if (!AsalMi(q))
                throw new Exception($"{q} asal sayı değildir!");
            if (p == q)
                throw new Exception("p ve q farklı asal sayılar olmalıdır!");
        }

        /// <summary>
        
        /// </summary>
        private bool AsalMi(BigInteger n)
        {
            if (n < 2) return false;
            if (n < 4) return true;
            if (n % 2 == 0 || n % 3 == 0) return false;

            for (BigInteger i = 5; i * i <= n; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
       
        /// </summary>
        private BigInteger PublikUstelBul(BigInteger phi)
        {
           
            BigInteger e = 65537;
            if (e < phi && BigInteger.GreatestCommonDivisor(e, phi) == 1)
                return e;

           
            for (e = 3; e < phi; e += 2)
            {
                if (BigInteger.GreatestCommonDivisor(e, phi) == 1)
                    return e;
            }

            throw new Exception("Uygun bir e değeri bulunamadı. Farklı asal sayılar deneyin.");
        }

        /// <summary>
       
        /// </summary>
        private BigInteger ModulerTers(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger x0 = 0, x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger t = m;

                m = a % m;
                a = t;
                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0)
                x1 += m0;

            return x1;
        }
    }
}
