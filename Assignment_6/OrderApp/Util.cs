using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderApp
{
    internal class Util
    {
        public static void ExcuteMySQL(string text)
        {
            string mysqlcon = "server=localhost;database=OrderManager;user=root;password=123456";
            MySqlConnection conn = new MySqlConnection(mysqlcon);
            MySqlCommand cmd = new MySqlCommand(text, conn);
            conn.Open();
            //Console.WriteLine("连接成功");
            int res = cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
