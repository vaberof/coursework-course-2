using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject.Impl
{
    internal class DecompositionService : IDecompositionService
    {
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
