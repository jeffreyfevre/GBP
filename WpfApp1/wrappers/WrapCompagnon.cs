using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            sqlCommand.CommandText = "INSERT INTO compagnon (nom,telephone,cout_horaire,date_embauche,compagnon_com,prenom) VALUES ('" + compagnon._Name + "','" + compagnon._Telephone + "','" + compagnon._CoutHoraire + "','" + compagnon._DateEmbauche.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + compagnon._Commentaire + "','" + compagnon._Prenom + "')";
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery();
            Compagnon lastComp = getLastCompagnonAdded();
            for (int i = 0; i < compagnon._Chantiers.Count; i++)
            {

                sqlCommand.CommandText = "INSERT INTO compagnons_chantier  (id_chantier,id_compagnon) VALUES ('" + compagnon._Chantiers[i]._Id + "','" + lastComp._Id + "')";
                sqlCommand.ExecuteNonQuery();
            }
            sqlite_conn.Close();

        }
        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Compagnon readCompagnon(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM compagnon WHERE id_compagnon=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            Compagnon compagnon = new Compagnon();  
            if (rdr.Read())
            {
                compagnon = convertDataToObject(rdr);
            }
            return compagnon;

        }
        public Compagnon readCompagnonListLess(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM compagnon WHERE id_compagnon=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            Compagnon compagnon = new Compagnon();
            if (rdr.Read())
            {
                compagnon = convertDataToObjectListLess(rdr);
            }
            return compagnon;

        }
        public void updateCompagnon(Compagnon compagnon, int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            sqlCommand.CommandText = "UPDATE compagnon SET nom = '" + compagnon._Name + "', telephone = '" + compagnon._Telephone + "', cout_horaire = '" + compagnon._CoutHoraire + "', date_embauche = '" + compagnon._DateEmbauche.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', compagnon_com = '" + compagnon._Commentaire + "',prenom = '" + compagnon._Prenom + "' WHERE id_compagnon =" + id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM compagnons_chantier WHERE id_compagnon=" + id;
            sqlCommand.ExecuteNonQuery();
            for (int i = 0; i < compagnon._Chantiers.Count; i++)
            {

                sqlCommand.CommandText = "INSERT INTO compagnons_chantier  (id_chantier,id_compagnon) VALUES ('" + compagnon._Chantiers[i]._Id + "','" + compagnon._Id + "')";
                sqlCommand.ExecuteNonQuery();
            }

        }
        public Compagnon getLastCompagnonAdded()
        {
            List<Compagnon> allComp = getAllCompagnon();
            Compagnon lastCompagnon = allComp.OrderByDescending(x => x._Id).FirstOrDefault();
            return lastCompagnon;
        }
        public void deleteCompagnon(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM compagnon WHERE id_compagnon=" + id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM compagnons_chantier WHERE id_compagnon=" + id;
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
                listCompagnon.Add(dev);
            }

            return listCompagnon;

        }
        public List<Compagnon> searchCompagnonByName(string name)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM compagnon WHERE nom='" + name + "'";
            List<Compagnon> listCompagnon = new List<Compagnon>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {

                Compagnon compagnon = convertDataToObject(reader);
                listCompagnon.Add(compagnon);
                Console.WriteLine($@"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
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
            sqlite_conn.Close();

            return listCompanons;
        }
        //je sais que je peux use le constructeur mais je pref comme ca
        private Compagnon convertDataToObject(SqliteDataReader reader)
        {
            Compagnon compagnon = new Compagnon();
            compagnon._Id = reader.GetInt32(0);
            compagnon._Name = reader.GetString(1);
            compagnon._Telephone = reader.GetString(2);
            compagnon._CoutHoraire = reader.GetInt32(3);
            compagnon._DateEmbauche = reader.GetDateTime(4);
            compagnon._Commentaire = reader.GetString(5);
            compagnon._Chantiers = getAllChantiersForOneCompagnon(compagnon._Id);

            return compagnon;
        }
        private Compagnon convertDataToObjectListLess(SqliteDataReader reader)
        {
            Compagnon compagnon = new Compagnon();
            compagnon._Id = reader.GetInt32(0);
            compagnon._Name = reader.GetString(1);
            compagnon._Telephone = reader.GetString(2);
            compagnon._CoutHoraire = reader.GetInt32(3);
            compagnon._DateEmbauche = reader.GetDateTime(4);
            compagnon._Commentaire = reader.GetString(5);

            return compagnon;
        }
        public List<Chantier> getAllChantiersForOneCompagnon(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM compagnons_chantier WHERE Id=" + id;
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
                Chantier chantier = wrapChantier.readChantierListless(listId[i]);
                listChantiers.Add(chantier);
            }

            return listChantiers;
        }
        
        
    }
}
