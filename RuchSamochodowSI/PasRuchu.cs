using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class PasRuchu
    {
        double waga;
        double czasPrzejazdu;

        int zrodlo;
        int zrodloSkrzyzowanaia;
        List<int> kierunki;
        List<int> kierunkiSkrzyzowania;

        int miejesca;

        Queue<Pojazd> pojazdy;

        public PasRuchu(double waga, double czas, int zrodlo, int zrodloSkrzyzowanaia, List<int> kierunki, List<int> kierunkiSkrzyzowania, int miejsca)
        {
            pojazdy = new Queue<Pojazd>();

            this.waga = waga;
            czasPrzejazdu = czas;
            this.zrodloSkrzyzowanaia = zrodloSkrzyzowanaia;
            this.zrodlo = zrodlo;
            this.kierunki = kierunki;
            this.kierunkiSkrzyzowania = kierunkiSkrzyzowania;
            this.miejesca = miejsca;
        }

        public void DodajPojazd(Pojazd pojazd)
        {
            pojazd.Zatrzymaj();
            pojazdy.Enqueue(pojazd);
        }
        
        public int WolnychMiejsc()
        {
            return miejesca - pojazdy.Count();
        }
        public int ZajetychMiejsc()
        {
            return pojazdy.Count();
        }

        public Boolean CzyOczekuje()
        {
            if (pojazdy.Count() > 0)
                return true;
            else
                return false;
        }
        public void Przejazd(Swiatla swiatla)
        {
            if (pojazdy.Count() == 0)
                return;
            Pojazd pojazd = pojazdy.First();

            if( swiatla.CzyZielone(zrodloSkrzyzowanaia, kierunkiSkrzyzowania[kierunki.FindIndex(k => k == pojazd.KolejneSkrzyzowanie())]))
            {
                pojazdy.Dequeue();
                pojazd.RuszZeSwiatel();
            }
        }

        public double CzasPrzejazdu()
        {
            return czasPrzejazdu;
        }

        public double WagaOdcinka()
        {
            return waga * czasPrzejazdu;
        }
        public int Zrodlo()
        {
            return zrodlo;
        }

        public List<int> Kierunki()
        {
            return kierunki;
        }

        public Boolean CzyKierunek(int kierunek)
        {
            foreach(int kier in kierunki)
            {
                if (kier == kierunek)
                    return true;
            }
            return false;
        }
    }
}
