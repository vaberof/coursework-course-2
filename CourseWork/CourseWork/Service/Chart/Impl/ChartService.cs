using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace CourseWork.Service.Chart.FirstLevel
{
    internal class ChartService : IFirstLevelChartService, ISecondLevelChartService, IFourthLevelChartService
    {
        public void AddXYLine(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            chart.ChartAreas[0].AxisX.Title = "Эпоха";
            chart.ChartAreas[0].AxisY.Title = "Значение";

            chart.Series.Add(serieName);

            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;

            chart.Series[serieName].MarkerStyle = MarkerStyle.Circle;
            chart.Series[serieName].MarkerSize = 6;
            chart.Series[serieName].MarkerColor = chart.Series[serieName].Color;
            chart.Series[serieName].ChartType = SeriesChartType.Line;
            chart.Series[serieName].ToolTip = "X = #VALX, Y = #VALY";

            for (int i = 0; i < yValues.Count; i++)
            {
                chart.Series[serieName].Points.AddXY(xValues[i], yValues[i]);
                chart.Series[serieName].Points[i].Label = i.ToString();
            }
        }

        public void AddMAndAlphaLine(string serieName, List<double> MValues, List<double> alphaValues, System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            chart.ChartAreas[0].AxisX.Title = "М";
            chart.ChartAreas[0].AxisY.Title = "Альфа";
            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart.Series.Add(serieName);
            chart.Series[serieName].MarkerStyle = MarkerStyle.Circle; // стиль маркера точки данных
            chart.Series[serieName].MarkerSize = 10;
            chart.Series[serieName].MarkerColor = chart.Series[serieName].Color;
            chart.Series[serieName].ChartType = SeriesChartType.Line;
            chart.Series[serieName].ToolTip = "X = #VALX, Y = #VALY";

            for (int i = 0; i < MValues.Count; i++)
            {
                chart.Series[serieName].Points.AddXY(MValues[i], alphaValues[i]);
                chart.Series[serieName].Points[i].Label = i.ToString();
            }
        }

        public void AddPredictedValue(string serieName, List<double> xValues, List<double> yValues, System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            chart.Series.Add(serieName);
            chart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart.Series[serieName].ChartType = SeriesChartType.Point;
            chart.Series[serieName].MarkerStyle = MarkerStyle.Circle;
            chart.Series[serieName].MarkerSize = 10;
            chart.Series[serieName].Points.AddXY(xValues.Last(), yValues.Last());
            chart.Series[serieName].Points.Last().Label = (xValues.Count - 1).ToString();
            chart.Series[serieName].ToolTip = "X = #VALX, Y = #VALY";
        }

        public void RemoveLine(System.Windows.Forms.DataVisualization.Charting.Chart chart, string serieName)
        {
            chart.Series[serieName].Points.Clear();
            chart.Series.Remove(chart.Series[serieName]);
        }

        public void ClearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }

            chart.Series.Clear();
        }
    }
}
