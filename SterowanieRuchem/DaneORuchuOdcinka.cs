using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie danych o ruchu na odcinku łączącym 2 skrzyżowania
     * o numerach poczatek i koniec,
     * koniec to skrzyżowanie w którego strone odbywa się ruch
     */
    class DaneORuchuOdcinka
    {
        List<CzasPrzejazdu> czasy;
        List<CzasPrzejazdu> wjazdy;

        public int poczatek { get; }
        public int koniec { get; }

        int suma;

        public DaneORuchuOdcinka(int poczatek, int koniec)
        {
            this.poczatek = poczatek;
            this.koniec = koniec;
            this.suma = 0;

            czasy = new List<CzasPrzejazdu>();
            wjazdy = new List<CzasPrzejazdu>();
        }


        public void ZajerejstrujWjazd(Czas czas, int rejestracja)
        {
            wjazdy.Add(new CzasPrzejazdu(czas, rejestracja));
        }

        /*
         * Zapisanie czasu przejazdu jeśli zajerejstrowano wjazd
         */
        public void ZajerejstrujZjazd(Czas czas, int rejestracja)
        {
            // wyszukanie czasu Wjazdu na odcinek
            CzasPrzejazdu cz = wjazdy.FirstOrDefault(c => c.rejerstracja == rejestracja);
            if (cz != null)
            {
                czasy.Add(new CzasPrzejazdu(czas, cz.PodajGodzine()));

                // dla uproszczenia poxniejszych obliczen
                // dodanie czas do puli
                suma += czasy.Last().PodajDlugoscPrzejazdu();

                // usunięcie zajerejstrowanego czasu Wjazdu
                wjazdy.Remove(cz);
            }
            // jesli nie znalexlismy czasu Wjazdu to nic nie robimy
        }

        /*
         * Usuwanie nieaktualnych danych
         * Czasy zjazdów starsze niż wiek [Czas]
         * Czasy wjazdów starsze niż x krotność średniej nie mniej niz wiek
         * (aby usunac pojazdy ktore opuscily ruch na tym odcinku)
         */
        public void UsunNieAktualne(Czas czas, Czas wiek, int x = 4)
        {
            List<CzasPrzejazdu> usun = new List<CzasPrzejazdu>();
            double srednia = PodajSredniCzas() * x;
            if (srednia < wiek.PodajWSekundach())
                srednia = wiek.PodajWSekundach();

            foreach (CzasPrzejazdu wjazd in wjazdy)
            {
                if (Czas.RoznicaWSekundach(czas, wjazd.PodajGodzine()) > srednia)
                    usun.Add(wjazd);
                else
                    break;  // czasy sa dodawane w kolejnosci wiec nastepne zostaja
            }
            foreach (CzasPrzejazdu u in usun)
            {
                wjazdy.Remove(u);
            }
            usun = new List<CzasPrzejazdu>();

            foreach (CzasPrzejazdu cz in czasy)
            {
                if (Czas.RoznicaWSekundach(czas, cz.PodajGodzine()) > wiek.PodajWSekundach())
                    usun.Add(cz);
                else
                    break;  // czasy sa dodawane w kolejnosci wiec nastepne zostaja
            }
            foreach(CzasPrzejazdu u in usun)
            {
                suma -= u.PodajDlugoscPrzejazdu();
                czasy.Remove(u);
            }

        }

        public double PodajSredniCzas()
        {
            if (suma == 0)
                return 0;
            else if (czasy.Count() == 0)
                return -1;
            else
                return suma / czasy.Count();
        }

        public int PodajIlePojazdowWGodzine()
        {
            return czasy.Count();
        }
        public int PodajIleNaOdcinku()
        {
            return wjazdy.Count();
        }

        public Boolean CzyOdcinek(int poczatek, int koniec)
        {
            if (this.poczatek == poczatek && this.koniec == koniec)
                return true;
            else
                return false;
        }
    }
}
