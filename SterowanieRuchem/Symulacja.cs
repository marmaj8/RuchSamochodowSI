using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SterowanieRuchem
{
    class Symulacja
    {
        private Mapa mapa;
        private List<Pojazd> pojazdy;
        private Czas czas;
        private DaneORuchu bazaDanych;
        private DaneORuchu bazaDanychKontrolna;
        private Boolean czyKontrolne;
        private SterowanieSi si;
        

        public Symulacja(Czas czas, DaneORuchu bazaDanych, DaneORuchu bazaDanychKontrolna, Mapa mapa, SterowanieSi si = null)
        {
            this.mapa = new Mapa(mapa);
            this.pojazdy = new List<Pojazd>();
            this.czas = czas;
            this.bazaDanych = bazaDanych;
            this.bazaDanychKontrolna = bazaDanychKontrolna;

            if (si == null)
            {
                this.czyKontrolne = true;
                this.si = null;
            }
            else
            {
                this.czyKontrolne = false;
                this.si = si;
            }
        }
        

        // Wykonanie 1 sekundy symulacji ruchu
        public void Symuluj()
        {
            List<Pojazd> doUsuniecia = new List<Pojazd>();
            
            if (!czyKontrolne)
                mapa.UstawNoweSchematySi(bazaDanych, bazaDanychKontrolna, si, czas);
                
            mapa.RuchSkrzyzowan();

            foreach (Pojazd pojazd in pojazdy)
            {
                pojazd.Jedz();

                if (pojazd.CzyUCelu())
                {
                    doUsuniecia.Add(pojazd);
                }
                else if (pojazd.czyJedzie && pojazd.czasJazdy >= mapa.CzasDojazdu(pojazd.PoprzednieSkrzyzowanie(), pojazd.NajblizszeSkrzyzowanie()))
                {
                    mapa.DojazdPojazdu(pojazd);
                }
            }

            foreach (Pojazd pojazd in doUsuniecia)
            {
                pojazdy.Remove(pojazd);
            }

            pojazdy.Count();
        }

        // Wpuszczenie do ruchu nowego pojazdu na dana trase
        public void GenerujPojazd(Trasa trasa)
        {
            pojazdy.Add(new Pojazd(trasa, bazaDanych));
        }

        public void WylaczSi(int id)
        {
            if (!czyKontrolne)
                mapa.WylaczSi(id);
        }
        public void WlaczSi(int id)
        {
            if (!czyKontrolne)
                mapa.UstawSi(id, bazaDanych, bazaDanychKontrolna, si, czas);
        }
    }
}
