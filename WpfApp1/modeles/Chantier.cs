using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.modeles
{
    internal class Chantier
    {
        public int _Id { get; set; }
        public enum State
        {
            // items of the enum
            Inconnue = 0,
            [Description("En cours")]
            Encours = 1,    //0
            Programme = 2,   //1
            Termine = 3,      //2
            Annule = 4,      //3

        }
        public string _Numero { get; set; }
        public string _Telephone { get; set; }

        public DateTime _DateCreation { get; set; }
        public DateTime _DateFin { get; set; }
        public State _Etat { get; set; }
        public string _Adresse { get; set; }
        public string _ZipCode { get; set; }
        public string _NomChantier { get; set; }
        public string _Commentaire { get; set; }
        public List<TraceComptable> _devis { get; set; }
        public List<TraceComptable> _factures { get; set; }
        public List<Compagnon> _compagnon { get; set; }
        public string jToString()
        {
            return "id : " + this._Id.ToString() + ", adresse : " + this._Adresse + ", nom chantier : " + this._NomChantier + ", commentaire :" + this._Commentaire;
        }


        public Chantier()
        {
        }

        public Chantier(int id, string numero, string telephone, DateTime dateCreation, DateTime dateFin, State etat, string adresse, string zipCode, string nomChantier, string commentaire, List<TraceComptable> devis, List<TraceComptable> factures, List<Compagnon> compagnon)
        {
            _Id = id;
            _Numero = numero;
            _Telephone = telephone;
            _DateCreation = dateCreation;
            _DateFin = dateFin;
            _Etat = etat;
            _Adresse = adresse;
            _ZipCode = zipCode;
            _NomChantier = nomChantier;
            _Commentaire = commentaire;
            _devis = devis;
            _factures = factures;
            _compagnon = compagnon;
            Console.WriteLine("L'objet créé : " + this.ToString());

        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}