using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    class ZestawDanychSieci
    {
        public int NrSkrzyzowania { get; }
        double[] idealneCzasy;
        double[] srednieCzasy;
        double[] srednieStareCzasy;
        double[] pojazdy;
        double[] pojazdyStare;
        double[] czyOczekujeLewo;
        double[] czyOczekujeProsto;
        double[] czyOczekujePrawo;

        double[] odOstaniejZmianSwiatleNa;

        double[] idealneCzasySasiadow;
        double[] srednieCzasySasiadow;
        double[] pojazdySasiadow;
        double[] opoxnienieSasaiada;

        double[] wyjscie;

        double sumaSrednichStarych;

        public ZestawDanychSieci(int nr)
        {
            NrSkrzyzowania = nr;
            idealneCzasy = new double[4];
            srednieCzasy = new double[4];
            srednieStareCzasy = new double[4];
            pojazdy = new double[4];
            pojazdyStare = new double[4];
            czyOczekujeLewo = new double[4];
            czyOczekujeProsto = new double[4];
            czyOczekujePrawo = new double[4];

            odOstaniejZmianSwiatleNa = new double[6];

            idealneCzasySasiadow = new double[16];
            srednieCzasySasiadow = new double[16];
            pojazdySasiadow = new double[16];
            opoxnienieSasaiada = new double[4];

            wyjscie = new double[6];

            sumaSrednichStarych = 0;
        }

        public void UstawDanePasa(int nr, double idealny, double srednia, double sredniaSt, double ile, double ileSt, Boolean lewo, Boolean prosto, Boolean prawo)
        {
            sumaSrednichStarych -= srednieStareCzasy[nr];
            sumaSrednichStarych += sredniaSt;

            idealneCzasy[nr] = idealny;
            srednieCzasy[nr] = srednia;
            srednieStareCzasy[nr] = sredniaSt;
            pojazdy[nr] = ile;
            pojazdyStare[nr] = ileSt;

            if (lewo)
                czyOczekujeLewo[nr] = 1;
            if (prosto)
                czyOczekujeProsto[nr] = 1;
            if (prawo)
                czyOczekujePrawo[nr] = 1;
        }
        public void UstawZmianyCzasow(double [] czasy)
        {
            czasy.CopyTo(odOstaniejZmianSwiatleNa, 0);
        }
        public void UstawPasSasiada(int nr, double idealny, double srednia, double ile)
        {
            idealneCzasySasiadow[nr] = idealny;
            srednieCzasySasiadow[nr] = srednia;
            pojazdySasiadow[nr] = ile;
        }
        public void UstawWyjscie(double [] wyjscie)
        {
            wyjscie.CopyTo(this.wyjscie, 0);
        }

        private void ZanegujWyjscie()
        {
            double max = wyjscie.Max();
            for(int i =0; i < 6; i++)
            {
                if (wyjscie[i] == max)
                    wyjscie[i] = 0;
                else
                    wyjscie[i] = 0.5;
            }
        }

        private void WzmocnijWyjscie()
        {
            double max = wyjscie.Max();
            for (int i = 0; i < 6; i++)
            {
                if (wyjscie[i] == max)
                    wyjscie[i] = 1;
            }
        }

        // Sprawdzenie czy dzialanie SI dalo dobry efekt
        // i przeliczenie wyjscia w celu nauki
        public void SprawdxPoprawnosc(double srednia)
        {
            if (srednia <= sumaSrednichStarych)
                WzmocnijWyjscie();
            else
                ZanegujWyjscie();
        }

        // Tablica danych wejsciowych do sieci neuronowej
        public double[] TablicaWejscia()
        {
            double[] tab = new double[86];

            int j = 0;
            for(int i = 0; i < 4; i++)
            {
                tab[j] = idealneCzasy[i];
                j++;
                tab[j] = srednieCzasy[i];
                j++;
                tab[j] = srednieStareCzasy[i];
                j++;
                tab[j] = pojazdy[i];
                j++;
                tab[j] = pojazdyStare[i];
                j++;
                tab[j] = czyOczekujeLewo[i];
                j++;
                tab[j] = czyOczekujeProsto[i];
                j++;
                tab[j] = czyOczekujePrawo[i];
                j++;
            }

            for(int i = 0; i < 6; i++)
            {
                tab[j] = odOstaniejZmianSwiatleNa[i];
                j++;
            }

            for(int i = 0; i < 16; i++)
            {
                tab[j] = idealneCzasySasiadow[i];
                j++;
                tab[j] = srednieCzasySasiadow[i];
                j++;
                tab[j] = pojazdySasiadow[i];
                j++;
            }

            return tab;
        }
        
        public double[] TablicaWyjscia()
        {
            return wyjscie;
        }

        public void NormalizujWejscie()
        {
            //NORMALIZACJA DROG DOCHODZACY DO SKRZYZOWANIA
            double idMax = idealneCzasy.Max();
            double poMax = pojazdy.Max();
            double poStMax = pojazdyStare.Max();

            for (int i = 0; i < 4; i++)
            {
                srednieCzasy[i] = srednieCzasy[i] - idealneCzasy[i];
                srednieStareCzasy[i] = srednieStareCzasy[i] - idealneCzasy[i];

                if (idMax != 0)
                    idealneCzasy[i] = idealneCzasy[i] / idMax;
                if (poMax != 0)
                    pojazdy[i] = pojazdy[i] / poMax;
                if (poStMax != 0)
                    pojazdyStare[i] = pojazdyStare[i] / poStMax;
            }
            double srMax = srednieCzasy.Max();
            double srStMax = srednieStareCzasy.Max();
            for (int i = 0; i < 4; i++)
            {
                if (srMax != 0)
                    srednieCzasy[i] = srednieCzasy[i] / srMax;
                if (srStMax != 0)
                    srednieStareCzasy[i] = srednieStareCzasy[i] / srStMax;
            }


            // NORMALIZACJA CZASOW ZMIAN SWIATEL
            double tMax = odOstaniejZmianSwiatleNa.Max();
            for (int i = 0; i < 6; i++)
            {
                if (tMax != 0)
                    odOstaniejZmianSwiatleNa[i] = odOstaniejZmianSwiatleNa[i] / tMax;
            }

            // NORMALIZACJA DANYCH SASIADOW
            double idSaMax = idealneCzasySasiadow.Max();
            double poSaMax = pojazdySasiadow.Max();
            for(int i = 0; i < 16; i++)
            {
                if (poSaMax != 0)
                    pojazdySasiadow[i] = pojazdySasiadow[i] / poSaMax;
                srednieCzasySasiadow[i] = srednieCzasySasiadow[i] - idealneCzasySasiadow[i];
                if(idSaMax != 0)
                    idealneCzasySasiadow[i] = idealneCzasySasiadow[i] / idSaMax;
            }
            double srSaMax = srednieCzasySasiadow.Max();
            for (int i = 0; i < 16; i++)
            {
                opoxnienieSasaiada[i / 4] += srednieCzasySasiadow[i];

                if (srSaMax != 0)
                    srednieCzasySasiadow[i] = srednieCzasySasiadow[i] / srSaMax;
            }
            double opSaMax = opoxnienieSasaiada.Max();
            for(int i = 0; i < 4; i++)
            {
                if (opSaMax != 0)
                    opoxnienieSasaiada[i] = opoxnienieSasaiada[i] / opSaMax;
            }
        }
    }
}
