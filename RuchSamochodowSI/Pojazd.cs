using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Pojazd
    {
        Boolean czyKontrolne;
        DaneORuchu bazaDanych;
        Czas czas;
        public static int nastepnaRejestracja = 1;
        int rejestracja;
        
        List<int> trasa;

        int czasNaOdcinku;

        Boolean jedzie;

        
        public Pojazd(Trasa trasa, DaneORuchu bazaDanych, Czas czas, Boolean czyKontrolne)
        {
            rejestracja = nastepnaRejestracja;
            nastepnaRejestracja++;

            jedzie = true;

            this.trasa = trasa.PodajListe();
            this.bazaDanych = bazaDanych;
            this.czas = czas;
            this.czyKontrolne = czyKontrolne;
        }

        public int PoprzednieSkrzyzowanie()
        {
            return trasa[0];
        }
        public int NajblizszeSkrzyzowanie()
        {
            return trasa[1];
        }
        public int KolejneSkrzyzowanie()
        {
            if (trasa.Count() < 3)
                return -1;
            else
                return trasa[2];
        }
        public Boolean CzyJedzie()
        {
            return jedzie;
        }
        public void Jedz(int czas = 1)
        {
            czasNaOdcinku += czas;
        }
        public void Zatrzymaj()
        {
            jedzie = false;
        }
        public void RuszZeSwiatel()
        {
            bazaDanych.DodajCzas(new CzasPrzejazdu(czasNaOdcinku, czas.PodajSekunde(), czas.PodajMinute(), czas.PodajGodzine()), 
                trasa[0], trasa[1], czyKontrolne);
            bazaDanych.DodajPojazd(trasa[1], trasa[2]);

            trasa.RemoveAt(0);
            czasNaOdcinku = 0;

            if (CzyUCelu())
                jedzie = false;
            else
                jedzie = true;
        }
        public int CzasNaOdcinku()
        {
            return czasNaOdcinku;
        }

        public Boolean CzyUCelu()
        {
            if (trasa.Count() > 2)
                return false;
            return true;
        }
    }
}
