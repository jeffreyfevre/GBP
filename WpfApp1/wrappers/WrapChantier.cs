using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using WpfApp1.modeles;

using static WpfApp1.modeles.Chantier;

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
            sqlCommand.CommandText = "INSERT INTO chantier (adresse,nom_chantier,chantier_com,telephone,date_creation,etat,zipcode,numero,date_fin) VALUES ('" + chantier._Adresse + "','" + chantier._NomChantier + "','" + chantier._Commentaire + "','" + chantier._Telephone + "','" + chantier._DateCreation.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + chantier._Etat + "','" + chantier._ZipCode + "','" + chantier._Numero + "','" + chantier._DateFin.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery();
            Chantier ch = getLastChantierAdded();
            insertInTableAssociation2(chantier,ch._Id);
            sqlite_conn.Close();


        }

        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Chantier readChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM chantier WHERE id_chantier=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            Chantier chantier = new Chantier();
            if (rdr.Read())
            {
                chantier = convertDataToObject(rdr);
                return chantier;
            }
            sqlite_conn.Close();

            return chantier;
        }
        public Chantier getLastChantierAdded()
        {
            List<Chantier> allchant= getAllChantier();
            Chantier lastChantier = allchant.OrderByDescending(x => x._Id ).FirstOrDefault();
            return lastChantier;
        }
        private void insertInTableAssociation(Chantier chantier)
        {
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            if (chantier._factures != null)
            {
                for (int i = 0; i < chantier._factures.Count; i++)
                {

                    sqlCommand.CommandText = "INSERT INTO chantier_trace  (id_trace,id_chantier) VALUES ('" + chantier._factures[i]._Id + "','" + chantier._Id + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (chantier._devis != null)
            {
                for (int i = 0; i < chantier._devis.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO chantier_trace  (id_trace,id_chantier) VALUES ('" + chantier._devis[i]._Id + "','" + chantier._Id + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (chantier._compagnon != null)
            {
                for (int i = 0; i < chantier._compagnon.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO compagnons_chantier  (id_compagnon,id_chantier,date_debut,date_fin) VALUES ('" + chantier._devis[i] + "','" + chantier._Id + "','" + chantier._DateCreation + "','" + chantier._DateFin + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void insertInTableAssociation2(Chantier chantier,int id)
        {
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            if (chantier._factures != null)
            {
                for (int i = 0; i < chantier._factures.Count; i++)
                {

                    sqlCommand.CommandText = "INSERT INTO chantier_trace  (id_trace,id_chantier) VALUES ('" + chantier._factures[i]._Id + "','" + id + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (chantier._devis != null)
            {
                for (int i = 0; i < chantier._devis.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO chantier_trace  (id_trace,id_chantier) VALUES ('" + chantier._devis[i]._Id + "','" + id + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (chantier._compagnon != null)
            {
                for (int i = 0; i < chantier._compagnon.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO compagnons_chantier  (id_compagnon,id_chantier,date_debut,date_fin) VALUES ('" + chantier._devis[i] + "','" + id + "','" + chantier._DateCreation + "','" + chantier._DateFin + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void updateTableAssociation(Chantier chantier, SqliteCommand sqlCommand)
        {
            if (chantier._factures.Count != 0)
            {
                for (int i = 0; i < chantier._factures.Count; i++)
                {
                    sqlCommand.CommandText = "UPDATE chantier_trace SET id_trace = '" + chantier._factures[i]._Id + "', id_chantier = '" + chantier._Id + "' WHERE id_chantier = '" + chantier._Id + "' AND id_trace = " + chantier._factures[i]._Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (chantier._devis.Count != 0)
            {
                for (int i = 0; i < chantier._devis.Count; i++)
                {
                    sqlCommand.CommandText = "UPDATE chantier_trace SET id_trace = '" + chantier._devis[i]._Id + "', id_chantier = '" + chantier._Id + "' WHERE id_chantier = '" + chantier._Id + "' AND id_trace = " + chantier._devis[i]._Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (chantier._compagnon.Count != 0)
            {
                for (int i = 0; i < chantier._compagnon.Count; i++)
                {
                    sqlCommand.CommandText = "UPDATE compagnons_chantier SET id_compagnon = '" + chantier._compagnon[i]._Id + "', id_chantier = '" + chantier._Id + "' WHERE id_chantier = '" + chantier._Id + "' AND id_compagnon = " + chantier._compagnon[i]._Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }

        }
        //TODO update
        public void updateChantier(Chantier chantier, int id)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            sqlCommand.CommandText = "UPDATE chantier SET adresse ='" + chantier._Adresse + "', nom_chantier ='" + chantier._NomChantier + "', chantier_com ='" + chantier._Commentaire + "' , telephone ='" + chantier._Telephone + "', date_creation = '" + chantier._DateCreation.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', etat = '" + chantier._Etat + "', zipcode = '" + chantier._ZipCode + "',numero= '" + chantier._Numero + "',date_fin = '" + chantier._DateFin.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' WHERE id_chantier =" + id;
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery(); ;
            if (chantier._devis.Count != 0)
            {
                sqlCommand.CommandText = "DELETE FROM chantier_trace WHERE id_chantier=" + id;
                sqlCommand.ExecuteNonQuery();

            }
            if (chantier._factures.Count != 0)
            {
                sqlCommand.CommandText = "DELETE FROM chantier_trace WHERE id_chantier=" + id;
                sqlCommand.ExecuteNonQuery();

            }
            if (chantier._compagnon.Count != 0)
            {
                sqlCommand.CommandText = "DELETE FROM compagnon_chantier WHERE id_chantier=" + id;
                sqlCommand.ExecuteNonQuery();

            }
            if (chantier != null)
            {
                insertInTableAssociation(chantier);

            }
            //pas tres beau

        }

        public void deleteChantier(Chantier chantier)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM chantier WHERE id_chantier=" + chantier._Id;
            sqlCommand.ExecuteNonQuery();
            if (chantier._devis.Count != 0)
            {
                sqlCommand.CommandText = "DELETE FROM chantier_trace WHERE id_chantier=" + chantier._Id;
                sqlCommand.ExecuteNonQuery();

            }
            if (chantier._factures.Count != 0)
            {
                sqlCommand.CommandText = "DELETE FROM chantier_trace WHERE id_chantier=" + chantier._Id;
                sqlCommand.ExecuteNonQuery();

            }
            if (chantier._compagnon.Count != 0)
            {
                sqlCommand.CommandText = "DELETE FROM compagnon_chantier WHERE id_chantier=" + chantier._Id;
                sqlCommand.ExecuteNonQuery();

            }
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

                    string where = param.Key + "==\'" + param.Value + "\'";
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
            sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier='" + name + "'";
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
            chantier._Telephone = reader.GetString(4);
            chantier._DateCreation = reader.GetDateTime(5);
            chantier._Etat = (State)reader.GetInt32(6);
            chantier._ZipCode = reader.GetString(7);
            chantier._Numero = reader.GetString(8);
            chantier._DateFin = reader.GetDateTime(9);
            chantier._factures = getAllFactureForOneChantier(chantier._Id);
            chantier._devis = getAlldevisForOneChantier(chantier._Id);
            chantier._compagnon = getAllCompagnonsForOneChantier(chantier._Id);
            return chantier;
        }

        public List<TraceComptable> getAllFactureForOneChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM chantier_trace WHERE id_Chantier=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(0);
                listId.Add(idGet);
            }
            WrapTraceComptable wrapTrace = new WrapTraceComptable();
            List<TraceComptable> listTrace = new List<TraceComptable>();
            for (int i = 0; i < listId.Count; i++)
            {
                //je vois pas l'interet de faire ca 
                TraceComptable facture = wrapTrace.readTraceComptable(listId[i]);
                if (facture != null && facture._Type == 0)
                {
                    listTrace.Add(facture);
                }
            }
            return listTrace;
        }
        public List<TraceComptable> getAlldevisForOneChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM chantier_trace WHERE id_Chantier=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(2);
                listId.Add(idGet);
            }
            WrapTraceComptable wrapTrace = new WrapTraceComptable();
            List<TraceComptable> listTrace = new List<TraceComptable>();
            for (int i = 0; i < listId.Count; i++)
            {
                //je vois pas l'interet de faire ca 
                TraceComptable devis = wrapTrace.readTraceComptable(listId[i]);
                if (devis != null && devis._Type == 0)
                {
                    listTrace.Add(devis);
                }
            }
            return listTrace;
        }

        public List<Compagnon> getAllCompagnonsForOneChantier(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM compagnons_chantier WHERE id_chantier=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(0);
                listId.Add(idGet);
            }
            WrapCompagnon wrapFacture = new WrapCompagnon();
            List<Compagnon> listFacture = new List<Compagnon>();
            for (int i = 0; i < listId.Count; i++)
            {
                Compagnon compagnon = wrapFacture.getAllCompagnon().Where(x => x._Id == listId[i]).FirstOrDefault();
                listFacture.Add(compagnon);
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
