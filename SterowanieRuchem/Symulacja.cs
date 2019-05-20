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

        public Symulacja(DaneORuchu bazaDanych, DaneORuchu bazaDanychKontrolna, Boolean czyKontrolne = false)
        {
            czas = new Czas();
            mapa = new Mapa(bazaDanych);
            pojazdy = new List<Pojazd>();
            this.bazaDanych = bazaDanych;
            this.bazaDanychKontrolna = bazaDanychKontrolna;
            this.czyKontrolne = czyKontrolne;
        }
        public Symulacja(DaneORuchu bazaDanych, DaneORuchu bazaDanychKontrolna, SterowanieSi si)
        {
            czas = new Czas();
            mapa = new Mapa(bazaDanych);
            pojazdy = new List<Pojazd>();
            this.bazaDanych = bazaDanych;
            this.bazaDanychKontrolna = bazaDanychKontrolna;
            this.czyKontrolne = false;
            this.si = si;
        }


        public void SymulacjaTestowa()
        {
            mapa.MapaTestowa();
        }
        
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

            czas.UplywCzasu();
        }

        public void GenerujPojazd(Trasa trasa)
        {
            pojazdy.Add(new Pojazd(trasa, bazaDanych));
        }

        public void GenerujPojazdy()
        {
            List<Trasa> trasy = mapa.GenerujTrasy(czas.godzin);

            foreach (Trasa trasa in trasy)
            {
                GenerujPojazd(trasa);
            }
        }

        public void WylaczSi(int id)
        {
            mapa.WylaczSi(id);
        }
        public void WlaczSi(int id)
        {
            mapa.UstawSi(id, bazaDanych, bazaDanychKontrolna, si, czas);
        }
    }
}
