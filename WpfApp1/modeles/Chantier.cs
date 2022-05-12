﻿using System.Collections.Generic;
using System.ComponentModel;
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
        public enum State 
        {
            // items of the enum
            Inconnue=0,
            [Description("En cours")]
            Encours=1,    //0
            Programmé=2,   //1
            Terminé=3,      //2
            Annulé=4,      //3
                   
        } 
        public State _Etat { get; set; }
        public string _Adresse { get; set; }
        public string _NomChantier { get; set; }
        public string _Commentaire { get; set; }
        public string _telephone { get; set; }
        public List<Devis> _devis { get; set; }
        public List<Facture> _factures { get; set; }
        public List<Compagnon> _compagnon { get; set; }
        public string jToString()
        {
            return "id : " + this._Id.ToString() + ", adresse : " + this._Adresse + ", nom chantier : " + this._NomChantier + ", commentaire :" + this._Commentaire;
        }
        

        public Chantier()
        {
        }

        public Chantier(int id, State etat, string adresse, string nomChantier, string commentaire, List<Devis> devis, List<Facture> factures)
        {
            _Id = id;
            _Etat = etat;
            _Adresse = adresse;
            _NomChantier = nomChantier;
            _Commentaire = commentaire;
            _devis = devis;
            _factures = factures;
        }
    }
}