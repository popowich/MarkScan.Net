using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace MarkScan.Data
{
    /// <summary>
    /// Абстрактный класс базовый для классов работы с базами данных
    /// </summary>
    public abstract class ABaseIO
    {
        /// <summary>
        /// Флаг открытой транзакции
        /// </summary>
        protected bool bOpenTransaction = false;
        /// <summary>
        /// Объект базового соединения
        /// </summary>
        protected DbConnection myConnect;
        /// <summary>
        /// Объект транзакции
        /// </summary>
        protected DbTransaction myTranseaction;
        /// <summary>
        /// Уровень изоляции транзакции
        /// </summary>
        protected IsolationLevel lLevelTransaction = IsolationLevel.Serializable;

        /// <summary>
        /// Объект соединения
        /// </summary>
        public DbConnection MyConnect
        {
            get { return myConnect; }
        }
        /// <summary>
        /// Объект транзакций соединения
        /// </summary>
        public DbTransaction MyTranceaction
        {
            get { return myTranseaction; }
        }
        /// <summary>
        /// Уровень изоляции транзакций
        /// </summary>
        public IsolationLevel LevelTrancaction
        {
            get { return lLevelTransaction; }
            set { lLevelTransaction = value; }
        }
        /// <summary>
        /// Открыто ли соединение
        /// </summary>
        public bool IsOpenConnect
        {
            get
            {
                return myConnect != null && myConnect.State == ConnectionState.Open;
            }

        }
        /// <summary>
        /// Транзакция открыта
        /// </summary>
        public bool IsOpenTransaction
        {
            get { return bOpenTransaction; }
        }
        /// <summary>
        /// Строка подключения к базе данных
        /// </summary>
        public abstract string StringConnect { get; }

        /// <summary>
        /// Открыть соединение
        /// </summary>
        /// <returns></returns>
        public abstract bool OpenConnect();
        /// <summary>
        /// Закрыть соединение
        /// </summary>
        public void CloseConnect()
        {
            if (IsOpenConnect && myConnect != null && myConnect.State == ConnectionState.Open)
            {
                CommiteTarnsaction();
                if (this.IsOpenConnect)
                {
                    myConnect.Close();
                }
            }
        }
        /// <summary>
        /// Завершить транзакцию
        /// </summary>
        public void CommiteTarnsaction()
        {
            if (bOpenTransaction)
            {
                try
                {
                    bOpenTransaction = false;
                    myTranseaction.Commit();
                }
                catch (Exception ex)
                {

                }
            }
        }
        /// <summary>
        /// Откат транзакции
        /// </summary>
        public void RollBackTarnsaction()
        {
            if (bOpenTransaction)
            {
                try
                {
                    bOpenTransaction = false;
                    myTranseaction.Rollback();
                }
                catch (Exception ex)
                {

                }
            }
        }
        /// <summary>
        /// Начать новую транзакцию
        /// </summary>
        public Exception BeginTransaction()
        {
            try
            {
                CommiteTarnsaction();
                if (IsOpenConnect)
                {
                    myTranseaction = myConnect.BeginTransaction(LevelTrancaction);
                    bOpenTransaction = true;
                }

                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
        /// <summary>
        /// Тест соединения с базой данных
        /// </summary>
        /// <returns>Ошибка возникшая при соединении</returns>
        public Exception TestConnect()
        {
            try
            {
                myConnect.Open();
                myConnect.Close();
            }
            catch (Exception ex)
            {
                return ex;
            }

            return null;
        }


        /// <summary>
        /// Метод выполнения SQL запроса
        /// </summary>
        /// <param name="_sSQLcomand">Строка SQL</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public abstract bool ExeSQLCommand(string _sql);
        /// <summary>
        /// Метод выполнения SQL запроса
        /// </summary>
        /// <param name="_sSQLcomand">Строка SQL</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public abstract bool ExeSQLCommand(string _sql, List<IDbDataParameter> _parametrs);
        /// <summary>
        /// Выборка данных из таблицы
        /// </summary>
        /// <param name="select">Строка выборки</param>
        /// <param name="razdelitel">Строка разделитель ячеек</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public abstract List<object[]> Select(string _select);
        /// <summary>
        /// Вставка данных в таблицу
        /// </summary>
        /// <param name="sAddData">Строка добавления</param>
        /// <param name="sTaible">Имя таблицы</param>
        /// <param name="bAutoSelectID">Автоматически задать ID</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public virtual bool InsertInto(string _values, string _tables)
        {
            string sql = "";
            //генерация строкм запроса
            sql = "INSERT INTO " + _tables + " VALUES(";

            sql += _values + ")";
            return ExeSQLCommand(sql);
        }
        /// <summary>
        /// Обновляет данные таблицы. Старые перезаписываются, новые вставляются
        /// </summary>
        /// <param name="sAddData">Строка добавления</param>
        /// <param name="sTaible">Имя таблицы</param>
        /// <param name="bAutoSelectID">Автоматически задать ID</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public virtual bool Replace(string _values, string _table)
        {
            string sql = "";
            //генерация строкм запроса
            sql = "REPLACE INTO " + _table + " VALUES(";

            sql += _values + ")";
            return ExeSQLCommand(sql);
        }
        /// <summary>
        /// Обновление данных в таблице
        /// </summary>
        /// <param name="sDataUpdate">Строка обновления</param>
        /// <param name="sWhere">Условие</param>
        /// <param name="sTaible">Имя таблицы</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public virtual bool Update(string _values, string _where, string _tables)
        {
            string sql = "UPDATE " + _tables + " set " + _values + " where " + _where;
            return ExeSQLCommand(sql);
        }
        /// <summary>
        /// Удаление строк в таблице по условию
        /// </summary>
        /// <param name="sWhere">Условие удаления</param>
        /// <param name="sTaible">Имя таблицы</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public virtual bool Delete(string _where, string _table)
        {
            string sql = "DELETE from " + _table + " where " + _where;
            return ExeSQLCommand(sql);
        }
    }
    /// <summary>
    /// Параметрический класс
    /// </summary>
    public class EventArgsBase : EventArgs
    {
        public EventArgsBase()
        {
        }
        public EventArgsBase(Exception _exept, string _string_sql, string _sAddition)
        {
            exept = _exept;
            sAddition = _sAddition;
            string_sql = _string_sql;
        }

        public Exception exept;
        public string string_sql = "";
        public string sAddition = "";
    }
}
