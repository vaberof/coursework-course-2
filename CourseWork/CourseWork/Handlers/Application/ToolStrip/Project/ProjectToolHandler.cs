using CourseWork.Domain.TechnogenicObject;
using CourseWork.Infra.Storage.Sqlite;
using Microsoft.WindowsAPICodePack.Shell.Interop;
using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace CourseWork.Handlers.Application.ToolStrip.Project
{
    public class ProjectToolHandler
    {
        public string SqliteFilePath { get; private set; }
        public string PngFilePath { get; private set; }
        public string TxtFilePath { get; private set; }

        // путь к архиву 
        private string path;

        // Полный путь к директории, где находится архив с названием "Вариант n".
        // Пример: C:\\user\\directory 
        private string pathToArchive;
        // название архива без расширения.
        // Пример: Вариант 2
        private string archiveName;
        public TechnogenicObject OpenProject()
        {
            OpenFileDialog openProject = new OpenFileDialog();

            openProject.Title = "Выберите архив с проектом";
            openProject.Filter = "Archive Files|*.rar;*.zip";
            openProject.Multiselect = false;

            if (openProject.ShowDialog() != DialogResult.OK)
            {
                throw new Exception("Нужно выбрать архив с проектом");
            }

            string pathWithArchiveName = Path.GetFullPath(openProject.FileName);            

            if (pathWithArchiveName == "")
            {
                throw new Exception("Нужно указать путь до архива");
            }

            archiveName = Path.GetFileNameWithoutExtension(pathWithArchiveName);

            string pathToExtractArchive = Path.GetDirectoryName(pathWithArchiveName);

            pathToArchive = pathToExtractArchive;

            //MessageBox.Show("path to archive = " + pathToArchive);
            //MessageBox.Show("path to extract = " + pathToExtractArchive);

            extractArchive(pathWithArchiveName, pathToExtractArchive);

            return getObjectDescriptionFromTxtFile(TxtFilePath);
        }

        public void SaveZipArchive()
        {
            saveZipArchive(pathToArchive, archiveName);
        }       

        private void extractArchive(string pathWithArchiveName, string pathToExtractArchive)
        {
            using (var archive = ArchiveFactory.Open(pathWithArchiveName))
            {
                foreach (var entry in archive.Entries.Where(entry => !entry.IsDirectory))
                {
                    entry.WriteToDirectory(pathToExtractArchive, new ExtractionOptions()
                    {
                        ExtractFullPath = true,
                        Overwrite = true
                    });
                }
            }

            readExtractedArchiveDirectory(pathToExtractArchive + "\\" + archiveName);
        }

        private void readExtractedArchiveDirectory(string path)
        {           
            int dbFilesCount = Directory.GetFiles(path, "*.sqlite").Length;
            int pngFilesCount = Directory.GetFiles(path, "*.png").Length;
            int txtFilesCount = Directory.GetFiles(path, "*.txt").Length;

            SqliteFilePath = processDbFiles(dbFilesCount, path);
            PngFilePath =  processPngFiles(pngFilesCount, path);
            TxtFilePath =  processTxtFiles(txtFilesCount, path);
        }

        private void saveZipArchive(string path, string name)
        {
            try
            {
                using (var archive = ZipArchive.Create())
                {
                    archive.AddAllFromDirectory(pathToArchive + "\\" + archiveName);
                    archive.SaveTo(pathToArchive + "\\" + archiveName + ".zip", CompressionType.Deflate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить zip архив: " + ex.Message);
            }
        }

        private string processDbFiles(int count, string path)
        {
            switch (count)
            {
                case 1:
                    return Path.GetFullPath(Directory.GetFiles(path, "*.sqlite")[0]);
                default:
                    return selectFilePath(path);
            }
        }

        private string processPngFiles(int count, string path)
        {
            switch (count)
            {
                case 1:
                    return Path.GetFullPath(Directory.GetFiles(path, "*.png")[0]);
                default:
                    return selectFilePath(path);
            }
        }

        private string processTxtFiles(int count, string path)
        {
            switch (count)
            {
                case 1:
                    return Path.GetFullPath(Directory.GetFiles(path, "*.txt")[0]);
                default:
                    return selectFilePath(path);
            }
        }

        private TechnogenicObject getObjectDescriptionFromTxtFile(string filePath)
        {
            try
            {
                List<string> lines = File.ReadAllLines(filePath, Encoding.Unicode).ToList();

                int taskOption = 0;
                string technogeniceObjectType = "";
                int structuralBlocksCount = 0;
                int geodeticMarksCount = 0;
                int measurementEpochsCount = 0;
                double measurementAccuracy = 0;

                foreach (string line in lines)
                {
                    parseTaskOption(line, ref taskOption);

                    parseTechnogenicObjectType(line, ref technogeniceObjectType);

                    parseStructuralBlocksCount(line, ref structuralBlocksCount);

                    parseGeodeticMarksCount(line, ref geodeticMarksCount);

                    parseMeasurementEpochsCount(line, ref measurementEpochsCount);

                    parseMeasurementAccuracy(line, ref measurementAccuracy);
                }

                return new TechnogenicObject(taskOption, technogeniceObjectType, structuralBlocksCount, geodeticMarksCount, measurementEpochsCount, measurementAccuracy);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка во время чтения текстового файла: " + ex.Message);
            }

            return null;
        }               

        private string selectFilePath(string initialPath)
        {
            OpenFileDialog chooseFile = new OpenFileDialog();
            chooseFile.InitialDirectory = initialPath;
            chooseFile.Multiselect = false;

            if (chooseFile.ShowDialog() != DialogResult.OK)
            {
                throw new Exception("Нужно выбрать файл");
            }

            return Path.GetFullPath(chooseFile.FileName);
        }

        private void parseTaskOption(string line, ref int taskOption)
        {
            if (line.StartsWith("Вариант"))
            {
                List<string> splittedLine = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //MessageBox.Show(splittedLine[1]);
                taskOption = Convert.ToInt32(splittedLine[1]);
            }
        }

        private void parseTechnogenicObjectType(string line, ref string technogeniceObjectType)
        {
            if (line.StartsWith("Тип"))
            {
                List<string> splittedLine = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //MessageBox.Show(splittedLine[1]);
                technogeniceObjectType = splittedLine[1];
            }
        }

        private void parseStructuralBlocksCount(string line, ref int structuralBlocksCount)
        {
            if (line.StartsWith("Количество структурных"))
            {
                List<string> splittedLine = line.Split(' ').ToList();
                //MessageBox.Show(splittedLine[3]);
                structuralBlocksCount = Convert.ToInt32(splittedLine[3]);
            }
        }

        private void parseGeodeticMarksCount(string line, ref int geodeticMarksCount)
        {
            if (line.StartsWith("Количество геодезических марок"))
            {
                List<string> splittedLine = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                // MessageBox.Show(splittedLine[1]);
                geodeticMarksCount = Convert.ToInt32(splittedLine[1]);
            }
        }

        private void parseMeasurementEpochsCount(string line, ref int measurementEpochsCount)
        {
            if (line.StartsWith("Количество эпох"))
            {
                List<string> splittedLine = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //MessageBox.Show(splittedLine[1].Split(' ')[1]);
                measurementEpochsCount = Convert.ToInt32(splittedLine[1].Split(' ')[1]);
            }
        }

        private void parseMeasurementAccuracy(string line, ref double measurementAccuracy)
        {
            if (line.StartsWith("Точность"))
            {
                List<string> splittedLine = line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //MessageBox.Show(splittedLine[1].Remove(splittedLine[1].Length - 1));
                measurementAccuracy = Convert.ToDouble(splittedLine[1].Remove(splittedLine[1].Length - 1));
            }
        }
    }

}
