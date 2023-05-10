using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface ITechogenicObjectStorage
    {
        void OpenConnection();
        void CloseConnection();
        void CreateEpochCountColumn();
        void UpdateEpochCount(int epochCount);
        int GetEpochCount();
        void FillDataTable(DataTable table);
        void AddRow(double value);
        void AddValuesInRow(int column, int row, double value);
        void DeleteRowFromTable(List<int> selectedRowsIndexes);
    }
}
