using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CourseWork.Service.Calculation;

namespace CourseWork.Service.Decomposition
{
    public class DecompositionService : IFirstLevelDecompositionService, ISecondLevelDecompositionService, IFourthLevelDecompositionService
    {
        ICalculationService calculationsService;

        public DecompositionService(ICalculationService calculationsService)
        {
            this.calculationsService = calculationsService;
        }

        public void CalculateValuesInCoordinatesTable(
            ref List<List<double>> calculatedValues,
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView mainCoordinatesTable,
            DataTable dataTable,
            double epsilon,
            double alpha)
        {
            DataGridView lowerBoundTable = calculationsService.CalculateLowerOrUpperBound(
            dataTable,
            epsilon,
            mainCoordinatesTable,
                "-");

            DataGridView upperBoundTable = calculationsService.CalculateLowerOrUpperBound(
                dataTable,
                epsilon,
                mainCoordinatesTable,
                "+");

            // ЗНАЧЕНИЯ M ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> MValues = calculationsService.CalculateMValues(mainCoordinatesTable);
            List<double> lowerBoundMValues = calculationsService.CalculateMValues(lowerBoundTable);
            List<double> upperBoundMValues = calculationsService.CalculateMValues(upperBoundTable);

            // ЗНАЧЕНИЯ АЛЬФЫ ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> alphaValues = calculationsService.CalculateAlphaValues(mainCoordinatesTable, MValues);
            List<double> lowerBoundAlphaValues = calculationsService.CalculateAlphaValues(lowerBoundTable, lowerBoundMValues);
            List<double> upperBoundAlphaValues = calculationsService.CalculateAlphaValues(upperBoundTable, upperBoundMValues);

            // ПРОГНОЗНЫЕ ЗНАЧЕНИЯ M ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> predictedMValues = calculationsService.GetPredictedValues(MValues, alpha);
            List<double> predictedLowerBoundMValues = calculationsService.GetPredictedValues(lowerBoundMValues, alpha);
            List<double> predictedUpperBoundMValues = calculationsService.GetPredictedValues(upperBoundMValues, alpha);

            // ПРОГНОЗНЫЕ ЗНАЧЕНИЯ АЛЬФЫ ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> predictedAlphaValues = calculationsService.GetPredictedValues(alphaValues, alpha);
            List<double> predictedLowerBoundAlphaValues = calculationsService.GetPredictedValues(lowerBoundAlphaValues, alpha);
            List<double> predictedUpperBoundAlphaValues = calculationsService.GetPredictedValues(upperBoundAlphaValues, alpha);

            // Значения для таблицы с оценкой
            calculatedAlphaAndMValues["MValues"] = MValues;
            calculatedAlphaAndMValues["lowerBoundMValues"] = lowerBoundMValues;
            calculatedAlphaAndMValues["upperBoundMValues"] = upperBoundMValues;

            calculatedAlphaAndMValues["predictedMValues"] = predictedMValues;
            calculatedAlphaAndMValues["predictedLowerBoundMValues"] = predictedLowerBoundMValues;
            calculatedAlphaAndMValues["predictedUpperBoundMValues"] = predictedUpperBoundMValues;

            calculatedAlphaAndMValues["alphaValues"] = alphaValues;
            calculatedAlphaAndMValues["lowerBoundAlphaValues"] = lowerBoundAlphaValues;
            calculatedAlphaAndMValues["upperBoundAlphaValues"] = upperBoundAlphaValues;

            calculatedAlphaAndMValues["predictedAlphaValues"] = predictedAlphaValues;
            calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"] = predictedLowerBoundAlphaValues;
            calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"] = predictedUpperBoundAlphaValues;

            // Значения для таблицы со всеми расчетами
            calculatedValues.Add(MValues);
            calculatedValues.Add(alphaValues);
            calculatedValues.Add(predictedMValues);
            calculatedValues.Add(predictedAlphaValues);

            calculatedValues.Add(lowerBoundMValues);
            calculatedValues.Add(lowerBoundAlphaValues);
            calculatedValues.Add(predictedLowerBoundMValues);
            calculatedValues.Add(predictedLowerBoundAlphaValues);

            calculatedValues.Add(upperBoundMValues);
            calculatedValues.Add(upperBoundAlphaValues);
            calculatedValues.Add(predictedUpperBoundMValues);
            calculatedValues.Add(predictedUpperBoundAlphaValues);
        }

        public void FillValuesInEstimationTable(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView)
        {
            calculationsService.CalculateAndFillDiameterColumn(ref calculatedAlphaAndMValues, dataGridView);
            calculationsService.CalculateAndFillLColumn(ref calculatedAlphaAndMValues, dataGridView);
            fillEstimationColumn(ref calculatedAlphaAndMValues, dataGridView);
        }

        public void CalculateValuesInMarksCoordinatesTable(
            ref List<List<double>> calculatedValues,
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            List<string> marks,
            DataGridView mainCoordinatesTable,
            double epsilon,
            double alpha)
        {
            DataGridView lowerBoundTable = calculationsService.CalculateLowerOrUpperBoundWithMarks(
            mainCoordinatesTable,
            marks,
            epsilon,
            "-");

            DataGridView upperBoundTable = calculationsService.CalculateLowerOrUpperBoundWithMarks(
                mainCoordinatesTable,
                marks,
                epsilon,
                "+");

            // ЗНАЧЕНИЯ M ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> MValues = calculationsService.CalculateMValuesWithMarks(mainCoordinatesTable, marks);
            List<double> lowerBoundMValues = calculationsService.CalculateMValuesWithMarks(lowerBoundTable, marks);
            List<double> upperBoundMValues = calculationsService.CalculateMValuesWithMarks(upperBoundTable, marks);

            // ЗНАЧЕНИЯ АЛЬФЫ ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> alphaValues = calculationsService.CalculateAlphaValuesWithMarks(mainCoordinatesTable, MValues, marks);
            List<double> lowerBoundAlphaValues = calculationsService.CalculateAlphaValuesWithMarks(lowerBoundTable, lowerBoundMValues, marks);
            List<double> upperBoundAlphaValues = calculationsService.CalculateAlphaValuesWithMarks(upperBoundTable, upperBoundMValues, marks);

            // ПРОГНОЗНЫЕ ЗНАЧЕНИЯ M ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> predictedMValues = calculationsService.GetPredictedValues(MValues, alpha);
            List<double> predictedLowerBoundMValues = calculationsService.GetPredictedValues(lowerBoundMValues, alpha);
            List<double> predictedUpperBoundMValues = calculationsService.GetPredictedValues(upperBoundMValues, alpha);

            // ПРОГНОЗНЫЕ ЗНАЧЕНИЯ АЛЬФЫ ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> predictedAlphaValues = calculationsService.GetPredictedValues(alphaValues, alpha);
            List<double> predictedLowerBoundAlphaValues = calculationsService.GetPredictedValues(lowerBoundAlphaValues, alpha);
            List<double> predictedUpperBoundAlphaValues = calculationsService.GetPredictedValues(upperBoundAlphaValues, alpha);

            // Значения для таблицы с оценкой
            calculatedAlphaAndMValues["MValues"] = MValues;
            calculatedAlphaAndMValues["lowerBoundMValues"] = lowerBoundMValues;
            calculatedAlphaAndMValues["upperBoundMValues"] = upperBoundMValues;

            calculatedAlphaAndMValues["predictedMValues"] = predictedMValues;
            calculatedAlphaAndMValues["predictedLowerBoundMValues"] = predictedLowerBoundMValues;
            calculatedAlphaAndMValues["predictedUpperBoundMValues"] = predictedUpperBoundMValues;

            calculatedAlphaAndMValues["alphaValues"] = alphaValues;
            calculatedAlphaAndMValues["lowerBoundAlphaValues"] = lowerBoundAlphaValues;
            calculatedAlphaAndMValues["upperBoundAlphaValues"] = upperBoundAlphaValues;

            calculatedAlphaAndMValues["predictedAlphaValues"] = predictedAlphaValues;
            calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"] = predictedLowerBoundAlphaValues;
            calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"] = predictedUpperBoundAlphaValues;

            // Значения для таблицы со всеми расчетами
            calculatedValues.Add(MValues);
            calculatedValues.Add(alphaValues);
            calculatedValues.Add(predictedMValues);
            calculatedValues.Add(predictedAlphaValues);

            calculatedValues.Add(lowerBoundMValues);
            calculatedValues.Add(lowerBoundAlphaValues);
            calculatedValues.Add(predictedLowerBoundMValues);
            calculatedValues.Add(predictedLowerBoundAlphaValues);

            calculatedValues.Add(upperBoundMValues);
            calculatedValues.Add(upperBoundAlphaValues);
            calculatedValues.Add(predictedUpperBoundMValues);
            calculatedValues.Add(predictedUpperBoundAlphaValues);
        }

        public void CreateCoordinatesTableColumns(DataGridView coordinatesTable)
        {
            coordinatesTable.Rows.Clear();
            coordinatesTable.Columns.Clear();

            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());
            coordinatesTable.Columns.Add(new DataGridViewTextBoxColumn());

            coordinatesTable.Columns[0].Name = "Эпоха";

            coordinatesTable.Columns[1].Name = "М";
            coordinatesTable.Columns[2].Name = "Альфа°";
            coordinatesTable.Columns[3].Name = "Прогноз M";
            coordinatesTable.Columns[4].Name = "Прогноз Альфа°";

            coordinatesTable.Columns[5].Name = "М+";
            coordinatesTable.Columns[6].Name = "Альфа+°";
            coordinatesTable.Columns[7].Name = "Прогноз M+";
            coordinatesTable.Columns[8].Name = "Прогноз Альфа+°";

            coordinatesTable.Columns[9].Name = "М-";
            coordinatesTable.Columns[10].Name = "Альфа-°";
            coordinatesTable.Columns[11].Name = "Прогноз M-";
            coordinatesTable.Columns[12].Name = "Прогноз Альфа°-";
        }

        public void CreateEstimationTableColumns(DataGridView estimationTable)
        {
            estimationTable.Rows.Clear();
            estimationTable.Columns.Clear();

            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());
            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());
            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());
            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());
            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());
            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());
            estimationTable.Columns.Add(new DataGridViewTextBoxColumn());

            estimationTable.Columns[0].Name = "Эпоха";

            estimationTable.Columns[1].Name = "М-";
            estimationTable.Columns[2].Name = "М";
            estimationTable.Columns[3].Name = "М+";

            estimationTable.Columns[4].Name = "2E";
            estimationTable.Columns[5].Name = "L";

            estimationTable.Columns[6].Name = "Состояние";
        }

        public void FillMarksInCheckedListBox(int marksCount, CheckedListBox checkedListBox)
        {
            for (int i = 1; i <= marksCount; i++)
            {
                checkedListBox.Items.Add(i);
            }
        }

        public List<double> getPredictedValues(List<double> marksValues, double alpha)
        {
            return calculationsService.GetPredictedValues(marksValues, alpha);
        }
       
        private void fillEstimationColumn(
            ref Dictionary<string, List<double>> calculatedAlphaAndMValues,
            DataGridView dataGridView)
        {
            // в колонке 6 отображается значение Состояния объекта
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                if (Convert.ToDouble(dataGridView.Rows[i].Cells[5].Value) < (Convert.ToDouble(dataGridView.Rows[i].Cells[4].Value) / 2))
                {
                    dataGridView.Rows[i].Cells[6].Value = "В пределе";
                    dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Green;

                }
                else if (Convert.ToDouble(dataGridView.Rows[i].Cells[5].Value) == (Convert.ToDouble(dataGridView.Rows[i].Cells[4].Value) / 2))
                {
                    dataGridView.Rows[i].Cells[6].Value = "Точка бифуркации";
                    dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Yellow;
                }
                else
                {
                    dataGridView.Rows[i].Cells[6].Value = "Выход за границу";
                    dataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red;
                }
            }
        }       
    }
}
