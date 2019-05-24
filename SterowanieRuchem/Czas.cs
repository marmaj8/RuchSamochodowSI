namespace SterowanieRuchem
{
    /*
     * Przetrzymywanie i obsługa upływu czasu
     * z dokladnością do godzin, minut i sekund
     */
    class Czas
    {
        public int sekundy { get; set; }
        public int minuty { get; set; }
        public int godziny { get; set; }

        public Czas(int s =0, int m = 0, int h = 0)
        {
            sekundy = s;
            minuty = m;
            godziny = h;

            PoprawZapis();
        }

        public Czas(Czas c)
        {
            sekundy = c.sekundy;
            minuty = c.minuty;
            godziny = c.godziny;
        }

        private void PoprawZapis()
        {
            if (sekundy >= 60)
            {
                int tmp = sekundy;
                sekundy = tmp % 60;

                minuty += tmp / 60;
            }
            if (minuty >= 60)
            {
                int tmp = minuty;
                minuty = tmp % 60;

                godziny += tmp / 60;
            }
            if (godziny >= 24)
            {
                godziny = godziny % 24;
            }
        }

        public void UplywCzasu(int s = 1, int m = 0, int g = 0)
        {
            sekundy += s;
            minuty += m;
            godziny += g;

            PoprawZapis();
        }

        public int PodajWSekundach()
        {
            return (godziny * 60 + minuty) * 60 + sekundy;
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
