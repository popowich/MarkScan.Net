
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace OnlineUpdate
{
    /// <summary>
    /// Утилиты Assembly
    /// </summary>
    public static class AssemblyUtilites
    {
        /// <summary>
        /// Получить версию сборки
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static Version GetVersionAssembly(string _path)
        {
            try
            {
                if (Path.GetExtension(_path) == ".exe" || Path.GetExtension(_path) == ".dll")
                {
                    return Version.Parse(System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).FileVersion);
                }
            }
            catch
            {

            }

            return null;
        }
        /// <summary>
        /// Получить имя сборки
        /// </summary>
        /// <param name="_path"></param>
        /// <returns></returns>
        public static string GetAppInternalName(string _path)
        {
            try
            {
                if (Path.GetExtension(_path) == ".exe" || Path.GetExtension(_path) == ".dll")
                {
                    return System.Diagnostics.FileVersionInfo.GetVersionInfo(_path).InternalName;
                }
            }
            catch
            {

            }
            return null;
        }
        /// <summary>
        /// Найти файл по имени сборки
        /// </summary>
        /// <param name="_pathDir"></param>
        /// <param name="_nameAsembly"></param>
        /// <returns></returns>
        public static string GetPathFileByInternalName(string _pathDir, string _nameAsembly)
        {
            try
            {
                string[] files = Directory.GetFiles(_pathDir);
                foreach (string file in files)
                {
                    try
                    {
                        if (Path.GetExtension(file) == ".exe")
                        {
                            if (GetAppInternalName(file) == _nameAsembly)
                                return file;
                        }
                    }
                    catch { }                  
                }
            }
            catch { }

            return null;
        }
        /// <summary>
        /// Найти файл по имени сборки
        /// </summary>
        /// <param name="_pathDir"></param>
        /// <param name="_nameAsembly"></param>
        /// <returns></returns>
        public static string[] GetAllPathFileByInternalName(string _pathDir, string _nameAsembly)
        {
            List<string> mas = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(_pathDir);
                foreach (string file in files)
                {
                    try
                    {
                        if (Path.GetExtension(file) == ".exe")
                        {
                            if (GetAppInternalName(file) == _nameAsembly)
                                mas.Add(file);
                        }
                    }
                    catch { }
                }
            }
            catch { }

            return mas.ToArray();
        }
        /// <summary>
        /// Вернет хеш MD5 файла
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFilesMD5(string path)
        {
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    byte[] Sum = new MD5CryptoServiceProvider().ComputeHash(stream);
                    string result = BitConverter.ToString(Sum).Replace("-", String.Empty);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to get MD5 hash", ex);
            }
        }
    }
    /// <summary>
    /// Утилиты XML
    /// </summary>
    public static class XMLUtilites
    {
        /// <summary>
        /// Получить содержание тэга
        /// </summary>
        /// <param name="_document"></param>
        /// <param name="_tag"></param>
        /// <returns></returns>
        public static string GetInnerTextXML(XmlDocument _document, string _tag)
        {
            if (_document.GetElementsByTagName(_tag).Count > 0)
                return _document.GetElementsByTagName(_tag)[0].InnerText;
            else
                return "";
        }
        /// <summary>
        /// Получить содержание тэга
        /// </summary>
        /// <param name="_list"></param>
        /// <param name="_tag"></param>
        /// <returns></returns>
        public static string GetInnerTextXML(XmlNodeList _list, string _tag)
        {
            foreach (XmlNode nod in _list)
            {
                if (nod.Name == _tag)
                    return nod.InnerText;
            }

            return "";
        }
        /// <summary>
        /// Получить содержание тэга
        /// </summary>
        /// <param name="_document"></param>
        /// <param name="_tag"></param>
        /// <returns></returns>
        public static XmlNode GetNodeXML(XmlDocument _document, string _tag)
        {
            if (_document.GetElementsByTagName(_tag).Count > 0)
                return _document.GetElementsByTagName(_tag)[0];
            else
                return null;
        }
        /// <summary>
        /// Получить атрибут узла XML
        /// </summary>
        /// <param name="_node"></param>
        /// <param name="_name"></param>
        /// <returns></returns>
        public static string GetAtributeTextXML(XmlNode _node, string _name)
        {
            if (_node.Attributes.GetNamedItem(_name) != null)
                return _node.Attributes.GetNamedItem(_name).InnerText;
            else
                return "";
        }
    }
}
