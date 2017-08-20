using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ICSharpCode.SharpZipLib.BZip2;

namespace CreatorReleases
{
    /// <summary>
    /// Файл релиза
    /// </summary>
    public class FileRelease
    {
        /// <summary>
        /// Это каталог
        /// </summary>
        public bool IsDirectory { get; private set; }
        /// <summary>
        /// Родительский каталог
        /// </summary>
        public FileRelease ParentDirr { get; set; }
        /// <summary>
        /// Исходный путь к файлу
        /// </summary>
        public string SourcePath { get; set; }
        /// <summary>
        /// Конечное размещение
        /// </summary>
        public string DestPath { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileNameOriginal { get; set; }
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// MD5
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// Размер файла в Кб
        /// </summary>
        public long SizeKb { get; set; }

        public string PathInstall { get; set; }

        public string PathDownload { get; set; }

        /// <summary>
        /// Дочерние элементы
        /// </summary>
        public List<FileRelease> ChildFiles { get; private set; }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public FileRelease(string _destPath)
        {
            this.IsDirectory = true;
            this.DestPath = _destPath;
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public FileRelease(bool _isDirr, string _source, FileRelease _parentDirr)
        {
            this.ChildFiles = new List<FileRelease>();
            this.IsDirectory = _isDirr;
            this.SourcePath = _source;
            this.ParentDirr = _parentDirr;

            this.DestPath = BildRealeas.GetCurrDirrForRealeas() + _source.Replace(AppSettings.Settings.CurrDirSourceFilesApp, "");
            this.FileNameOriginal = Path.GetFileName(_source);
            this.FileName = this.FileNameOriginal;

            if (!this.IsDirectory)
            {
                PathInstall = DestPath.Replace(BildRealeas.GetCurrDirrForRealeas(), "");

                if (Transliteration.IsKiril(this.FileNameOriginal))
                {
                    this.FileName = new Transliteration().GetTranse(this.FileName).Replace("-", "").Replace(" ", "");

                    PathDownload = DestPath.Replace(BildRealeas.GetCurrDirrForRealeas(), "").Replace(this.FileNameOriginal, "") + this.FileName;
                    this.DestPath = DestPath.Replace(this.FileNameOriginal, "") + this.FileName;
                }
                else
                    PathDownload = DestPath.Replace(BildRealeas.GetCurrDirrForRealeas(), "").Replace("-", "");

                this.MD5 = Utilites.GetFilesMD5(this.SourcePath);
                this.SizeKb = new FileInfo(_source).Length;
            }
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public FileRelease(bool _isDirr, string _source, string _dest, FileRelease _parentDirr)
        {
            this.ChildFiles = new List<FileRelease>();
            this.IsDirectory = _isDirr;
            this.SourcePath = _source;
            this.ParentDirr = _parentDirr;
            this.DestPath = _dest;
            this.FileNameOriginal = Path.GetFileName(_source);
            this.FileName = this.FileNameOriginal;

            if (!this.IsDirectory)
            {
                PathInstall = DestPath.Replace(BildRealeas.GetCurrDirrForRealeas(), "");

                if (Transliteration.IsKiril(this.FileNameOriginal))
                {
                    this.FileName = new Transliteration().GetTranse(this.FileName).Replace("-", "").Replace(" ", "");

                    PathDownload = DestPath.Replace(BildRealeas.GetCurrDirrForRealeas(), "").Replace(this.FileNameOriginal, "") + this.FileName;
                    this.DestPath = DestPath.Replace(this.FileNameOriginal, "") + this.FileName;
                }
                else
                    PathDownload = DestPath.Replace(BildRealeas.GetCurrDirrForRealeas(), "").Replace("-", "");

                this.MD5 = Utilites.GetFilesMD5(this.SourcePath);
                this.SizeKb = new FileInfo(_source).Length / 1024;
            }
        }


        public void CopyFileFromSource(bool compressFiles)
        {
            if (compressFiles)
            {
                FileInfo fileToBeZipped = new FileInfo(SourcePath);
                FileInfo zipFileName = new FileInfo(string.Concat(DestPath, ".bz2"));
                using (FileStream fileToBeZippedAsStream = fileToBeZipped.OpenRead())
                {
                    using (FileStream zipTargetAsStream = zipFileName.Create())
                    {
                        BZip2.Compress(fileToBeZippedAsStream, zipTargetAsStream, 4096);
                    }
                }
                DestPath = DestPath + ".bz2";
                FileNameOriginal = FileNameOriginal + ".bz2";
                FileName = FileName + ".bz2";
                PathInstall = PathInstall + ".bz2";
                PathDownload = PathDownload + ".bz2";
            }
            else
            {   //Копируем исходный файл
                File.Copy(SourcePath, DestPath, true);
            }

            MD5 = Utilites.GetFilesMD5(DestPath);
            this.SizeKb = new FileInfo(DestPath).Length;
        }
    }
}
