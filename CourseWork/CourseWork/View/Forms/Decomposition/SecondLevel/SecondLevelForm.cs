using CourseWork.Service.Chart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        private DataGridView dataGridTable;

        public SecondLevelForm(
            ISecondLevelDecompositionService decompositionService,
            ISecondLevelChartService chartService,
            int structuralBlocksCount, 
            int geodeticMarksCount,
            double epsilon,
            double alpha,
            DataGridView dataGridTable)
        {
            this.decompositionService = decompositionService;
            this.chartService = chartService;

            this.structuralBlocksCount = structuralBlocksCount;
            this.marksCount = geodeticMarksCount;
            this.distributedMarks = new Dictionary<string, List<string>>();
            this.needMarksOnEachBlock = geodeticMarksCount / structuralBlocksCount;
            this.distributedMarksCount = 0;

            // объекты для вычислений 
            
            this.epsilon = epsilon;
            this.alpha = alpha;

            this.dataGridTable = dataGridTable;

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
        }

        private void initCalculationsAndGraphsFormTab()
        {
            fillChooseBlockComboBox(ChooseBlockCalculationsAndGraphsComboBox);
            selectChooseBlockComboBoxDefaultItem(ChooseBlockCalculationsAndGraphsComboBox);
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
            decompositionService.CreateCoordinatesTableColumns(SecondLevelCoordinatesMarksDataGridView);
            fillEpochColumnInCoordinatesTable();

            decompositionService.CalculateValuesInMarksCoordinatesTable(
                ref coordinatesTableValues,
                ref calculatedAlphaAndMValues,
                distributedMarks[ChooseBlockCalculationsAndGraphsComboBox.SelectedItem.ToString()],
                dataGridTable,                
                epsilon,
                alpha);

            fillCalculatedValuesInCoordinatesTable();
            fillEstimationEpochValues();
        }

        private void fillEstimationTable()
        {
            decompositionService.CreateEstimationTableColumns(SecondLevelEstimationMarksDataGridView);
            fillEpochColumnInEstimationTable();
            fillPredictedMValuesInEstimationTable();

            decompositionService.FillValuesInEstimationTable(
                ref calculatedAlphaAndMValues,
                SecondLevelEstimationMarksDataGridView);
        }

        private void fillEpochColumnInEstimationTable()
        {
            for (int i = 0; i < calculatedAlphaAndMValues["Epoches"].Count - 1; i++)
            {
                SecondLevelEstimationMarksDataGridView.Rows.Add();
                SecondLevelEstimationMarksDataGridView.Rows[i].Cells[0].Value = Convert.ToInt32(calculatedAlphaAndMValues["Epoches"][i]);
            }
        }

        private void fillPredictedMValuesInEstimationTable()
        {
            // в колонках [1-3] отображаются значения M 

            for (int i = 0; i < calculatedAlphaAndMValues["lowerBoundMValues"].Count; i++)
            {
                SecondLevelEstimationMarksDataGridView.Rows[i].Cells[1].Value = calculatedAlphaAndMValues["lowerBoundMValues"][i];
            }

            for (int i = 0; i < calculatedAlphaAndMValues["MValues"].Count; i++)
            {
                SecondLevelEstimationMarksDataGridView.Rows[i].Cells[2].Value = calculatedAlphaAndMValues["MValues"][i];
            }

            for (int i = 0; i < calculatedAlphaAndMValues["upperBoundMValues"].Count; i++)
            {
                SecondLevelEstimationMarksDataGridView.Rows[i].Cells[3].Value = calculatedAlphaAndMValues["upperBoundMValues"][i];
            }

            // Добавляем прогнозные значения для каждой M
            int lastEstimationTableRowIndex = SecondLevelEstimationMarksDataGridView.Rows.Count - 2;
            int lastPredictedMValueIndex = calculatedAlphaAndMValues["predictedMValues"].Count - 1;

            SecondLevelEstimationMarksDataGridView.Rows[lastEstimationTableRowIndex].Cells[1].Value = calculatedAlphaAndMValues["predictedLowerBoundMValues"][lastPredictedMValueIndex];
            SecondLevelEstimationMarksDataGridView.Rows[lastEstimationTableRowIndex].Cells[2].Value = calculatedAlphaAndMValues["predictedMValues"][lastPredictedMValueIndex];
            SecondLevelEstimationMarksDataGridView.Rows[lastEstimationTableRowIndex].Cells[3].Value = calculatedAlphaAndMValues["predictedUpperBoundMValues"][lastPredictedMValueIndex];
        }

        private void fillEpochColumnInCoordinatesTable()
        {
            
            for (int row = 0; row < dataGridTable.Rows.Count; row++)
            {
                SecondLevelCoordinatesMarksDataGridView.Rows.Add();
                SecondLevelCoordinatesMarksDataGridView.Rows[row].Cells[0].Value = dataGridTable.Rows[row].Cells[0].Value;
            }

            SecondLevelCoordinatesMarksDataGridView.Rows[SecondLevelCoordinatesMarksDataGridView.Rows.Count - 2].Cells[0].Value =
                Convert.ToInt32(dataGridTable.Rows[SecondLevelCoordinatesMarksDataGridView.Rows.Count - 3].Cells[0].Value.ToString()) + 1;
        }

        private void fillCalculatedValuesInCoordinatesTable()
        {
            for (int i = 0; i < coordinatesTableValues.Count; i++)
            {
                for (int j = 0; j < coordinatesTableValues[i].Count; j++)
                {
                    SecondLevelCoordinatesMarksDataGridView.Rows[j].Cells[i + 1].Value = Convert.ToDouble(coordinatesTableValues[i][j]);
                }
            }
        }
        private void fillEstimationEpochValues()
        {
            List<double> epoches = new List<double>();

            for (int row = 0; row < SecondLevelCoordinatesMarksDataGridView.Rows.Count; row++)
            {
                epoches.Add(Convert.ToDouble(SecondLevelCoordinatesMarksDataGridView.Rows[row].Cells[0].Value));
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

        private void fillMarksListBox()
        {
            for (int i = 1; i <= marksCount; i++)
            {
                MarksListBox.Items.Add(i);
            }
        }

        private void initDistributedMarks()
        {
            for (int i = 0; i < structuralBlocksCount; i++)
            {
                distributedMarks[blockNames[i]] = new List<string>();
            }
        }

        // при двойном клике по марке
        private void distibuteMark(object sender, EventArgs e)
        {
            if (hasDistrubutedMarks()) {
                MessageBox.Show("Все марки для блока " + ChooseBlockComboBox.SelectedItem.ToString() + " распределены");
                return;
            }

            if (MarksListBox.SelectedItem != null)
            {
                // добавляем выбанную марку в словарь с распределенными марками по блоку
                distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].
                    Add(Convert.ToString(MarksListBox.SelectedItem));

                DistributedMarksListBox.Items.Add(MarksListBox.SelectedItem);
                MarksListBox.Items.Remove(MarksListBox.SelectedItem);                
            }

            distributedMarksCount++;
            changeNeedToDistributeLabel();

            if (needMarksOnEachBlock * structuralBlocksCount == distributedMarksCount)
            {
                MessageBox.Show("Все марки распределены!");
                initCalculationsAndGraphsFormTab();
            }
        }

        // при нажатии на кнопку
        private void addMark(object sender, EventArgs e)
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

            // если выбрана марка пользователем - добавляем ее
            if (MarksListBox.SelectedItem != null)
            {
                // добавляем выбанную марку в словарь с распределенными марками по блоку
                distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].
                    Add(Convert.ToString(MarksListBox.SelectedItem));

                DistributedMarksListBox.Items.Add(MarksListBox.SelectedItem);
                MarksListBox.Items.Remove(MarksListBox.SelectedItem);                
            }
            // иначе добавляем первую доступную из списка            
            else
            {
                // добавляем выбанную марку в словарь с распределенными марками по блоку
                distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].
                    Add(Convert.ToString(MarksListBox.Items[0]));

                DistributedMarksListBox.Items.Add(MarksListBox.Items[0]);
                MarksListBox.Items.Remove(MarksListBox.Items[0]);                
            }

            distributedMarksCount++;
            changeNeedToDistributeLabel();

            if (needMarksOnEachBlock * structuralBlocksCount == distributedMarksCount)
            {
                MessageBox.Show("Все марки распределены!");
                initCalculationsAndGraphsFormTab();
            }
        }

        // при двойном клике по марке
        private void deleteDistibutedMark(object sender, EventArgs e)
        {
            if (DistributedMarksListBox.SelectedItem != null)
            {
                // удаляем выбанную марку из словаря с распределенными марками по блоку
                distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].
                    Remove(Convert.ToString(DistributedMarksListBox.SelectedItem));

                MarksListBox.Items.Add(DistributedMarksListBox.SelectedItem);
                DistributedMarksListBox.Items.Remove(DistributedMarksListBox.SelectedItem);
            }

            distributedMarksCount--;
            changeNeedToDistributeLabel();
        }

        // при нажатии на кнопку
        private void deleteMark(object sender, EventArgs e)
        {
            if (DistributedMarksListBox.Items.Count == 0)
            {
                MessageBox.Show("Нет доступных марок для удаления");
                return;
            }

            // если выбрана марка пользователем - удаляем ее
            if (DistributedMarksListBox.SelectedItem != null)
            {
                // удаляем выбанную марку из словаря с распределенными марками по блоку
                distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].
                    Remove(Convert.ToString(DistributedMarksListBox.SelectedItem));

                MarksListBox.Items.Add(DistributedMarksListBox.SelectedItem);
                DistributedMarksListBox.Items.Remove(DistributedMarksListBox.SelectedItem);                
            }
            // иначе добавляем первую доступную из списка            
            else
            {
                // удаляем выбанную марку из словаря с распределенными марками по блоку
                distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].
                    Remove(Convert.ToString(DistributedMarksListBox.Items[0]));

                MarksListBox.Items.Add(DistributedMarksListBox.Items[0]);
                DistributedMarksListBox.Items.Remove(DistributedMarksListBox.Items[0]);                
            }

            distributedMarksCount--;
            changeNeedToDistributeLabel();
        }

        private bool hasDistrubutedMarks()
        {
            return distributedMarks[ChooseBlockComboBox.SelectedItem.ToString()].Count == needMarksOnEachBlock;
        }


        // при смене блока заполняем распределенные марки
        private void ChooseBlockComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DistributedMarksListBox.Items.Clear();
            fillDistributedMarksListBox();
            changeNeedToDistributeLabel();
        }

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

        // доступные названия блоков
        private void initBlockNames()
        {
            this.blockNames = new List<string>();
            this.blockNames.Add("А");
            this.blockNames.Add("Б");
            this.blockNames.Add("В");
            this.blockNames.Add("Г");
            this.blockNames.Add("Д");
        }

        // сколько марок еще нужно распределить на текущий выбранный блок
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

        private void selectChooseBlockComboBoxDefaultItem(ComboBox comboBox)
        {
            if (structuralBlocksCount == 0)
            {                
                return;
            }

            comboBox.SelectedIndex = 0;
        }

        // МЕТОДЫ ДЛЯ ОТРИСОВКИ ГРАФИКА
        private void LowerBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M-";
            if (SecondLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["lowerBoundMValues"],
                    SecondLevelMChart);
            }
        }

        private void MCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M";
            if (SecondLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["MValues"],
                    SecondLevelMChart);
            }
        }

        private void UpperBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "M+";
            if (SecondLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["upperBoundMValues"],
                    SecondLevelMChart);
            }
        }

        private void PredictedLowerBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М-";
            if (SecondLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["predictedLowerBoundMValues"],
                    SecondLevelMChart);
            }
        }

        private void PredictedMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М";
            if (SecondLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedMValues"],
                SecondLevelMChart);
            }
        }

        private void PredictedUpperBoundMCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз М+";
            if (SecondLevelMChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelMChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                    serieName,
                    calculatedAlphaAndMValues["Epoches"],
                    calculatedAlphaAndMValues["predictedUpperBoundMValues"],
                    SecondLevelMChart);
            }
        }

        private void LowerBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа-";
            if (SecondLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["lowerBoundAlphaValues"],
                SecondLevelAlphaChart);
            }
        }

        private void AlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа";
            if (SecondLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["alphaValues"],
                SecondLevelAlphaChart);
            }
        }

        private void UpperBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Альфа+";
            if (SecondLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["upperBoundAlphaValues"],
                SecondLevelAlphaChart);
            }
        }

        private void PredictedLowerBoundAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа-";
            if (SecondLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"],
                SecondLevelAlphaChart);
            }
        }

        private void PredictedAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа";
            if (SecondLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedAlphaValues"],
                SecondLevelAlphaChart);
            }
        }

        private void PredictedUpperAlphaCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Альфа+";
            if (SecondLevelAlphaChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelAlphaChart, serieName);
            }
            else
            {
                chartService.AddXYLine(
                serieName,
                calculatedAlphaAndMValues["Epoches"],
                calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"],
                SecondLevelAlphaChart);
            }
        }

        // МЕТОДЫ ДЛЯ ГРАФИКА ПРОГНОЗНОЙ ФУНКЦИИ
        private void LowerBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Функция отклика-";
            if (SecondLevelResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddMAndAlphaLine(
                serieName,
                calculatedAlphaAndMValues["lowerBoundMValues"],
                calculatedAlphaAndMValues["lowerBoundAlphaValues"],
                SecondLevelResponseFunctionChart);
            }
        }

        private void PredictedLowerBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Функция отклика-";
            if (SecondLevelResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddPredictedValue(
                serieName,
                calculatedAlphaAndMValues["predictedLowerBoundMValues"],
                calculatedAlphaAndMValues["predictedLowerBoundAlphaValues"],
                SecondLevelResponseFunctionChart);
            }
        }

        private void ResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Функция отклика";
            if (SecondLevelResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddMAndAlphaLine(
                serieName,
                calculatedAlphaAndMValues["MValues"],
                calculatedAlphaAndMValues["alphaValues"],
                SecondLevelResponseFunctionChart);
            }
        }

        private void PredictedResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Функция отклика";
            if (SecondLevelResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddPredictedValue(
                serieName,
                calculatedAlphaAndMValues["predictedMValues"],
                calculatedAlphaAndMValues["predictedAlphaValues"],
                SecondLevelResponseFunctionChart);
            }
        }

        private void UpperBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Функция отклика+";
            if (SecondLevelResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddMAndAlphaLine(
                serieName,
                calculatedAlphaAndMValues["upperBoundMValues"],
                calculatedAlphaAndMValues["upperBoundAlphaValues"],
                SecondLevelResponseFunctionChart);
            }
        }

        private void PredictedUpperBoundResponseFunction_CheckedChanged(object sender, EventArgs e)
        {
            string serieName = "Прогноз Функция отклика+";
            if (SecondLevelResponseFunctionChart.Series.IndexOf(serieName) != -1)
            {
                chartService.RemoveLine(SecondLevelResponseFunctionChart, serieName);
            }
            else
            {
                chartService.AddPredictedValue(
                serieName,
                calculatedAlphaAndMValues["predictedUpperBoundMValues"],
                calculatedAlphaAndMValues["predictedUpperBoundAlphaValues"],
                SecondLevelResponseFunctionChart);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ObjectPictureBox_Click(object sender, EventArgs e)
        {

        }

        private void ChooseBlockCalculationsAndGraphsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillCoordinatesAndEstimationTable();
            // clearGraph();
        }

        private void SecondLevelEstimationDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }  

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
