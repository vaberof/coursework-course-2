using CourseWork.Service;
using CourseWork.Service.Chart;
using CourseWork.Service.Decomposition;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWork.View.Forms.Decomposition.FourthLevel
{
    public partial class FourthLevelForm : Form
    {
        IFourthLevelDecompositionService decompositionService;
        IFourthLevelChartService chartService;

        private int marksCount;
        private double alpha;

        private DataGridView mainCoordinatesTable;        

        public FourthLevelForm(
            IFourthLevelDecompositionService decompositionService, 
            IFourthLevelChartService chartService, 
            int marksCount,
            double aplha,
            DataGridView mainCoordinatesTable)
        {            
            this.decompositionService = decompositionService;
            this.chartService = chartService;
            this.marksCount = marksCount;
            this.alpha = aplha;
            this.mainCoordinatesTable = mainCoordinatesTable;

            InitializeComponent();
        }

        private void FourthLevelForm_Load(object sender, EventArgs e)
        {
            decompositionService.FillMarksInCheckedListBox(marksCount, MarksCheckedListBox);
        }

        private void MarksCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (MarksCheckedListBox.SelectedItem != null)
            {
                string serieName = MarksCheckedListBox.SelectedItem.ToString();

                if (ResponseFunctionChart.Series.IndexOf(serieName) != -1)
                {
                    removeXYLine(serieName);
                }
                else
                {
                    addXYLine(MarksCheckedListBox.SelectedItem.ToString());
                }
            }
        }

        private void ClearCheckedListBoxAndChartButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MarksCheckedListBox.Items.Count; i++)
            {
                MarksCheckedListBox.SetItemChecked(i, false);
            }

            chartService.ClearChart(ResponseFunctionChart);
        }

        private void addXYLine(string mark)
        {
            List<double> epoches = getEpoches();
            List<double> marksValues = getMarksValues(mark);

            chartService.AddXYLine(mark, epoches, marksValues, ResponseFunctionChart);

            List<double> predictedMarksValues = getPredictedMarksValues(marksValues);

            string predictedMarkSerie = "Прогноз " + mark;

            epoches.Add(epoches.Last() + 1);
            chartService.AddPredictedValue(predictedMarkSerie, epoches, predictedMarksValues, ResponseFunctionChart);
        }
        
        private void removeXYLine(string mark)
        {
            string predictedMarkSerie = "Прогноз " + mark;
            chartService.RemoveLine(ResponseFunctionChart, mark);
            chartService.RemoveLine(ResponseFunctionChart, predictedMarkSerie);
        }

        private List<double> getMarksValues(string mark)
        {
            List<double> marksValues = new List<double>();

            for (int i = 0; i < mainCoordinatesTable.Rows.Count - 1; i++)
            {
                marksValues.Add(Convert.ToDouble(mainCoordinatesTable.Rows[i].Cells[mark].Value));
            }

            return marksValues;
        }

        private List<double> getPredictedMarksValues(List<double> marksValues)
        {
            return decompositionService.getPredictedValues(marksValues, alpha);
        }

        private List<double> getEpoches()
        {
            List<double> epoches = new List<double>();

            for (int i = 0; i < mainCoordinatesTable.Rows.Count - 1; i++)
            {
                epoches.Add(Convert.ToInt32(mainCoordinatesTable.Rows[i].Cells[0].Value));
            }

            return epoches;
        }  
    }
}
