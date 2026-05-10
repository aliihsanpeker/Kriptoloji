using SifreliIletisim.Algoritmalar;
using System;
using System.Numerics;
using System.Text;

namespace SifreliIletisimProjesi.Algoritmalar
{
    /// <summary>
    /// RSA Asimetrik Şifreleme Algoritması
    /// 
    /// Kullanım: Anahtar alanına iki asal sayı girilir (Örn: 61,53)
    /// 
    /// Adımlar:
    /// 1. n = p * q hesaplanır (ortak anahtar bileşeni)
    /// 2. phi(n) = (p-1) * (q-1) hesaplanır (Euler totient)
    /// 3. e seçilir: 1 < e < phi(n) ve gcd(e, phi(n)) = 1
    /// 4. d hesaplanır: d * e ≡ 1 (mod phi(n))  (modüler ters)
    /// 5. Şifreleme: C = M^e mod n
    /// 6. Çözme:     M = C^d mod n
    /// </summary>
    public class RsaSifre : ISifreleme
    {
        public string Sifrele(string metin, string anahtar)
        {
            // Anahtar formatı: "p,q" (iki asal sayı)
            var asallar = AnahtariAyristir(anahtar);
            BigInteger p = asallar.Item1;
            BigInteger q = asallar.Item2;

            AsalKontrol(p, q);

            BigInteger n = p * q;
            BigInteger phi = (p - 1) * (q - 1);
            BigInteger e = PublikUstelBul(phi);

            StringBuilder sonuc = new StringBuilder();

            // Her karakteri tek tek şifrele
            for (int i = 0; i < metin.Length; i++)
            {
                BigInteger m = (BigInteger)metin[i];

                if (m >= n)
                    throw new Exception("Karakter değeri n'den büyük. Daha büyük asal sayılar kullanın.");

                // C = M^e mod n
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

            // Şifreli metin boşluklarla ayrılmış sayılardan oluşur
            string[] parcalar = sifreliMetin.Trim().Split(' ');

            foreach (string parca in parcalar)
            {
                if (string.IsNullOrWhiteSpace(parca)) continue;

                BigInteger c = BigInteger.Parse(parca);

                // M = C^d mod n
                BigInteger m = BigInteger.ModPow(c, d, n);

                sonuc.Append((char)(int)m);
            }

            return sonuc.ToString();
        }

        // ---------- Yardımcı Metotlar ----------

        /// <summary>
        /// Anahtar stringini p ve q olarak ayırır
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
        /// p ve q'nun asal olduğunu kontrol eder
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
        /// Basit asal sayı testi (küçük-orta boyutlu sayılar için)
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
        /// phi(n) ile aralarında asal olan en küçük e değerini bulur
        /// Genellikle 65537 tercih edilir, uygun değilse 3'ten itibaren arar
        /// </summary>
        private BigInteger PublikUstelBul(BigInteger phi)
        {
            // Standart RSA public exponent
            BigInteger e = 65537;
            if (e < phi && BigInteger.GreatestCommonDivisor(e, phi) == 1)
                return e;

            // 65537 uygun değilse küçükten itibaren ara
            for (e = 3; e < phi; e += 2)
            {
                if (BigInteger.GreatestCommonDivisor(e, phi) == 1)
                    return e;
            }

            throw new Exception("Uygun bir e değeri bulunamadı. Farklı asal sayılar deneyin.");
        }

        /// <summary>
        /// Genişletilmiş Öklid Algoritması ile modüler ters hesaplar
        /// d * e ≡ 1 (mod phi)
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
