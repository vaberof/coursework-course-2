using CourseWork.Infra.Storage.Sqlite;
using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork.Handlers.Application.ToolStrip.Project
{
    public class ProjectToolHandler
    {
        public string SqliteFilePath;
        public string PngFilePath;
        public string TxtFilePath;

        public void OpenProject()
        {
            string pathToArchive = "";
            OpenFileDialog openArchive = new OpenFileDialog();

            openArchive.Title = "Выберите архив с проектом";
            openArchive.Filter = "Archive Files|*.rar;*.zip";
            openArchive.Multiselect = false;

            if (openArchive.ShowDialog() != DialogResult.OK)
            {
                throw new Exception("Нужно выбрать архив с проектом");
            }

            pathToArchive = Path.GetFullPath(openArchive.FileName);            

            if (pathToArchive == "")
            {
                throw new Exception("Нужно указать путь до архива");
            }

            string pathToExtractArchive = Path.GetDirectoryName(pathToArchive);

            using (var archive = ArchiveFactory.Open(pathToArchive))
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

            readExtractedArchiveDirectory(pathToExtractArchive + "\\" + Path.GetFileNameWithoutExtension(openArchive.FileName));
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
    }

}
