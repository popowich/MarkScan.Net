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
        }

        internal void BackUpFiles(string backUpVersion, List<FileInstall> filesFileInstalls)
        {
            string dir = _rootDirBackUp + "\\" + backUpVersion;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (var file in filesFileInstalls)
            {
                string pathDest = file.SourceFile.Replace(_rootDirSource, _rootDirBackUp);

                file.CopyFile(pathDest);
            }
        }

        internal void RecoveryFiles(string recoveryVersion)
        {
            string dir = _rootDirBackUp + "\\" + recoveryVersion;

            if (!Directory.Exists(dir))
                throw  new Exception($"Каталог восстановления версии {recoveryVersion} приложения не обнаружен");

            var files = Directory.GetFiles(dir, null, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                string pathDest = file.Replace(_rootDirBackUp, _rootDirSource);

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
