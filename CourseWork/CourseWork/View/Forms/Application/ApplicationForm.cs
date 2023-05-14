using CourseWork.Domain.TechnogenicObject;
using CourseWork.Handlers.Application.ToolStrip.Project;
using CourseWork.Infra.Storage.Sqlite;
using CourseWork.Infra.Storage.Sqlite.TechnogenicObject;
using CourseWork.Service.Chart.FirstLevel;
using CourseWork.Service.Chart.Impl;
using CourseWork.View.Forms.Application;
using CourseWork.View.Forms.Decomposition;
using CourseWork.View.Forms.Decomposition.FourthLevel;
using CourseWork.View.Forms.Decomposition.SecondLevel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class ApplicationForm : Form
    {
        // handlers
        private ProjectToolHandler projectToolHandler;

        // db object
        private Sqlite db;
        
        // storage
        private ITechogenicObjectStorage technogenicObjectStorage;

        // domain object
        private TechnogenicObject technogenicObject;

        // domain service
        private ITechnogenicObjectService technogenicObjectService;

        // application service
        DecompositionService decompositionService;
        ChartService chartService;

        private int epochCount;

        public ApplicationForm(ProjectToolHandler projectToolHandler, ICalculationsService calculationsService)
        {
            this.chartService = new ChartService();
            this.projectToolHandler = projectToolHandler;
            this.decompositionService = new DecompositionService(calculationsService);
            InitializeComponent();
        }
        
        private void ApplicationForm_Load(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
        
        private void OpenProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (technogenicObjectStorage != null)
                {
                    technogenicObjectStorage.CloseConnection();                    
                }

                if (ObjectPictureBox.Image != null)
                {
                    ObjectPictureBox.Image.Dispose();
                    ObjectPictureBox.Image = null;
                }

                // initialize all necessary objects
                technogenicObject = projectToolHandler.OpenProject();

                db = new Sqlite(projectToolHandler.SqliteFilePath);
                technogenicObjectStorage = new TechnogenicObjectStorage(db);
                technogenicObjectService = new TechnogenicObjectService(technogenicObjectStorage);


                // technogenicObjectStorage.CreateValuesTable(
                // alpha, technogenicObject.MeasurementAccuracy, technogenicObject.StructuralBlocksCount, convertImageToBytes(
                // projectToolHandler.PngFilePath), technogenicObject.StructuralBlocksCount);


                technogenicObjectStorage.CreateEpochCountColumn();

                technogenicObjectService.FillDataGridTable(dataGridViewTable);

                // после первоначальной прогрузки таблицы с новой колонкой Количество_эпох
                // устанавливаю значение в колонке в бд == количество строчек в дата грид - 1
                // и после каждого удаления 
                
                // надо проверять если уже 0 строчек, то надо просто epochCount 
                // скорее всего нельзя удалить все эпохи..

                epochCount = Convert.ToInt32(
                    dataGridViewTable.Rows[dataGridViewTable.RowCount - 2].Cells[0].Value.ToString()
                    );
                
                //MessageBox.Show("newEpochCount : " + epochCount);

                technogenicObjectStorage.UpdateEpochCount(epochCount);

                showObjectPicture(projectToolHandler.PngFilePath);

                resizeDataGridTable();

                AlphaTextBox.Text = "0,9";
                EpsilonTextBox.Text = Convert.ToString(technogenicObject.MeasurementAccuracy);

                setObjectDescription();               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void addEpochButton_Click(object sender, EventArgs e)
        {
            technogenicObjectService.AddEpoch(dataGridViewTable);
            technogenicObjectService.FillDataGridTable(dataGridViewTable);
            resizeDataGridTable();
        }

        //TODO: добавить перерисовку датагрид
        private void deleteEpochButton_Click(object sender, EventArgs e)
        {

            List<int> selectedRowIndexes = new List<int>();

            foreach (DataGridViewRow row in dataGridViewTable.SelectedRows)
            {
                selectedRowIndexes.Add(row.Index);

                //dataGridViewTable.Rows.Remove(row);
            }

            technogenicObjectService.DeleteEpoches(dataGridViewTable, selectedRowIndexes);                
            
            for (int i = 0; i < selectedRowIndexes.Count; i++)
            {
                dataGridViewTable.Rows.RemoveAt(selectedRowIndexes[i]);
            }

            technogenicObjectService.FillDataGridTable(dataGridViewTable);
            resizeDataGridTable();
        }

        private void resizeDataGridTable()
        {
            for (int column = 0; column < dataGridViewTable.ColumnCount; column++)
            {
                dataGridViewTable.Columns[column].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void showObjectPicture(string pngFilePath)
        {
            ObjectPictureBox.Load(pngFilePath);
            ObjectPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void ObjectShowDescriptionToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            ObjectDesciptionLabel.Visible = true;
        }

        private void ObjectShowDescriptionToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            ObjectDesciptionLabel.Visible = false;
        }

        private void ObjectDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ObjectDesciptionLabel.Text, "Описание объекта");
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // открывается форма с выбором типа архива
            using (var archiveTypesForm = new ArchiveTypesForm())
            {
                var result = archiveTypesForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string archiveType = archiveTypesForm.ArchiveType;

                    technogenicObjectStorage.CloseConnection();
                    ObjectPictureBox.Image.Dispose();
                    ObjectPictureBox.Image = null;

                    projectToolHandler.SaveArchive(archiveType);

                    technogenicObjectStorage.OpenConnection();
                    //technogenicObjectStorage = new TechnogenicObjectStorage(sqlite);
                    //technogenicObjectService = new TechnogenicObjectService(technogenicObjectStorage);

                    // TODO: вынести в функцию
                    technogenicObjectService.FillDataGridTable(dataGridViewTable);
                    showObjectPicture(projectToolHandler.PngFilePath);

                    resizeDataGridTable();

                    AlphaTextBox.Text = "0,9";
                    EpsilonTextBox.Text = Convert.ToString(technogenicObject.MeasurementAccuracy);

                    epochCount = dataGridViewTable.RowCount - 1;
                    MessageBox.Show("epochCount: " + epochCount);

                    setObjectDescription();
                }
            }

        }

        // private void setupInitialObjectValues() {...}

        private void setObjectDescription()
        {
            string objectDescpiption = string.Format(
                        "Вариант задания: {0}\n" +
                        "Тип техногенного объекта: {1}\n" +
                        "Количество структурных блоков: {2}\n" +
                        "Количество геодезических марок, закрепленных в теле объекта: {3}\n" +
                        "Количество эпох измерений: {4}\n" +
                        "Точность измерений: {5}м",
                        technogenicObject.TaskOption,
                        technogenicObject.Type,
                        technogenicObject.StructuralBlocksCount,
                        technogenicObject.GeodeticMarksCount,
                        technogenicObject.MeasurementEpochsCount,
                        technogenicObject.MeasurementAccuracy);

            ObjectDesciptionLabel.Text = objectDescpiption;
        }

        private void dataGridViewTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
               

        private void toolStripDropDownButton1_Click_1(object sender, EventArgs e)
        {

        }

        

        private void ApplicationForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void EpsilonLabel_Click(object sender, EventArgs e)
        {

        }        

        private void AlphaLabel_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void TopMenuToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FirstLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FirstLevelForm firstLevelForm = new FirstLevelForm(
                technogenicObject.MeasurementAccuracy,
                Convert.ToDouble(AlphaTextBox.Text),
                decompositionService,
                chartService,
                dataGridViewTable, 
                technogenicObjectService.GetDataTable());

            firstLevelForm.Show();
        }

        private void SecondLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SecondLevelForm secondLevelForm = new SecondLevelForm(
                decompositionService,
                chartService,
                technogenicObject.StructuralBlocksCount,
                technogenicObject.GeodeticMarksCount,
                technogenicObject.MeasurementAccuracy,
                Convert.ToDouble(AlphaTextBox.Text),
                dataGridViewTable);

            secondLevelForm.Show();
        }

        private void ObjectToolStripDropDownButton_Click(object sender, EventArgs e)
        {

        }

        private void FourthLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FourthLevelForm fourthLevelForm = new FourthLevelForm(
                decompositionService,
                chartService,
                technogenicObject.GeodeticMarksCount,
                Convert.ToDouble(AlphaTextBox.Text),
                dataGridViewTable);

            fourthLevelForm.Show();
        }

    }
}
