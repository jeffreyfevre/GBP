namespace WpfApp1.modeles
{
    class Facture
    {
        public int _Id { get; set; }
        public int _TempsEffectif { get; set; }
        public int _CoutEffectif { get; set; }
        public string _Commentaire { get; set; }

        public Facture()
        {
        }

        public Facture(int id, int tempsEffectif, int coutEffectif, string commentaire)
        {
            _Id = id;
            _TempsEffectif = tempsEffectif;
            _CoutEffectif = coutEffectif;
            _Commentaire = commentaire;
        }
    }
}
