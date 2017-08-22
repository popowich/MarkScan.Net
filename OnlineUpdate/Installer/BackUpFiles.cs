using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Installer
{
    class BackUManager
    {
        private string _rootDirBackUp;
        private string _rootDirSource;


        internal BackUManager(string rootDirBackUp, string rootDirSource)
        {
            _rootDirBackUp = rootDirBackUp;
            _rootDirSource = rootDirSource;

            if (!Directory.Exists(rootDirBackUp))
                Directory.CreateDirectory(rootDirBackUp);
        }


        internal void BackUpFiles(string backUpVersion, List<FileInstall> filesFileInstalls)
        {
            string rootDirBackUp = _rootDirBackUp + "\\" + backUpVersion;

            if (!Directory.Exists(rootDirBackUp))
                Directory.CreateDirectory(rootDirBackUp);

            foreach (var file in filesFileInstalls)
            {
                if(!File.Exists(file.DestFile))
                    continue;

                string pathDest = file.DestFile.Replace(_rootDirSource, rootDirBackUp);

                file.CopyFile(pathDest);
            }
        }

        internal void RecoveryFiles(string recoveryVersion)
        {
            string rootDirBackUp = _rootDirBackUp + "\\" + recoveryVersion;

            if (!Directory.Exists(rootDirBackUp))
                throw new Exception($"Каталог восстановления версии {recoveryVersion} приложения не обнаружен");

            var files = Directory.GetFiles(rootDirBackUp);

            foreach (var file in files)
            {
                string pathDest = file.Replace(rootDirBackUp, _rootDirSource);

                if (File.Exists(pathDest))
                {
                    FileInfo fi = new FileInfo(pathDest);
                    if (fi.IsReadOnly)
                        fi.IsReadOnly = false;

                    fi.Delete();
                }

                File.Copy(file, pathDest, true);
            }
        }

    }
}
