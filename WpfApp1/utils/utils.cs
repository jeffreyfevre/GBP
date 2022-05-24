using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.utils
{
    public static class utils
    {
        public static string generateInsertQuery(string table, string[] ids, string[] values)
        {
            string query = "INSERT INTO " + table + " (";
            for (int i = 0; i < ids.Length; i++)
            {
                query += ids[i] + ',';
            }
            query += "VALUES ('";
            for (int i = 0; i < values.Length; i++)
            {
                query += values[i] + "',";
            }
            query += ")";
            return query;

        }

    }
}
