using System;
using System.Collections.Generic;

namespace SifreliIletisim.Algoritmalar
{
    public class HillClimbing
    {
        private Random _random = new Random();

        // Hedeflenen yükseklik
        private const double TargetHeight = 10.0;
        
        // Enerji tasarrufu katsayısı (Enerjinin hatayı ne kadar etkileyeceğini belirler)
        private const double EnergyWeight = 0.05;

        /// <summary>
        /// Hill Climbing algoritmasını kullanarak en iyi itiş gücünü bulur.
        /// </summary>
        /// <returns>En iyi itiş gücü, ulaşılan yükseklik ve harcanan enerji</returns>
        public (double BestThrust, double FinalHeight, double TotalEnergy) Optimize()
        {
            // Başlangıç durumu: Rastgele bir itiş gücü (0-20 arası)
            double currentThrust = _random.NextDouble() * 20.0;
            double currentFitness = CalculateFitness(currentThrust);

            bool improved = true;
            int iteration = 0;
            const int maxIterations = 1000;
            const double stepSize = 0.1; // Komşuluk arama adımı

            while (improved && iteration < maxIterations)
            {
                improved = false;
                iteration++;

                // Komşuları kontrol et (biraz arttır veya biraz azalt)
                double nextThrustUp = currentThrust + stepSize;
                double nextThrustDown = currentThrust - stepSize;

                double fitnessUp = CalculateFitness(nextThrustUp);
                double fitnessDown = CalculateFitness(nextThrustDown);

                // Daha iyi bir komşu bulursak oraya taşın
                if (fitnessUp > currentFitness)
                {
                    currentThrust = nextThrustUp;
                    currentFitness = fitnessUp;
                    improved = true;
                }
                else if (fitnessDown > currentFitness)
                {
                    currentThrust = nextThrustDown;
                    currentFitness = fitnessDown;
                    improved = true;
                }
            }

            double finalHeight = SimulateHeight(currentThrust);
            double totalEnergy = CalculateEnergy(currentThrust);

            return (currentThrust, finalHeight, totalEnergy);
        }

        /// <summary>
        /// Fitness (Uygunluk) Fonksiyonu: 
        /// Hata ne kadar az ve harcanan enerji ne kadar düşükse fitness o kadar yüksektir.
        /// </summary>
        private double CalculateFitness(double thrust)
        {
            if (thrust < 0) return double.MinValue; // Negatif itiş gücü mümkün değil

            double height = SimulateHeight(thrust);
            double error = Math.Abs(height - TargetHeight);
            double energy = CalculateEnergy(thrust);

            // Fitness = - (Hata + Enerji Maliyeti)
            // Maksimum fitness için bu değeri maksimize ediyoruz (negatifi minimize ediyoruz)
            return -(error + (energy * EnergyWeight));
        }

        /// <summary>
        /// Verilen itiş gücüne göre ulaşılan yüksekliği simüle eder.
        /// Örnek model: Height = Thrust * 1.5 (Basit bir lineer model)
        /// </summary>
        private double SimulateHeight(double thrust)
        {
            return thrust * 1.5; 
        }

        /// <summary>
        /// Harcanan enerjiyi hesaplar.
        /// Örnek model: Energy = Thrust^2 (Güç arttıkça enerji karesel artar)
        /// </summary>
        private double CalculateEnergy(double thrust)
        {
            return Math.Pow(thrust, 2);
        }
    }
}
