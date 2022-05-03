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
        public List<Devis> _devis { get; set; }
        public List<Facture> _factures { get; set; }
        public string jToString()
        {
            return "id : " + this._Id.ToString() + ", adresse : " + this._Adresse + ", nom chantier : " + this._NomChantier + ", commentaire :" + this._Commentaire;
        }
        

        public Chantier()
        {
        }

        public Chantier(int id, string adresse, string nomChantier, string commentaire, List<Devis> devis, List<Facture> factures)
        {
            _Id = id;
            _Adresse = adresse;
            _NomChantier = nomChantier;
            _Commentaire = commentaire;
            _devis = devis;
            _factures = factures;
        }
    }
}