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

        public List<double> CalculateMValues(DataGridView dataGridTable)
        {
            List<double> MValues = new List<double>();
            int startColumn = 0;
            int endRow = 0;

            if (dataGridTable.Columns[0].Name.Equals("Эпоха"))
            {
                endRow = dataGridTable.RowCount - 1;
                startColumn = 1;
            }
            else endRow = dataGridTable.RowCount - 2;            

            for (int i = 0; i < endRow; i++)
            {
                double MSquareSum = 0;

                for (int j = startColumn; j < dataGridTable.ColumnCount; j++)
                {
                    MSquareSum += Math.Pow(Convert.ToDouble(dataGridTable.Rows[i].Cells[j].Value), 2);
                }

                MValues.Add(Math.Sqrt(MSquareSum));
            }

            return MValues;
        }

        public List<double> CalculateAlphaValues(DataGridView dataGridTable, List<double> MValues)
        {
            List<double> alphaValues = new List<double>();
            alphaValues.Add(0);

            double arccosValue = 0;
            double productsSum = 0;
            double firstRowValue = 0;
            double currentRowValue = 0;

            int endRow = 0;
            int startColumn = 0;

            if (dataGridTable.Columns[0].Name.Equals("Эпоха"))
            {
                startColumn = 1;
                endRow = dataGridTable.RowCount - 2;
            }
            else endRow = dataGridTable.RowCount - 3;

            for (int i = 0; i < endRow; i++)
            {
                productsSum = 0;

                for (int j = startColumn; j < dataGridTable.ColumnCount; j++)
                {
                    firstRowValue = Convert.ToDouble(dataGridTable.Rows[0].Cells[j].Value);
                    currentRowValue = Convert.ToDouble(dataGridTable.Rows[i + 1].Cells[j].Value);
                    productsSum += firstRowValue * currentRowValue;
                }

                productsSum /= MValues[0];
                productsSum /= MValues[i + 1];
                productsSum = Math.Round(productsSum, 15);

                arccosValue = Math.Acos(productsSum);

                alphaValues.Add(arccosValue);
            }
            return alphaValues;
        }

        public DataGridView CalculateLowerOrUpperBound(DataTable dataTable, double epsilon, DataGridView dataGridTable, string arithmeticOperator)
        {
            DataGridView table = fillDataGridView(dataTable);
            
            for (int i = 0; i < dataGridTable.Rows.Count; i++)
            {
                for (int j = 1; j < dataGridTable.ColumnCount; j++)
                {
                    if (arithmeticOperator.Equals("-"))
                    {
                        table.Rows[i].Cells[j].Value = Convert.ToDouble(dataGridTable.Rows[i].Cells[j].Value) - epsilon;
                    }
                    else
                    {
                        table.Rows[i].Cells[j].Value = Convert.ToDouble(dataGridTable.Rows[i].Cells[j].Value) + epsilon;
                    }
                }
            }
            return table;
        }

        public List<double> GetPredictedValues(List<double> valuesToPredict, double exponentialCoefficient)
        {
            List<double> predictedValues = new List<double>();
            double averageSum = 0;
            double value = 0;

            if (valuesToPredict[0] == 0)
            {
                for (int i = 1; i < valuesToPredict.Count; i++)
                {
                    averageSum += valuesToPredict[i];
                }
                averageSum /= valuesToPredict.Count - 1;
                double firstRowValue = exponentialCoefficient * valuesToPredict[0] + (1 - exponentialCoefficient) * averageSum;
                predictedValues.Add(firstRowValue);
            }
            else
            {
                double firstRowValue = exponentialCoefficient * valuesToPredict[0] + (1 - exponentialCoefficient) * valuesToPredict.Average();
                predictedValues.Add(firstRowValue);
            }

            for (int i = 1; i < valuesToPredict.Count; i++)
            {
                value = exponentialCoefficient * valuesToPredict[i] + (1 - exponentialCoefficient) * predictedValues[i - 1];
                predictedValues.Add(value);
            }

            value = exponentialCoefficient * predictedValues.Average() + (1 - exponentialCoefficient) * predictedValues.Last();
            predictedValues.Add(value);

            return predictedValues;          
        }

        private DataGridView fillDataGridView(DataTable dataTable)
        {
            DataGridView dataGridView = new DataGridView();

            // TODO: count - 1 потому что послендяя колонка "количество эпох"
            for (int column = 0; column < dataTable.Columns.Count - 1; column++)
            {
                string ColName = dataTable.Columns[column].ColumnName;
                dataGridView.Columns.Add(ColName, ColName);
                dataGridView.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                dataGridView.Rows.Add(dataTable.Rows[row].ItemArray);
            }

            return dataGridView;
        }
    }
}
