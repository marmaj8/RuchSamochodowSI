using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie schematu kolejności świateł
     * nr w pozycje odpowiadaja zielonemu światlu w kierunkach
     * 0. v^ 
     * 1. ^ 
     * 2. v 
     * 3. <> 
     * 4. < 
     * 5. >
     * dlugosci to czas w sekundach danej pozycji
     */
    class SchematSwiatel
    {
        public List<int> pozycje { get; set; }
        public List<int> dlugosci { get; set; }

        public SchematSwiatel() { }
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
        public int PodajIloscPozycji()
        {
            return pozycje.Count();
        }
    }
}
