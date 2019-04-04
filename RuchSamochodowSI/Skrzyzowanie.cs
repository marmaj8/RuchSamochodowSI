using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Skrzyzowanie
    {
        static int maxId;
        int id;
        List<PasRuchu> pasyRuchu;
        Swiatla swiatla;

        SchematRuchu generowany;
        SchematRuchu usuwany;

        public int PodajId()
        {
            return id;
        }
        public Skrzyzowanie(int id, List<PasRuchu> pasy, Swiatla swiatla, SchematRuchu ruch, SchematRuchu usuwanyRuch, DaneORuchu bazaDanych)
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
                    //bazaDanych.DodajOdcinek(pas.Zrodlo(), id);
                zrodlo = pas.Zrodlo();
            }
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
        public List<int> PodajKierunki()
        {
            HashSet<int> kierunki = new HashSet<int>();
            foreach (PasRuchu pas in pasyRuchu)
            {
                foreach (int kierunek in pas.Kierunki())
                {
                    kierunki.Add(kierunek);
                }
            }
            return kierunki.ToList();
        }

        public void DodajPojazd(Pojazd pojazd)
        {
            pasyRuchu.Where(p => p.Zrodlo() == pojazd.PoprzednieSkrzyzowanie() && p.CzyKierunek(pojazd.KolejneSkrzyzowanie()))
                .OrderBy(p => p.ZajetychMiejsc()).First().DodajPojazd(pojazd);
        }

        public List<int> PodajZrodla()
        {
            HashSet<int> zrodla = new HashSet<int>();
            foreach(PasRuchu pas in pasyRuchu)
            {
                zrodla.Add(pas.Zrodlo());
            }

            return zrodla.ToList();
        }
    }
}
