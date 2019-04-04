using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class SchematSwiatel
    {
        List<int> pozycje;
        List<int> dlugosci;

        public SchematSwiatel(List<int> pozycje, List<int> dlugosci)
        {
            this.pozycje = pozycje;
            this.dlugosci = dlugosci;
        }

        public int PodajPozycje(int numer)
        {
            return pozycje[numer % pozycje.Count()];
        }
        public int PodajDlugosc(int numer)
        {
            return dlugosci[numer % pozycje.Count()];
        }
    }
}
