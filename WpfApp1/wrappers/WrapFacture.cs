using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modèles;

namespace WpfApp1.wrappers
{
    internal class WrapFacture
    {
        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");


        public void createFacture(Facture facture)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = @"INSERT INTO facture (temps_effectif,cout_effectif,facture_com) VALUES ('" + facture._TempsEffectif+ "','" + facture._CoutEffectif + "','" + facture._Commentaire + "')";
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery();

        }
        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Facture readFacture(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM facture WHERE facture=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            return convertDataToObject(rdr);
        }
        public List<Facture> searchChantierByName(string name)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier=" + name;
            List<Facture> listFacture = new List<Facture>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Facture fac = convertDataToObject(reader);
                listFacture.Add(fac);
            }
            return listFacture;
        }
    
       //les chants du dictionnaires sont celui de la des champs de la bdd
        public List<Facture> searchChantierMultiParam(Dictionary<string, string> dic)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            string Query = "SELECT * FROM chantier WHERE";
            //sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier=" + name;
            foreach (KeyValuePair<string, string> param in dic)
            { 
                string where = param.Key+"="+param.Value;
                Query += where;
            }

            sqlCommand.CommandText = Query;
            List<Facture> listFacture = new List<Facture>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Facture fac = convertDataToObject(reader);
                listFacture.Add(fac);
            }
            return listFacture;
        }
        public void updateFacture(Facture facture)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            var args = new Dictionary<string, object>
            {
                {"@id", facture._Id},
                {"@tempsEffectif", facture._TempsEffectif},
                {"@coutEffectif", facture._CoutEffectif},
                {"@com", facture._Commentaire }
            };

            sqlCommand.CommandText = "UPDATE facture SET temps_effectif = @tempsEffectif, cout_effectif = @coutEffectif, facture_com = @com WHERE Id = @id";
            sqlCommand.ExecuteNonQuery();
        }
        public void deleteFacture(Facture facture)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM facture WHERE id_facture=" + facture._Id;
            sqlCommand.ExecuteNonQuery();

        }
        //je sais que je peux use le constructeur mais je pref comme ca
        private Facture convertDataToObject(SqliteDataReader reader)
        {
            Facture facture= new Facture();
            facture._Id = reader.GetInt32(0);
            facture._TempsEffectif = reader.GetInt32(1);
            facture._CoutEffectif= reader.GetInt32(2);
            facture._Commentaire = reader.GetString(3);
            return facture;
        }
        public List<Facture> getAllFacture()
        {
            List<Facture> listFacture = new List<Facture>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM facture";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Facture fac = convertDataToObject(reader);
                listFacture.Add(fac);
            }
            return listFacture;
        }
        public List<Facture> searchFacturesMultiParam(Dictionary<string, string> dic)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            string Query = "SELECT * FROM facture WHERE ";
            //sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier=" + name;
            foreach (KeyValuePair<string, string> param in dic)
            {
                if (param.Value != "")
                {
                    string where = param.Key + "==\'" + param.Value + "\'";
                    Query += where + " && ";
                }
            }
            Query = Query.Substring(0, Query.Length - 3);
            sqlCommand.CommandText = Query;
            Console.WriteLine(Query);
            List<Facture> listFacture = new List<Facture>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Facture ch = convertDataToObject(reader);
                listFacture.Add(ch);
            }
            return listFacture;
        }
        private void logFacturefromBDD(SqliteDataReader reader)
        {
            while (reader.Read())
            {
                Console.WriteLine($@"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
            }
        }
    }
}
