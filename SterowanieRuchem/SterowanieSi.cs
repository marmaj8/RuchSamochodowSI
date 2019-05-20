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
        public static int CO_ILE_ZMIAN_SWIATEL = 30;
        static double PROG_NAUKI = 0.5;    // dokladnosc wstepnego uczenia
        static double MAX_PRZEJSC_BEZ_ZMIANY = 100;

        ActivationNetwork siecNeuronowa;
        BackPropagationLearning nauczyciel;

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

            siecNeuronowa = new ActivationNetwork(
                  new SigmoidFunction(), // funkcja aktywacji
                  86,                    // wielkosc wejscia
                  86*2,                     // wielkosc 1. warstwy ukrytej (kolejne po przecinku)
                  86*2,
                  6                      // wielkoscy wyjscia
                  );

            siecNeuronowa.Randomize();
            nauczyciel = new BackPropagationLearning(siecNeuronowa);

            UczenieWstepne();
        }

        void UczenieWstepne()
        {
            // zestaw nauczania wstepnego
            double[][] zestaw_wejsciowy = new double[][]{
                new double[]{
                    //idealnie, aktualnie, wczesniej, pojazdo, wczesniej czy, czy, czy,
                    60, 120, 120, 25, 25, 0, 1, 1,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 120, 120, 25, 25, 0, 1, 1,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 10, 10, 10, 10, 10,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                },
                new double[]{
                    60, 120, 120, 25, 25, 1, 1, 1,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 120, 120, 25, 25, 0, 0, 1,
                    10, 60, 10, 10, 10, 10,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                },
                new double[]{
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 120, 120, 25, 25, 0, 0, 1,
                    60, 120, 120, 25, 25, 1, 1, 1,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    10, 10, 60, 10, 10, 10,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                },
                new double[]{
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 120, 120, 25, 25, 0, 0, 1,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 120, 120, 25, 25, 0, 0, 1,
                    10, 10, 10, 60, 10, 10,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                },
                new double[]{
                    60, 120, 120, 25, 25, 0, 0, 1,
                    60, 120, 120, 25, 25, 1, 1, 1,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    10, 10, 10, 10, 60, 10,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                },
                new double[]{
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 61, 61, 5, 5, 0, 0, 0,
                    60, 120, 120, 25, 25, 0, 0, 1,
                    60, 120, 120, 25, 25, 1, 1, 1,
                    10, 10, 10, 10, 10, 60,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                    60, 90, 25,
                }
                /* PUSTE
                new double[]{
                    60, 61, 61 ,5 , 5, 0, 0, 0,
                    60, 61, 61 ,5 , 5, 0, 0, 0,
                    60, 61, 61 ,5 , 5, 0, 0, 0,
                    60, 61, 61 ,5 , 5, 0, 0, 0,
                    10, 10, 10, 10, 10, 10,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,

                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                    60, 61, 5,
                },
                */
            };
            double[][] zestaw_wyjsciowy = new double[][]{
                new double[]{ 1,0,0,0,0,0 },
                new double[]{ 0,1,0,0,0,0 },
                new double[]{ 0,0,1,0,0,0 },
                new double[]{ 0,0,0,1,0,0 },
                new double[]{ 0,0,0,0,1,0 },
                new double[]{ 0,0,0,0,0,1 },
                //new double[]{ 0,0,0,0,0,0 },
            };

            double blad = 1;
            double poprzedni_blad = 1;
            double odZmiany = 0;
            Boolean czyUczyc = true;

            while (czyUczyc)
            {
                // run epoch of learning procedure
                blad = nauczyciel.RunEpoch(zestaw_wejsciowy, zestaw_wyjsciowy);

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
                }
            }
        }

        public SchematSwiatel GenSchemat(int nr, Mapa mapa, DaneORuchu baza, DaneORuchu kontrolne, Czas czas)
        {
            Skrzyzowanie sk = mapa.PodajSkrzyzowanie(nr);
            List<double> dane = new List<double>();


            foreach(int zrodlo in sk.PodajZrodla4Strony())
            {
                // czy istnieje polaczenie
                if ( zrodlo == -1)
                {
                    dane.Add(0);
                    dane.Add(0);
                    dane.Add(0);
                    dane.Add(0);
                    dane.Add(0);
                    dane.Add(0);
                    dane.Add(0);
                    dane.Add(0);
                }
                else
                {
                    dane.Add(sk.CzasDojazdu(zrodlo));                                       // idealny czas odcinka
                    dane.Add(baza.PodajSredniCzas(zrodlo, sk.PodajId()));                   // aktualny sredni czas
                    dane.Add(kontrolne.PodajSredniCzas(zrodlo, sk.PodajId(), czas.godzin)); // czas archiwalny (poprzedni dzien lub kontrolny)
                    dane.Add(baza.PojazdowNaOdcinku(zrodlo, sk.PodajId()));                 // aktualnie na odcinku
                    dane.Add(kontrolne.PodajIlePojazdowWgodzine(zrodlo, sk.PodajId(), czas.godzin));    // wielkosc ruchu archiwalna (poprzedni dzien lub kontrolne)

                    // Czy cos oczekuje na pasie
                    {
                        if (sk.CzyOczekujeWKierunku(zrodlo, 0))
                            dane.Add(1);
                        else
                            dane.Add(0);
                    }
                    {
                        if (sk.CzyOczekujeWKierunku(zrodlo, 1))
                            dane.Add(1);
                        else
                            dane.Add(0);
                    }
                    {
                        if (sk.CzyOczekujeWKierunku(zrodlo, 2))
                            dane.Add(1);
                        else
                            dane.Add(0);
                    }
                }
            }

            // Kiedy byla zmiana na pozcje swiatel
            foreach (int sw in sk.PodajKiedZmienionoSwiatlaNaPozycje())
            {
                dane.Add(sw);
            }
            
            // Dane sasiadow
            foreach ( int saId in sk.PodajKierunki())
            {
                if (saId == -1)
                {
                    for(int i=0; i < 12; i++)
                    {
                        dane.Add(0);
                    }
                }
                else
                {
                    Skrzyzowanie sa = mapa.PodajSkrzyzowanie(saId);

                    foreach (int zr in sa.PodajZrodla4Strony())
                    {
                        if (zr == -1)
                        {
                            dane.Add(0);
                            dane.Add(0);
                            dane.Add(0);
                        }
                        else
                        {
                            dane.Add(sa.CzasDojazdu(zr));                       // idealny czas
                            dane.Add(baza.PodajSredniCzas(zr, saId));           // aktualny sredni czas
                            dane.Add(baza.PodajIlePojazdowWgodzine(zr, saId));  // ile aktualnie pojazdow
                        }
                    }
                }
            }


            List<int> kierunki = sk.PodajKierunki();

            // WRZUCENIE NA SIEĆ
            double[] output = siecNeuronowa.Compute(dane.ToArray());
            double max = 0;
            int wynik = 0;
            for (int i = 0; i < 6; i++)
            {
                if (output[i] >= max)
                {
                    wynik = i;
                    max = output[i];
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
        }




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
         /*
        private List<double> Generuj(int pozycja)
        {
            Random rand = new Random();

            double[] tab = new double[86];

            int idealny;


            for(int i = 0; i < 4; i++)
            {
                idealny = rand.Next(30, 60 * 5);
                tab[i * 8] = idealny;
            }
        }
        */
    }
}
