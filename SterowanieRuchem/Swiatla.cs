using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie ustawien sygnalizacji
     * I obsluga zmiany swiatel
     */
    class Swiatla
    {
        public SchematSwiatel schemat { get; set; }
        public SchematSwiatel schematSi { get; set; }
        public Boolean sterowanieSI { get; set; }

        public int pozycjaWSchemacie { get; set; }
        public int pozycja { get; set; }                 // nr pozycji swiatel [0-5]
        public int[] odZmiany { get; set; }     // ile sekund minelo od zmiany na tą pozycje swiatel

        public Swiatla() { }
        public Swiatla(SchematSwiatel schemat)
        {
            this.schemat = schemat;
            pozycja = 0;
            pozycjaWSchemacie = 0;
            odZmiany = new int[] { 0, 0, 0, 0, 0, 0 };
            sterowanieSI = false;
            schematSi = null;
        }
        public Swiatla(Swiatla sw)
        {
            this.schemat = sw.schemat;
            this.sterowanieSI = false;
            this.pozycjaWSchemacie = sw.pozycjaWSchemacie;
            this.pozycja = sw.pozycja;

            this.odZmiany = new int[sw.odZmiany.Length];
            sw.odZmiany.CopyTo(this.odZmiany, 0);
            schematSi = null;
        }

        public void WylaczSi()
        {
            sterowanieSI = false;
        }
        public void UstawSchematSi(SchematSwiatel schemat)
        {
            schematSi = schemat;
            sterowanieSI = true;
        }
        
        // zmienianie swiatel w wyniku uplywu czasu
        public void Swiec(int czas = 1)
        {
            SchematSwiatel schemat;
            if (sterowanieSI)
                schemat = this.schematSi;
            else
                schemat = this.schemat;

            for (int i = 0; i < odZmiany.Length; i++)
            {
                odZmiany[i]++;
            }

            if (odZmiany[pozycja] >= schemat.PodajDlugosc(pozycja))
            {
                pozycjaWSchemacie++;
                pozycja = schemat.PodajPozycje(pozycjaWSchemacie);
                odZmiany[pozycja] = 0;
            }

            if (pozycjaWSchemacie >= schemat.PodajIloscPozycji())
                pozycjaWSchemacie = 0;
        }

        // zrodlo i kierunek wg numereacji skrzyżowania [0-3]
        // odwrotnie do ruchu wskazowek zegara
        public Boolean CzyZielone(int zrodlo, int kierunek)
        {
            switch (pozycja)
            {
                // DOL GORA
                case 0:
                    if (zrodlo == 0 && (kierunek == 1 || kierunek == 2))
                        return true;
                    if (zrodlo == 2 && (kierunek == 3 || kierunek == 0))
                        return true;
                    break;
                // DOL
                case 1:
                    if (zrodlo == 0)
                        return true;
                    if (zrodlo == 3 && kierunek == 0)
                        return true;
                    break;
                // GORA
                case 2:
                    if (zrodlo == 2)
                        return true;
                    if (zrodlo == 1 && kierunek == 2)
                        return true;
                    break;
                // LEWO PRAWO
                case 3:
                    if (zrodlo == 1 && (kierunek == 2 || kierunek == 3))
                        return true;
                    if (zrodlo == 3 && (kierunek == 0 || kierunek == 1))
                        return true;
                    break;
                // PRAWO
                case 4:
                    if (zrodlo == 1)
                        return true;
                    if (zrodlo == 0 && kierunek == 1)
                        return true;
                    break;
                // LEWO
                case 5:
                    if (zrodlo == 3)
                        return true;
                    if (zrodlo == 2 && kierunek == 3)
                        return true;
                    break;
            }
            return false;
        }

        public Boolean CzySi()
        {
            return sterowanieSI;
        }

        public int OdZmiany()
        {
            return odZmiany[pozycja];
        }
    }
}
