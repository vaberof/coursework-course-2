using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace CourseWork.Infra.Storage.Sqlite
{
    public class Sqlite
    {
        public SQLiteConnection Connection;
        public DataTable Table;

        public Sqlite(string sqliteFilePath)
        {
            Connection = new SQLiteConnection("Data Source=" + sqliteFilePath + ";Version=3;");
            Connection.Open();
            SQLiteCommand command = new SQLiteCommand();
            command.Connection = Connection;

            Table = new DataTable();
        }   
    }
}
