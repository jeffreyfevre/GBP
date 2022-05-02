﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modeles;

namespace WpfApp1.wrappers
{
    internal class WrapDevis
    {
        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");


        public void createDevis(Devis devis)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO devis VALUES ('" + devis._Id + "','" + devis._TempsPrevu + "','" + devis._CoutPrevu + "','" + devis._Commentaire + "')";
            Console.WriteLine(sqlCommand.CommandText);
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
        }
        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Devis readDevis(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM devis WHERE id_devis=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            return convertDataToObject(rdr);
        }
        public void updateDevis(Devis devis)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            var args = new Dictionary<string, object>
            {
                {"@id", devis._Id},
                {"@tempsPrevu", devis._TempsPrevu},
                {"@coutPrevu", devis._CoutPrevu},
                {"@com", devis._Commentaire }
            };

            sqlCommand.CommandText = "UPDATE devis SET temps_prevu = @tempsPrevu, cout_prevu = @coutPrevu, devis_com = @com WHERE Id = @id";
            sqlCommand.ExecuteNonQuery();
        }
        public void deleteDevis(Devis devis)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM devis WHERE id_devis=" + devis._Id;
            sqlCommand.ExecuteNonQuery();

        }
        //je sais que je peux use le constructeur mais je pref comme ca
        private Devis convertDataToObject(SqliteDataReader reader)
        {
            var devis = new Devis();
            devis._Id = reader.GetInt32(0);
            devis._TempsPrevu = reader.GetInt32(1);
            devis._CoutPrevu = reader.GetInt32(2);
            devis._Commentaire = reader.GetString(3);
            return devis;
        }
        public List<Devis> getAllDevis()
        {
            List<Devis> listDevis = new List<Devis>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM devis";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Devis dev = convertDataToObject(reader);

                listDevis.Add(dev);
            }
            return listDevis;
        }
        public List<Devis> searchDevisMultiParam(Dictionary<string, string> dic)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            string Query = "SELECT * FROM devis WHERE ";
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
            List<Devis> listDevis = new List<Devis>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                Devis ch = convertDataToObject(reader);
                listDevis.Add(ch);
            }
            return listDevis;
        }
        private void logDevisfromBDD(SqliteDataReader reader)
        {
            while (reader.Read())
            {
                Console.WriteLine($@"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetString(2)} {reader.GetString(3)}");
            }
        }

    }
}
