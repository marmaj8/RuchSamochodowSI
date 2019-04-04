using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Czas
    {
        int sekunda;
        int minuta;
        private int godzina;

        public Czas(int sekunda = 0, int minuta =0, int godzina = 0)
        {
            this.sekunda = sekunda;
            this.minuta = minuta;
            this.godzina = godzina;


            if (sekunda >= 60)
            {
                int tmp = sekunda;
                sekunda = sekunda % 60;

                this.minuta += tmp / 60;
            }
            if (minuta >= 60)
            {
                int tmp = minuta;
                minuta = minuta % 60;

                this.godzina += tmp / 60;
            }
            godzina = godzina % 24;
        }
        public void UplywSekund(int sekundy = 1)
        {
            sekunda += sekundy;
            if (sekunda == 60)
            {
                sekunda = 0;
                UplywMinut(1);
            }
        }
        public void UplywMinut(int minuty = 1)
        {
            minuta += minuty;
            if (minuta == 60)
            {
                minuta = 0;
                UplywGodzin(1);
            }
        }
        public void UplywGodzin(int godziny = 1)
        {
            godzina += godziny;
            if (godzina == 24)
            {
                godzina = 0;
            }
        }

        public int PodajGodzine()
        {
            return godzina;
        }
        public int PodajMinute()
        {
            return minuta;
        }
        public int PodajSekunde()
        {
            return sekunda;
        }

        public int PodajWSekundach()
        {
            return (godzina * 60 + minuta) * 60 + sekunda;
        }
    }
}
