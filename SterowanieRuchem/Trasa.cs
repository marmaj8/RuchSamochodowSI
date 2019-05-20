using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    class Trasa
    {
        /*
         * Przechowywanie kolejnych numerow skrzyzowan na trasie
         */
        public List<int> trasa { get; private set; }
        public double koszt { get; private set; }

        public Trasa(int start)
        {
            trasa = new List<int> { start };
            koszt = 0;
        }

        public Trasa(Trasa trasa)
        {
            this.trasa = new List<int>(trasa.trasa);
            this.koszt = trasa.koszt;
        }

        public void DodajSkrzyzowanie(int id, double koszt)
        {
            trasa.Add(id);
            this.koszt += koszt;
        }

        public Boolean CzyCykl()
        {
            if (trasa.Count() > 3)
            {
                int t1 = trasa[trasa.Count() - 2];
                int t2 = trasa[trasa.Count() - 1];
                for (int i = 0; i < trasa.Count() - 2; i++)
                {
                    if (trasa[i] == t1 && trasa[i + 1] == t2)
                        return true;
                }
            }
            return false;
        }

        // Usuwanie pierwszego skrzyzowania na liscie
        // Reprezentacja ukonczenia odcinka
        public void SkasujPozycje()
        {
            trasa.RemoveAt(0);
        }
    }
}
