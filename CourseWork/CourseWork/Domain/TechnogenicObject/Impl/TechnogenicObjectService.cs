using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    internal class TechnogenicObjectService : ITechnogenicObjectService //, IDecompositionService
    {
        private ITechogenicObjectStorage technogenicObjectStorage;
        private DataTable dataTable;
        public TechnogenicObjectService(ITechogenicObjectStorage technogenicObjectStorage)
        {
            this.technogenicObjectStorage = technogenicObjectStorage;
            this.dataTable = new DataTable();
        }

        public DataTable GetDataTable()
        {
            return this.dataTable;
        }

        // TODO: переименовать этот метод для заполнения конкретно таблицы со значениями
        // и основную таблицу тоже переименовать из dataGridTable -> во что-нбиудь получше
        public void FillDataGridTable(DataGridView dataGridTable)
        {
            this.dataTable = new DataTable();

            technogenicObjectStorage.FillDataTable(dataTable);

            dataGridTable.Columns.Clear();
            dataGridTable.Rows.Clear();

            // TODO: count - 1 потому что послендяя колонка "количество эпох"
            for (int column = 0; column < dataTable.Columns.Count - 1; column++)
            {
                string columnName = dataTable.Columns[column].ColumnName;

                dataGridTable.Columns.Add(columnName, columnName);
                dataGridTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                dataGridTable.Rows.Add(dataTable.Rows[row].ItemArray);
            }
        }

        public void AddEpoch(DataGridView dataGridTable)
        {
            int epochCount = technogenicObjectStorage.GetEpochCount();
            epochCount++;

            double delta = 0;
            double averageDelta = 0;            

            int newRow = dataGridTable.RowCount - 1;
            Random random = new Random();

            dataGridTable.Rows[newRow].Cells[0].Value = epochCount - 1;

            technogenicObjectStorage.AddRow(epochCount);

            for (int i = 1; i < dataGridTable.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridTable.Rows.Count - 1; j++)
                {
                    if (Convert.ToDouble(dataGridTable.Rows[j + 1].Cells[i].Value) != 0)
                    {
                        delta = Math.Abs(Convert.ToDouble(dataGridTable.Rows[j].Cells[i].Value) - Convert.ToDouble(dataGridTable.Rows[j + 1].Cells[i].Value));
                    }

                    averageDelta += delta;
                    delta = 0;
                }

                averageDelta /= dataGridTable.Rows.Count;

                double newCellValue = random.NextDouble() * (averageDelta - (-averageDelta)) + averageDelta;

                dataGridTable.Rows[newRow].Cells[i].Value = Math.Round(newCellValue + Convert.ToDouble(dataGridTable.Rows[newRow - 1].Cells[i].Value), 4);
                technogenicObjectStorage.AddValuesInRow(i, epochCount, Convert.ToDouble(dataGridTable.Rows[newRow].Cells[i].Value));

               averageDelta = 0;
            }
            dataGridTable.Rows.Add();      
            technogenicObjectStorage.UpdateEpochCount(epochCount);
        }

        public void DeleteEpoches(DataGridView dataGridTable, List<int> selectedRowsIndexes)
        {
            List<int> epochValues = new List<int>();

            for (int i = 0; i < selectedRowsIndexes.Count; i++)
            {
                // получаем значение эпохи в выбранной строке
                string epoch = dataGridTable.Rows[selectedRowsIndexes[i]].Cells[0].Value.ToString();
                epochValues.Add(Convert.ToInt32(epoch));
            }
            technogenicObjectStorage.DeleteRowFromTable(epochValues);
        }       
    }
}
