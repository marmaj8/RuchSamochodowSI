using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przetrzymywanie i obsługa upływu czasu
     * z dokladnością do godzin, minut i sekund
     */
    class Czas
    {
        public int sekund { get; set; }
        public int minut { get; set; }
        public int godzin { get; set; }

        public Czas(int s =0, int m = 0, int h = 0)
        {
            sekund = s;
            minut = m;
            godzin = h;

            PoprawZapis();
        }

        public Czas(Czas c)
        {
            sekund = c.sekund;
            minut = c.minut;
            godzin = c.godzin;
        }

        private void PoprawZapis()
        {
            if (sekund >= 60)
            {
                int tmp = sekund;
                sekund = tmp % 60;

                minut += tmp / 60;
            }
            if (minut >= 60)
            {
                int tmp = minut;
                minut = tmp % 60;

                godzin += tmp / 60;
            }
            if (godzin >= 24)
            {
                godzin = godzin % 24;
            }
        }

        public void UplywCzasu(int s = 1, int m = 0, int g = 0)
        {
            sekund += s;
            minut += m;
            godzin += g;

            PoprawZapis();
        }

        public int PodajWSekundach()
        {
            return (godzin * 60 + minut) * 60 + sekund;
        }
        
        public static int RoznicaWSekundach(Czas t1, Czas t2)
        {
            int t = t1.PodajWSekundach() - t2.PodajWSekundach();
            if (t < 0)
                t += 24 * 60 * 60;
            return t;
        }
    }
}
