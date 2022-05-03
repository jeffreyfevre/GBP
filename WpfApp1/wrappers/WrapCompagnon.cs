using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modèles;
using WpfApp1.modeles;

namespace WpfApp1.wrappers
{
    internal class WrapCompagnon
    {

        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");


        public void createCompagnon(Compagnon compagnon)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO compagnon VALUES ('" + compagnon._Id + "','" + compagnon._Name + "','" + compagnon._Telephone + "','" + compagnon._CoutHoraire + "','" + compagnon._DateEmbauche.ToString() + "','" + compagnon._Commentaire+"')";
            Console.WriteLine(sqlCommand.CommandText);
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
        }
        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Compagnon readCompagnon(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM compagnon WHERE id_compagnon=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            return convertDataToObject(rdr);
        }
        public void updateCompagnon(Compagnon compagnon)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            var args = new Dictionary<string, object>
            {
                {"@id", compagnon._Id},
                {"@name", compagnon._Name},
                {"@tel", compagnon._Telephone},
                {"@ch", compagnon._CoutHoraire},
                {"@DE", compagnon._DateEmbauche},
                {"@com", compagnon._Commentaire }
            };

            sqlCommand.CommandText = "UPDATE compagnon SET name = @name, telephone = @tel, cout_horaire = @ch , date_embauche = @DE, compagnon_com = @com WHERE Id = @id";
            sqlCommand.ExecuteNonQuery();
            for (int i = 0; i < compagnon._Chantiers.Count; i++)
            {
                var args2 = new Dictionary<string, object>
                {
                    {"@id_compagnon", compagnon._Id},
                    {"@id_chantier", compagnon._Chantiers[i]},
                };
                sqlCommand.CommandText = "UPDATE chantier_xcompagnon SET id_compagnon = @id_compagnon, id_chantier = @id_chantier WHERE id_chantier ==@id_chantier&& id_compagnon==id_compagnon";
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void deleteCompagnon(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM compagnon WHERE id_compagnon=" + id;
            sqlCommand.ExecuteNonQuery();

        }
        public List<Compagnon> getAllCompagnon()
        {
            List<Compagnon> listCompagnon = new List<Compagnon>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM compagnon";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Compagnon dev = convertDataToObject(reader);
                Console.WriteLine("coucou");
                Console.WriteLine("compagnon :"+dev.jToString());
                listCompagnon.Add(dev);
            }
            return listCompagnon;
        }
        public List<Compagnon> searchCompagnonsMultiParam(Dictionary<string, string> dic)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            string Query = "SELECT * FROM compagnon WHERE ";
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
            List<Compagnon> listCompanons = new List<Compagnon>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Compagnon ch = convertDataToObject(reader);
                listCompanons.Add(ch);
            }
            return listCompanons;
        }
        //je sais que je peux use le constructeur mais je pref comme ca
        private Compagnon convertDataToObject(SqliteDataReader reader)
        {
            Compagnon compagnon = new Compagnon();
            compagnon._Id = reader.GetInt32(0);
            compagnon._Name = reader.GetString(1);
            compagnon._Telephone = reader.GetInt32(2);
            compagnon._CoutHoraire = reader.GetInt32(3);
            compagnon._DateEmbauche = reader.GetString(4);
            compagnon._Commentaire = reader.GetString(5);
            compagnon._Chantiers = getAllChantiersForOneCompagnon(compagnon._Id);
            return compagnon;
        }
        private List<Chantier> getAllChantiersForOneCompagnon(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM compagnon_xchantier WHERE id_compagnon=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(1);
                listId.Add(idGet);
            }
            WrapChantier wrapChantier = new WrapChantier();
            List<Chantier> listChantiers = new List<Chantier>();
            for (int i = 0; i < listId.Count; i++)
            {
                Chantier chantier = wrapChantier.readChantier(listId[i]);
                listChantiers.Add(chantier);
            }
            return listChantiers;
        }
        private void logCompagnonfromBDD(SqliteDataReader reader)
        {
            while (reader.Read())
            {
                Console.WriteLine($@"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
            }
        }
    }
}
