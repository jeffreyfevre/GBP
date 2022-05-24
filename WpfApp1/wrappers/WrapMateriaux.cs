using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modeles;

namespace WpfApp1.wrappers
{
    internal class WrapMateriaux
    {
        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");

        public void createMateriaux(Materiaux mat)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO materiaux (nom,fournisseur_id,prix,description) VALUES ('" + mat._Nom + "','" + mat._FournisseurId + "','" + mat._Prix + "','" + mat._Description + "')";
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery();

        }

        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Materiaux readMateriaux(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM materiaux WHERE id=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();

            return convertDataToObject(rdr);
        }

        public void updateMateriaux(Materiaux mat)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            var args = new Dictionary<string, object>
            {

                {"@id", mat._Id},
                {"@nom", mat._Nom},
                {"@fournisseur_id", mat._FournisseurId},
                {"@description", mat._Description},
                {"@prix", mat._Prix}
            };

            sqlCommand.CommandText = "UPDATE materiaux SET nom = @nom, fournisseur_id = @fournisseur_id , description = @description, prix = @prix WHERE id = @id";
            sqlCommand.ExecuteNonQuery();

            //pas tres beau

        }
        private Materiaux convertDataToObject(SqliteDataReader reader)
        {
            Materiaux mat = new Materiaux();

            mat._Id = reader.GetInt32(0);
            mat._Nom = reader.GetString(1);
            mat._FournisseurId = reader.GetInt32(2);
            mat._Prix = reader.GetInt32(2);
            mat._Description = reader.GetString(3);
            return mat;
        }

        public void deleteMateriaux(Materiaux mat)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM chantier WHERE =" + mat._Id;
            sqlCommand.ExecuteNonQuery();
        }
        public List<Materiaux> getAllMateriaux()
        {

            List<Materiaux> listMateriaux = new List<Materiaux>();
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM materiaux";
            SqliteDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {

                Materiaux mat = convertDataToObject(reader);

                listMateriaux.Add(mat);
            }
            return listMateriaux;
        }
    }
}
