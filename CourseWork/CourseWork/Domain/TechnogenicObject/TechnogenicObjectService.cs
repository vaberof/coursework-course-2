using CourseWork.Infra.Storage.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Domain.TechnogenicObject
{
    internal class TechnogenicObjectService : ITechnogenicObjectService
    {
        private ITechogenicObjectStorage technogenicObjectStorage;
        public TechnogenicObjectService(ITechogenicObjectStorage technogenicObjectStorage)
        {
            this.technogenicObjectStorage = technogenicObjectStorage;
        }

        public void FillDataGridTable(DataGridView dataGridTable)
        {
            technogenicObjectStorage.FillDataGridTable(dataGridTable);
        }

        public void DeleteEpoches(List<int> selectedRowsIndexes)
        {
            technogenicObjectStorage.DeleteRowFromTable(selectedRowsIndexes);
        }
    }
}
