using System;

namespace WpfApp1.modèles
{
    class Compagnon
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public int _Telephone { get; set; }
        public int _CoutHoraire { get; set; }
        public string _DateEmbauche { get; set; }
        public string _Commentaire { get; set; }

        public Compagnon()
        {
        }
        public string jToString()
        {
            return this._Id.ToString() + this._CoutHoraire.ToString();
        }

        public Compagnon(int id, string name, int telephone, int coutHoraire, string dateEmbauche, string commentaire)
        {
            _Id = id;
            _Name = name;
            _Telephone = telephone;
            _CoutHoraire = coutHoraire;
            _DateEmbauche = dateEmbauche;
            _Commentaire = commentaire;
        }
    }
}
