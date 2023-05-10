using CourseWork.Domain.TechnogenicObject;
using CourseWork.Service.Decomposition.FirstLevel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWork.View.Forms.Decomposition
{
    public partial class FirstLevelForm : Form
    {
        // domain service
        private IDecompositionService decompositionService;

        // application service (подумать куда убрать, ведь это вообще не очень то и сервис)
        private IFirstLevelChartService chartService;

        private Dictionary<string, List<double>> calculatedAlphaAndMValues;

        private double epsilon;
        private DataGridView dataGridTable;
        private DataTable dataTable;
        private double alpha;

        public FirstLevelForm(
            double epsilon,
            double alpha,
            IDecompositionService decompositionService,
            DataGridView dataGridTable,
            DataTable dataTable)
        {
            this.decompositionService = decompositionService;
            this.chartService = new ChartService();
            this.calculatedAlphaAndMValues = new Dictionary<string, List<double>>();
            this.epsilon = epsilon;
            this.dataGridTable = dataGridTable;
            this.dataTable = dataTable;
            this.alpha = alpha;
            InitializeComponent();
        }

        private void FirstLevelForm_Load(object sender, EventArgs e)
        {
            fillCoordinatesTable();
            fillEstimationTable();
        }

        private void fillCoordinatesTable()
        {
            List<List<double>> calculatedValues = getCalculatedValuesInCoordinatesTable();
            createCoordinatesTableColumns();
            fillEpochColumnInCoordinatesTable();
            fillCalculatedValuesInCoordinatesTable(calculatedValues);
            fillEstimationEpochValues();
        }

        private void fillEstimationTable()
        {
            createEstimationTableColumns();
            fillEpochColumnInEstimationTable();
            fillCalculatedValuesInEstimationTable();
        }

        private void createCoordinatesTableColumns()
        {
            FirstLevelCoordinatesDataGridView.Rows.Clear();
            FirstLevelCoordinatesDataGridView.Columns.Clear();

            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelCoordinatesDataGridView.Columns.Add(new DataGridViewTextBoxColumn());

            FirstLevelCoordinatesDataGridView.Columns[0].Name = "Эпоха";

            FirstLevelCoordinatesDataGridView.Columns[1].Name = "М";
            FirstLevelCoordinatesDataGridView.Columns[2].Name = "Альфа";
            FirstLevelCoordinatesDataGridView.Columns[3].Name = "Прогноз M";
            FirstLevelCoordinatesDataGridView.Columns[4].Name = "Прогноз Альфа";

            FirstLevelCoordinatesDataGridView.Columns[5].Name = "М+";
            FirstLevelCoordinatesDataGridView.Columns[6].Name = "Альфа+";
            FirstLevelCoordinatesDataGridView.Columns[7].Name = "Прогноз M+";
            FirstLevelCoordinatesDataGridView.Columns[8].Name = "Прогноз Альфа+";

            FirstLevelCoordinatesDataGridView.Columns[9].Name = "М-";
            FirstLevelCoordinatesDataGridView.Columns[10].Name = "Альфа-";
            FirstLevelCoordinatesDataGridView.Columns[11].Name = "Прогноз M-";
            FirstLevelCoordinatesDataGridView.Columns[12].Name = "Прогноз Альфа-";
        }

        private List<List<double>> getCalculatedValuesInCoordinatesTable()
        {
            List<List<double>> calculatedCoordinatesValues = new List<List<double>>();

            DataGridView lowerBoundTable = decompositionService.CalculateLowerOrUpperBound(
                dataTable,
                epsilon,
                dataGridTable,
                "-");

            DataGridView upperBoundTable = decompositionService.CalculateLowerOrUpperBound(
                dataTable,
                epsilon,
                dataGridTable,
                "+");

            // ЗНАЧЕНИЯ M ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> MValues = decompositionService.CalculateMValues(dataGridTable);
            List<double> lowerBoundMValues = decompositionService.CalculateMValues(lowerBoundTable);
            List<double> upperBoundMValues = decompositionService.CalculateMValues(upperBoundTable);

            // ЗНАЧЕНИЯ АЛЬФЫ ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> alphaValues = decompositionService.CalculateAlphaValues(dataGridTable, MValues);
            List<double> lowerBoundAlphaValues = decompositionService.CalculateAlphaValues(lowerBoundTable, lowerBoundMValues);
            List<double> upperBoundAlphaValues = decompositionService.CalculateAlphaValues(upperBoundTable, upperBoundMValues);

            // ПРОГНОЗНЫЕ ЗНАЧЕНИЯ M ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> predictedMValues = decompositionService.GetPredictedValues(MValues, alpha);
            List<double> predictedLowerBoundMValues = decompositionService.GetPredictedValues(lowerBoundMValues, alpha);
            List<double> predictedUpperBoundMValues = decompositionService.GetPredictedValues(upperBoundMValues, alpha);

            // ПРОГНОЗНЫЕ ЗНАЧЕНИЯ АЛЬФЫ ДЛЯ ОСНОВНОЙ ТАБЛИЦЫ, НИЖНЕЙ И ВЕРХНЕЙ ГРАНИЦ
            List<double> predictedAlphaValues = decompositionService.GetPredictedValues(alphaValues, alpha);
            List<double> predictedLowerBoundAlphaValues = decompositionService.GetPredictedValues(lowerBoundAlphaValues, alpha);
            List<double> predictedUpperBoundAlphaValues = decompositionService.GetPredictedValues(upperBoundAlphaValues, alpha);

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
            calculatedCoordinatesValues.Add(MValues);
            calculatedCoordinatesValues.Add(alphaValues);
            calculatedCoordinatesValues.Add(predictedMValues);
            calculatedCoordinatesValues.Add(predictedAlphaValues);

            calculatedCoordinatesValues.Add(lowerBoundMValues);
            calculatedCoordinatesValues.Add(lowerBoundAlphaValues);
            calculatedCoordinatesValues.Add(predictedLowerBoundMValues);
            calculatedCoordinatesValues.Add(predictedLowerBoundAlphaValues);

            calculatedCoordinatesValues.Add(upperBoundMValues);
            calculatedCoordinatesValues.Add(upperBoundAlphaValues);
            calculatedCoordinatesValues.Add(predictedUpperBoundMValues);
            calculatedCoordinatesValues.Add(predictedUpperBoundAlphaValues);

            return calculatedCoordinatesValues;
        }

        private void fillCalculatedValuesInCoordinatesTable(List<List<double>> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                for (int j = 0; j < values[i].Count; j++)
                {
                    FirstLevelCoordinatesDataGridView.Rows[j].Cells[i + 1].Value = Convert.ToDouble(values[i][j]);
                }
            }
        }

        private void fillEpochColumnInCoordinatesTable()
        {
            for (int row = 0; row < dataGridTable.Rows.Count; row++)
            {
                FirstLevelCoordinatesDataGridView.Rows.Add();
                FirstLevelCoordinatesDataGridView.Rows[row].Cells[0].Value = dataGridTable.Rows[row].Cells[0].Value;
            }

            FirstLevelCoordinatesDataGridView.Rows[FirstLevelCoordinatesDataGridView.Rows.Count - 2].Cells[0].Value =
                Convert.ToInt32(dataGridTable.Rows[FirstLevelCoordinatesDataGridView.Rows.Count - 3].Cells[0].Value.ToString()) + 1;
        }

        private void createEstimationTableColumns()
        {
            FirstLevelEstimationDataGridView.Rows.Clear();
            FirstLevelEstimationDataGridView.Columns.Clear();

            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());
            FirstLevelEstimationDataGridView.Columns.Add(new DataGridViewTextBoxColumn());

            FirstLevelEstimationDataGridView.Columns[0].Name = "Эпоха";

            FirstLevelEstimationDataGridView.Columns[1].Name = "М-";
            FirstLevelEstimationDataGridView.Columns[2].Name = "М";
            FirstLevelEstimationDataGridView.Columns[3].Name = "М+";

            FirstLevelEstimationDataGridView.Columns[4].Name = "2E";
            FirstLevelEstimationDataGridView.Columns[5].Name = "L";

            FirstLevelEstimationDataGridView.Columns[6].Name = "Состояние";
        }

        private void fillCalculatedValuesInEstimationTable()
        {
            fillPredictedMValuesInEstimationTable();
            calculateValuesInEstimationTable();
        }

        private void fillPredictedMValuesInEstimationTable()
        {
            // в колонках [1-3] отображаются значения M 

            for (int i = 0; i < calculatedAlphaAndMValues["lowerBoundMValues"].Count; i++)
            {
                FirstLevelEstimationDataGridView.Rows[i].Cells[1].Value = calculatedAlphaAndMValues["lowerBoundMValues"][i];
            }

            for (int i = 0; i < calculatedAlphaAndMValues["MValues"].Count; i++)
            {
                FirstLevelEstimationDataGridView.Rows[i].Cells[2].Value = calculatedAlphaAndMValues["MValues"][i];
            }

            for (int i = 0; i < calculatedAlphaAndMValues["upperBoundMValues"].Count; i++)
            {
                FirstLevelEstimationDataGridView.Rows[i].Cells[3].Value = calculatedAlphaAndMValues["upperBoundMValues"][i];
            }

            // Добавляем прогнозные значения для каждой M
            int lastEstimationTableRowIndex = FirstLevelEstimationDataGridView.Rows.Count - 2;
            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            FirstLevelEstimationDataGridView.Rows[lastEstimationTableRowIndex].Cells[1].Value = calculatedAlphaAndMValues["predictedLowerBoundMValues"][lastPredictedMValueIndex];
            FirstLevelEstimationDataGridView.Rows[lastEstimationTableRowIndex].Cells[2].Value = calculatedAlphaAndMValues["predictedMValues"][lastPredictedMValueIndex];
            FirstLevelEstimationDataGridView.Rows[lastEstimationTableRowIndex].Cells[3].Value = calculatedAlphaAndMValues["predictedUpperBoundMValues"][lastPredictedMValueIndex];
        }

        private void calculateValuesInEstimationTable()
        {
            calculateDiameterColumn();
            calculateLColumn();
            calculateEstimationColumn();
        }

        // Diameter = 2E
        private void calculateDiameterColumn()
        {
            // в колонке 4 отображается значение 2E

            for (int i = 0; i < calculatedAlphaAndMValues["lowerBoundMValues"].Count; i++)
            {
                FirstLevelEstimationDataGridView.Rows[i].Cells[4].Value =
                    Math.Abs(calculatedAlphaAndMValues["lowerBoundMValues"][i] - calculatedAlphaAndMValues["upperBoundMValues"][i]); // * 2
            }

            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            FirstLevelEstimationDataGridView.Rows[lastPredictedMValueIndex].Cells[4].Value =
                   Math.Abs(calculatedAlphaAndMValues["predictedLowerBoundMValues"][lastPredictedMValueIndex] - calculatedAlphaAndMValues["predictedUpperBoundMValues"][lastPredictedMValueIndex]);
        }

        private void calculateLColumn()
        {
            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            // в колонке 5 отображается значение L
            for (int i = 0; i < calculatedAlphaAndMValues["MValues"].Count; i++)
            {
                FirstLevelEstimationDataGridView.Rows[i].Cells[5].Value =
                    Math.Abs(calculatedAlphaAndMValues["MValues"][0] - calculatedAlphaAndMValues["MValues"][i]);
            }

            FirstLevelEstimationDataGridView.Rows[lastPredictedMValueIndex].Cells[5].Value =
                   Math.Abs(calculatedAlphaAndMValues["MValues"][0] - calculatedAlphaAndMValues["predictedMValues"][lastPredictedMValueIndex]);
        }
        private void calculateEstimationColumn()
        {
            // в колонке 6 отображается значение Состояния объекта
            for (int i = 0; i < FirstLevelEstimationDataGridView.Rows.Count - 1; i++)
            {
                if (Convert.ToDouble(FirstLevelEstimationDataGridView.Rows[i].Cells[5].Value) < (Convert.ToDouble(FirstLevelEstimationDataGridView.Rows[i].Cells[4].Value) / 2))
                {
                    FirstLevelEstimationDataGridView.Rows[i].Cells[6].Value = "В пределе";
                    FirstLevelEstimationDataGridView.Rows[i].Cells[6].Style.BackColor = Color.Green;

                }
                else if (Convert.ToDouble(FirstLevelEstimationDataGridView.Rows[i].Cells[5].Value) == (Convert.ToDouble(FirstLevelEstimationDataGridView.Rows[i].Cells[4].Value) / 2))
                {
                    FirstLevelEstimationDataGridView.Rows[i].Cells[6].Value = "Точка бифуркации";
                    FirstLevelEstimationDataGridView.Rows[i].Cells[6].Style.BackColor = Color.Yellow;
                }
                else
                {
                    FirstLevelEstimationDataGridView.Rows[i].Cells[6].Value = "Выход за границу";
                    FirstLevelEstimationDataGridView.Rows[i].Cells[6].Style.BackColor = Color.Red;
                }
            }
        }

        private void fillEpochColumnInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["Epoches"].Count - 1; i++)
            {
                FirstLevelEstimationDataGridView.Rows.Add();
                FirstLevelEstimationDataGridView.Rows[i].Cells[0].Value = Convert.ToInt32(calculatedAlphaAndMValues["Epoches"][i]);
            }
        }

        private void fillEstimationEpochValues()
        {
            List<double> epoches = new List<double>();

            for (int row = 0; row < FirstLevelCoordinatesDataGridView.Rows.Count; row++)
            {
                epoches.Add(Convert.ToDouble(FirstLevelCoordinatesDataGridView.Rows[row].Cells[0].Value));
            }
            calculatedAlphaAndMValues["Epoches"] = epoches;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter_1(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void FirstLevelCoordinatesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FirstLevelMChart_Click(object sender, EventArgs e)
        {

        }

        // МЕТОДЫ ДЛЯ ОТРИСОВКИ ГРАФИКА
        private void LowerBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M-";
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["lowerBoundMValues"],
                    FirstLevelMChart);
            }
        }

        private void MCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M";
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["MValues"],
                    FirstLevelMChart);
            }
        }

        private void UpperBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M+";
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["upperBoundMValues"],
                    FirstLevelMChart);
            }
        }

        private void PredictedLowerBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М-";
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["predictedLowerBoundMValues"],
                    FirstLevelMChart);
            }
        }

        private void PredictedMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М";
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1) {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else { chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"], 
                calculatedAlphaAndMValues["predictedMValues"], 
                FirstLevelMChart);
            }
        }

        private void PredictedUpperBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М+";
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else { 
                chartService.AddXYLine(
                    serieName, 
                    calculatedAlphaAndMValues["Epoches"], 
                    calculatedAlphaAndMValues["predictedUpperBoundMValues"], 
                    FirstLevelMChart); 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void LowerBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа-";
            if (FirstLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["lowerBoundAlphaValues"],
                FirstLevelAlphaChart);
            }
        }

        private void AlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа";
            if (FirstLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["alphaValues"],
                FirstLevelAlphaChart);
            }
        }

        private void UpperBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа+";
            if (FirstLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["upperBoundAlphaValues"],
                FirstLevelAlphaChart);
            }
        }

        private void PredictedLowerBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа-";
            if (FirstLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"],
                FirstLevelAlphaChart);
            }
        }

        private void PredictedAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа";
            if (FirstLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedAlphaValues"],
                FirstLevelAlphaChart);
            }
        }

        private void PredictedUpperAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа+";
            if (FirstLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"],
                FirstLevelAlphaChart);
            }
        }
    }
}
