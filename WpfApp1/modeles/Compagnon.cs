using System;
using System.Collections.Generic;
using WpfApp1.modeles;

namespace WpfApp1.modèles
{
    class Compagnon
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public string _Telephone { get; set; }
        public int _CoutHoraire { get; set; }
        public string _DateEmbauche { get; set; }
        public string _Commentaire { get; set; }
        public string _Prenom { get; set; }
   
   

        public List<Chantier> _Chantiers { get; set; }
        public Compagnon()
        {
        }

        public Compagnon(int id , string name, string telephone, int coutHoraire, string dateEmbauche, string commentaire ,string prenom, List<Chantier> chantiers)
        {
            _Id = id;
            _Name = name;
            _Telephone = telephone;
            _CoutHoraire = coutHoraire;
            _DateEmbauche = dateEmbauche;
            _Commentaire = commentaire;
            _Chantiers = chantiers;
            _Prenom = prenom;
        }

        public string jToString()
        {
            return this._Id.ToString() + this._CoutHoraire.ToString();
        }

        
    }
}
