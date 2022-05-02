using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modèles;


namespace WpfApp1.modeles
{   
    class Chantier
    {
        public int _Id { get; set; }
        public string _Adresse { get; set; }
        public string _NomChantier { get; set; }
        public string _Commentaire { get; set; }
        public Devis[] _devis { get; set; }
        public Facture[] _factures { get; set; }
        public string jToString()
        {
            return "id : " + this._Id.ToString() + ", adresse : " + this._Adresse + ", nom chantier : " + this._NomChantier + ", commentaire :" + this._Commentaire;
        }
        public Chantier(int id, string adresse, string nomChantier, string commentaire, Devis[] devis, Facture[] factures)
        {
            _Id = id;
            _Adresse = adresse;
            _NomChantier = nomChantier;
            this._Commentaire = commentaire;
            this._devis = devis;
            this._factures = factures;
        }
        public Chantier(int id, string adresse, string nomChantier, string commentaire)
        {
            _Id = id;
            _Adresse = adresse;
            _NomChantier = nomChantier;
            this._Commentaire = commentaire;

        }

        public Chantier()
        {
        }

        

    }
}