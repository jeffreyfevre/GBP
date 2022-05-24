using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;


namespace WpfApp1.wrapper
{
    public class ManageBDD
    {

        public ManageBDD()
        {

        }
        public void initBDD()
        {
            initFolderBDD();
            SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");
            sqlite_conn.Open();
            var sqlCommand = sqlite_conn.CreateCommand();
            //chantier
            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS chantier ( id_chantier   INTEGER NOT NULL, adresse   VARCHAR(50), nom_chantier  VARCHAR(50), chantier_com  VARCHAR(500), telephone VARCHAR(50),date_creation DATETIME, etat  INTEGER, zipcode   INTEGER, numero    INTEGER,date_fin  DATETIME,PRIMARY KEY(id_chantier AUTOINCREMENT))";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS chantier_trace ( id INTEGER NOT NULL, id_chantier INTEGER NOT NULL, id_trace INTEGER NOT NULL, PRIMARY KEY(id AUTOINCREMENT) );";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS compagnon ( id_compagnon INTEGER NOT NULL, nom VARCHAR(20), telephone VARCHAR(12), cout_horaire NUMERIC, date_embauche DATETIME, compagnon_com VARCHAR(50), prenom VARCHAR(20), PRIMARY KEY(id_compagnon AUTOINCREMENT) )";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS compagnons_chantier ( id INTEGER NOT NULL, id_compagnon INTEGER NOT NULL, id_chantier INTEGER NOT NULL, date_debut DATETIME, date_fin DATETIME, PRIMARY KEY(id AUTOINCREMENT) )";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS devis_compagnons ( id INTEGER NOT NULL, id_compagnon INTEGER NOT NULL, id_trace INTEGER NOT NULL, prix INTEGER, PRIMARY KEY(id AUTOINCREMENT) )";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS devis_materiaux(id INTEGER NOT NULL, id_materiaux INTEGER NOT NULL, id_trace INTEGER NOT NULL, prix INTEGER, quantite INTEGER, PRIMARY KEY(id AUTOINCREMENT))";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS fournisseur ( id INTEGER NOT NULL, nom VARCHAR(50), commentaire VARCHAR(500), telephone VARCHAR(12), adresse VARCHAR(50), zipcode TEXT, PRIMARY KEY(id AUTOINCREMENT) )";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS materiaux ( id INTEGER NOT NULL, nom VARCHAR(50), fournisseur_id INTEGER, prix INTEGER, description VARCHAR(50), PRIMARY KEY(id AUTOINCREMENT) )";
            sqlCommand.ExecuteNonQuery();

            sqlCommand.CommandText = "CREATE TABLE IF NOT EXISTS trace_comptable ( id INTEGER NOT NULL, prix NUMERIC, date_creation DATETIME, type INTEGER, commentaire TEXT, temps NUMERIC, PRIMARY KEY(id AUTOINCREMENT) )";
            sqlCommand.ExecuteNonQuery();



        }
        public void initFolderBDD()
        {
            // Specify the directory you want to manipulate.
            string path = @"C:\ProgramData\GBD";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                //di.Delete();
                Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }


        }


    }
}
