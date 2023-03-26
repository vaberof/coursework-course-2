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

namespace CourseWork.Infra.Storage.Sqlite.TechnogenicObject
{
    internal class TechnogenicObjectStorage : ITechogenicObjectStorage
    {
        private Sqlite db;

        public TechnogenicObjectStorage(Sqlite db)
        {
            this.db = db;
        }

        public void FillDataGridTable(DataGridView dataGridTable)
        {
            string query = "SELECT * FROM [" + getTableName() + "]";

            db.Table.Rows.Clear();
            db.Table.Columns.Clear();

            SQLiteCommand command = new SQLiteCommand(db.Connection);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, db.Connection);

            adapter.Fill(db.Table);

            for (int i = 1; i < db.Table.Columns.Count; i++)
            {
                string replaceCommosToDots = "UPDATE [" + getTableName() + "] SET[" + i + "] = REPLACE([" + i + "],',','.')";
                command.CommandText = replaceCommosToDots;
                command.ExecuteNonQuery();
                Thread.Sleep(10);
            }

            db.Table.Rows.Clear();
            db.Table.Columns.Clear();

            adapter = new SQLiteDataAdapter(query, db.Connection);
            adapter.Fill(db.Table);

            dataGridTable.Columns.Clear();
            dataGridTable.Rows.Clear();

            for (int column = 0; column < db.Table.Columns.Count; column++)
            {
                string columnName = db.Table.Columns[column].ColumnName;

                dataGridTable.Columns.Add(columnName, columnName);
                dataGridTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            }

            for (int row = 0; row < db.Table.Rows.Count; row++)
            {
                dataGridTable.Rows.Add(db.Table.Rows[row].ItemArray);
            }
        }

        public void DeleteRowFromTable(List<int> selectedRowsIndexes)
        {
            string convertedIndexes = "";

            for (int i = 0; i < selectedRowsIndexes.Count; i++)
            {                
                convertedIndexes += selectedRowsIndexes[i].ToString();
                if (i < selectedRowsIndexes.Count - 1)
                {
                    convertedIndexes += ",";
                }
            }
            
            string SQLQuery = "DELETE FROM [" + getTableName() + "] WHERE Эпоха IN " + "(" + convertedIndexes + ")";
            SQLiteCommand command = new SQLiteCommand(db.Connection);
            command.CommandText = SQLQuery;
            command.ExecuteNonQuery();
        }

        private string getTableName()
        {
            string query = "SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;";

            SQLiteCommand command = new SQLiteCommand(query, db.Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            string columnNames = "";

            while (reader.Read())
            {
                columnNames = reader.GetString(0);
            }

            return columnNames;
        }
    }    
}
