using System;
using System.Collections.Generic;
using WpfApp1.modeles;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace WpfApp1.modeles
{
    internal class Compagnon
    {
        public int _Id { get; set; }
        public string _Name { get; set; }
        public string _Telephone { get; set; }
        public int _CoutHoraire { get; set; }
        public DateTime _DateEmbauche { get; set; }
        public string _Commentaire { get; set; }
        public string _Prenom { get; set; }



        public List<Chantier> _Chantiers { get; set; }
        public List<TraceComptable> _Factures { get; set; }
        public List<TraceComptable> _Devis { get; set; }


        public Compagnon(int id, string name, string telephone, int coutHoraire, DateTime dateEmbauche, string commentaire, string prenom, List<Chantier> chantiers, List<TraceComptable> factures, List<TraceComptable> devis)
        {
            _Id = id;
            _Name = name;
            _Telephone = telephone;
            _CoutHoraire = coutHoraire;
            _DateEmbauche = dateEmbauche;
            _Commentaire = commentaire;
            _Prenom = prenom;
            _Chantiers = chantiers;
            _Factures = factures;
            _Devis = devis;
            Console.WriteLine("L'objet créé : " + this.ToString());
        }
        public Compagnon()
        {
        }
        public string jToString()
        {
            return this._Id.ToString() + this._CoutHoraire.ToString();
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
