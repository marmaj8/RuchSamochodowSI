using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    /*
     * Przetrzymywanie danych o pasie ruchu
     * oraz Kolejki pojazdów
     */
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
        
        // Przejazd pojazdu przez skrzyżowanie
        public void Przejazd(Swiatla swiatla)
        {
            if (pojazdy.Count() == 0)
                return;
            Pojazd pojazd = pojazdy.First();

            if (swiatla.CzyZielone(zrodloSkrzyzowanaia, kierunkiSkrzyzowania[kierunki.FindIndex(k => k == pojazd.KolejneSkrzyzowanie())]))
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
            int[] kier = new int[] { -1, -1, -1, -1 };


            for (int i = 0; i < kierunkiSkrzyzowania.Count(); i++)
            {
                kier[kierunkiSkrzyzowania[i]] = kierunki[i];
            }

            return kier.ToList();
        }

        public Boolean CzyKierunek(int kierunek)
        {
            foreach (int kier in kierunki)
            {
                if (kier == kierunek)
                    return true;
            }
            return false;
        }

        public int PodajZrodloSkrzyzowania()
        {
            return zrodloSkrzyzowanaia;
        }

        // czy to skret w kierunku
        // 0 lewo, 1 prosto, 2 prawo
        public Boolean CzySkret(int kierunek)
        {
            int zrodlo = zrodloSkrzyzowanaia;
            switch(kierunek)
            {
                case 0:
                    zrodlo--;
                    break;
                case 1:
                    zrodlo += 2;
                    break;
                case 2:
                    zrodlo++;
                    break;
            }
            if (zrodlo < 0)
                zrodlo += 4;
            else if (zrodlo >= 4)
                zrodlo -= 4;

            if (kierunkiSkrzyzowania.Contains(zrodlo))
                return true;
            else
                return false;
        }
    }
}
