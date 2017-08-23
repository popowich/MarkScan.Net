using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace MarkScan.Data
{
    public class SQLiteDataSource : ABaseIO
    {
        /// <summary>
        /// путь к файлу базы
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// Версия
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Строка подключения
        /// </summary>
        public override string StringConnect
        {
            get
            {
                SQLiteConnectionStringBuilder strBild = new SQLiteConnectionStringBuilder();
                strBild.DataSource = FileName;
                strBild.Version = Version;

                return strBild.ConnectionString;
            }
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SQLiteDataSource()
        {
            Version = 3;
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SQLiteDataSource(string _stringConnect)
            : this()
        {
            SQLiteConnectionStringBuilder strBild = new SQLiteConnectionStringBuilder("Data Source=" + _stringConnect);
            this.FileName = strBild.DataSource;
            this.Version = strBild.Version;
        }
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SQLiteDataSource(string _fileName, int _version)
            : this()
        {
            this.FileName = _fileName;
            this.Version = _version;
        }

        /// <summary>
        /// Открыть соединение
        /// </summary>
        /// <returns></returns>
        public override bool OpenConnect()
        {
            if (IsOpenConnect)
                CloseConnect();
            try
            {
                myConnect = new SQLiteConnection(this.StringConnect);
                myConnect.Open();
                BeginTransaction();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Метод выполнения SQL запроса
        /// </summary>
        /// <param name="_sSQLcomand">Строка SQL</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public override bool ExeSQLCommand(string _sSQLcomand)
        {
            try
            {
                if (!IsOpenTransaction)
                {
                    Exception ex = BeginTransaction();
                    if (ex != null)
                        throw ex;
                }

                SQLiteCommand cmd = new SQLiteCommand(_sSQLcomand, (SQLiteConnection)myConnect);
                cmd.ExecuteNonQuery();
                CommiteTarnsaction();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Метод выполнения SQL запроса
        /// </summary>
        /// <param name="_sSQLcomand">Строка SQL</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public override bool ExeSQLCommand(string _sSQLcomand, List<IDbDataParameter> _parametrs)
        {
            if (!IsOpenTransaction)
            {
                Exception ex = BeginTransaction();
                if (ex != null)
                    throw ex;
            }

            SQLiteCommand cmd = new SQLiteCommand(_sSQLcomand, (SQLiteConnection)myConnect);
            cmd.Parameters.AddRange(_parametrs.ToArray());
            cmd.ExecuteNonQuery();
            CommiteTarnsaction();

            return true;
        }
        /// <summary>
        /// Выборка данных из таблицы
        /// </summary>
        /// <param name="select">Строка выборки</param>
        /// <param name="razdelitel">Строка разделитель ячеек</param>
        /// <param name="_bTranceCommit">Завершить транзакцию</param>
        /// <returns></returns>
        public override List<object[]> Select(string select)
        {
            List<object[]> masOut = new List<object[]>();
            try
            {
                if (!IsOpenTransaction)
                {
                    Exception ex = BeginTransaction();
                    if (ex != null)
                        throw ex;
                }

                SQLiteCommand Read = new SQLiteCommand(select, (SQLiteConnection)myConnect);
                SQLiteDataReader read = Read.ExecuteReader();

                while (read.Read())
                {
                    object[] obb = new object[read.FieldCount];
                    read.GetValues(obb);
                    masOut.Add(obb);
                }
                read.Close();
                CommiteTarnsaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return masOut;
        }
    }
}
