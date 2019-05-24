using System;
using System.Collections.Generic;
using System.Linq;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie danych o skrzyżowaniu
     * połączenie pasów z sygnalizacją
     * umieszczanie pojazdów na odpowiednich pasach
     * symulacja zachowania skrzyżowania
     */
    class Skrzyzowanie
    {
        public static int maxId { get; set; }
        public int id { get; set; }
        public List<PasRuchu> pasyRuchu { get; set; }
        public Swiatla swiatla { get; set; }

        public SchematRuchu generowany { get; set; }
        public SchematRuchu usuwany { get; set; }

        public int PodajId()
        {
            return id;
        }
        public Skrzyzowanie() { }
        public Skrzyzowanie(int id, List<PasRuchu> pasy, Swiatla swiatla, SchematRuchu ruch, SchematRuchu usuwanyRuch)
        {
            this.id = id;
            if (id > maxId)
                maxId = id;
            this.pasyRuchu = pasy;
            this.swiatla = swiatla;
            this.generowany = ruch;
            this.usuwany = usuwanyRuch;

            int zrodlo = -1;
            foreach (PasRuchu pas in pasy)
            {
                if (pas.Zrodlo() != zrodlo)
                    zrodlo = pas.Zrodlo();
            }
        }

        public Skrzyzowanie(Skrzyzowanie sk)
        {
            this.id = sk.id;
            this.pasyRuchu = new List<PasRuchu>();
            foreach(PasRuchu pas in sk.pasyRuchu)
            {
                this.pasyRuchu.Add(new PasRuchu(pas));
            }

            this.swiatla = new Swiatla(sk.swiatla);
            this.generowany = sk.generowany;
            this.usuwany = sk.usuwany;
        }

        public void SymulujRuch()
        {
            swiatla.Swiec(1);
            foreach (PasRuchu pas in pasyRuchu)
            {
                pas.Przejazd(swiatla);
            }
        }

        public void SymulujRuch(int czas)
        {
            for (int i = 0; i < czas; i++)
            {
                SymulujRuch();
            }
        }

        public int WagaGenerowanegoRuchu(int godzina)
        {
            return generowany.PodajRuch(godzina);
        }
        public int WagaUsuwanegoRuchu(int godzina)
        {
            return usuwany.PodajRuch(godzina);
        }

        public double CzasDojazdu(int zrodlo)
        {
            PasRuchu pas = pasyRuchu.Find(p => p.Zrodlo() == zrodlo);
            if (pas == null)
                return -1;
            else
                return pas.CzasPrzejazdu();
        }

        public double WagaOdcinka(int zrodlo)
        {
            PasRuchu pas = pasyRuchu.Find(p => p.Zrodlo() == zrodlo);
            if (pas == null)
                return -1;
            else
                return pas.WagaOdcinka();
        }


        // lista kierunkow w ktore mozna skrecic
        public List<int> PodajKierunki(int zrodlo)
        {
            HashSet<int> kierunki = new HashSet<int>();
            foreach (PasRuchu pas in pasyRuchu.Where(p => p.Zrodlo() == zrodlo))
            {
                foreach (int kierunek in pas.Kierunki())
                {
                    kierunki.Add(kierunek);
                }
            }
            return kierunki.ToList();
        }


        // lista wszystkich kierunkow
        public List<int> PodajKierunki()
        {
            int[] kierunki = new int[] { -1, -1, -1, -1 };

            foreach (PasRuchu pas in pasyRuchu)
            {
                List<int> kier = pas.Kierunki();

                for (int i = 0; i < kier.Count(); i++)
                {
                    if (kier[i] != -1)
                        kierunki[i] = kier[i];
                }
            }
            return kierunki.ToList();
        }

        public void DodajPojazd(Pojazd pojazd)
        {
            pasyRuchu.Where(p => p.Zrodlo() == pojazd.PoprzednieSkrzyzowanie() && p.CzyKierunek(pojazd.KolejneSkrzyzowanie()))
                .OrderBy(p => p.ZajetychMiejsc()).First().DodajPojazd(pojazd);
        }

        // lista kierunkow z ktorych mozna dojechac do skrzyzowania
        public List<int> PodajZrodla()
        {
            HashSet<int> zrodla = new HashSet<int>();
            foreach (PasRuchu pas in pasyRuchu)
            {
                zrodla.Add(pas.Zrodlo());
            }

            return zrodla.ToList();
        }

        // lista kierunkow z ktorych mozna dojechac do skrzyzowania
        // z ewentualnymi pustymi miejscami oznaczonymi -1
        public List<int> PodajZrodla4Strony()
        {
            int[] zrodla = new int[] { -1, -1, -1, -1 };
            foreach (PasRuchu pas in pasyRuchu)
            {
                zrodla[pas.PodajZrodloSkrzyzowania()] = pas.Zrodlo();
            }
            return zrodla.ToList();
        }

        public void UstawSchematSi(SchematSwiatel schematSi)
        {
            swiatla.UstawSchematSi(schematSi);
        }

        // czy oczekuje w kierunek skrętu 0 lewo, 1 prosto, 2 prawo
        public Boolean CzyOczekujeWKierunku(int zrodlo, int kierunek)
        {
            List<PasRuchu> pasy = pasyRuchu.Where(p => p.Zrodlo() == zrodlo && p.CzyOczekuje() && p.CzySkret(kierunek)).ToList();

            if (pasy.Count() == 0)
                return false;
            else
                return true;
        }

        public List<int> PodajKiedZmienionoSwiatlaNaPozycje()
        {
            return swiatla.odZmiany.ToList();
        }

        public int PodajKiedyOstatniaZmianaSwiatel()
        {
            return swiatla.OdZmiany();
        }

        public Boolean CzySi()
        {
            return swiatla.CzySi();
        }

        public void WylaczSi()
        {
            swiatla.WylaczSi();
        }
    }
}
