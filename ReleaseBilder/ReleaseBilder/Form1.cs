using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ReleaseBilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<DataFile> _files = new List<DataFile>();

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = d.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(textBox1.Text);
            foreach (var file in files)
            {
                if(!file.EndsWith(".exe") && !file.EndsWith(".dll"))
                    continue;

                string currentMD5 = GetFilesMD5(file);
                _files.Add(new DataFile()
                {
                    Path = file,
                    MD5 = currentMD5
                });
            }

            System.Xml.XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?><head></head>");

            System.Xml.XmlNode element = null;

            element = document.CreateElement("UpgradeToVersion");
            element.InnerText = textBox2.Text;
            document.DocumentElement.AppendChild(element);

            element = document.CreateElement("PublicationDate");
            element.InnerText = textBox3.Text;
            document.DocumentElement.AppendChild(element);

            element = document.CreateElement("UpdateDescriptiones");
            element.InnerText = richTextBox1.Text;
            document.DocumentElement.AppendChild(element);

            var elementUpdateFiles = document.CreateElement("UpdateFiles");
            document.DocumentElement.AppendChild(elementUpdateFiles);

            foreach (var file in _files)
            {
                System.Xml.XmlNode elementCild = document.CreateElement("File");

                elementCild.Attributes.Append(document.CreateAttribute("PathInstall")).InnerText = "\\" + new  FileInfo(file.Path).Name;
                elementCild.Attributes.Append(document.CreateAttribute("PathDownload")).InnerText = "/" + new FileInfo(file.Path).Name;
                elementCild.Attributes.Append(document.CreateAttribute("MD5")).InnerText = file.MD5;

                elementUpdateFiles.AppendChild(elementCild);
            }

            //Сохраняем xml файл
            document.Save("UpdateDescription.xml");
        }

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReleaseBilder.Properties.Settings.Default.Save();
        }
    }

    class DataFile
    {
        public string Path { get; set; }
        public string MD5 { get; set; }
    }
}
