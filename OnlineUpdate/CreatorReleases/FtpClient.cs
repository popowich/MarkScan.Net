using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatorReleases
{
    /// <summary>
    /// Реализация устойчивого FTP клиента
    /// </summary>
    public class FtpClient : IDisposable
    {
        /// <summary>
        /// Ftp клиент
        /// </summary>
        private BytesRoad.Net.Ftp.FtpClient ftpClient = null;
        /// <summary>
        /// Адрес сервера
        /// </summary>
        private string url;
        /// <summary>
        /// Порт соединения
        /// </summary>
        private int port;
        /// <summary>
        /// Имя пользователя
        /// </summary>
        private string userName;
        /// <summary>
        /// Пароль
        /// </summary>
        private string password;
        /// <summary>
        /// Текущий путь
        /// </summary>
        private string currPath;

        /// <summary>
        /// Соединение установлено
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (this.ftpClient != null)
                    return this.ftpClient.IsConnected;
                else
                    return false;
            }
        }

        public FtpClient(string _url, int _port, string _userName, string _password)
        {
            this.url = _url;
            this.port = _port;
            this.userName = _userName;
            this.password = _password;
        }

        public void Dispose()
        {
            if (this.ftpClient != null)
            {
                try
                {
                    ftpClient.Disconnect(5000);
                }
                catch { }

                ftpClient.Dispose();
            }
        }

        public void Connect()
        {
            //Выполнить соединение и авторизацию
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    if (this.ftpClient != null)
                        this.ftpClient.Dispose();

                    this.ftpClient = new BytesRoad.Net.Ftp.FtpClient();
                    this.ftpClient.PassiveMode = true;
                    BytesRoad.Net.Ftp.FtpResponse re = this.ftpClient.Connect(5000, this.url, this.port);
                    this.ftpClient.Login(5000, this.userName, this.password);

                    //Восстановить позицию тек. каталога
                    if (!string.IsNullOrEmpty(currPath))
                    {
                        string[] pathes = currPath.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string str in pathes)
                            this.ftpClient.ChangeDirectory(2000, str);
                    }

                    break;
                }
                catch (Exception ex)
                {
                    if (countTry == 2)
                        throw ex;

                    System.Threading.Thread.Sleep(7000);
                }

                countTry++;
            }
        }

        public bool TestConnect()
        {
            var client = new BytesRoad.Net.Ftp.FtpClient();
            client.PassiveMode = true;
            BytesRoad.Net.Ftp.FtpResponse re = client.Connect(5000, this.url, this.port);
            client.Login(5000, this.userName, this.password);

            client.Disconnect(5000);
            client.Dispose();

            return true;
        }

        public BytesRoad.Net.Ftp.FtpItem[] GetDirectoryList()
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    return this.ftpClient.GetDirectoryList(2000);
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }

            return null;
        }

        public void ChangeDirectory(string _path)
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    this.ftpClient.ChangeDirectory(2000, _path);
                    this.currPath += "\\" + _path;
                    break;
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }
        }

        public void ChangeDirectoryUp()
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    this.ftpClient.ChangeDirectoryUp(2000);
                    int index = this.currPath.LastIndexOf('\\');
                    if (index > -1)
                        this.currPath = this.currPath.Remove(index);

                    break;
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }
        }

        public string CreateDirectory(string _name)
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    return this.ftpClient.CreateDirectory(2000, _name);
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }

            return null;
        }

        public void PutFile(string _name, FileStream _stream)
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    this.ftpClient.PutFile(600000, _name, _stream);
                    break;
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }
        }

        public void DeleteFile(string _name)
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    this.ftpClient.DeleteFile(5000, _name);
                    break;
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }
        }

        public void DeleteDirectory(string _path)
        {
            int countTry = 0;
            while (countTry < 3)
            {
                try
                {
                    this.ftpClient.DeleteDirectory(5000, _path);
                    break;
                }
                catch (Exception ex)
                {
                    this.Connect();
                    if (countTry == 2)
                        throw ex;
                }
                countTry++;
            }
        }

        public void RecursiveDeleteDirectory(string _path)
        {
            this.ChangeDirectory(_path);

            //Ищем и удаляем существующую корневую дирректорию 
            BytesRoad.Net.Ftp.FtpItem[] files = this.GetDirectoryList();
            foreach (BytesRoad.Net.Ftp.FtpItem file in files)
            {
                if (file.ItemType != BytesRoad.Net.Ftp.FtpItemType.Directory)
                {
                    this.DeleteFile(file.Name);
                }
            }
            foreach (BytesRoad.Net.Ftp.FtpItem dir in files)
            {
                if (dir.ItemType == BytesRoad.Net.Ftp.FtpItemType.Directory && dir.Name != ".." && dir.Name != ".")
                {
                    this.RecursiveDeleteDirectory(dir.Name);
                }
            }

            this.ChangeDirectoryUp();
            this.DeleteDirectory(_path);
        }
    }
}
