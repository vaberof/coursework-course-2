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
using System.Xml.Linq;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CourseWork.Infra.Storage.Sqlite.TechnogenicObject
{
    internal class TechnogenicObjectStorage : ITechogenicObjectStorage
    {
        const string technogenicObjectValuesTable = "technogenic_object_values";

        private Sqlite db;

        public TechnogenicObjectStorage(Sqlite db)
        {
            this.db = db;
        }

        public void OpenConnection()
        {
            db.Connection.Open();
        }

        public void CloseConnection()
        {
            db.Connection.Close();
        }

        public void UpdateNextEpochValue(int nextEpochValue)
        {
            using (var command = new SQLiteCommand($"UPDATE {technogenicObjectValuesTable} SET next_epoch_value = @nextEpochValue", db.Connection))
            {
                command.Parameters.AddWithValue("@nextEpochValue", nextEpochValue);
                command.ExecuteNonQuery();
            }
        }

        public int GetNextEpochValue()
        {
            int epochCount = 0;
            
            using (var command = new SQLiteCommand($"SELECT next_epoch_value FROM {technogenicObjectValuesTable}", db.Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        epochCount = Convert.ToInt32(reader["next_epoch_value"]);
                    }
                }
            }            

            return epochCount;
        }

        public void CreateTechnogenicObjectValuesTable(double alpha, double epsilon, byte[] image, int nextEpochValue)
        {
            SQLiteCommand command = new SQLiteCommand(db.Connection);            
            command.CommandText = $"CREATE TABLE IF NOT EXISTS {technogenicObjectValuesTable} " +
                $"(Alpha REAL, Epsilon REAL, Image BLOB, next_epoch_value INTEGER)";
            command.ExecuteNonQuery();

            int rowsCount = getRowsCountInTable(technogenicObjectValuesTable);
            //MessageBox.Show("rowsCount: " +  rowsCount.ToString());

            if (rowsCount == 0)
            { 
                command.CommandText = $"INSERT INTO {technogenicObjectValuesTable} " +
                    $"(alpha, epsilon, image, next_epoch_value) VALUES (@alpha, @epsilon, @image, @nextEpochValue)";
                
                command.Parameters.AddWithValue("@alpha", alpha);
                command.Parameters.AddWithValue("@epsilon", epsilon);
                command.Parameters.AddWithValue("@image", image);
                command.Parameters.AddWithValue("@nextEpochValue", nextEpochValue);

                command.ExecuteNonQuery();
            }            
        }

        public void GetTechnogenicObjectValues(ref double alpha, ref double epsilon, ref byte[] imageBytes, ref int nextEpochValue) 
        {
            string getTechnogenicObjectValuesQuery = $"SELECT alpha, epsilon, image, next_epoch_value FROM {technogenicObjectValuesTable}";

            SQLiteCommand command = new SQLiteCommand(getTechnogenicObjectValuesQuery, db.Connection);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                alpha = reader.GetDouble(0);
                epsilon = reader.GetDouble(1);
                imageBytes = (byte[])reader[2];
                nextEpochValue = reader.GetInt32(3);
            }
            reader.Close();
        }

        public void UpdateAlphaAndEpsilon(double alpha, double epsilon)
        {
            string updateAlphaAndEpsilonQuery = $"UPDATE {technogenicObjectValuesTable} SET alpha = @alpha, epsilon = @epsilon";

            using (SQLiteCommand command = new SQLiteCommand(updateAlphaAndEpsilonQuery, db.Connection))
            {
                command.Parameters.AddWithValue("@alpha", alpha);
                command.Parameters.AddWithValue("@epsilon", epsilon);

                command.ExecuteNonQuery();
            }
        }        

        public void FillDataTable(DataTable dataTable)
        {
            string getAllQuery = "SELECT * FROM [" + getTableName() + "]";

            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            SQLiteCommand command = new SQLiteCommand(db.Connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(getAllQuery, db.Connection);

            adapter.Fill(dataTable);

            // count - 1 потому что послендяя колонка "количество эпох"
            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                string replaceCommosToDots = "UPDATE [" + getTableName() + "] SET[" + i + "] = REPLACE([" + i + "],',','.')";
                command.CommandText = replaceCommosToDots;
                command.ExecuteNonQuery();
                //Thread.Sleep(10);
            }

            dataTable.Rows.Clear();
            dataTable.Columns.Clear();

            adapter = new SQLiteDataAdapter(getAllQuery, db.Connection);
            adapter.Fill(dataTable);            
        }

        public void AddRow(double value)
        {
            string addNewEpochQuery = "INSERT INTO [" + getTableName() + "] (Эпоха) VALUES (\"" + value + "\")";
            SQLiteCommand command = new SQLiteCommand(db.Connection);
            command.CommandText = addNewEpochQuery;
            command.ExecuteNonQuery();
        }

        public void AddValuesInRow(int column, int row, double value)
        {            
            string convertedValue = value.ToString().Replace(',', '.');

            string UpdateEpochValuesQuery = "UPDATE [" + getTableName() + "] SET \"" + column + "\" = \"" + convertedValue + "\" WHERE Эпоха = \'" + row + "\'";
            SQLiteCommand command = new SQLiteCommand(db.Connection);
            command.CommandText = UpdateEpochValuesQuery;
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
            
            string deleteEpochValuesQuery = "DELETE FROM [" + getTableName() + "] WHERE Эпоха IN " + "(" + convertedEpochs + ")";
            SQLiteCommand command = new SQLiteCommand(db.Connection);
            command.CommandText = deleteEpochValuesQuery;
            command.ExecuteNonQuery();
        }

        private int getRowsCountInTable(string tableName)
        {
            object rowCount;

            using (SQLiteCommand command = new SQLiteCommand($"SELECT COUNT(*) FROM {tableName}", db.Connection))
            {
                rowCount = command.ExecuteScalar();
            }

            if (rowCount == null)
            {
                return 0;
            }

            return Convert.ToInt32(rowCount);
        }

        private string getTableName()
        {
            string query = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";

            SQLiteCommand command = new SQLiteCommand(query, db.Connection);
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
