using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace CourseWork.Infra.Storage.Sqlite
{
    public class Sqlite
    {
        public SQLiteConnection connection;

        public Sqlite(string sqliteFilePath)
        {
            connection = new SQLiteConnection("Data Source=" + sqliteFilePath + ";Version=3;");
            connection.Open();
        } 
    }
}
