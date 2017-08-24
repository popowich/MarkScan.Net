using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarkScan.Models;
using OnlineUpdate;

namespace MarkScan.Data
{
    internal class DataBaseManager
    {
        private static DataBaseManager _instance;

        private SQLiteDataSource _dataSource;

        internal DataBaseManager()
        {
            _createDataBase();

            _dataSource = new SQLiteDataSource();
        }

        internal static DataBaseManager GetManager()
        {
            return _instance ?? (_instance = new DataBaseManager());
        }

        private void _createDataBase()
        {
            try
            {
                if (File.Exists(AppSettings.UserWorkDir + "\\database.sqlite"))
                    File.Delete(AppSettings.UserWorkDir + "\\database.sqlite");

                File.WriteAllBytes(AppSettings.UserWorkDir + "\\database.sqlite", Properties.Resources.database);
            }
            catch (Exception ex)
            {
                AppSettings.HandlerException(new Exception("Error create database", ex));
            }
        }

        internal void SaveInventoryData(string mark, string alccode)
        {
            
        }

        internal void SaveWriteOffData(string mark, string alccode)
        {

        }

        internal List<string[]> ReadInventoryData()
        {
            throw new Exception();
        }

        internal List<string[]> ReadWriteOffData()
        {
            throw new Exception();
        }

        internal void ClearInventoryData()
        {
            
        }

        internal void ClearWriteOffData()
        {

        }
    }
}
