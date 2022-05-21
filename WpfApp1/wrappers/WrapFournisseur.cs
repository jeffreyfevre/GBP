using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.modeles;


namespace WpfApp1.wrappers
{
    internal class WrapFournisseur
    {
        SqliteConnection sqlite_conn = new SqliteConnection(@"Data Source=C:\ProgramData\GBD\GBP_BDD.db");

        public void createMateriaux(Fournisseur four)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "INSERT INTO fournisseur (nom,commentaire,telephone,adresse,zipcode) VALUES ('" + four._Nom + "','" + four._Commentaire + "','" + four._Telephone + "','" + four._Adresse + "','" + four._Zipcode+ "')";
            Console.WriteLine(sqlCommand.CommandText);
            sqlCommand.ExecuteNonQuery();

        }

        // A noter quand on recup les données avec GetInt32() alors que c'est un string la fonction return 0; 
        // et pareil our GetString()
        public Fournisseur readMateriaux(int id)
        {
            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "SELECT * FROM fournisseur WHERE id=" + id;
            SqliteDataReader rdr = sqlCommand.ExecuteReader();

            return convertDataToObject(rdr);
        }

        public void updateMateriaux(Fournisseur four)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();

            var args = new Dictionary<string, object>
            {

                {"@id", four._Id},
                {"@nom", four._Nom},
                {"@commentaire", four._Commentaire},
                {"@telephone", four._Telephone},
                {"@zipcode", four._Zipcode},
                {"@adresse", four._Adresse}
            };

            sqlCommand.CommandText = "UPDATE fournisseur SET nom = @nom, fournisseur_id = @fournisseur_id , description = @description, prix = @prix WHERE id = @id";
            sqlCommand.ExecuteNonQuery();

            //pas tres beau

        }
        private Fournisseur convertDataToObject(SqliteDataReader reader)
        {
            Fournisseur four = new Fournisseur();

            four._Id = reader.GetInt32(0);
            four._Nom = reader.GetString(1);
            four._Commentaire = reader.GetString(2);
            four._Telephone = reader.GetString(3);
            four._Adresse = reader.GetString(4);
            four._Zipcode= reader.GetString(4);
            return four;
        }

        public void deleteMateriaux(Materiaux mat)
        {

            sqlite_conn.Open();
            SqliteCommand sqlCommand = sqlite_conn.CreateCommand();
            sqlCommand.CommandText = "DELETE FROM fournisseur WHERE =" + mat._Id;
            sqlCommand.ExecuteNonQuery();
        }
    }
}

