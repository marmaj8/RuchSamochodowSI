using System;
using System.Collections.Generic;
using System.Linq;

namespace SterowanieRuchem
{
    class Mapa
    {
        public List<Skrzyzowanie> skrzyzowania { get; set; }        // lista skrzyzowan
        List<Trasa> zapisaneTrasy;                                  // znalezione optymalne trasy

        public Mapa()
        {
            skrzyzowania = new List<Skrzyzowanie>();
            zapisaneTrasy = new List<Trasa>();
        }

        public Mapa(Mapa mapa)
        {
            zapisaneTrasy = new List<Trasa>();
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
                new PasRuchu(1, 33, 8, 1, new List<int> { 4, 6, 2}, new List<int> { 2, 3, 0}, 20),
                new PasRuchu(1, 32, 4, 3, new List<int> { 6, 2, 8}, new List<int> { 3, 0, 1}, 20),
                new PasRuchu(1, 29, 6, 3, new List<int> { 2, 8, 4}, new List<int> { 0, 1, 2}, 20),
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

        /*
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
        */

        public void DodajSkrzyzowanie(Skrzyzowanie skrzyzowanie)
        {
            skrzyzowania.Add(skrzyzowanie);
        }

        // przeszukanie zapisanych tras w celu znalezienia odpowiedniej
        private Trasa ZapisanaTrasa(int poczatek, int koniec)
        {
            //return null;
            foreach (Trasa t in zapisaneTrasy)
            {
                if (t.trasa.First() == poczatek && t.trasa.Last() == koniec)
                    return t;
            }
            return null;
        }
        
        public Trasa PodajTrase(int poczatek, int koniec)
        {
            // sprawdzenie czy wyznaczylismy juz taka trase
            Trasa trasa = ZapisanaTrasa(poczatek, koniec);
            if (trasa != null)
                return trasa;

            List<Trasa> mozliwe = new List<Trasa>();
            trasa = new Trasa(poczatek);

            List<int> koszty = new List<int>();

            Skrzyzowanie sk = skrzyzowania.First(s => s.PodajId() == poczatek);
            foreach(int kierunek in sk.PodajKierunki())
            {
                if (kierunek != -1)
                {
                    Trasa tmp = PodajTrase(trasa, poczatek, kierunek, koniec, koszty);
                    if (tmp != null)
                        mozliwe.Add(tmp);
                }
            }


            if (mozliwe.Count() > 0)
            {
                Trasa tmp = mozliwe.OrderBy(m => m.koszt).First();
                zapisaneTrasy.Add(tmp);
                return tmp;
            }

            return null;
        }

        private Trasa PodajTrase(Trasa trasaPoczatkowa, int skad, int przez, int koniec, List<int> koszty)
        {
            Trasa trasa = new Trasa(trasaPoczatkowa);
            Skrzyzowanie sk = skrzyzowania.First(s => s.PodajId() == przez);
            trasa.DodajSkrzyzowanie(przez, sk.WagaOdcinka(skad));
            // jestli to koneic trasy
            if (przez == koniec)
            {
                koszty.Add( (int)trasa.koszt);
                return trasa;
            }
            // jesli znaleziono szybsza trase kasujemy
            if (koszty.Count() > 0 && trasa.koszt > koszty.Min())
                return null;
            // jesli trasa jest cyklem trase kasujemy
            if (trasa.CzyCykl())
                return null;
            
            List<Trasa> mozliwe = new List<Trasa>();

            
            foreach (int kierunek in sk.PodajKierunki())
            {
                if (kierunek != -1)
                {
                    Trasa tmp = PodajTrase(trasa, przez, kierunek, koniec, koszty);
                    if (tmp != null)
                        mozliwe.Add(tmp);
                }
            }

            
            if (mozliwe.Count() > 0)
            {
                Trasa tmp = mozliwe.OrderBy(m => m.koszt).First();
                koszty.Add( (int)tmp.koszt);
                return tmp;
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


        private List<int> WielkosciGenerowanegoRuchu(int godzina)
        {
            List<int> tab = new List<int>();

            foreach(Skrzyzowanie sk in skrzyzowania)
            {
                tab.Add(sk.WagaGenerowanegoRuchu(godzina));
            }
            return tab;
        }
        private List<int> WielkosciKasowanegoRuchu(int godzina)
        {
            List<int> tab = new List<int>();

            foreach (Skrzyzowanie sk in skrzyzowania)
            {
                tab.Add(sk.WagaUsuwanegoRuchu(godzina));
            }
            return tab;
        }

        public List<Trasa> GenerujTrasy(int godzina, double mnoznik = 10, double maxOdchyl = 0.2)
        {
            Random rand = new Random();
            List<Trasa> trasy = new List<Trasa>();
            List<int> generowane = WielkosciGenerowanegoRuchu(godzina);
            List<int> kasowane = WielkosciKasowanegoRuchu(godzina);
            int gen = generowane.Sum();
            int kas = kasowane.Sum();
            int ile = (int)(gen * mnoznik * (1 - (rand.NextDouble() - 0.5) * 2 * maxOdchyl ));
            int start;
            int koniec;
            int sum;
            int i;

            while(ile > 0)
            {
                // LOSOWANIE WARTOSCI POCZATKA I KONCA
                start = rand.Next(gen);
                koniec = rand.Next(kas);

                // ZNALEZIENIE POCZATKA I KONCA
                // O WYLOSOWANEJ WARTOSCI
                sum = 0;
                i = 0;
                foreach(int g in generowane)
                {
                    sum += g;
                    if (start < sum)
                    {
                        start = skrzyzowania[i].id;
                        break;
                    }
                    i++;
                }
                sum = 0;
                i = 0;
                foreach (int k in kasowane)
                {
                    sum += k;
                    if (koniec < sum)
                    {
                        koniec = skrzyzowania[i].id;
                        break;
                    }
                    i++;
                }

                if (start != koniec)
                {
                    Trasa tmp = PodajTrase(start, koniec);

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
                if (skrzyzowanie.CzySi() && skrzyzowanie.PodajKiedyOstatniaZmianaSwiatel() >= SterowanieSi.CO_ILE_ZMIANA_SWIATEL)
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
