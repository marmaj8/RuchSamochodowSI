using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Swiatla
    {
        SchematSwiatel schemat;

        int pozycja;
        int dlugosc;

        public Swiatla(SchematSwiatel schemat)
        {
            this.schemat = schemat;
            pozycja = 0;
            dlugosc = 0;
        }

        public void Swiec(int czas)
        {
            dlugosc += czas;
            if (dlugosc >= schemat.PodajDlugosc(pozycja))
            {
                pozycja++;
                dlugosc = 0;
            }
        }

        public Boolean CzyZielone(int zrodlo, int kierunek)
        {
            switch(schemat.PodajPozycje(pozycja))
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
    }
}
