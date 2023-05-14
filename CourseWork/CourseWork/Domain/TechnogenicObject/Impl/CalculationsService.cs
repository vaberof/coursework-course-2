﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject.Impl
{
    internal class CalculationsService : ICalculationsService
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

        // TODO: переименовать MValues -> vectorsMValues
        public List<double> CalculateMValuesWithMarks(DataGridView elevatorTable, List<string> marks)
        {
            Double M = 0;

            List<Double> values = new List<Double>();

            List<Double> listOfMValues = new List<Double>();

            for (int i = 0; i < elevatorTable.RowCount - 1; i++)
            {
                foreach (DataGridViewColumn col in elevatorTable.Columns)
                {
                    if (marks.Contains(col.Name))
                    {
                        values.Add(Convert.ToDouble(elevatorTable.Rows[i].Cells[col.Name].Value));

                    }

                }
                foreach (double c in values)
                {
                    M += (c * c);
                }
                listOfMValues.Add(Math.Sqrt(M));
                M = 0;
                values.Clear();

            }
            return listOfMValues;

            /*List<double> MValues = new List<double>();

            for (int i = 0; i < dataGridTable.RowCount - 1; i++)
            {
                double MSquareSum = 0;

                foreach (DataGridViewColumn col in dataGridTable.Columns)
                {
                    if (marks.Contains(col.Name))
                    {
                        MSquareSum += Math.Pow(Convert.ToDouble(dataGridTable.Rows[i].Cells[col.Name].Value), 2);
                    }
                }
                MValues.Add(Math.Sqrt(MSquareSum));
            }
            return MValues;*/
        }

        public List<double> CalculateAlphaValuesWithMarks(DataGridView dataGridTable, List<double> MValues, List<string> marks)
        {
            List<double> alphaValues = new List<double>();
            alphaValues.Add(0);

            double arccosValue = 0;
            double productsSum = 0;
            double firstRowValue = 0;
            double currentRowValue = 0;

            for (int i = 0; i < dataGridTable.Rows.Count - 2; i++)
            {
                productsSum = 0;
                foreach (DataGridViewColumn col in dataGridTable.Columns)
                {
                    if (marks.Contains(col.Name))
                    {
                        firstRowValue = Convert.ToDouble(dataGridTable.Rows[0].Cells[col.Name].Value);
                        currentRowValue = Convert.ToDouble(dataGridTable.Rows[i + 1].Cells[col.Name].Value);
                        productsSum += firstRowValue * currentRowValue;
                    }
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

        public DataGridView CalculateLowerOrUpperBoundWithMarks(DataGridView dataGridTable, List<string> marks, double epsilon, string arithmeticOperator)
        {
            //MessageBox.Show(dataGridTable.Rows.Count.ToString()) ;
            DataGridView dataGridView = new DataGridView();

            for (int i = 0; i < marks.Count; i++)
            {
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn());
                dataGridView.Columns[i].Name = marks[i];
            }

            for (int i = 0; i < dataGridTable.Rows.Count - 1; i++)
            {
                dataGridView.Rows.Add();

                foreach (DataGridViewColumn col in dataGridTable.Columns)
                {
                    if (marks.Contains(col.Name))
                    {
                        if (arithmeticOperator.Equals("-"))
                        {
                            dataGridView.Rows[i].Cells[col.Name].Value = Convert.ToDouble(dataGridTable.Rows[i].Cells[col.Name].Value) - epsilon;
                            //MessageBox.Show("значение в строке:" + dataGridView.Rows[i].Cells[col.Name].Value.ToString());
                        }
                        else
                        {   // MessageBox.Show("значение в строке:" + dataGridView.Rows[i].Cells[col.Name].Value.ToString());
                            dataGridView.Rows[i].Cells[col.Name].Value = Convert.ToDouble(dataGridTable.Rows[i].Cells[col.Name].Value) + epsilon;
                        }

                    }
                }
            }

            MessageBox.Show("datagridView: "+dataGridView.Rows.Count.ToString());
            MessageBox.Show("datagridTable: " + dataGridTable.Rows.Count.ToString());
            return dataGridView;
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
           // MessageBox.Show("last predicted value:" + value.ToString());
            predictedValues.Add(value);

            return predictedValues;
        }

        public void CalculateAndFillDiameterColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView)
        {
            // в колонке 4 отображается значение 2E
           // MessageBox.Show("количество строк в estimation table: "+ dataGridView.Rows.Count.ToString());
            //MessageBox.Show("количество элементов в ловер баунд: " + calculatedAlphaAndMValues["lowerBoundMValues"].Count.ToString());

            for (int i = 0; i < calculatedAlphaAndMValues["lowerBoundMValues"].Count; i++)
            {
                dataGridView.Rows[i].Cells[4].Value =
                    Math.Abs(calculatedAlphaAndMValues["lowerBoundMValues"][i] - calculatedAlphaAndMValues["upperBoundMValues"][i]); // * 2
            }

            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            dataGridView.Rows[lastPredictedMValueIndex].Cells[4].Value =
                   Math.Abs(calculatedAlphaAndMValues["predictedLowerBoundMValues"][lastPredictedMValueIndex] - calculatedAlphaAndMValues["predictedUpperBoundMValues"][lastPredictedMValueIndex]);
        }

        public void CalculateAndFillLColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView)
        {
            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            // в колонке 5 отображается значение L
            for (int i = 0; i < calculatedAlphaAndMValues["MValues"].Count; i++)
            {
                dataGridView.Rows[i].Cells[5].Value =
                    Math.Abs(calculatedAlphaAndMValues["MValues"][0] - calculatedAlphaAndMValues["MValues"][i]);
            }

            dataGridView.Rows[lastPredictedMValueIndex].Cells[5].Value =
                   Math.Abs(calculatedAlphaAndMValues["MValues"][0] - calculatedAlphaAndMValues["predictedMValues"][lastPredictedMValueIndex]);
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
