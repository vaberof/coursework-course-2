using CourseWork.Handlers.Application.ToolStrip.Project;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    internal class TechnogenicObjectService : ITechnogenicObjectService
    {
        private ITechogenicObjectStorage technogenicObjectStorage;
        private DataTable dataTable;
        public TechnogenicObjectService(ITechogenicObjectStorage technogenicObjectStorage)
        {
            this.technogenicObjectStorage = technogenicObjectStorage;
            this.dataTable = new DataTable();
        }

        public DataTable GetDataTable()
        {
            return this.dataTable;
        }

        public void FillMainCoordinatesTable(DataGridView mainCoordinatesTable)
        {
            this.dataTable = new DataTable();

            technogenicObjectStorage.FillDataTable(dataTable);

            mainCoordinatesTable.Columns.Clear();
            mainCoordinatesTable.Rows.Clear();

            for (int column = 0; column < dataTable.Columns.Count; column++)
            {
                string columnName = dataTable.Columns[column].ColumnName;

                mainCoordinatesTable.Columns.Add(columnName, columnName);
                mainCoordinatesTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                mainCoordinatesTable.Rows.Add(dataTable.Rows[row].ItemArray);
            }
        }

        public void AddEpoch(DataGridView mainCoordinatesTable, ref int currentEpochCount)
        {
            currentEpochCount++;

            double delta = 0;
            double averageDelta = 0;
            Random random = new Random();

            int newRowIndex = mainCoordinatesTable.RowCount - 1;

            int nextEpochValue = technogenicObjectStorage.GetNextEpochValue();

            mainCoordinatesTable.Rows[newRowIndex].Cells[0].Value = nextEpochValue;

            technogenicObjectStorage.AddRow(nextEpochValue);
            technogenicObjectStorage.UpdateNextEpochValue(nextEpochValue + 1);

            for (int i = 1; i < mainCoordinatesTable.Columns.Count; i++)
            {
                for (int j = 0; j < mainCoordinatesTable.Rows.Count - 1; j++)
                {
                    if (Convert.ToDouble(mainCoordinatesTable.Rows[j + 1].Cells[i].Value) != 0)
                    {
                        delta = Math.Abs(Convert.ToDouble(mainCoordinatesTable.Rows[j].Cells[i].Value) - Convert.ToDouble(mainCoordinatesTable.Rows[j + 1].Cells[i].Value));
                    }

                    averageDelta += delta;
                    delta = 0;
                }

                averageDelta /= mainCoordinatesTable.Rows.Count;

                double newCellValue = random.NextDouble() * (averageDelta - (-averageDelta)) + averageDelta;

                mainCoordinatesTable.Rows[newRowIndex].Cells[i].Value = 
                    Math.Round(newCellValue + Convert.ToDouble(mainCoordinatesTable.Rows[newRowIndex - 1].Cells[i].Value), 4);

                technogenicObjectStorage.AddValuesInRow(i, nextEpochValue, Convert.ToDouble(mainCoordinatesTable.Rows[newRowIndex].Cells[i].Value));

                averageDelta = 0;
            }

            mainCoordinatesTable.Rows.Add();      

            this.dataTable = new DataTable();
            technogenicObjectStorage.FillDataTable(dataTable);
        }

        public void DeleteEpochs(DataGridView mainCoordinatesTable, List<int> selectedRowsIndexes, ref int currentEpochCount)
        {
            if (currentEpochCount - selectedRowsIndexes.Count < 2)
            {
                MessageBox.Show("Нельзя удалить все эпохи, должны остаться минимум 2");
                return;
            }

            List<int> epochValues = new List<int>();

            for (int i = 0; i < selectedRowsIndexes.Count; i++)
            {
                // получаем значение эпохи в выбранной строке
                string epoch = mainCoordinatesTable.Rows[selectedRowsIndexes[i]].Cells[0].Value.ToString();
                epochValues.Add(Convert.ToInt32(epoch));
            }
            technogenicObjectStorage.DeleteRowFromTable(epochValues);

            currentEpochCount -= selectedRowsIndexes.Count;
        }

        public void CreateTechnogenicObjectValuesTable(double alpha, double epsilon, string pngFilePath, int nextEpochValue)
        {
            byte[] convertedImage = convertImageToBytes(pngFilePath);

            technogenicObjectStorage.CreateTechnogenicObjectValuesTable(alpha, epsilon, convertedImage, nextEpochValue);
        }

        public void GetTechnogenicObjectValues(ref double alpha, ref double epsilon, ref Image image, ref int nextEpochCount)
        {
            byte[] imageBytes = null;

            technogenicObjectStorage.GetTechnogenicObjectValues(ref alpha, ref epsilon, ref imageBytes, ref nextEpochCount);

            image = convertBytesToImage(imageBytes);
        }

        public void UpdateAlphaAndEpsilon(double alpha, double epsilon)
        {
            technogenicObjectStorage.UpdateAlphaAndEpsilon(alpha, epsilon);
        }

        public void GetTechnogenicObjectImage()
        {
            // byte[] image = technogenicObjectStorage.GetTechnogenicObjectImage();
            // return convertBytesToImage(image);
        }

        private byte[] convertImageToBytes(string pngFilePath)
        {
            Image img = Image.FromFile(pngFilePath);
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bytes = ms.ToArray();
            }
            return bytes;
        }
        private Image convertBytesToImage(byte[] imageBytes)
        {
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = new Bitmap(ms);
            return image;
        }
    }
}
