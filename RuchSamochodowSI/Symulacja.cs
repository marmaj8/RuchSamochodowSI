using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuchSamochodowSI
{
    class Symulacja
    {
        private Mapa mapa;
        private List<Pojazd> pojazdy;
        private Czas czas;
        private DaneORuchu bazaDanych;
        private Boolean czyKontrolne;

        public Symulacja(DaneORuchu bazaDanych, Boolean czyKontrolne = false)
        {
            czas = new Czas();
            mapa = new Mapa(bazaDanych);
            pojazdy = new List<Pojazd>();
            this.bazaDanych = bazaDanych;
            this.czyKontrolne = czyKontrolne;
        }


        public void SymulacjaTestowa()
        {
            mapa.MapaTestowa();
        }

        public void GenerujPojazd(Trasa trasa)
        {
            pojazdy.Add(new Pojazd(trasa, bazaDanych, czas, czyKontrolne));
        }

        public void Symuluj()
        {
            List<Pojazd> doUsuniecia = new List<Pojazd>();
            mapa.RuchSkrzyzowan();


            foreach (Pojazd pojazd in pojazdy)
            {
                pojazd.Jedz();

                if (pojazd.CzyUCelu())
                {
                    doUsuniecia.Add(pojazd);
                }
                else if (pojazd.CzyJedzie() && pojazd.CzasNaOdcinku() >= mapa.CzasDojazdu(pojazd.PoprzednieSkrzyzowanie(), pojazd.NajblizszeSkrzyzowanie()))
                {
                    mapa.DojazdPojazdu(pojazd);
                }
            }

            foreach(Pojazd pojazd in doUsuniecia)
            {
                pojazdy.Remove(pojazd);
            }

            pojazdy.Count();

            czas.UplywSekund();
        }

        public void GenerujPojazdy()
        {
            List <Trasa> trasy = mapa.GenerujTrasy(czas.PodajGodzine());

            foreach(Trasa trasa in trasy)
            {
                pojazdy.Add(new Pojazd(trasa,bazaDanych,czas,czyKontrolne));
            }
        }

    }
}
