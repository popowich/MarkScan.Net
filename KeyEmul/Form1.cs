using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyEmul
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);

            //Эмулируем ввод штрих-кода товара
           // BaseIO.UtilitesWinAPI.EmulatePressKey(Keys.F11);
            foreach(char ch in textBox1.Text)
            {
                BaseIO.UtilitesWinAPI.EmulatePressKey(BaseIO.UtilitesWinAPI.ConvertCharToKey(ch));
                //Задержка между нажатиями
                System.Threading.Thread.Sleep(10);
            }

           // BaseIO.UtilitesWinAPI.EmulatePressKey(Keys.Enter);
            //System.Threading.Thread.Sleep(10);
           // BaseIO.UtilitesWinAPI.EmulatePressKey(System.Windows.Forms.Keys.Down);
        }
    }
}
