namespace SterowanieRuchem
{
    /*
     * Czas przejazdu pojazzdu o danej rejestracji
     * Rejestracja -1 oznacza brak zapisanej
     * 
     * Konkretna godzina lub przedział czasu
     * (interpretacja poza)
     */
    class CzasPrzejazdu
    {
        public int rejerstracja { get; }
        Czas czas;
        public int dlugosc { get; }
        
        public CzasPrzejazdu(Czas czas, int rejerstracja = -1)
        {
            this.czas = new Czas(czas);
            this.rejerstracja = rejerstracja;
            this.dlugosc = 0;
        }
        public CzasPrzejazdu(Czas t1, Czas t2, int rejerstracja = -1)
        {
            this.rejerstracja = rejerstracja;
            this.czas = new Czas(t1);

            //dlugosc = t1.PodajWSekundach() - t2.PodajWSekundach();
            dlugosc = Czas.RoznicaWSekundach(t1, t2);
        }

        public Czas PodajGodzine()
        {
            return new Czas(czas);
        }
        public int PodajDlugoscPrzejazdu()
        {
            return dlugosc;
        }
    }
}
