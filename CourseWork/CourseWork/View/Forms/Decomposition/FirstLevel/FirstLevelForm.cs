using CourseWork.Domain.TechnogenicObject;
using CourseWork.Service.Chart;
using CourseWork.Service.Chart.FirstLevel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace CourseWork.View.Forms.Decomposition
{
    public partial class FirstLevelForm : Form
    {
        // application service
        private IFirstLevelDecompositionService decompositionService;        
        private IFirstLevelChartService chartService;

        private Dictionary<string, List<double>> calculatedAlphaAndMValues;
        private List<List<double>> coordinatesTableValues;

        private double epsilon;
        private double alpha;

        private DataGridView dataGridTable;
        private DataTable dataTable;

        public FirstLevelForm(
            double epsilon,
            double alpha,
            IFirstLevelDecompositionService decompositionService,
            IFirstLevelChartService chartService,
            DataGridView dataGridTable,
            DataTable dataTable)
        {
            this.decompositionService = decompositionService;
            this.chartService = chartService;

            this.calculatedAlphaAndMValues = new Dictionary<string, List<double>>();
            this.coordinatesTableValues = new List<List<double>>();

            this.epsilon = epsilon;
            this.alpha = alpha;

            this.dataGridTable = dataGridTable;
            this.dataTable = dataTable;
            InitializeComponent();
        }

        private void FirstLevelForm_Load(object sender, EventArgs e)
        {
            fillCoordinatesTable();
            fillEstimationTable();
        }

        private void fillCoordinatesTable()
        {
            decompositionService.CreateCoordinatesTableColumns(FirstLevelCoordinatesDataGridView);
            fillEpochColumnInCoordinatesTable();

            decompositionService.CalculateValuesInCoordinatesTable(
                ref coordinatesTableValues, 
                ref calculatedAlphaAndMValues,
                dataGridTable,
                dataTable,
                epsilon, 
                alpha);

            fillCalculatedValuesInCoordinatesTable();
            fillEstimationEpochValues();
        }

        private void fillEstimationTable()
        {
            decompositionService.CreateEstimationTableColumns(FirstLevelEstimationDataGridView);
            fillEpochColumnInEstimationTable();
            fillPredictedMValuesInEstimationTable();

            decompositionService.FillValuesInEstimationTable(
                ref calculatedAlphaAndMValues,
                FirstLevelEstimationDataGridView);
        }

        private void fillCalculatedValuesInCoordinatesTable()
        {
            for (int i = 0; i < coordinatesTableValues.Count; i++)
            {
                for (int j = 0; j < coordinatesTableValues[i].Count; j++)
                {
                    FirstLevelCoordinatesDataGridView.Rows[j].Cells[i + 1].Value = Convert.ToDouble(coordinatesTableValues[i][j]);
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
            if (FirstLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(FirstLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
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
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["predictedUpperBoundMValues"],
                    FirstLevelMChart);
            }
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

        // кнопка очистить на графике
        private void button2_Click(object sender, EventArgs e)
        {

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

        
    }
}
