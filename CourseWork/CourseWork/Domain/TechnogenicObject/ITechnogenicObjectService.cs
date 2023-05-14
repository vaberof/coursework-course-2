using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    public interface ITechnogenicObjectService
    {
        void FillMainCoordinatesTable(DataGridView mainCoordinatesTable);
        void AddEpoch(DataGridView mainCoordinatesTable, ref int currentEpochCount);
        void DeleteEpochs(DataGridView mainCoordinatesTable, List<int> selectedRowsIndexes, ref int currentEpochCount); 
        DataTable GetDataTable();
    }
}
