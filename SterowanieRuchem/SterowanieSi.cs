using AForge.Neuro;
using AForge.Neuro.Learning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    class SterowanieSi
    {
        //static double PROG_NAUKI = 0.1;    // dokladnosc wstepnego uczenia
        static double PROG_NAUKI = 100;    // dokladnosc wstepnego uczenia
        public static int CO_ILE_ZMIAN_SWIATEL = 30;
        public static int WIELKOSC_WEJSCIE = 86;
        public static int WIELKOSC_UKRYTE = 46;
        public static int WIELKOSC_WYJSCIE = 6;

        static double MAX_PRZEJSC_BEZ_ZMIANY = 100;

        ActivationNetwork siecNeuronowa;
        //BackPropagationLearning nauczyciel;
        EvolutionaryLearning ewolutor;


        List<ZestawDanychSieci> zestawyUczace;

        /* DANE WEJSCIOWE
         * 
         * Dane odcinka wejsciowego skryżowania x 4     = 32
         * - idealny czas odcinka
         * - średni czas odcinka przez ostatnia godzine
         * - średni czas odcinka dla tej godziny poprzedniego dnia
         * - ilość pojazdów na odcinku
         * - średni czas odcinka dla tej godziny poprzedniego dnia
         * - czy coś na kierunku [0, 1] x 3     (lewo prosto prawo)
         * 
         * Czas od zmiany na pozycje swiatel x 6
         * 
         * Dane sasiadow x 4                = 48
         * - Dance odcinka wejsciowego x 4
         * - - idealny czas odcinka
         * - - średni czas odcinka przez ostatnia godzine
         * - - ilosc na odcinku
         * 
         * RAZEM 86
         */
        public SterowanieSi()
        {
            zestawyUczace = new List<ZestawDanychSieci>();
            siecNeuronowa = new ActivationNetwork(
                  new SigmoidFunction(), // funkcja aktywacji
                  WIELKOSC_WEJSCIE,                    // wielkosc wejscia
                  WIELKOSC_UKRYTE,                     // wielkosc 1. warstwy ukrytej (kolejne po przecinku)
                  WIELKOSC_WYJSCIE                      // wielkoscy wyjscia
                  );

            siecNeuronowa.Randomize();

            //nauczyciel = new BackPropagationLearning(siecNeuronowa);
            //nauczyciel.LearningRate = 0.1;

            ewolutor = new EvolutionaryLearning(siecNeuronowa, 100);

            UczenieWstepne();
        }
        
        public void ZaladujSiecZPliku(string plik)
        {
            siecNeuronowa = (ActivationNetwork) ActivationNetwork.Load(plik);
        }

        public void ZapiszSiecDoPliku(string plik)
        {
            siecNeuronowa.Save(plik);
        }

        void WygenerujZestawUczacy()
        {

            ZestawDanychSieci zestaw = new ZestawDanychSieci(0);
            bool t = true;

            Random rand = new Random();
            int pos = rand.Next(6);
            int max = rand.Next(40, 300);
            int maxP = rand.Next(15, 30);
            int maxS = rand.Next(40, 300);
            int maxPS = rand.Next(15, 30);
            int idealny;
            int sredni;
            int sredniSt;
            int pojazdow;
            int pojazdowSt;
            bool[] s = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                if (rand.Next(2) == 1)
                    s[i] = true;
                else
                    s[i] = false;
            }
            double[] czasy = new double[6];
            for(int i = 0; i < 6; i++)
            {
                czasy[i] = CO_ILE_ZMIAN_SWIATEL * rand.Next(1,10);
            }
            double czMax = czasy.Max() + CO_ILE_ZMIAN_SWIATEL;
            switch (pos)
            {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0 || i == 1)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[0] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if ( i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP)+5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 1, 0, 0, 0, 0, 0 });
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[1] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 1, 0, 0, 0, 0 });
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 2)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[2] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 1, 0, 0, 0 });
                    break;
                case 3:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1 || i == 3)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[3] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 0, 1, 0, 0 });
                    break;
                case 4:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[4] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 0, 0, 1, 0 });
                    break;
                case 5:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 3)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[5] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 0, 0, 0, 1 });
                    break;
            }

            zestawyUczace.Add(zestaw);
        }
        void WygenerujZestawUczacy(int pos)
        {

            ZestawDanychSieci zestaw = new ZestawDanychSieci(0);
            bool t = true;
            bool f = false;

            Random rand = new Random();
            int max = rand.Next(40, 300);
            int maxP = rand.Next(15, 30);
            int maxS = rand.Next(40, 300);
            int maxPS = rand.Next(15, 30);
            int idealny;
            int sredni;
            int sredniSt;
            int pojazdow;
            int pojazdowSt;
            bool[] s = new bool[3];
            for (int i = 0; i < 3; i++)
            {
                if (rand.Next(2) == 1)
                    s[i] = true;
                else
                    s[i] = false;
            }
            double[] czasy = new double[6];
            for (int i = 0; i < 6; i++)
            {
                czasy[i] = CO_ILE_ZMIAN_SWIATEL * rand.Next(1, 10);
            }
            double czMax = czasy.Max() + CO_ILE_ZMIAN_SWIATEL;
            switch (pos)
            {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0 || i == 1)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[0] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 1, 0, 0, 0, 0, 0 });
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[1] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 1, 0, 0, 0, 0 });
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 2)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[2] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 1, 0, 0, 0 });
                    break;
                case 3:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1 || i == 3)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[3] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 0, 1, 0, 0 });
                    break;
                case 4:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 1)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[4] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 0, 0, 1, 0 });
                    break;
                case 5:
                    for (int i = 0; i < 4; i++)
                    {
                        if (i == 3)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(20) + max;
                            sredniSt = idealny + rand.Next(20) + max;
                            pojazdow = maxP + rand.Next(10) + 10;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, t, t, t);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(max - 10);
                            sredniSt = idealny + rand.Next(max - 10);
                            pojazdow = rand.Next(maxP) + 5;
                            pojazdowSt = pojazdow * 9 + rand.Next(pojazdow);

                            zestaw.UstawDanePasa(i, idealny, sredni, sredniSt, pojazdow, pojazdowSt, s[0], s[1], s[2]);
                        }
                    }
                    czasy[5] = czMax;
                    zestaw.UstawZmianyCzasow(czasy);
                    for (int i = 0; i < 16; i++)
                    {
                        if (i == 0)
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS - 10);
                            pojazdow = (rand.Next(maxP) + 5) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                        else
                        {
                            idealny = rand.Next(40, 300);
                            sredni = idealny + rand.Next(maxS);
                            pojazdow = (rand.Next(maxP) + 5 + rand.Next(10)) * 9;
                            zestaw.UstawPasSasiada(i, idealny, sredni, pojazdow);
                        }
                    }
                    zestaw.UstawWyjscie(new double[] { 0, 0, 0, 0, 0, 1 });
                    break;
            }

            zestawyUczace.Add(zestaw);
        }

        void UczenieWstepne()
        {
            double blad = 1;
            double poprzedni_blad = 1;
            double odZmiany = 0;
            Boolean czyUczyc = true;

            int j = 0;
            for (int i = 0; i < 6; i++)
            {
                WygenerujZestawUczacy(j % 6);
                j++;
            }
            List<double[]> wejscie = new List<double[]>();
            List<double[]> wyjscie = new List<double[]>();
            foreach (ZestawDanychSieci zestawDanych in zestawyUczace)
            {
                zestawDanych.NormalizujWejscie();
                wejscie.Add(zestawDanych.TablicaWejscia());
                wyjscie.Add(zestawDanych.TablicaWyjscia());
            }

            double[][] wejscieTab = wejscie.ToArray();
            double[][] wyjscieTab = wyjscie.ToArray();

            while (czyUczyc)
            {
                // run epoch of learning procedure
                //blad = nauczyciel.RunEpoch(wejscieTab, wyjscieTab);
                blad = ewolutor.RunEpoch(wejscieTab, wyjscieTab);

                if (blad <= PROG_NAUKI)
                {
                    czyUczyc = false;
                }
                else if (poprzedni_blad == blad)
                {
                    if (odZmiany >= MAX_PRZEJSC_BEZ_ZMIANY)
                    {
                        czyUczyc = false;
                    }
                    else
                    {
                        odZmiany++;
                    }
                }
                else
                {
                    poprzedni_blad = blad;
                    odZmiany = 0 ;
                }
            }
            zestawyUczace.Clear();
        }

        public SchematSwiatel GenSchemat(int nr, Mapa mapa, DaneORuchu baza, DaneORuchu kontrolne, Czas czas)
        {
            ZestawDanychSieci zestaw = new ZestawDanychSieci(nr);

            Skrzyzowanie sk = mapa.PodajSkrzyzowanie(nr);

            // Pobranie danych o ruchu wejsciowym na skrzyzowanie
            int nrDrogiSkrzyzowania = 0;
            foreach (int zrodlo in sk.PodajZrodla4Strony())
            {
                if ( zrodlo == -1)
                {
                    zestaw.UstawDanePasa(nrDrogiSkrzyzowania, 0, 0, 0, 0, 0, false, false, false);
                }
                else
                {
                    zestaw.UstawDanePasa(
                        nrDrogiSkrzyzowania,
                        sk.CzasDojazdu(zrodlo),
                        baza.PodajSredniCzas(zrodlo, sk.PodajId()),
                        kontrolne.PodajSredniCzas(zrodlo, sk.PodajId(), czas.godzin),
                        baza.PojazdowNaOdcinku(zrodlo, sk.PodajId()),
                        kontrolne.PodajIlePojazdowWgodzine(zrodlo, sk.PodajId(), czas.godzin),
                        sk.CzyOczekujeWKierunku(zrodlo, 0),
                        sk.CzyOczekujeWKierunku(zrodlo, 1),
                        sk.CzyOczekujeWKierunku(zrodlo, 2)
                        );
                }
                nrDrogiSkrzyzowania++;
            }

            // Kiedy byla zmiana swiatel na dana pozycje
            int[] zmianySwiatel = sk.PodajKiedZmienionoSwiatlaNaPozycje().ToArray();
            zestaw.UstawZmianyCzasow(new double[] {
                zmianySwiatel[0],
                zmianySwiatel[1],
                zmianySwiatel[2],
                zmianySwiatel[3],
                zmianySwiatel[4],
                zmianySwiatel[5]
                }
            );

            // Pobranie danych o ruchu na sasiednich skrzyzowanaich
            int nrSasiadaSkrzyzowania = 0;
            foreach (int saId in sk.PodajKierunki())
            {
                nrDrogiSkrzyzowania = 0;
                if (saId == -1)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        zestaw.UstawPasSasiada(
                            nrSasiadaSkrzyzowania * 4 + nrDrogiSkrzyzowania,
                            0,
                            0,
                            0
                            );
                        nrDrogiSkrzyzowania++;
                    }
                }
                else
                {
                    Skrzyzowanie sa = mapa.PodajSkrzyzowanie(saId);

                    foreach (int zr in sa.PodajZrodla4Strony())
                    {
                        if (zr == -1)
                        {
                            zestaw.UstawPasSasiada(
                                nrSasiadaSkrzyzowania * 4 + nrDrogiSkrzyzowania,
                                0,
                                0,
                                0
                                );
                        }
                        else
                        {
                            zestaw.UstawPasSasiada(
                                nrSasiadaSkrzyzowania * 4 + nrDrogiSkrzyzowania,
                                sa.CzasDojazdu(zr),
                                baza.PodajSredniCzas(zr, saId),
                                baza.PodajIlePojazdowWgodzine(zr, saId)
                                );
                        }
                        nrDrogiSkrzyzowania++;
                    }
                }
                nrSasiadaSkrzyzowania++;
            }

            // WRZUCENIE NA SIEĆ
            zestaw.NormalizujWejscie();
            double[] wyjscie = siecNeuronowa.Compute( zestaw.TablicaWejscia() );
            zestaw.UstawWyjscie(wyjscie);
            zestawyUczace.Add(zestaw);
            
            double max = 0;
            int wynik = 0;
            for (int i = 0; i < 6; i++)
            {
                if (wyjscie[i] >= max)
                {
                    wynik = i;
                    max = wyjscie[i];
                }
            }
            
            SchematSwiatel schemat;
            switch( wynik )
            {
                case 0:
                    schemat = new SchematSwiatel(new List<int> { 0 }, new List<int> { 60 });
                    break;
                case 1:
                    schemat = new SchematSwiatel(new List<int> { 1 }, new List<int> { 60 });
                    break;
                case 2:
                    schemat = new SchematSwiatel(new List<int> { 2 }, new List<int> { 60 });
                    break;
                case 3:
                    schemat = new SchematSwiatel(new List<int> { 3 }, new List<int> { 60 });
                    break;
                case 4:
                    schemat = new SchematSwiatel(new List<int> { 4 }, new List<int> { 60 });
                    break;
                default:
                    schemat = new SchematSwiatel(new List<int> { 5 }, new List<int> { 60 });
                    break;
            }

            return schemat;
            
                //return new SchematSwiatel(new List<int> { }, new List<int> { });
        }
    }
}
