using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.modeles
{
    internal class Materiaux
    {
        public Materiaux()
        {
        }

        public Materiaux(int id, string nom, int fournisseurId, float prix, string description)
        {
            _Id = id;
            _Nom = nom;
            _FournisseurId = fournisseurId;
            _Prix = prix;
            _Description = description;
            Console.WriteLine("L'objet créé : " + this.ToString());

        }

        public int _Id { get; set; }
        public string _Nom { get; set; }
        public int _FournisseurId { get; set; }
        public float _Prix { get; set; }

        public string _Description { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
