using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CourseWork.Domain.TechnogenicObject;
using System.Runtime.InteropServices;
using System.Windows.Documents;

namespace CourseWork.Infra.Storage.Sqlite.TechnogenicObject
{
    internal class TechnogenicObjectStorage : ITechogenicObjectStorage
    {
        private Sqlite db;

        public TechnogenicObjectStorage(Sqlite db)
        {
            this.db = db;
        }

        public void OpenConnection()
        {
            db.connection.Open();
        }

        public void CloseConnection()
        {
            db.connection.Close();
        }

        public void CreateEpochCountColumn()
        {
            using (var command = new SQLiteCommand("PRAGMA table_info(Данные)", db.connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetString(1) == "Количество_эпох")
                        {
                            return;
                        }
                    }
                }
            }
            
            using (var command = new SQLiteCommand("ALTER TABLE Данные ADD COLUMN Количество_эпох INTEGER;", db.connection))
            {
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEpochCount(int epochCount)
        {
            using (var command = new SQLiteCommand("UPDATE Данные SET Количество_эпох = @epochCount", db.connection))
            {
                command.Parameters.AddWithValue("@epochCount", epochCount);
                command.ExecuteNonQuery();
            }
        }

        public int GetEpochCount()
        {
            int epochCount = 0;
            
            using (var command = new SQLiteCommand("SELECT Количество_эпох FROM Данные", db.connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        epochCount = Convert.ToInt32(reader["Количество_эпох"]);
                    }
                }
            }            

            return epochCount;
        }

        public void FillDataTable(DataTable dataTable)
        {
            string query = "SELECT * FROM [" + getTableName() + "]";

            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            SQLiteCommand command = new SQLiteCommand(db.connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, db.connection);

            adapter.Fill(dataTable);

            // count - 1 потому что послендяя колонка "количество эпох"
            for (int i = 1; i < dataTable.Columns.Count - 1; i++)
            {
                string replaceCommosToDots = "UPDATE [" + getTableName() + "] SET[" + i + "] = REPLACE([" + i + "],',','.')";
                command.CommandText = replaceCommosToDots;
                command.ExecuteNonQuery();
                //Thread.Sleep(10);
            }

            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            adapter = new SQLiteDataAdapter(query, db.connection);
            adapter.Fill(dataTable);            
        }

        public void AddRow(double value)
        {
            string SQLQuery = "INSERT INTO [" + getTableName() + "] (Эпоха) VALUES (\"" + value + "\")";
            SQLiteCommand command = new SQLiteCommand(db.connection);
            command.CommandText = SQLQuery;
            command.ExecuteNonQuery();
        }

        public void AddValuesInRow(int column, int row, double value)
        {            
            string convertedValue = value.ToString().Replace(',', '.');

            string SQLQuery = "UPDATE [" + getTableName() + "] SET \"" + column + "\" = \"" + convertedValue + "\" WHERE Эпоха = \'" + row + "\'";
            SQLiteCommand command = new SQLiteCommand(db.connection);
            command.CommandText = SQLQuery;
            command.ExecuteNonQuery();
        }

        public void DeleteRowFromTable(List<int> epochs)
        {
            string convertedEpochs = "";

            for (int i = 0; i < epochs.Count; i++)
            {                
                convertedEpochs += epochs[i].ToString();
                if (i < epochs.Count - 1)
                {
                    convertedEpochs += ",";
                }
            }
            
            string SQLQuery = "DELETE FROM [" + getTableName() + "] WHERE Эпоха IN " + "(" + convertedEpochs + ")";
            SQLiteCommand command = new SQLiteCommand(db.connection);
            command.CommandText = SQLQuery;
            command.ExecuteNonQuery();
        }

        private string getTableName()
        {
            string query = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";

            SQLiteCommand command = new SQLiteCommand(query, db.connection);
            SQLiteDataReader reader = command.ExecuteReader();
            string tableName = "";

            while (reader.Read())
            {
                tableName = reader.GetString(0);
            }

            return tableName;
        }
    }    
}
