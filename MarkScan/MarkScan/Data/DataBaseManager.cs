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

            try
            {
                DeleteAllRemainingProduct();
            }
            catch (Exception e)
            {
                _dataSource.CloseConnect();
                _createDataBase(true);
            }
        }

        internal static DataBaseManager GetManager()
        {
            return _instance ?? (_instance = new DataBaseManager());
        }
        /// <summary>
        /// Создать базу данных
        /// </summary>
        private void _createDataBase(bool delBase = false)
        {
            try
            {
                if (!File.Exists(AppSettings.UserWorkDir + "\\database.sqlite") || delBase)
                    File.WriteAllBytes(AppSettings.UserWorkDir + "\\database.sqlite", Properties.Resources.database);
            }
            catch (Exception ex)
            {
                AppSettings.HandlerException(new Exception("Error create database", ex));
            }
        }

        #region Inventory

        /// <summary>
        /// Сохранить данные марки инвентаризации
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="alccode"></param>
        internal void SaveInventoryMark(string mark, string alccode)
        {
            var res = _dataSource.InsertInto("'" + mark + "'," +
                                             "'" + alccode + "'," +
                                             "'" + Tools.ConvertUnixDate.ConvertInUnixTimestamp(DateTime.Now) + "'", "`Inventory` (`mark`,`alccode`,`dateCreate`)");

        }
        /// <summary>
        /// Прочитать данные марок инвентаризации
        /// </summary>
        /// <returns></returns>
        internal List<object[]> ReadInventoryMark()
        {
            List<object[]> res = _dataSource.Select("SELECT * FROM `Inventory`");

            return res;
        }
        /// <summary>
        /// Удалить данные марки инвентаризации
        /// </summary>
        /// <param name="mark"></param>
        internal void DeleteInventoryMark(string mark)
        {
            _dataSource.Delete("`mark`='" + mark + "'", "`Inventory`");
        }
        /// <summary>
        /// Удалить все данные марок инвентаризации
        /// </summary>
        internal void DeleteAllInventoryMark()
        {
            _dataSource.Delete("", "`Inventory`");
        }
        /// <summary>
        /// Существуют отсканированные марки инвентаризации
        /// </summary>
        /// <returns></returns>
        internal bool ExistInventoryMark()
        {
            return _dataSource.Select("SELECT id FROM `Inventory`").Count > 0;
        }

        #endregion

        #region WriteOff

        /// <summary>
        /// Сохранить данные марки списания
        /// </summary>
        /// <param name="mark"></param>
        /// <param name="alccode"></param>
        internal void SaveWriteOffMark(string mark, string alccode)
        {
            var res = _dataSource.InsertInto("'" + mark + "'," +
                                          "'" + alccode + "'," +
                                          "'" + Tools.ConvertUnixDate.ConvertInUnixTimestamp(DateTime.Now) + "'", "`WriteOff` (`mark`,`alccode`,`dateCreate`)");
        }
        /// <summary>
        /// Прочитать данные марок списания
        /// </summary>
        /// <returns></returns>
        internal List<object[]> ReadWriteOffMark()
        {
            List<object[]> res = _dataSource.Select("SELECT * FROM `WriteOff`");

            return res;
        }
        /// <summary>
        ///  Удалить данные марки списания
        /// </summary>
        /// <param name="mark"></param>
        internal void DeleteWriteOffMark(string mark)
        {
            _dataSource.Delete("`mark`='" + mark + "'", "`WriteOff`");
        }
        /// <summary>
        /// Удалить все данные марок списания
        /// </summary>
        internal void DeleteAllWriteOffMark()
        {
            _dataSource.Delete("", "`WriteOff`");
        }
        /// <summary>
        /// Существуют отсканированные марки списания
        /// </summary>
        /// <returns></returns>
        internal bool ExistWriteOffMark()
        {
            return _dataSource.Select("SELECT id FROM `WriteOff`").Count > 0;
        }

        #endregion

        #region Remaining
        /// <summary>
        /// Записать даннык в таблицу остатков
        /// </summary>
        /// <param name="product"></param>
        internal void SaveRemainingProduct(Network.JsonWrapers.ProductionRemainings product)
        {
            var res = _dataSource.InsertInto("'" + product.Id + "'," +
                                             "'" + product.Position + "'," +
                                             "'" + product.FullName.Replace("'", "") + "'," +
                                             "'" + product.ShortName + "'," +
                                             "'" + product.AlcCode + "'," +
                                             "'" + product.Capacity + "'," +
                                             "'" + product.AlcVolume + "'," +
                                             "'" + product.EgaisQuantity + "'," +
                                             "'" + product.RealQuantity + "'," +
                                             "'" + product.ProductVCode + "'"
                                             , "`Remainings` (`id_service`,`position`,`fullName`,`shortName`,`alcCode`,`capacity`,`alcVolume`,`egaisQuantity`,`realQuantity`,`productVCode`)");
        }
        /// <summary>
        /// Получить данные остатков по alcCode
        /// </summary>
        /// <param name="alcCode"></param>
        /// <returns></returns>
        internal object[] ReadeRemainingProductForAlcCode(string alcCode)
        {
            List<object[]> res = _dataSource.Select("SELECT * FROM `Remainings` where `alcCode`='" + alcCode + "'");

            return res.Count > 0 ? res[0] : null;
        }
        /// <summary>
        /// Удалить все данные из таблицы остатков
        /// </summary>
        internal void DeleteAllRemainingProduct()
        {
            _dataSource.Delete("", "`Remainings`");
        }

        #endregion
    }
}
