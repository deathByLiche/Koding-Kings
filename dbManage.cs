using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace TriviaMaze
{
    public class dbManage
    {
        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
       // SQLiteDataReader sqlite_datareader;
        public dbManage()
        {
            CreateStartupDatabase();
            loadQuestions();
        }
        private void loadQuestions()
        {
            sqlite_conn.Open();
            string answer1,a,answer2,b,answer3,c,answer4,d;
            //using (StreamReader sr = File.OpenText(@"C:\Users\CODYS\Desktop\Spring 2014\Software Engineering\TriviaMaze\questions.txt"))
            string dir =System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string file = dir + @"/Questions/questions.txt";
            StreamReader sr = new StreamReader(file);

                string line;
                string question;
                while ((line = sr.ReadLine()) != null)
                {
                    question = line;
                    answer1 = sr.ReadLine();
                    a = sr.ReadLine();
                    answer2 = sr.ReadLine();
                    b = sr.ReadLine();
                    answer3 = sr.ReadLine();
                    c = sr.ReadLine();
                    answer4 = sr.ReadLine();
                    d = sr.ReadLine();

                    SetConnection();
                    sqlite_conn.Open();
                    sqlite_cmd = sqlite_conn.CreateCommand();

                    sqlite_cmd.CommandText = "INSERT INTO Questions(Question,Answer1,a,Answer2,b,Answer3,c,Answer4,d )VALUES(@question,@answer1,@a,@answer2,@b,@answer3,@c,@answer4,@d);";
                    AddParameter(sqlite_cmd, "@question", DbType.String, question);
                    AddParameter(sqlite_cmd, "@answer1", DbType.String, answer1);
                    AddParameter(sqlite_cmd, "@a", DbType.String, a);
                    AddParameter(sqlite_cmd, "@answer2", DbType.String, answer2);
                    AddParameter(sqlite_cmd, "@b", DbType.String, b);
                    AddParameter(sqlite_cmd, "@answer3", DbType.String, answer3);
                    AddParameter(sqlite_cmd, "@c", DbType.String, c);
                    AddParameter(sqlite_cmd, "@answer4", DbType.String, answer4);
                    AddParameter(sqlite_cmd, "@d", DbType.String, d);
                    sqlite_cmd.ExecuteNonQuery();
                    sqlite_conn.Close();

            }
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
            ExecuteQuery("DROP TABLE IF EXISTS Questions;");
            ExecuteQuery("CREATE TABLE IF NOT EXISTS Questions (Question varchar(500),KEY AUTO_INCREMENT, Answer1 varchar(100),a varchar(1),Answer2 varchar(100),b varchar(1),Answer3 varchar(100),c varchar(1),Answer4 varchar(100),d varchar(1));");
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
