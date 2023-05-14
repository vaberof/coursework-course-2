using CourseWork.Domain.TechnogenicObject;
using CourseWork.Service.Chart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CourseWork.Service.Decomposition;

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

        private DataGridView mainCoordinatesTable;
        private DataTable dataTable;

        public FirstLevelForm(
            double epsilon,
            double alpha,
            IFirstLevelDecompositionService decompositionService,
            IFirstLevelChartService chartService,
            DataGridView mainCoordinatesTable,
            DataTable dataTable)
        {
            this.decompositionService = decompositionService;
            this.chartService = chartService;

            this.calculatedAlphaAndMValues = new Dictionary<string, List<double>>();
            this.coordinatesTableValues = new List<List<double>>();

            this.epsilon = epsilon;
            this.alpha = alpha;

            this.mainCoordinatesTable = mainCoordinatesTable;
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
            decompositionService.CreateCoordinatesTableColumns(coordinatesDataGridView);
            fillEpochColumnInCoordinatesTable();

            decompositionService.CalculateValuesInCoordinatesTable(
                ref coordinatesTableValues, 
                ref calculatedAlphaAndMValues,
                mainCoordinatesTable,
                dataTable,
                epsilon, 
                alpha);

            fillCalculatedValuesInCoordinatesTable();
            fillEstimationEpochValues();
        }

        private void fillEstimationTable()
        {
            decompositionService.CreateEstimationTableColumns(EstimationDataGridView);
            fillEpochColumnInEstimationTable();
            fillPredictedMValuesInEstimationTable();

            decompositionService.FillValuesInEstimationTable(
                ref calculatedAlphaAndMValues,
                EstimationDataGridView);
        }

        private void fillCalculatedValuesInCoordinatesTable()
        {
            for (int i = 0; i < coordinatesTableValues.Count; i++)
            {
                for (int j = 0; j < coordinatesTableValues[i].Count; j++)
                {
                    coordinatesDataGridView.Rows[j].Cells[i + 1].Value = Convert.ToDouble(coordinatesTableValues[i][j]);
                }
            }
        }

        private void fillEpochColumnInCoordinatesTable()
        {
            for (int row = 0; row < mainCoordinatesTable.Rows.Count; row++)
            {
                coordinatesDataGridView.Rows.Add();
                coordinatesDataGridView.Rows[row].Cells[0].Value = mainCoordinatesTable.Rows[row].Cells[0].Value;
            }

            coordinatesDataGridView.Rows[coordinatesDataGridView.Rows.Count - 2].Cells[0].Value =
                Convert.ToInt32(mainCoordinatesTable.Rows[coordinatesDataGridView.Rows.Count - 3].Cells[0].Value.ToString()) + 1;
        }

        private void fillPredictedMValuesInEstimationTable()
        {
            // в колонках [1-3] отображаются значения M 
            fillLowerBoundMValuesInEstimationTable();
            fillMValuesInEstimationTable();
            fillUpperBoundMValuesInEstimationTable();

            // Добавляем прогнозные значения для каждой M
            int lastEstimationTableRowIndex = EstimationDataGridView.Rows.Count - 2;
            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            EstimationDataGridView.Rows[lastEstimationTableRowIndex].Cells[1].Value = calculatedAlphaAndMValues["predictedLowerBoundMValues"][lastPredictedMValueIndex];
            EstimationDataGridView.Rows[lastEstimationTableRowIndex].Cells[2].Value = calculatedAlphaAndMValues["predictedMValues"][lastPredictedMValueIndex];
            EstimationDataGridView.Rows[lastEstimationTableRowIndex].Cells[3].Value = calculatedAlphaAndMValues["predictedUpperBoundMValues"][lastPredictedMValueIndex];
        }

        private void fillLowerBoundMValuesInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["lowerBoundMValues"].Count; i++)
            {
                EstimationDataGridView.Rows[i].Cells[1].Value = calculatedAlphaAndMValues["lowerBoundMValues"][i];
            }
        }

        private void fillMValuesInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["MValues"].Count; i++)
            {
                EstimationDataGridView.Rows[i].Cells[2].Value = calculatedAlphaAndMValues["MValues"][i];
            }
        }

        private void fillUpperBoundMValuesInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["upperBoundMValues"].Count; i++)
            {
                EstimationDataGridView.Rows[i].Cells[3].Value = calculatedAlphaAndMValues["upperBoundMValues"][i];
            }
        }
        private void fillEpochColumnInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["Epochs"].Count - 1; i++)
            {
                EstimationDataGridView.Rows.Add();
                EstimationDataGridView.Rows[i].Cells[0].Value = Convert.ToInt32(calculatedAlphaAndMValues["Epochs"][i]);
            }
        }

        private void fillEstimationEpochValues()
        {
            List<double> epochs = new List<double>();

            for (int row = 0; row < coordinatesDataGridView.Rows.Count; row++)
            {
                epochs.Add(Convert.ToDouble(coordinatesDataGridView.Rows[row].Cells[0].Value));
            }
            calculatedAlphaAndMValues["Epochs"] = epochs;
        }

        // МЕТОДЫ ДЛЯ ОТРИСОВКИ ГРАФИКА
        private void LowerBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M-";
            if (MChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(MChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epochs"],
                    calculatedAlphaAndMValues["lowerBoundMValues"],
                    MChart);
            }
        }

        private void MCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M";
            if (MChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(MChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epochs"],
                    calculatedAlphaAndMValues["MValues"],
                    MChart);
            }
        }

        private void UpperBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M+";
            if (MChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(MChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epochs"],
                    calculatedAlphaAndMValues["upperBoundMValues"],
                    MChart);
            }
        }

        private void PredictedLowerBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М-";
            if (MChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(MChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epochs"],
                    calculatedAlphaAndMValues["predictedLowerBoundMValues"],
                    MChart);
            }
        }

        private void PredictedMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М";
            if (MChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(MChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["predictedMValues"],
                MChart);
            }
        }

        private void PredictedUpperBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М+";
            if (MChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(MChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epochs"],
                    calculatedAlphaAndMValues["predictedUpperBoundMValues"],
                    MChart);
            }
        }        

        private void LowerBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа-";
            if (AlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(AlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["lowerBoundAlphaValues"],
                AlphaChart);
            }
        }

        private void AlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа";
            if (AlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(AlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["alphaValues"],
                AlphaChart);
            }
        }

        private void UpperBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа+";
            if (AlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(AlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["upperBoundAlphaValues"],
                AlphaChart);
            }
        }

        private void PredictedLowerBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа-";
            if (AlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(AlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"],
                AlphaChart);
            }
        }

        private void PredictedAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа";
            if (AlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(AlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["predictedAlphaValues"],
                AlphaChart);
            }
        }

        private void PredictedUpperAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа+";
            if (AlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(AlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epochs"],
                calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"],
                AlphaChart);
            }
        }

        // МЕТОДЫ ДЛЯ ГРАФИКОВ ФУНКЦИИ ОТКЛИКА
        private void LowerBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Функция отклика-";
            if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(ResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddMAndAlphaLine(
                serieName,
                calculatedAlphaAndMValues["lowerBoundMValues"],
                calculatedAlphaAndMValues["lowerBoundAlphaValues"],
                ResponseFunctionChart);
            }
        }

        private void PredictedLowerBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Функция отклика-";
            if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(ResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddPredictedValue(
                serieName,
                calculatedAlphaAndMValues["predictedLowerBoundMValues"],
                calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"],
                ResponseFunctionChart);
            }
        }

        private void ResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Функция отклика";
            if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(ResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddMAndAlphaLine(
                serieName,
                calculatedAlphaAndMValues["MValues"],
                calculatedAlphaAndMValues["alphaValues"],
                ResponseFunctionChart);
            }
        }

        private void PredictedResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Функция отклика";
            if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(ResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddPredictedValue(
                serieName,
                calculatedAlphaAndMValues["predictedMValues"],
                calculatedAlphaAndMValues["predictedAlphaValues"],
                ResponseFunctionChart);
            }
        }

        private void UpperBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Функция отклика+";
            if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(ResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddMAndAlphaLine(
                serieName,
                calculatedAlphaAndMValues["upperBoundMValues"],
                calculatedAlphaAndMValues["upperBoundAlphaValues"],
                ResponseFunctionChart);
            }
        }

        private void PredictedUpperBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Функция отклика+";
            if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(ResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddPredictedValue(
                serieName,
                calculatedAlphaAndMValues["predictedUpperBoundMValues"],
                calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"],
                ResponseFunctionChart);
            }
        }
        private void MChartClearButton_Click(object sender, EventArgs e)
        {
            clearGroupBoxCheckBoxes(MChartGroupBox);
            chartService.ClearChart(MChart);
        }

        private void AlphaChartClearButton_Click(object sender, EventArgs e)
        {
            clearGroupBoxCheckBoxes(AlphaChartGroupBox);
            chartService.ClearChart(AlphaChart);
        }

        private void ResponseFunctionChartClearButton_Click(object sender, EventArgs e)
        {
            clearGroupBoxCheckBoxes(ResponseFunctionGroupBox);
            chartService.ClearChart(ResponseFunctionChart);
        }

        private void clearGroupBoxCheckBoxes(GroupBox groupBox)
        {
            foreach (CheckBox c in groupBox.Controls.OfType<CheckBox>())
            {
                c.Checked = false;
            }
        }        
    }
}
