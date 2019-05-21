using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    class Mapa
    {
        public List<Skrzyzowanie> skrzyzowania { get; set; }

        public Mapa()
        {
            skrzyzowania = new List<Skrzyzowanie>();
        }

        public Mapa(Mapa mapa)
        {
            skrzyzowania = new List<Skrzyzowanie>();
            foreach ( Skrzyzowanie sk in mapa.skrzyzowania)
            {
                skrzyzowania.Add( new Skrzyzowanie(sk));
            }
        }
        
        public void MapaTestowa()
        {
            Skrzyzowanie sk;
            SchematRuchu sr;
            SchematSwiatel ss;
            Swiatla sw;
            List<PasRuchu> psy;


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 2, 0, new List<int> { 7, 9}, new List<int> { 1, 3}, 20),
                new PasRuchu(2, 25, 7, 1, new List<int> { 9, 2}, new List<int> { 3, 0}, 20),
                new PasRuchu(2, 28, 9, 3, new List<int> { 2, 7}, new List<int> { 0, 1}, 20),
            };
            sk = new Skrzyzowanie(1, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 6, 0, new List<int> { 5, 1}, new List<int> { 1, 3}, 20),
                new PasRuchu(2, 27, 5, 1, new List<int> { 1, 6}, new List<int> { 3, 0}, 20),
                new PasRuchu(2, 28, 1, 3, new List<int> { 6, 5}, new List<int> { 0, 1}, 20),
            };
            sk = new Skrzyzowanie(7, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 4, 0, new List<int> { 9, 7}, new List<int> { 1, 3}, 20),
                new PasRuchu(2, 27, 9, 1, new List<int> { 7, 4}, new List<int> { 3, 0}, 20),
                new PasRuchu(2, 25, 7, 3, new List<int> { 4, 9}, new List<int> { 0, 1}, 20),
            };
            sk = new Skrzyzowanie(5, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 8, 0, new List<int> { 1, 5}, new List<int> { 1, 3}, 20),
                new PasRuchu(2, 27, 1, 1, new List<int> { 5, 8}, new List<int> { 3, 0}, 20),
                new PasRuchu(2, 28, 5, 3, new List<int> { 8, 1}, new List<int> { 0, 1}, 20),
            };
            sk = new Skrzyzowanie(9, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);




            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 32, 1, 0, new List<int> { 8, 3, 6}, new List<int> { 1, 2, 3}, 20),
                new PasRuchu(2, 33, 8, 1, new List<int> { 3, 6, 1}, new List<int> { 2, 3, 0}, 20),
                new PasRuchu(1, 30, 3, 3, new List<int> { 6, 1, 8}, new List<int> { 3, 0, 1}, 20),
                new PasRuchu(2, 27, 6, 3, new List<int> { 1, 8, 3}, new List<int> { 0, 1, 2}, 20),
            };
            sk = new Skrzyzowanie(2, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 31, 2, 0, new List<int> { 8, 4, 6}, new List<int> { 1, 2, 3}, 20),
                new PasRuchu(1, 33, 8, 1, new List<int> { 4, 6, 1}, new List<int> { 2, 3, 0}, 20),
                new PasRuchu(1, 32, 4, 3, new List<int> { 6, 2, 8}, new List<int> { 3, 0, 1}, 20),
                new PasRuchu(1, 29, 6, 3, new List<int> { 2, 8, 3}, new List<int> { 0, 1, 2}, 20),
            };
            sk = new Skrzyzowanie(3, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 3, 0, new List<int> { 8, 5, 6}, new List<int> { 1, 2, 3}, 20),
                new PasRuchu(2, 27, 8, 1, new List<int> { 5, 6, 3}, new List<int> { 2, 3, 0}, 20),
                new PasRuchu(1, 30, 5, 3, new List<int> { 6, 3, 8}, new List<int> { 3, 0, 1}, 20),
                new PasRuchu(2, 25, 6, 3, new List<int> { 3, 8, 5}, new List<int> { 0, 1, 2}, 20),
            };
            sk = new Skrzyzowanie(4, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 3, 0, new List<int> { 4, 7, 2}, new List<int> { 1, 2, 3}, 20),
                new PasRuchu(2, 15, 4, 1, new List<int> { 7, 2, 3}, new List<int> { 2, 3, 0}, 20),
                new PasRuchu(1, 25, 7, 3, new List<int> { 2, 3, 4}, new List<int> { 3, 0, 1}, 20),
                new PasRuchu(2, 31, 2, 3, new List<int> { 3, 4, 7}, new List<int> { 0, 1, 2}, 20),
            };
            sk = new Skrzyzowanie(6, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);


            sr = new SchematRuchu(new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 120, 110, 100, 90, 80, 70, 60, 50, 40, 30, 20, 10 });
            ss = new SchematSwiatel(new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 20, 20, 20, 20, 20, 20 });
            sw = new Swiatla(ss);
            //ps = new PasRuchu(1, 30, 2, new List<int> { }, new List<int> { }, 20);

            psy = new List<PasRuchu> {
                new PasRuchu(1, 30, 9, 0, new List<int> { 4, 3, 2}, new List<int> { 1, 2, 3}, 20),
                new PasRuchu(2, 31, 4, 1, new List<int> { 3, 2, 9}, new List<int> { 2, 3, 0}, 20),
                new PasRuchu(1, 37, 3, 3, new List<int> { 2, 9, 4}, new List<int> { 3, 0, 1}, 20),
                new PasRuchu(2, 15, 2, 3, new List<int> { 9, 4, 3}, new List<int> { 0, 1, 2}, 20),
            };
            sk = new Skrzyzowanie(8, psy, sw, sr, sr);
            DodajSkrzyzowanie(sk);
        }

        public Trasa LosujSztywnaTrase()
        {
            List<Trasa> trasy = new List<Trasa>();
            Trasa tmp;

            tmp = new Trasa(9);
            tmp.DodajSkrzyzowanie(1, 1);
            tmp.DodajSkrzyzowanie(2, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(5, 1);
            trasy.Add(tmp);

            tmp = new Trasa(1);
            tmp.DodajSkrzyzowanie(7, 1);
            tmp.DodajSkrzyzowanie(5, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(2, 1);
            trasy.Add(tmp);

            tmp = new Trasa(7);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(2, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(5, 1);
            trasy.Add(tmp);

            tmp = new Trasa(4);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(2, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(9, 1);
            tmp.DodajSkrzyzowanie(1, 1);
            trasy.Add(tmp);

            tmp = new Trasa(5);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(7, 1);
            tmp.DodajSkrzyzowanie(1, 1);
            tmp.DodajSkrzyzowanie(9, 1);
            tmp.DodajSkrzyzowanie(5, 1);
            tmp.DodajSkrzyzowanie(7, 1);
            trasy.Add(tmp);

            tmp = new Trasa(9);
            tmp.DodajSkrzyzowanie(5, 1);
            tmp.DodajSkrzyzowanie(7, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(9, 1);
            trasy.Add(tmp);

            tmp = new Trasa(2);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            trasy.Add(tmp);

            tmp = new Trasa(4);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(2, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(7, 1);
            tmp.DodajSkrzyzowanie(5, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            trasy.Add(tmp);

            tmp = new Trasa(1);
            tmp.DodajSkrzyzowanie(7, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(2, 1);
            tmp.DodajSkrzyzowanie(1, 1);
            trasy.Add(tmp);

            tmp = new Trasa(5);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            trasy.Add(tmp);

            tmp = new Trasa(6);
            tmp.DodajSkrzyzowanie(2, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(4, 1);
            tmp.DodajSkrzyzowanie(6, 1);
            tmp.DodajSkrzyzowanie(3, 1);
            tmp.DodajSkrzyzowanie(8, 1);
            trasy.Add(tmp);

            Random rand = new Random();

            return trasy[rand.Next() % trasy.Count()];
        }

        public void DodajSkrzyzowanie(Skrzyzowanie skrzyzowanie)
        {
            skrzyzowania.Add(skrzyzowanie);
        }

        public void MapaZPliku(string plik)
        {

        }

        public Trasa PodajTrase(int poczatek, int koniec)
        {
            List<Trasa> trasy = new List<Trasa>();
            foreach (int kierunek in skrzyzowania.Find(s => s.PodajId() == poczatek).PodajKierunki())
            {
                Trasa temp = PodajTrase(poczatek, kierunek, koniec, new Trasa(poczatek));
                if (temp != null)
                    trasy.Add(temp);
            }

            if (trasy.Count() > 0)
            {
                return trasy.OrderBy(t => t.koszt).First();
            }

            return null;
        }

        public Trasa PodajTrase(int skad, int poczatek, int koniec, Trasa trasa)
        {
            trasa.DodajSkrzyzowanie(poczatek, skrzyzowania.Find(s => s.PodajId() == poczatek).WagaOdcinka(skad));

            if (trasa.trasa.Last() == koniec)
                return trasa;
            if (trasa.CzyCykl())
                return null;


            List<Trasa> trasy = new List<Trasa>();
            List<int> kierunki = skrzyzowania.Find(s => s.PodajId() == poczatek).PodajKierunki(skad);

            foreach (int kierunek in kierunki)
            {
                Trasa temp = PodajTrase(poczatek, kierunek, koniec, new Trasa(trasa));
                if (temp != null)
                    trasy.Add(temp);
            }

            if (trasy.Count() > 0)
            {
                return trasy.OrderBy(t => t.koszt).First();
            }

            return null;
        }

        public List<int> PodajIdSkrzyzowan()
        {
            HashSet<int> numery = new HashSet<int>();

            foreach (Skrzyzowanie sk in skrzyzowania)
            {
                numery.Add(sk.PodajId());
            }

            return numery.ToList();
        }

        public List<Trasa> GenerujTrasy(int godzina, double mnoznik = 3600.0, double maxOdchyl = 0.2)
        {
            List<Trasa> trasy = new List<Trasa>();
            List<int> numery = new List<int>();
            List<double> wagiZrodel = new List<double>();
            List<double> wagiCelow = new List<double>();

            double ruch = 0;
            int usuwane = 0;
            int start, koniec;
            double ile;


            Random rand = new Random();
            double los1, los2;
            double odchyl = 1 + (rand.NextDouble() * 2 - 1) * maxOdchyl;

            foreach (Skrzyzowanie sk in skrzyzowania)
            {
                ruch += sk.WagaGenerowanegoRuchu(godzina);
                usuwane += sk.WagaUsuwanegoRuchu(godzina);
                numery.Add(sk.PodajId());
                wagiCelow.Add(usuwane);
                wagiZrodel.Add(ruch);
            }

            //ile = ((ruch * odchyl / mnoznik) / rand.NextDouble());
            ile = rand.Next(2);


            while (ile >= 1)
            {
                Trasa tmp;
                start = numery[0];
                koniec = numery[0];
                los1 = rand.NextDouble() * ruch / mnoznik;
                los2 = rand.NextDouble() * usuwane;

                for (int j = numery.Count() - 1; j >= 0; j--)
                {
                    if (wagiZrodel[j] < los1)
                    {
                        start = numery[j];
                        break;
                    }
                }
                for (int j = numery.Count() - 1; j >= 0; j--)
                {
                    if (wagiCelow[j] < los2)
                    {
                        koniec = numery[j];
                        break;
                    }
                }
                if (start != koniec)
                {
                    //tmp = PodajTrase(start, koniec);
                    tmp = LosujSztywnaTrase();
                    if (tmp.trasa.Count() > 2)
                    {
                        trasy.Add(tmp);
                        ile--;
                    }
                }
            }

            return trasy;
        }

        public void RuchSkrzyzowan()
        {
            foreach (Skrzyzowanie skrzyzowanie in skrzyzowania)
            {
                skrzyzowanie.SymulujRuch();
            }
        }

        public void UstawNoweSchematySi(DaneORuchu baza, DaneORuchu bazaKontrolna, SterowanieSi si, Czas czas)
        {
            foreach (Skrzyzowanie skrzyzowanie in skrzyzowania)
            {
                if (skrzyzowanie.CzySi() && skrzyzowanie.PodajKiedyOstatniaZmianaSwiatel() >= SterowanieSi.CO_ILE_ZMIAN_SWIATEL)
                {
                    skrzyzowanie.UstawSchematSi( si.GenSchemat(skrzyzowanie.PodajId(), this, baza, bazaKontrolna, czas) );
                }
            }
        }

        public void UstawSi(int id, DaneORuchu baza, DaneORuchu bazaKontrolna, SterowanieSi si, Czas czas)
        {
            skrzyzowania.First(s => s.PodajId() == id).UstawSchematSi(si.GenSchemat(id, this, baza, bazaKontrolna, czas));
        }

        public void WylaczSi(int id)
        {
            skrzyzowania.First(s => s.PodajId() == id).WylaczSi();

        }

        public double CzasDojazdu(int zrodlo, int cel)
        {
            return skrzyzowania.First(s => s.PodajId() == cel).CzasDojazdu(zrodlo);
        }

        public void DojazdPojazdu(Pojazd pojazd)
        {
            skrzyzowania.First(s => s.PodajId() == pojazd.NajblizszeSkrzyzowanie()).DodajPojazd(pojazd);
        }


        public void PrzekazListePolaczenDoBazy(DaneORuchu baza)
        {
            foreach (Skrzyzowanie skrzyzowanie in skrzyzowania)
            {
                foreach (int zrodlo in skrzyzowanie.PodajZrodla())
                {
                    baza.DodajOdcinek(zrodlo, skrzyzowanie.PodajId());
                }
            }
        }

        public Skrzyzowanie PodajSkrzyzowanie(int id)
        {
            return skrzyzowania.First(sk => sk.PodajId() == id);
        }
    }
}
