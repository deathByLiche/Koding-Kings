using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace TriviaMaze
{
    public class dbManage
    {
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;
        public dbManage()
        {
            // InitializeComponent();
            CreateStartupDatabase();
        }
        private IDbDataParameter AddParameter(IDbCommand command, string paramName, DbType type, object value)
        {
            IDbDataParameter par = command.CreateParameter();
            par.ParameterName = paramName;
            par.DbType = type;
            if (value != null)
                par.Value = value;
            else
                par.Value = DBNull.Value;
            command.Parameters.Add(par);
            return par;
        }
        private void CreateStartupDatabase()
        {
            ExecuteQuery("CREATE TABLE IF NOT EXISTS Questions (Question varchar(500),KEY INT PRIMARY KEY NOT NULL, Answer1 varchar(100),Answer2 varchar(100),Answer3 varchar(100),Answer4 varchar(100));");
            ExecuteQuery("CREATE TABLE IF NOT EXISTS Rankings (Initials varchar(3), Score int, Id INT AUTO_INCREMENT);");
        }

        private void SetConnection()
        {
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
        }
        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            // open the connection:
            sqlite_conn.Open();

            // create a new SQL command:
            sqlite_cmd = sqlite_conn.CreateCommand();

            // Let the SQLiteCommand object know our SQL-Query:
            sqlite_cmd.CommandText = txtQuery;

            // Now lets execute the SQL ;D
            sqlite_cmd.ExecuteNonQuery();

            //cleanup and close our connection:
            sqlite_conn.Close();

        }
    }
}
