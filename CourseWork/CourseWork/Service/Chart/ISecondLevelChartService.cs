using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWork.Service.Chart
{
    public interface ISecondLevelChartService
    {
        void AddXYLine(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart);
        void AddMAndAlphaLine(string serieName, List<double> MValues, List<double> alphaValues, System.Windows.Forms.DataVisualization.Charting.Chart chart);
        void AddPredictedValue(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart);
        void RemoveLine(System.Windows.Forms.DataVisualization.Charting.Chart chart, string serieName); //TODO: поменять местами параметры
        void ClearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart);
    }
}
