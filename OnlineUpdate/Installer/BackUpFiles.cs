using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Installer
{
    class BackUpFiles
    {
        private string _currentVersion;
        private string _rootDirBackUp;
        private string _rootDirSoyrce;

        internal BackUpFiles(string currentVersion, string rootDirBackUp, string rootDirSoyrce)
        {
            _currentVersion = currentVersion;
            _rootDirBackUp = rootDirBackUp;
            _rootDirSoyrce = rootDirSoyrce;
        }

        internal void BackUp(List<FileInstall> filesFileInstalls)
        {
            string dir = _rootDirBackUp + "\\" + _currentVersion;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            foreach (var file in filesFileInstalls)
            {
                string ff = file.SourceFile.Replace(_rootDirSoyrce, "");
                string backUpPath = _rootDirBackUp + ff;

                file.CopyFile(backUpPath);
            }
        }

    }
}
