using CourseWork.Service.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using CourseWork.Service.Decomposition;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.View.Forms.Decomposition.SecondLevel
{
    public partial class SecondLevelForm : Form
    {
        private ISecondLevelDecompositionService decompositionService;
        private ISecondLevelChartService chartService;

        private int structuralBlocksCount;                       // количество структурных блоков 
        private int marksCount;                                  // количество геодезических марок
        private Dictionary<string, List<string>> distributedMarks;
        private List<string> blockNames;
        private int needMarksOnEachBlock;
        private int distributedMarksCount;

        private Dictionary<string, List<double>> calculatedAlphaAndMValues;
        private List<List<double>> coordinatesTableValues;

        private double epsilon;
        private double alpha;
        string pngFilePath;

        private DataGridView mainCoordinatesTable;

        public SecondLevelForm(
            ISecondLevelDecompositionService decompositionService,
            ISecondLevelChartService chartService,
            int structuralBlocksCount,
            int geodeticMarksCount,
            double epsilon,
            double alpha,
            string pngFilePath,
            DataGridView mainCoordinatesTable)
        {
            this.decompositionService = decompositionService;
            this.chartService = chartService;

            this.structuralBlocksCount = structuralBlocksCount;
            this.marksCount = geodeticMarksCount;
            this.distributedMarks = new Dictionary<string, List<string>>();
            this.needMarksOnEachBlock = geodeticMarksCount / structuralBlocksCount;
            this.distributedMarksCount = 0;

            this.epsilon = epsilon;
            this.alpha = alpha;
            this.pngFilePath = pngFilePath;
            this.mainCoordinatesTable = mainCoordinatesTable;

            initBlockNames();

            InitializeComponent();
        }
        private void SecondLevelForm_Load(object sender, EventArgs e)
        {
            fillChooseBlockComboBox(ChooseBlockComboBox);
            selectChooseBlockComboBoxDefaultItem(ChooseBlockComboBox);
            initDistributedMarks();
            initNeedToDistributeLabel();
            fillMarksListBox();
            showObjectPicture();
            CalculationsAndChartsTabPage.Enabled = false;
        }

        // --------------------МЕТОДЫ ДЛЯ СТРАНИЦЫ ФОРМЫ "Распределение марок по блокам"--------------------

        // При смене блока заполняем распределенные марки в listBox
        private void ChooseBlockComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistributedMarksListBox.Items.Clear();
            fillDistributedMarksListBox();
            changeNeedToDistributeLabel();
        }

        private void fillMarksListBox()
        {
            for (int i = 1; i <= marksCount; i++)
            {
                MarksListBox.Items.Add(i);
            }
        }

        // Заполняем уже распределенные марки в listBox
        private void fillDistributedMarksListBox()
        {
            if (!distributedMarks.ContainsKey(ChooseBlockComboBox.SelectedItem.ToString()))
            {
                return;
            }

            for (int i = 0; i < distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].Count; i++)
            {
                DistributedMarksListBox.Items.Add(distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()][i]);
            }
        }

        // Добавление марки в listBox с распределенными марками при двойном клике
        private void addMarkOnDoubleClick(object sender, EventArgs e)
        {
            if (MarksListBox.SelectedItem == null)
            {
                return;
            }

            if (hasDistrubutedMarks())
            {
                MessageBox.Show("Все марки для блока " + ChooseBlockComboBox.SelectedItem.ToString() + " распределены");
                return;
            }
            
            processMarkAddition(ChooseBlockComboBox.SelectedItem.ToString(), MarksListBox.SelectedItem);

            distributedMarksCount++;
            changeNeedToDistributeLabel();
            

            if (needMarksOnEachBlock * structuralBlocksCount == distributedMarksCount)
            {
                MessageBox.Show("Все марки распределены!");
                initCalculationsAndChartsFormTab();
            }
        }        

        // Добавление марки в listBox с распределенными марками при нажатии на кнопку
        private void addMarkOnPressButton(object sender, EventArgs e)
        {
            if (hasDistrubutedMarks())
            {
                MessageBox.Show("Все марки для блока " + ChooseBlockComboBox.SelectedItem.ToString() + " распределены");
                return;
            }

            if (MarksListBox.Items.Count == 0)
            {
                MessageBox.Show("Нет доступных марок для распределения");
                return;
            }

            // Если выбрана марка пользователем - добавляем ее
            if (MarksListBox.SelectedItem != null)
            {
                processMarkAddition(ChooseBlockComboBox.SelectedItem.ToString(), MarksListBox.SelectedItem);
            }
            else
            {
                processMarkAddition(ChooseBlockComboBox.SelectedItem.ToString(), MarksListBox.Items[0]);              
            }

            distributedMarksCount++;
            changeNeedToDistributeLabel();

            if (needMarksOnEachBlock * structuralBlocksCount == distributedMarksCount)
            {
                MessageBox.Show("Все марки распределены!");
                initCalculationsAndChartsFormTab();
            }
        }

        // Удаление марки в listBox с распределенными марками при двойном клике
        private void deleteDistributedMarkOnDoubleClick(object sender, EventArgs e)
        {
            if (DistributedMarksListBox.SelectedItem != null)
            {
                processMarkDeletion(ChooseBlockComboBox.SelectedItem.ToString(), DistributedMarksListBox.SelectedItem);

                distributedMarksCount--;
                changeNeedToDistributeLabel();
            }
        }

        // Удаление марки в listBox с распределенными марками при нажатии на кнопку
        private void deleteMarkOnPressButton(object sender, EventArgs e)
        {
            if (DistributedMarksListBox.Items.Count == 0)
            {
                MessageBox.Show("Нет доступных марок для удаления");
                return;
            }

            if (DistributedMarksListBox.SelectedItem != null)
            {
                processMarkDeletion(ChooseBlockComboBox.SelectedItem.ToString(), DistributedMarksListBox.SelectedItem);
            }
            else
            {
                processMarkDeletion(ChooseBlockComboBox.SelectedItem.ToString(), DistributedMarksListBox.Items[0]);
            }

            distributedMarksCount--;
            changeNeedToDistributeLabel();
        }

        private void processMarkAddition(string blockName, object selectedMark)
        {
            distributedMarks[blockName].Add(selectedMark.ToString());

            DistributedMarksListBox.Items.Add(selectedMark);
            MarksListBox.Items.Remove(selectedMark);
        }

        private void processMarkDeletion(string blockName, object selectedMark)
        {
            distributedMarks[blockName].Remove(selectedMark.ToString());

            MarksListBox.Items.Add(selectedMark);
            DistributedMarksListBox.Items.Remove(selectedMark);
        }

        private bool hasDistrubutedMarks()
        {
            return distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].Count == needMarksOnEachBlock;
        }

        private void showObjectPicture()
        {
            ObjectPictureBox.Load(pngFilePath);
            ObjectPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void initBlockNames()
        {
            this.blockNames = new List<string>();
            this.blockNames.Add("А");
            this.blockNames.Add("Б");
            this.blockNames.Add("В");
            this.blockNames.Add("Г");
            this.blockNames.Add("Д");
        }

        private void initNeedToDistributeLabel()
        {
            NeedToDistributeLabel.Text = "Осталось распределить марок: " + Convert.ToString(needMarksOnEachBlock);
        }

        private void changeNeedToDistributeLabel()
        {
            if (!distributedMarks.ContainsKey(ChooseBlockComboBox.SelectedItem.ToString()))
            {
                return;
            }

            NeedToDistributeLabel.Text = "Осталось распределить марок: " +
                Convert.ToString(needMarksOnEachBlock - distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].Count);
        }

        // Инициализация словаря с распределенными марками. 
        // Словарь вида: "A": [1,2,3,4,5]
        private void initDistributedMarks()
        {
            for (int i = 0; i < structuralBlocksCount; i++)
            {
                distributedMarks[blockNames[i]] = new List<string>();
            }
        }

        // --------------------МЕТОДЫ ДЛЯ СТРАНИЦЫ ФОРМЫ "Расчеты и графики"--------------------

        private void initCalculationsAndChartsFormTab()
        {
            CalculationsAndChartsTabPage.Enabled = true;
            fillChooseBlockComboBox(ChooseBlockCalculationsAndChartsComboBox);
            selectChooseBlockComboBoxDefaultItem(ChooseBlockCalculationsAndChartsComboBox);
        }

        private void fillCoordinatesAndEstimationTable()
        {
            this.calculatedAlphaAndMValues = new Dictionary<string, List<double>>();
            this.coordinatesTableValues = new List<List<double>>();

            fillCoordinatesTable();
            fillEstimationTable();
        }

        private void fillCoordinatesTable()
        {
            decompositionService.CreateCoordinatesTableColumns(CoordinatesMarksDataGridView);
            fillEpochColumnInCoordinatesTable();

            decompositionService.CalculateValuesInMarksCoordinatesTable(
                ref coordinatesTableValues,
                ref calculatedAlphaAndMValues,
                distributedMarks[ChooseBlockCalculationsAndChartsComboBox.SelectedItem.ToString()],
                mainCoordinatesTable,
                epsilon,
                alpha);

            fillCalculatedValuesInCoordinatesTable();
        }

        private void fillEstimationTable()
        {
            fillEstimationEpochValues();
            decompositionService.CreateEstimationTableColumns(EstimationMarksDataGridView);
            fillEpochColumnInEstimationTable();
            fillPredictedMValuesInEstimationTable();

            decompositionService.FillValuesInEstimationTable(
                ref calculatedAlphaAndMValues,
                EstimationMarksDataGridView);
        }
        private void fillEpochColumnInCoordinatesTable()
        {

            for (int row = 0; row < mainCoordinatesTable.Rows.Count; row++)
            {
                CoordinatesMarksDataGridView.Rows.Add();
                CoordinatesMarksDataGridView.Rows[row].Cells[0].Value = mainCoordinatesTable.Rows[row].Cells[0].Value;
            }

            CoordinatesMarksDataGridView.Rows[CoordinatesMarksDataGridView.Rows.Count - 2].Cells[0].Value =
                Convert.ToInt32(mainCoordinatesTable.Rows[CoordinatesMarksDataGridView.Rows.Count - 3].Cells[0].Value.ToString()) + 1;
        }

        private void fillCalculatedValuesInCoordinatesTable()
        {
            for (int i = 0; i < coordinatesTableValues.Count; i++)
            {
                for (int j = 0; j < coordinatesTableValues[i].Count; j++)
                {
                    CoordinatesMarksDataGridView.Rows[j].Cells[i + 1].Value = Convert.ToDouble(coordinatesTableValues[i][j]);
                }
            }
        }

        private void fillEpochColumnInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["Epoches"].Count - 1; i++)
            {
                EstimationMarksDataGridView.Rows.Add();
                EstimationMarksDataGridView.Rows[i].Cells[0].Value = Convert.ToInt32(calculatedAlphaAndMValues["Epoches"][i]);
            }
        }

        private void fillPredictedMValuesInEstimationTable()
        {
            // в колонках [1-3] отображаются значения M 
            fillLowerBoundMValuesInEstimationTable();
            fillMValuesInEstimationTable();
            fillUpperBoundMValuesInEstimationTable();

            // Добавляем прогнозные значения для каждой M
            int lastEstimationTableRowIndex = EstimationMarksDataGridView.Rows.Count - 2;
            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            EstimationMarksDataGridView.Rows[lastEstimationTableRowIndex].Cells[1].Value = calculatedAlphaAndMValues["predictedLowerBoundMValues"][lastPredictedMValueIndex];
            EstimationMarksDataGridView.Rows[lastEstimationTableRowIndex].Cells[2].Value = calculatedAlphaAndMValues["predictedMValues"][lastPredictedMValueIndex];
            EstimationMarksDataGridView.Rows[lastEstimationTableRowIndex].Cells[3].Value = calculatedAlphaAndMValues["predictedUpperBoundMValues"][lastPredictedMValueIndex];
        }

        private void fillLowerBoundMValuesInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["lowerBoundMValues"].Count; i++)
            {
                EstimationMarksDataGridView.Rows[i].Cells[1].Value = calculatedAlphaAndMValues["lowerBoundMValues"][i];
            }
        }

        private void fillMValuesInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["MValues"].Count; i++)
            {
                EstimationMarksDataGridView.Rows[i].Cells[2].Value = calculatedAlphaAndMValues["MValues"][i];
            }
        }

        private void fillUpperBoundMValuesInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["upperBoundMValues"].Count; i++)
            {
                EstimationMarksDataGridView.Rows[i].Cells[3].Value = calculatedAlphaAndMValues["upperBoundMValues"][i];
            }
        }
        
        private void fillEstimationEpochValues()
        {
            List<double> epoches = new List<double>();

            for (int row = 0; row < CoordinatesMarksDataGridView.Rows.Count; row++)
            {
                epoches.Add(Convert.ToDouble(CoordinatesMarksDataGridView.Rows[row].Cells[0].Value));
            }
            calculatedAlphaAndMValues["Epoches"] = epoches;
        }

        private void fillChooseBlockComboBox(ComboBox comboBox)
        {
            for (int i = 0; i < structuralBlocksCount; i++)
            {
                comboBox.Items.Add(blockNames[i]);
            }
        }

        private void selectChooseBlockComboBoxDefaultItem(ComboBox comboBox)
        {
            if (structuralBlocksCount == 0)
            {
                return;
            }

            comboBox.SelectedIndex = 0;
        }

        // При смене блока заполняем таблицы, очищаем графики и чекбоксы
        private void ChooseBlockCalculationsAndGraphsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCoordinatesAndEstimationTable();
            clearCheckBoxes();
            clearCharts();
        }

        private void clearCharts()
        {
            chartService.ClearChart(MChart);
            chartService.ClearChart(AlphaChart);
            chartService.ClearChart(ResponseFunctionChart);
        }

        private void clearCheckBoxes()
        {
            clearGroupBoxCheckBoxes(MChartGroupBox);
            clearGroupBoxCheckBoxes(AlphaChartGroupBox);
            clearGroupBoxCheckBoxes(ResponseFunctionGroupBox);
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

        // МЕТОДЫ ДЛЯ ОТРИСОВКИ ГРАФИКОВ
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
                    calculatedAlphaAndMValues["Epoches"],
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
                    calculatedAlphaAndMValues["Epoches"],
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
                    calculatedAlphaAndMValues["Epoches"],
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
                    calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
                    calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
                calculatedAlphaAndMValues["Epoches"],
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
    }
}
