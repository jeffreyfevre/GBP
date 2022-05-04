using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using WpfApp1.modeles;
using WpfApp1.modèles;

namespace WpfApp1.wrappers
{
    //TODO les champs de types listes et elles ne sont pas dans la bdd
    internal class WrapChantier
    {
        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");

        public WrapChantier()
        {
        }

        public void createChantier(Chantier chantier)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO chantier (adresse,nom_chantier,chantier_com,etat) VALUES ('"+chantier._Adresse+"','"+chantier._NomChantier+"','"+chantier._Commentaire+ "','" + chantier._Etat + "')";
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery();
            for (int i = 0; i < chantier._factures.Count; i++)
            {
                var args2 = new Dictionary<string, object>
                {
                    {"@id_chantier", chantier._Id},
                    {"@id_facture", chantier._factures[i]},
                };
                sqlCommand.CommandText = "INSERT INTO chantier_xfacture  (id_facture,id_chantier) VALUES ('"+chantier._factures[i]+"','"+chantier._Id+"')";
                sqlCommand.ExecuteNonQuery();
            }
            for (int i = 0; i < chantier._devis.Count; i++)
            {
                var args2 = new Dictionary<string, object>
                {
                    {"@id_chantier", chantier._Id},
                    {"@id_devis", chantier._devis[i]},
                };
                sqlCommand.CommandText = "INSERT INTO chantier_xdevis  (id_devis,id_chantier) VALUES ('" + chantier._devis[i] + "','" + chantier._Id + "')";
                sqlCommand.ExecuteNonQuery();
            }
            //sqlCommand.CommandText = "INSERT INTO chantier VALUES ('0','9 rue julien chavoutier', 'maison36', 'il existe pas');";
        }
        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Chantier readChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM chantier WHERE id_chantier="+id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            return convertDataToObject(rdr);
        }
        public void updateChantier(Chantier chantier)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            var args = new Dictionary<string, object>
            {
                {"@id", chantier._Id},
                {"@adresse", chantier._Adresse },
                {"@nom", chantier._NomChantier },
                {"@com", chantier._Commentaire }
            };

            sqlCommand.CommandText = "UPDATE chantier SET adresse = @adresse, nom_chantier = @nom, chantier_com = @com WHERE Id == @id";
            sqlCommand.ExecuteNonQuery();
            //pas tres beau
            for (int i = 0; i < chantier._factures.Count; i++)
            {
                var args2 = new Dictionary<string,object>
                {
                    {"@id_chantier", chantier._Id},
                    {"@id_facture", chantier._factures[i]},
                };
                sqlCommand.CommandText = "UPDATE chantier_xfacture SET id_facture = @id_facture, id_chantier = @id_chantier WHERE id_chantier == @id_chantier&& id_facture==id_facture";
                sqlCommand.ExecuteNonQuery(); 
            }
            for (int i = 0; i < chantier._devis.Count; i++)
            {
                var args2 = new Dictionary<string, object>
                {
                    {"@id_chantier", chantier._Id},
                    {"@id_devis", chantier._devis[i]},
                };
                sqlCommand.CommandText = "UPDATE chantier_xdevis SET id_devis = @id_devis, id_chantier = @id_chantier WHERE id_chantier == @id_chantier&& id_devis == id_devis";
                sqlCommand.ExecuteNonQuery();
            }

        }
        public void deleteChantier(Chantier chantier)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM chantier WHERE id_chantier=" + chantier._Id;
            sqlCommand.ExecuteNonQuery();

        }
        public List<Chantier> searchChantierMultiParam(Dictionary<string, string> dic)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            string Query = "SELECT * FROM chantier WHERE ";
            //sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier=" + name;
            foreach (KeyValuePair<string, string> param in dic)
            {
                if (param.Value != "")
                {
                    string where = param.Key + "==\'" + param.Value+"\'";
                    Query += where + " && ";
                }
            }
            Query = Query.Substring(0, Query.Length - 3);
            sqlCommand.CommandText = Query;
            Console.WriteLine(Query);
            List<Chantier> listChantier = new List<Chantier>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Chantier ch = convertDataToObject(reader);
                listChantier.Add(ch);
            }
            return listChantier;
        }
        public List<Chantier> searchChantierByName(string name)
       {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier=" + name;
            List<Chantier> listChantier = new List<Chantier>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Chantier ch = convertDataToObject(reader);
                ch.jToString();
                listChantier.Add(ch);
                Console.WriteLine($@"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
            }
            return listChantier;
       }

        public List<Chantier> getAllChantier()
        {
            List<Chantier> listChantier = new List<Chantier>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM chantier";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Chantier ch = convertDataToObject(reader);
                ch.jToString();
                listChantier.Add(ch);    
            }
            return listChantier;
        }
        //je sais que j&e peux use le constructeur mais je pref comme ca
        private Chantier convertDataToObject(SqliteDataReader reader)
        {
            Chantier chantier = new Chantier();

            chantier._Id = reader.GetInt32(0);
            chantier._Adresse = reader.GetString(1);
            chantier._NomChantier = reader.GetString(2);
            chantier._Commentaire = reader.GetString(3);
            chantier._factures = getAllFactureForOneChantier(chantier._Id);
            chantier._devis = getAlldevisForOneChantier(chantier._Id);
            return chantier;
        }
        private List<Devis> getAlldevisForOneChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM chantier_xdevis WHERE id_chantier=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(1);
                listId.Add(idGet);
            }
            WrapDevis wrapDevis = new WrapDevis();
            List<Devis> listDevis = new List<Devis>();
            for (int i = 0; i < listId.Count; i++)
            {
                Devis devis = wrapDevis.readDevis(listId[i]);
                listDevis.Add(devis);
            }
            return listDevis;
        }
        private List<Facture> getAllFactureForOneChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM chantier_xfacture WHERE id_chantier=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(1);
                listId.Add(idGet);
            }
            WrapFacture wrapFacture = new WrapFacture();
            List<Facture> listFacture = new List<Facture>();
            for (int i = 0; i < listId.Count; i++)
            {
                Facture facture = wrapFacture.readFacture(listId[i]);
                listFacture.Add(facture);
            }
            return listFacture;
        }

        private void logChantierfromReader(SqliteDataReader reader)
        {
            while (reader.Read())
            {
                Console.WriteLine($@"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
            }
        }
        
    }
}
