using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class DaneORuchuOdcinka
    {
        List<CzasPrzejazdu> czasy;
        List<CzasPrzejazdu> pojazdy;
        int poczatek;
        int koniec;

        int suma;

        public DaneORuchuOdcinka(int poczatek, int koniec)
        {
            czasy = new List<CzasPrzejazdu>();
            pojazdy = new List<CzasPrzejazdu>();
            this.poczatek = poczatek;
            this.koniec = koniec;
            suma = 0;
        }

        public void DodajCzas(CzasPrzejazdu czas)
        {
            czasy.Add(czas);
            suma += czas.PodajDlugosc();
        }

        public void DodajPojazd(Czas czas)
        {
            pojazdy.Add(new CzasPrzejazdu(0, czas.PodajSekunde(), czas.PodajMinute(), czas.PodajGodzine()));
        }
        public void UsunStare(Czas czas, Czas minelo)
        {
            List<CzasPrzejazdu> usun = new List<CzasPrzejazdu>();
            foreach(CzasPrzejazdu przejazd in czasy)
            {
                if (!przejazd.CzyMinelo(czas, minelo))
                    break;
                else
                    usun.Add(przejazd);
            }

            foreach(CzasPrzejazdu przejazd in usun)
            {
                suma = suma - przejazd.PodajDlugosc();
                czasy.Remove(przejazd);
            }

            usun = new List<CzasPrzejazdu>();
            foreach (CzasPrzejazdu przejazd in pojazdy)
            {
                if (!przejazd.CzyMinelo(czas, minelo))
                    break;
                else
                    usun.Add(przejazd);
            }

            foreach (CzasPrzejazdu przejazd in usun)
            {
                pojazdy.Remove(przejazd);
            }
        }
        public int WielkoscRuchu()
        {
            return pojazdy.Count();
        }
        public double PodajSredniCzas()
        {
            if (suma == 0)
                return 0;
            else
                return suma / czasy.Count();
        }

        public int PodajPoczatek()
        {
            return poczatek;
        }
        public int PodajKoniec()
        {
            return koniec;
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
