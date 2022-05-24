using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.modeles
{
    internal class Fournisseur
    {
        public Fournisseur()
        {
        }

        public Fournisseur(int id, string nom, string telephone, string adresse, string zipcode, string commentaire, List<TraceComptable> devis, List<TraceComptable> facture)
        {
            _Id = id;
            _Nom = nom;
            _Telephone = telephone;
            _Adresse = adresse;
            _Zipcode = zipcode;
            _Commentaire = commentaire;
            _Devis = devis;
            _Facture = facture;
            Console.WriteLine("L'objet créé : " + this.ToString());

        }

        public int _Id { get; set; }
        public string _Nom { get; set; }
        public string _Telephone { get; set; }
        public string _Adresse { get; set; }
        public string _Zipcode { get; set; }
        public string _Commentaire { get; set; }
        public List<TraceComptable> _Devis { get; set; }
        public List<TraceComptable> _Facture { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
