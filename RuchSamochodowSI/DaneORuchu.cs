using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class DaneORuchu
    {
        List<DaneORuchuOdcinka> aktualne;
        List<DaneORuchuOdcinka> aktualneKontrolne;

        List<List<double>> srednie;
        List<List<double>> srednieKontrolne;

        List<List<int>> ruch;
        List<List<int>> ruchKontrolne;

        Czas czas;

        public DaneORuchu(Czas czas)
        {
            aktualne = new List<DaneORuchuOdcinka>();
            aktualneKontrolne = new List<DaneORuchuOdcinka>();
            srednie = new List<List<double>>();
            srednieKontrolne = new List<List<double>>();
            ruch = new List<List<int>>();
            ruchKontrolne = new List<List<int>>();

            for (int i = 0; i < 24; i++)
            {
                srednie.Add(new List<double>());
                srednieKontrolne.Add(new List<double>());
                ruch.Add(new List<int>());
                ruchKontrolne.Add(new List<int>());
            }

            this.czas = czas;

            int x = 5;
        }

        public void DodajOdcinek(int poczatek, int koniec)
        {
            aktualne.Add(new DaneORuchuOdcinka(poczatek, koniec));
            aktualneKontrolne.Add(new DaneORuchuOdcinka(poczatek, koniec));
            
            for (int i = 0; i < 24; i++)
            {
                srednie[i].Add(0);
                srednieKontrolne[i].Add(0);
                ruch[i].Add(0);
                ruchKontrolne[i].Add(0);
            }
        }

        public void ZapamietajWynikGodzinny()
        {
            int i = 0;
            foreach(DaneORuchuOdcinka odcinek in aktualne)
            {
                srednie[czas.PodajGodzine()][i] = odcinek.PodajSredniCzas();
                ruch[czas.PodajGodzine()][i] = odcinek.WielkoscRuchu();
                i++;
            }

            i = 0;
            foreach (DaneORuchuOdcinka odcinek in aktualneKontrolne)
            {
                srednieKontrolne[czas.PodajGodzine()][i] = odcinek.PodajSredniCzas();
                ruchKontrolne[czas.PodajGodzine()][i] = odcinek.WielkoscRuchu();
                i++;
            }
        }

        public void DodajCzas(CzasPrzejazdu czas, int poczatekOdcinka, int koniecOdcinka, Boolean czyKontrolne = false)
        {
            if (czyKontrolne)
                aktualneKontrolne.Find(a => a.CzyOdcinek(poczatekOdcinka, koniecOdcinka)).DodajCzas(czas);
            else
                aktualne.Find(a => a.CzyOdcinek(poczatekOdcinka, koniecOdcinka)).DodajCzas(czas);
        }
        public void DodajPojazd(int poczatekOdcinka, int koniecOdcinka, Boolean czyKontrolne = false)
        {
            if (czyKontrolne)
                aktualneKontrolne.Find(a => a.CzyOdcinek(poczatekOdcinka, koniecOdcinka)).DodajPojazd(czas);
            else
                aktualne.Find(a => a.CzyOdcinek(poczatekOdcinka, koniecOdcinka)).DodajPojazd(czas);
        }

        public void UsunStare()
        {
            foreach(DaneORuchuOdcinka dane in aktualne)
            {
                dane.UsunStare(czas, new Czas(0,0,1));
            }
            foreach (DaneORuchuOdcinka dane in aktualneKontrolne)
            {
                dane.UsunStare(czas, new Czas(0, 0, 1));
            }
        }

        public List<double> PodajSrednie(int godzina, Boolean czyKontrolne = false)
        {
            if (czyKontrolne)
                return srednieKontrolne[godzina % 24];
            else
                return srednie[godzina % 24];
        }
        public List<int> PodajRuch(int godzina, Boolean czyKontrolne = false)
        {
            if (czyKontrolne)
                return ruchKontrolne[godzina % 24];
            else
                return ruch[godzina % 24];
        }

        public List<double> PodajAktualneSrednie(Boolean czyKontrolne = false)
        {
            List<DaneORuchuOdcinka> aktualne;
            List<double> srednie = new List<double>();
            if (czyKontrolne)
                aktualne = this.aktualneKontrolne;
            else
                aktualne = this.aktualne;


            foreach(DaneORuchuOdcinka dane in aktualne)
            {
                srednie.Add(dane.PodajSredniCzas());
            }  
            return srednie;
        }

        public List<DaneDoWyswietlania> PrzgotujDaneDoWyswieltenia(Boolean czyKontrolne = false)
        {
            List<DaneDoWyswietlania> wyswietl = new List<DaneDoWyswietlania>();
            List<List<double>> srednie;
            if (czyKontrolne)
                srednie = srednieKontrolne;
            else
                srednie = this.srednie;

            foreach(DaneORuchuOdcinka dane in aktualne)
            {
                DaneDoWyswietlania tmp = new DaneDoWyswietlania();
                tmp.Poczatek = dane.PodajPoczatek();
                tmp.Koniec = dane.PodajKoniec();

                wyswietl.Add(tmp);
            }

            int godzina = 1;
            foreach (List<double> tmp in srednie)
            {
                int j = 0;
                foreach(double srednia in tmp)
                {
                    wyswietl[j].UstawSrednia(srednia, godzina);
                    j++;
                }
                godzina++;
            }

            return wyswietl;
        }
        public List<DaneDoWyswietlania> AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlania> wyswietl, Boolean czyKontrolne = false)
        {
            int godzina = czas.PodajGodzine();
            List<List<double>> srednie;
            if (czyKontrolne)
                srednie = srednieKontrolne;
            else
                srednie = this.srednie;

            int i = 0;
            foreach (double srednia in srednie[godzina])
            {
                wyswietl[i].UstawSrednia(srednia, godzina + 1);
            }

            return wyswietl;
        }
    }
}
