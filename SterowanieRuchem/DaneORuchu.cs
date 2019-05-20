using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie danych o ruchu z ostatich 24h
     */
    class DaneORuchu
    {
        List<DaneORuchuOdcinka> aktualne;
        
        List<DaneOruchuOdcinka24h> dane24;

        Czas czas;
        
        public DaneORuchu(Czas czas)
        {
            this.czas = czas;

            aktualne = new List<DaneORuchuOdcinka>();

            dane24 = new List<DaneOruchuOdcinka24h>();
        }

        public void DodajOdcinek(int poczatek, int koniec)
        {
            aktualne.Add(new DaneORuchuOdcinka(poczatek, koniec));
            dane24.Add(new DaneOruchuOdcinka24h(poczatek, koniec));
        }
        
        public void ArchiwizujAktualne()
        {
            foreach( DaneORuchuOdcinka odcinek in aktualne)
            {
                dane24.First(d => d.CzyOdcinek(odcinek.poczatek, odcinek.koniec)).Aktualizuj(czas.godzin, odcinek.PodajSredniCzas(), odcinek.PodajIlePojazdowWGodzine());
            }
        }
        
        public void ZajerejstrujWjazd(int rejestracja, int poczatek, int koniec)
        {
            List<DaneORuchuOdcinka> odcinki;
                odcinki = aktualne;

            DaneORuchuOdcinka odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                odcinek.ZajerejstrujWjazd(czas, rejestracja);
            }
        }
        
        public void ZajerejstrujZjazd(int rejestracja, int poczatek, int koniec)
        {
            List<DaneORuchuOdcinka> odcinki;
                odcinki = aktualne;

            DaneORuchuOdcinka odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                odcinek.ZajerejstrujZjazd(czas, rejestracja);
            }
        }
        
        public void UsunStare()
        {
            Czas wiek = new Czas(0, 0, 1);
            foreach(DaneORuchuOdcinka odcinek in aktualne)
            {
                odcinek.UsunNieAktualne(czas, wiek);
            }
        }
        
        public double PodajSredniCzas(int poczatek, int koniec)
        {
            List<DaneORuchuOdcinka> odcinki;
                odcinki = aktualne;

            DaneORuchuOdcinka odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.PodajSredniCzas();
            }
            else return -1;
        }
        
        public double PodajIleNaOdcinku(int poczatek, int koniec)
        {
            List<DaneORuchuOdcinka> odcinki;
                odcinki = aktualne;

            DaneORuchuOdcinka odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.PodajIleNaOdcinku();
            }
            else return -1;
        }
        
        public double PodajIlePojazdowWgodzine(int poczatek, int koniec)
        {
            List<DaneORuchuOdcinka> odcinki;
                odcinki = aktualne;

            DaneORuchuOdcinka odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.PodajIlePojazdowWGodzine();
            }
            else return -1;
        }

        
        public double PodajSredniCzas(int poczatek, int koniec, int godzina)
        {
            List<DaneOruchuOdcinka24h> odcinki;
                odcinki = dane24;

            DaneOruchuOdcinka24h odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.srednie[godzina];
            }
            else return -1;
        }
        
        public double PodajIlePojazdowWgodzine(int poczatek, int koniec, int godzina)
        {
            List<DaneOruchuOdcinka24h> odcinki;
                odcinki = dane24;

            DaneOruchuOdcinka24h odcinek = odcinki.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.pojazdy[godzina];
            }
            else return -1;
        }


        /*
         * 
         * 
         * 
         * 
         */

        public List<DaneDoWyswietlenia> PrzgotujDaneDoWyswieltenia()
        {
            List<DaneDoWyswietlenia> wyswietl = new List<DaneDoWyswietlenia>();

            foreach (DaneORuchuOdcinka dane in aktualne)
            {
                DaneDoWyswietlenia tmp = new DaneDoWyswietlenia(dane.poczatek, dane.koniec);

                wyswietl.Add(tmp);
            }
            wyswietl = AktualizujDaneDoWyswieltenia(wyswietl, false);

            return wyswietl;
        }
        public List<DaneDoWyswietlenia> AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlenia> wyswietl, Boolean czyKontrolne)
        {
            // aktualizacja danych
            for (int i = 0; i < dane24.Count(); i++)
            {
                for(int g = 0; g < 24; g++)
                {
                    wyswietl[i].UstawSrednia( dane24[i].srednie[g], g, czyKontrolne);
                }
            }

            return wyswietl;
        }
        
        public int PojazdowNaOdcinku(int poczatek, int koniec)
        {
            List<DaneORuchuOdcinka> dane;
                dane = aktualne;

            DaneORuchuOdcinka odcinek = dane.FirstOrDefault(a => a.CzyOdcinek(poczatek, koniec));

            if (odcinek != null)
            {
                return odcinek.PodajIleNaOdcinku();
            }
            else
            {
                return -1;
            }
        }
    }
}
