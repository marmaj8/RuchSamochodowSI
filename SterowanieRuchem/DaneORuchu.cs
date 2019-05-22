using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie danych o ruchu
     * rozbite na dokładne dane z ostatniej godziny
     * i wyniki godzinne z ostatniej doby
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
        
        // Zapisanie srednich wynikow z ostatniej godziny do listy srednich
        public void ArchiwizujAktualne()
        {
            foreach( DaneORuchuOdcinka odcinek in aktualne)
            {
                dane24.First(d => d.CzyOdcinek(odcinek.poczatek, odcinek.koniec))
                    .Aktualizuj(czas.godzin, odcinek.PodajSredniCzas(), odcinek.PodajIlePojazdowWGodzine());
            }

            /*
            for(int i = 0; i < aktualne.Count(); i++)
            {
                dane24[i].Aktualizuj(czas.godzin, aktualne[i].PodajSredniCzas(), aktualne[i].PodajIlePojazdowWGodzine());
            }
            */
        }
        
        public void ZajerejstrujWjazd(int rejestracja, int poczatek, int koniec)
        {
            DaneORuchuOdcinka odcinek = aktualne.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                odcinek.ZajerejstrujWjazd(czas, rejestracja);
            }
        }
        
        public void ZajerejstrujZjazd(int rejestracja, int poczatek, int koniec)
        {
            DaneORuchuOdcinka odcinek = aktualne.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                odcinek.ZajerejstrujZjazd(czas, rejestracja);
            }
        }
        
        // Usun nieaktualne (starsze niz godzina) zajerestrowane wjazdy i zjazdy
        public void UsunStare()
        {
            Czas wiek = new Czas(0, 0, 1);
            foreach(DaneORuchuOdcinka odcinek in aktualne)
            {
                odcinek.UsunNieAktualne(czas, wiek);
            }
        }
        
        // Podaj sredni czas wyliczony na podstawie danych z ostatniej godziny dla danego odcinka
        public double PodajSredniCzas(int poczatek, int koniec)
        {
            DaneORuchuOdcinka odcinek = aktualne.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.PodajSredniCzas();
            }
            else return -1;
        }

        // Podaj ile obecnie pojazdow znajduje sie na danym odcinku (zajerejstrowany wjazdy bez zjazdow)
        public int PojazdowNaOdcinku(int poczatek, int koniec)
        {
            DaneORuchuOdcinka odcinek = aktualne.FirstOrDefault(a => a.CzyOdcinek(poczatek, koniec));

            if (odcinek != null)
            {
                return odcinek.PodajIleNaOdcinku();
            }
            else
            {
                return -1;
            }
        }

        // Podaj sredni czas przejazdow danego odcinka w danym przedziale godzinowym
        public double PodajSredniCzas(int poczatek, int koniec, int godzina)
        {
            godzina--;
            if (godzina == -1)
                godzina = 23;
            DaneOruchuOdcinka24h odcinek = dane24.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.srednie[godzina];
            }
            else return -1;
        }

       // Podaj ile pojazdow zajerestrowano na danym odcinku w danym przedziale godzinowym
        public double PodajIlePojazdowWgodzine(int poczatek, int koniec, int godzina)
        {
            godzina--;
            if (godzina == -1)
                godzina = 23;
            DaneOruchuOdcinka24h odcinek = dane24.FirstOrDefault(o => o.CzyOdcinek(poczatek, koniec));
            if (odcinek != null)
            {
                return odcinek.pojazdy[godzina];
            }
            else return -1;
        }

        public double PodajSumeSrednichSkrzyzowania(int skrzyzwoanie, Czas czas)
        {
            double suma = 0;
            int godzina = czas.godzin;
            godzina--;
            if (godzina == -1)
                godzina = 23;

            List<DaneOruchuOdcinka24h> dane = dane24.Where(d => d.koniec == skrzyzwoanie).ToList();

            foreach(DaneOruchuOdcinka24h odcinek in dane)
            {
                suma += odcinek.srednie[godzina];
            }
            return suma;
        }

        public double PodajSumeSrednichSkrzyzowania(int skrzyzwoanie)
        {
            double suma = 0;

            List<DaneOruchuOdcinka24h> dane = dane24.Where(d => d.koniec == skrzyzwoanie).ToList();

            foreach (DaneOruchuOdcinka24h odcinek in dane)
            {
                suma += odcinek.skasowanaSrednia;
            }
            return suma;
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
            //wyswietl = AktualizujDaneDoWyswieltenia(wyswietl, false);

            return wyswietl;
        }

        // Dla trybu 2 symulacji
        public List<DaneDoWyswietlenia> AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlenia> wyswietl, Boolean czyKontrolne)
        {
            int godz = czas.godzin - 1;
            if (godz == -1)
                godz = 23;
            // aktualizacja danych
            for (int i = 0; i < dane24.Count(); i++)
            {
                wyswietl[i].UstawSrednia(dane24[i].srednie[godz], godz, czyKontrolne);
            }

            return wyswietl;
        }

        // Dla trybu 1 symulacji
        public List<DaneDoWyswietlenia> AktualizujDaneDoWyswieltenia(List<DaneDoWyswietlenia> wyswietl)
        {
            int godz = czas.godzin - 1;
            if (godz == -1)
                godz = 23;
            // aktualizacja danych
            for (int i = 0; i < dane24.Count(); i++)
            {
                wyswietl[i].UstawSrednia(dane24[i].srednie[godz], godz, false);
                wyswietl[i].UstawSrednia(dane24[i].skasowanaSrednia, godz, true);
            }
            return wyswietl;
        }
    }
}
