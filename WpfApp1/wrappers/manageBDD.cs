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
    public class manageBDD
    {

        public manageBDD()
        {

        }

        public void test()
        {
            // Specify the directory you want to manipulate.
            string path = @"C:\ProgramData\GBD";

            try
            {
                // Determine whether the directory exists.
                if(Directory.Exists( path ))
                {
                    Console.WriteLine( "That path exists already." );
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory( path );
                Console.WriteLine( "The directory was created successfully at {0}.", Directory.GetCreationTime( path ) );

                // Delete the directory.
                //di.Delete();
                Console.WriteLine( "The directory was deleted successfully." );
            }
            catch(Exception e)
            {
                Console.WriteLine( "The process failed: {0}", e.ToString() );
            }

            SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");
            sqlite_conn.Open();
            var sqlCommand = sqlite_conn.CreateCommand();
            //sqlCommand.CommandText = "CREATE TABLE chantier ( id_chantier INTEGER NOT NULL, adresse TEXT, nom_chantier TEXT, chantier_com TEXT, PRIMARY KEY(id_chantier AUTOINCREMENT) )";
            //sqlCommand.CommandText = "CREATE TABLE compagnon ( id_conpagnon INTEGER NOT NULL, name TEXT, telephone INTEGER, cout_horaire NUMERIC, date_time TEXT, compagnon_com TEXT, PRIMARY KEY(id_conpagnon AUTOINCREMENT) )";
            //sqlCommand.CommandText = "CREATE TABLE devis ( id_devis INTEGER NOT NULL, temps_prevu TEXT, cout_prevu NUMERIC, devis_com TEXT, PRIMARY KEY(id_devis AUTOINCREMENT) )";
            //sqlCommand.CommandText = "CREATE TABLE facture ( id_facture INTEGER NOT NULL, temps_effectif TEXT, cout_effectif NUMERIC, facture_com TEXT, PRIMARY KEY(id_facture AUTOINCREMENT) )";
            //sqlCommand.CommandText = "select * from chantier";
            //sqlCommand.CommandText = "DROP TABLE compagnon";
            //sqlCommand.CommandText = "INSERT INTO chantier VALUES ('0','9 rue julien chavoutier', 'maison36', 'il existe pas');";

            var rdr = sqlCommand.ExecuteReader();
            
            while (rdr.Read())
            {
                Console.WriteLine($@"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)} {rdr.GetString(3)}");
                
            }
        }


    }
}
