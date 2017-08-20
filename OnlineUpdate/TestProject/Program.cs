using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineUpdate;
using System.IO;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string userWorkDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\TestProject";
            string currDir = Directory.GetCurrentDirectory();
            string app = System.Reflection.Assembly.GetEntryAssembly().Location;

            if (!Directory.Exists(userWorkDir))
                Directory.CreateDirectory(userWorkDir);

            UpdateOptiones op = new UpdateOptiones("http://hurraycard.com/cashierwin/TestUpdate2", userWorkDir, currDir, app, true);
            UpdateManager.InitUpdateManager(op);

            UpdateManager upManager = new UpdateManager();
            upManager.ErrorEvent += UpManager_ErrorEvent;

            upManager.UpdateChecker.BeginCheckUpdateEvent += UpdateChecker_BeginCheckUpdateEvent;
            upManager.UpdateChecker.EndCheckCurrentFileEvent += UpdateChecker_EndCheckCurrentFileEvent;
            upManager.UpdateChecker.EndCheckUpdateEvent += UpdateChecker_EndCheckUpdateEvent;

            upManager.BeginDownloadFilesEvent += UpManager_BeginDownloadFilesEvent;
            upManager.BeginDownloadCurrentFileEvent += UpManager_BeginDownloadCurrentFileEvent;
            upManager.EndDownloadCurrentFileEvent += UpManager_EndDownloadCurrentFileEvent;
            upManager.EndDownloadFilesEvent += UpManager_EndDownloadFilesEvent;

            var check = upManager.CheckUpdates();

            if (check != null)
            {
                Console.WriteLine("New ver: " + check.UpgradeToVersion);
                Console.WriteLine("Publication date: " + check.PublicationDate);
                Console.WriteLine("Update descriptiones: " + check.UpdateDescriptiones);
                Console.WriteLine("..........");

                upManager.BeginUpdate();
            }

            Console.ReadKey();


        }

        private static void UpManager_BeginDownloadFilesEvent(object sender, OnlineUpdate.UpdaterEventArgs.BeginDownloadFilesEventArgs e)
        {
            Console.WriteLine("Begin download files: " + e.KolFiles);
        }

        private static void UpManager_BeginDownloadCurrentFileEvent(object sender, OnlineUpdate.MultithreadedDownload.StartDownloadEventArgs e)
        {
            Console.WriteLine("Downloading file: " + e.NameFile);
        }

        private static void UpManager_EndDownloadCurrentFileEvent(object sender, OnlineUpdate.UpdaterEventArgs.LoadedFileUpdateEventArgs e)
        {
            Console.WriteLine("Downloaded files: " + e.File.NameFileInstall);
        }

        private static void UpManager_EndDownloadFilesEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndLoadUpdateFilesEventArgs e)
        {
            Console.WriteLine("End downloade files");

           // Console.ReadKey();
        }



        private static void UpdateChecker_BeginCheckUpdateEvent(object sender, OnlineUpdate.UpdaterEventArgs.BeginCheckUpdateEventArgs e)
        {
            Console.WriteLine("Begin check update");
        }

        private static void UpdateChecker_EndCheckCurrentFileEvent(object sender, EventArgs e)
        {
        
        }

        private static void UpdateChecker_EndCheckUpdateEvent(object sender, OnlineUpdate.UpdaterEventArgs.EndChekUpdateEventArgs e)
        {
            Console.WriteLine("End check update: " + e.Description.CountFiles + " files");
        }



        private static void UpManager_ErrorEvent(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Error: " + e.GetException().Message);
        }
    }
}
