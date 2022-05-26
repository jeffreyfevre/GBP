using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modeles;
using static WpfApp1.modeles.TraceComptable;

namespace WpfApp1.wrappers
{
    internal class WrapTraceComptable
    {
        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");


        public void createTraceComptable(TraceComptable trace)
        {

            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO trace_comptable (prix,date_creation,type,commentaire,temps) VALUES ('" + trace._Prix + "','" + trace._DateCreation.ToString("yyyy-MM-dd HH:mm:ss.fff") + "','" + trace._Type + "','" + trace._Commentaire + "','" + trace._Temps + "')";
            sqlite_conn.Open();
            sqlCommand.ExecuteNonQuery();
            sqlite_conn.Close();

            TraceComptable tc = getLastTraceAdded();
            if (tc != null)
            {
                insertInTableAssociation2(trace,tc._Id);
            }
            Console.WriteLine(sqlCommand.CommandText);
            

        }
        private void insertInTableAssociation(TraceComptable tc)
        {
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            if (tc._Chantiers.Count != 0)
            {
                for (int i = 0; i < tc._Chantiers.Count; i++)
                {

                    sqlCommand.CommandText = "INSERT INTO chantier_trace  (id_trace,id_chantier) VALUES ('" + tc._Id + "','" + tc._Chantiers[i]._Id + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (tc._Compagnon.Count != 0)
            {
                for (int i = 0; i < tc._Compagnon.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO devis_compagnons  (id_trace,id_compagnon,prix) VALUES ('" + tc._Id + "','" + tc._Compagnon[i]._Id + "','" + tc._Prix + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            //TOTO ajouter la quantite
            if (tc._Materiaux.Count != 0)
            {
                for (int i = 0; i < tc._Materiaux.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO devis_materiaux  (id_trace,id_materiaux,prix) VALUES ('" + tc._Id + "','" + tc._Materiaux[i]._Id + "','" + tc._Prix + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private void insertInTableAssociation2(TraceComptable tc,int id)
        {
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            if (tc._Chantiers.Count != 0)
            {
                for (int i = 0; i < tc._Chantiers.Count; i++)
                {

                    sqlCommand.CommandText = "INSERT INTO chantier_trace  (id_trace,id_chantier) VALUES ('" + tc._Id + "','" + tc._Chantiers[i]._Id + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (tc._Compagnon.Count != 0)
            {
                for (int i = 0; i < tc._Compagnon.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO devis_compagnons  (id_trace,id_compagnon,prix) VALUES ('" + tc._Id + "','" + tc._Compagnon[i]._Id + "','" + tc._Prix + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
            //TOTO ajouter la quantite
            if (tc._Materiaux.Count != 0)
            {
                for (int i = 0; i < tc._Materiaux.Count; i++)
                {
                    sqlCommand.CommandText = "INSERT INTO devis_materiaux  (id_trace,id_materiaux,prix) VALUES ('" + tc._Id + "','" + tc._Materiaux[i]._Id + "','" + tc._Prix + "')";
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public TraceComptable readTraceComptable(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM trace_comptable WHERE id=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();
            if (rdr.Read())
            {
                TraceComptable tc = convertDataToObject(rdr);
                return tc;
            }
            return null;

        }
        public TraceComptable getLastTraceAdded()
        {
            List<TraceComptable> allTrace= getAllTraceComptable();
            TraceComptable lastTraceComptable = allTrace.OrderByDescending(x => x._Id).FirstOrDefault();
            return lastTraceComptable;
        }

        public List<TraceComptable> getAllTraceComptable()
        {

            List<TraceComptable> listTraceComptable = new List<TraceComptable>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM chantier";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {

                TraceComptable tc = convertDataToObject(reader);
                listTraceComptable.Add(tc);
            }
            return listTraceComptable;
        }

        public List<TraceComptable> searchChantierByName(int prix)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM trace_comptable WHERE prix=" + prix;
            List<TraceComptable> listTrace = new List<TraceComptable>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                TraceComptable trace = convertDataToObject(reader);
                listTrace.Add(trace);
            }
            return listTrace;
        }

        //les chants du dictionnaires sont celui de la des champs de la bdd
        public List<TraceComptable> searchChantierMultiParam(Dictionary<string, string> dic)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            string Query = "SELECT * FROM trace_comptable WHERE";
            //sqlCommand.CommandText = "SELECT * FROM chantier WHERE nom_chantier=" + name;
            foreach (KeyValuePair<string, string> param in dic)
            {
                string where = param.Key + "=" + param.Value;
                Query += where;
            }

            sqlCommand.CommandText = Query;
            List<TraceComptable> listTrace = new List<TraceComptable>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                TraceComptable tc = convertDataToObject(reader);
                listTrace.Add(tc);
            }
            return listTrace;
        }
        public void updateTraceComptable(TraceComptable tc)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();


            sqlCommand.CommandText = "UPDATE trace_comptable SET prix = '" + tc._Prix + "', date_creation = '" + tc._DateCreation + "', type = '" + tc._Type + "', commentaire ='" + tc._Commentaire + "' ,temps ='" + tc._Temps + "'  WHERE id = " + tc._Id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM chantier_trace WHERE id_trace=" + tc._Id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM devis_compagnons WHERE id_trace=" + tc._Id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM devis_materiaux WHERE id_trace=" + tc._Id;
            sqlCommand.ExecuteNonQuery();
            if (tc != null)
            {
                insertInTableAssociation(tc);
            }

        }
        public void deleteFacture(TraceComptable trace)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM trace_comptable WHERE id=" + trace._Id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM chantier_trace WHERE id_trace=" + trace._Id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM devis_compagnons WHERE id_trace=" + trace._Id;
            sqlCommand.ExecuteNonQuery();
            sqlCommand.CommandText = "DELETE FROM devis_materiaux WHERE id_trace=" + trace._Id;
            sqlCommand.ExecuteNonQuery();
        }
        //je sais que je peux use le constructeur mais je pref comme ca
        private TraceComptable convertDataToObject(SqliteDataReader reader)
        {
            TraceComptable trace = new TraceComptable();
            trace._Id = reader.GetInt32(0);
            trace._Prix = reader.GetInt32(1);
            trace._DateCreation = reader.GetDateTime(2);
            trace._Type = (Types)reader.GetInt32(3);
            trace._Temps = reader.GetInt32(5);
            trace._Commentaire = reader.GetString(4);
            trace._Materiaux = getAllMateriauxForOneTrace(trace._Id);
            trace._Compagnon = getAllCompagnonsForOneTrace(trace._Id);
            trace._Chantiers = getAllChantiernsForOneTrace(trace._Id);

            return trace;
        }

        public List<TraceComptable> getAllFacture()
        {
            List<TraceComptable> listFacture = new List<TraceComptable>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM trace_comptable WHERE type=\"Facture\"";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                TraceComptable fac = convertDataToObject(reader);
                listFacture.Add(fac);
            }
            return listFacture;
        }
        public List<TraceComptable> getAllDevis()
        {
            List<TraceComptable> listDevis = new List<TraceComptable>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM trace_comptable WHERE type=\"Devis\"";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                TraceComptable devis = convertDataToObject(reader);
                listDevis.Add(devis);
            }
            return listDevis;
        }
        public List<Materiaux> getAllMateriauxForOneTrace(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM devis_materiaux WHERE id_materiaux=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(1);
                listId.Add(idGet);
            }
            WrapMateriaux wrapMat = new WrapMateriaux();
            List<Materiaux> listMat = new List<Materiaux>();
            for (int i = 0; i < listId.Count; i++)
            {
                Materiaux mat = wrapMat.readMateriaux(listId[i]);
                listMat.Add(mat);
            }
            return listMat;
        }
        public List<Compagnon> getAllCompagnonsForOneTrace(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM devis_compagnons WHERE id_compagnon=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(1);
                listId.Add(idGet);
            }
            WrapCompagnon wrapComp = new WrapCompagnon();
            List<Compagnon> listComp = new List<Compagnon>();
            for (int i = 0; i < listId.Count; i++)
            {
                Compagnon comp = wrapComp.readCompagnon(listId[i]);
                listComp.Add(comp);
            }
            return listComp;
        }
        public List<Chantier> getAllChantiernsForOneTrace(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            List<int> listId = new List<int>();
            sqlCommand.CommandText = "SELECT * FROM chantier_trace WHERE id_trace=" + id;
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                int idGet = reader.GetInt32(1);
                listId.Add(idGet);
            }
            WrapChantier wrapChant = new WrapChantier();
            List<Chantier> listChant = new List<Chantier>();
            for (int i = 0; i < listId.Count; i++)
            {
                Chantier chant = wrapChant.readChantier(listId[i]);
                listChant.Add(chant);
            }
            return listChant;
        }
        public List<TraceComptable> searchFacturesMultiParam(Dictionary<string, string> dic)
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
            List<TraceComptable> listTrace = new List<TraceComptable>();
            SqliteDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                TraceComptable TC = convertDataToObject(reader);
                listTrace.Add(TC);
            }
            return listTrace;
        }
        private void updateTableAssociation(TraceComptable tc, SqliteCommand sqlCommand)
        {
            if (tc._Compagnon.Count != 0)
            {
                for (int i = 0; i < tc._Compagnon.Count; i++)
                {
                    sqlCommand.CommandText = "UPDATE devis_compagnons SET id_trace = '" + tc._Id + "', id_compagnon = '" + tc._Compagnon[i]._Id + "', prix = '" + tc._Prix + "' WHERE id_compagnon = '" + tc._Compagnon[i]._Id + "' AND id_trace = " + tc._Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            if (tc._Chantiers.Count != 0)
            {
                for (int i = 0; i < tc._Chantiers.Count; i++)
                {
                    sqlCommand.CommandText = "UPDATE chantier_trace SET id_trace = '" + tc._Id + "', id_chantier = '" + tc._Chantiers[i]._Id + "' WHERE id_chantier = '" + tc._Chantiers[i]._Id + "' AND id_trace = " + tc._Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }
            //TODO ajouter la quantite
            if (tc._Materiaux.Count != 0)
            {
                for (int i = 0; i < tc._Materiaux.Count; i++)
                {
                    sqlCommand.CommandText = "UPDATE devis_materieux SET id_trace = '" + tc._Id + "', id_materiaux = '" + tc._Materiaux[i]._Id + "', prix = '" + tc._Prix + "' WHERE id_trace = '" + tc._Id + "' AND id_materiaux = " + tc._Materiaux[i]._Id;
                    sqlCommand.ExecuteNonQuery();
                }
            }

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
