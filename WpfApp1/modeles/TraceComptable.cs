using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.modeles
{
    internal class TraceComptable
    {
        

        public TraceComptable()
        {
        }

        public TraceComptable(int id, float prix, float temps, DateTime dateCreation, Types type, string commentaire, List<Compagnon> compagnon, List<Materiaux> materiaux, List<Chantier> chantiers)
        {
            _Id = id;
            _Prix = prix;
            _Temps = temps;
            _DateCreation = dateCreation;
            _Type = type;
            _Commentaire = commentaire;
            _Compagnon = compagnon;
            _Materiaux = materiaux;
            _Chantiers = chantiers;
            Console.WriteLine("L'objet créé : " + this.ToString());

        }

        public int _Id { get; set; }
        public float _Prix  { get; set; }
        public float _Temps { get; set; }
        public DateTime _DateCreation { get; set; }
        public enum Types
        {
            // items of the enum
            Devis = 0,
            Facture = 1,    //0

        }

        public Types _Type { get; set; }
        public string _Commentaire { get; set; }

        public List<Compagnon> _Compagnon { get; set; }
        public List<Materiaux> _Materiaux { get; set; }
        public List<Chantier> _Chantiers { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
