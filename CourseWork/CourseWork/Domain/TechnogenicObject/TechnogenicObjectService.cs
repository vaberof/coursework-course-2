using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    internal class TechnogenicObjectService : ITechnogenicObjectService
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

        public void FillMainCoordinatesTable(DataGridView mainCoordinatesTable)
        {
            this.dataTable = new DataTable();

            technogenicObjectStorage.FillDataTable(dataTable);

            mainCoordinatesTable.Columns.Clear();
            mainCoordinatesTable.Rows.Clear();

            // count - 1 потому что послендяя колонка "Количество эпох"
            for (int column = 0; column < dataTable.Columns.Count - 1; column++)
            {
                string columnName = dataTable.Columns[column].ColumnName;

                mainCoordinatesTable.Columns.Add(columnName, columnName);
                mainCoordinatesTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                mainCoordinatesTable.Rows.Add(dataTable.Rows[row].ItemArray);
            }
        }

        public void AddEpoch(DataGridView mainCoordinatesTable, ref int currentEpochCount)
        {
            currentEpochCount++;

            double delta = 0;
            double averageDelta = 0;
            Random random = new Random();

            int newRowIndex = mainCoordinatesTable.RowCount - 1;

            int nextEpochValue = technogenicObjectStorage.GetEpochCount();

            mainCoordinatesTable.Rows[newRowIndex].Cells[0].Value = nextEpochValue;

            technogenicObjectStorage.AddRow(nextEpochValue);
            technogenicObjectStorage.UpdateEpochCount(nextEpochValue + 1);

            for (int i = 1; i < mainCoordinatesTable.Columns.Count; i++)
            {
                for (int j = 0; j < mainCoordinatesTable.Rows.Count - 1; j++)
                {
                    if (Convert.ToDouble(mainCoordinatesTable.Rows[j + 1].Cells[i].Value) != 0)
                    {
                        delta = Math.Abs(Convert.ToDouble(mainCoordinatesTable.Rows[j].Cells[i].Value) - Convert.ToDouble(mainCoordinatesTable.Rows[j + 1].Cells[i].Value));
                    }

                    averageDelta += delta;
                    delta = 0;
                }

                averageDelta /= mainCoordinatesTable.Rows.Count;

                double newCellValue = random.NextDouble() * (averageDelta - (-averageDelta)) + averageDelta;

                mainCoordinatesTable.Rows[newRowIndex].Cells[i].Value = 
                    Math.Round(newCellValue + Convert.ToDouble(mainCoordinatesTable.Rows[newRowIndex - 1].Cells[i].Value), 4);

                technogenicObjectStorage.AddValuesInRow(i, nextEpochValue, Convert.ToDouble(mainCoordinatesTable.Rows[newRowIndex].Cells[i].Value));

                averageDelta = 0;
            }

            mainCoordinatesTable.Rows.Add();      

            this.dataTable = new DataTable();
            technogenicObjectStorage.FillDataTable(dataTable);
        }

        public void DeleteEpoches(DataGridView mainCoordinatesTable, List<int> selectedRowsIndexes, ref int currentEpochCount)
        {
            if (currentEpochCount - selectedRowsIndexes.Count < 2)
            {
                MessageBox.Show("Нельзя удалить все эпохи, должны остаться минимум 2");
                return;
            }

            List<int> epochValues = new List<int>();

            for (int i = 0; i < selectedRowsIndexes.Count; i++)
            {
                // получаем значение эпохи в выбранной строке
                string epoch = mainCoordinatesTable.Rows[selectedRowsIndexes[i]].Cells[0].Value.ToString();
                epochValues.Add(Convert.ToInt32(epoch));
            }
            technogenicObjectStorage.DeleteRowFromTable(epochValues);

            currentEpochCount -= selectedRowsIndexes.Count;
        }       
    }
}
