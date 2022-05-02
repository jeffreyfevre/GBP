namespace WpfApp1.modeles
{
    class Devis
    {
        public int _Id { get; set; }
        public int _TempsPrevu { get; set; }
        public int _CoutPrevu { get; set; }
        public string _Commentaire { get; set; }

        public Devis()
        {
        }

        public Devis(int id, int tempsPrevu, int coutPrevu, string commentaire)
        {
            _Id = id;
            _TempsPrevu = tempsPrevu;
            _CoutPrevu = coutPrevu;
            _Commentaire = commentaire;
        }
    }
}
