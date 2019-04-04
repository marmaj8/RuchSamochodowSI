using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class CzasPrzejazdu
    {
        int dlugosc;
        Czas czas;

        public CzasPrzejazdu(int dlugosc, int sekunda, int minuta, int godzina)
        {
            czas = new Czas(sekunda, minuta, godzina);
            this.dlugosc = dlugosc;
        }

        public int PodajDlugosc()
        {
            return dlugosc;
        }

        public Boolean CzyMinelo(Czas czas, Czas minelo)
        {
            if (this.czas.PodajWSekundach() <= (czas.PodajWSekundach() - minelo.PodajWSekundach()) % (24 * 60 * 60))
                return true;
            else
                return false;
        }
    }
}
