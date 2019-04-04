using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Trasa
    {
        private List<int> trasa;
        private double koszt;

        public Trasa(int start)
        {
            trasa = new List<int> { start };
            koszt = 0;
        }
        public Trasa(Trasa trasa)
        {
            this.trasa = new List<int>(trasa.PodajListe());
            koszt = trasa.PodajKoszt();
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
                    if (trasa[i] == t1 && trasa[i+1] == t2)
                        return true;
                }
            }
            return false;
        }

        public double PodajKoszt()
        {
            return koszt;
        }

        public int PodajOstatni()
        {
            return trasa.Last();
        }
        public List<int> PodajListe()
        {
            return new List<int>(trasa);
        }
    }
}
