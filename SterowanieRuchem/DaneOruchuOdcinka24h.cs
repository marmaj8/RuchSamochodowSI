using System;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie danych o ruchu z ostatniej doby
     * na odcinku łączącym 2 skrzyżowania
     * o numerach poczatek i koniec,
     * koniec to skrzyżowanie w którego strone odbywa się ruch
     */
    class DaneOruchuOdcinka24h
    {
        public int poczatek { get; }
        public int koniec { get; }

        public double[] srednie { get; }
        public int[] pojazdy { get; }

        public double skasowanaSrednia { get; private set; }

        public DaneOruchuOdcinka24h(int poczatek, int koniec)
        {
            this.poczatek = poczatek;
            this.koniec = koniec;

            this.srednie = new double[24];
            this.pojazdy = new int[24];

            this.skasowanaSrednia = 0;
        }
        public void Aktualizuj(int godzina, double srednia, int ruch)
        {
            godzina = godzina % 24;
            godzina--;
            if (godzina == -1)
                godzina = 23;

            skasowanaSrednia = srednie[godzina];

            srednie[godzina] = srednia;
            pojazdy[godzina] = ruch;
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
