using SifreliIletisimProjesi.Ortak;
using System;
using System.Collections.Generic;
using System.Text;

namespace SifreliIletisim.Algoritmalar
{
    public class HillSifre : ISifreleme
    {
        private const int Mod = 29; // Türk Alfabesi Boyutu

        public string Sifrele(string metin, string anahtar)
        {
            int[,] keyMatrix = ParseKey(anahtar);
            string temizMetin = MetinIslemleri.MetniTemizle(metin);

            // Eğer metin uzunluğu 2'nin katı değilse sonuna 'X' (veya alfabedeki son harf) ekle
            if (temizMetin.Length % 2 != 0)
            {
                temizMetin += "X";
            }

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < temizMetin.Length; i += 2)
            {
                int p1 = MetinIslemleri.Alfabe.IndexOf(temizMetin[i]);
                int p2 = MetinIslemleri.Alfabe.IndexOf(temizMetin[i + 1]);

                int c1 = (keyMatrix[0, 0] * p1 + keyMatrix[0, 1] * p2) % Mod;
                int c2 = (keyMatrix[1, 0] * p1 + keyMatrix[1, 1] * p2) % Mod;

                // Modül sonucunun negatif olmamasını garanti et
                if (c1 < 0) c1 += Mod;
                if (c2 < 0) c2 += Mod;

                sonuc.Append(MetinIslemleri.Alfabe[c1]);
                sonuc.Append(MetinIslemleri.Alfabe[c2]);
            }

            return sonuc.ToString();
        }

        public string Coz(string metin, string anahtar)
        {
            int[,] keyMatrix = ParseKey(anahtar);
            int[,] invMatrix = InvertMatrix(keyMatrix);

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < metin.Length; i += 2)
            {
                int c1 = MetinIslemleri.Alfabe.IndexOf(metin[i]);
                int c2 = MetinIslemleri.Alfabe.IndexOf(metin[i + 1]);

                int p1 = (invMatrix[0, 0] * c1 + invMatrix[0, 1] * c2) % Mod;
                int p2 = (invMatrix[1, 0] * c1 + invMatrix[1, 1] * c2) % Mod;

                if (p1 < 0) p1 += Mod;
                if (p2 < 0) p2 += Mod;

                sonuc.Append(MetinIslemleri.Alfabe[p1]);
                sonuc.Append(MetinIslemleri.Alfabe[p2]);
            }

            return sonuc.ToString();
        }

        private int[,] ParseKey(string anahtar)
        {
            try
            {
                string[] parts = anahtar.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4)
                    throw new Exception("Hill Şifreleme için 4 adet sayı (2x2 matris) girmelisiniz. Örn: 3,5,1,2");

                int[,] matrix = new int[2, 2];
                matrix[0, 0] = int.Parse(parts[0]) % Mod;
                matrix[0, 1] = int.Parse(parts[1]) % Mod;
                matrix[1, 0] = int.Parse(parts[2]) % Mod;
                matrix[1, 1] = int.Parse(parts[3]) % Mod;

                // Determinant kontrolü
                int det = (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]) % Mod;
                if (det < 0) det += Mod;

                if (GCD(det, Mod) != 1)
                    throw new Exception("Girdiğiniz matrisin tersi alınamaz (Det ve 29 aralarında asal değil). Lütfen farklı sayılar deneyin.");

                return matrix;
            }
            catch (Exception ex)
            {
                throw new Exception("Anahtar hatası: " + ex.Message);
            }
        }

        private int[,] InvertMatrix(int[,] matrix)
        {
            int det = (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]) % Mod;
            if (det < 0) det += Mod;

            int invDet = ModInverse(det, Mod);

            int[,] inv = new int[2, 2];
            // [ d -b; -c a ] * invDet
            inv[0, 0] = (matrix[1, 1] * invDet) % Mod;
            inv[0, 1] = (-matrix[0, 1] * invDet) % Mod;
            inv[1, 0] = (-matrix[1, 0] * invDet) % Mod;
            inv[1, 1] = (matrix[0, 0] * invDet) % Mod;

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                    if (inv[i, j] < 0) inv[i, j] += Mod;

            return inv;
        }

        private int ModInverse(int a, int m)
        {
            a = a % m;
            for (int x = 1; x < m; x++)
                if ((a * x) % m == 1)
                    return x;
            return 1;
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
    }
}
