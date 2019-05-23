using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przechowywanie danych pojazdu
     * i podstawowe sterowanie
     */
    class Pojazd
    {
        public static int nastepnaRejestracja = 1;
        public int rejestracja { get; }

        public int czasJazdy { get; private set; }

        Trasa trasa;
        public Boolean czyJedzie { get; private set; }

        DaneORuchu bazaDanych;

        public Pojazd(Trasa trasa, DaneORuchu bazaDanych)
        {
            rejestracja = nastepnaRejestracja;
            nastepnaRejestracja++;

            czasJazdy = 0;

            this.trasa = new Trasa(trasa);
            this.bazaDanych = bazaDanych;

            bazaDanych.ZajerejstrujWjazd(rejestracja, trasa.trasa[0], trasa.trasa[1]);
            czyJedzie = true;
        }

        public int PoprzednieSkrzyzowanie()
        {
            return trasa.trasa[0];
        }
        public int NajblizszeSkrzyzowanie()
        {
            if (trasa.trasa.Count() < 2)
                return -1;
            else
                return trasa.trasa[1];
        }
        public int KolejneSkrzyzowanie()
        {
            if (trasa.trasa.Count() < 3)
                return -1;
            else
                return trasa.trasa[2];
        }

        public void Jedz(int czas = 1)
        {
            if (czyJedzie)
                czasJazdy += czas;
        }

        public void Zatrzymaj()
        {
            czyJedzie = false;
        }
        public void Rusz()
        {
            czyJedzie = true;
        }

        public Boolean CzyUCelu()
        {
            if (trasa.trasa.Count() > 2)
                return false;
            return true;
        }
        
        public void RuszZeSwiatel()
        {
            bazaDanych.ZajerejstrujZjazd(rejestracja, trasa.trasa[0], trasa.trasa[1]);
            bazaDanych.ZajerejstrujWjazd(rejestracja, trasa.trasa[1], trasa.trasa[2]);
            
            trasa.SkasujPozycje();
            czasJazdy = 0;

            if (CzyUCelu())
                czyJedzie = false;
            else
                czyJedzie = true;
        }
    }
}
