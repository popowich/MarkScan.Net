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

            _dataSource = new SQLiteDataSource(AppSettings.UserWorkDir + "\\database.sqlite");
            if (_dataSource.OpenConnect() == false)
                throw new Exception("Не удалось подулючиться к базе данных");
        }

        internal static DataBaseManager GetManager()
        {
            return _instance ?? (_instance = new DataBaseManager());
        }

        private void _createDataBase()
        {
            try
            {
                if (!File.Exists(AppSettings.UserWorkDir + "\\database.sqlite"))
                    File.WriteAllBytes(AppSettings.UserWorkDir + "\\database.sqlite", Properties.Resources.database);
            }
            catch (Exception ex)
            {
                AppSettings.HandlerException(new Exception("Error create database", ex));
            }
        }

        internal void SaveInventoryMark(string mark, string alccode)
        {
            var res = _dataSource.InsertInto("'" + mark + "'," +
                                          "'" + alccode + "'," +
                                          "'" + Tools.ConvertUnixDate.ConvertInUnixTimestamp(DateTime.Now) + "'", "`Inventory` (`mark`,`alccode`,`dateCreate`)");

        }

        internal void SaveWriteOffMark(string mark, string alccode)
        {
            var res = _dataSource.InsertInto("'" + mark + "'," +
                                          "'" + alccode + "'," +
                                          "'" + Tools.ConvertUnixDate.ConvertInUnixTimestamp(DateTime.Now) + "'", "`WriteOff` (`mark`,`alccode`,`dateCreate`)");
        }

        internal List<object[]> ReadInventoryMark()
        {
            List<object[]> res = _dataSource.Select("SELECT * FROM `Inventory`");

            return res;
        }

        internal List<object[]> ReadWriteOffMark()
        {
            List<object[]> res = _dataSource.Select("SELECT * FROM `WriteOff`");

            return res;
        }

        internal void DeleteInventoryMark(string mark)
        {
            _dataSource.Delete("`mark`='" + mark + "'", "`Inventory`");
        }

        internal void DeleteWriteOffMark(string mark)
        {
            _dataSource.Delete("`mark`='" + mark + "'", "`WriteOff`");
        }

        internal void ClearInventoryMark()
        {
            _dataSource.Delete("", "`Inventory`");
        }

        internal void ClearWriteOffMark()
        {
            _dataSource.Delete("", "`WriteOff`");
        }

        internal bool ExistInventoryMark()
        {
            return _dataSource.Select("SELECT id FROM `Inventory`").Count > 0;
        }

        internal bool ExistWriteOffMark()
        {
            return _dataSource.Select("SELECT id FROM `WriteOff`").Count > 0;
        }
    }
}
