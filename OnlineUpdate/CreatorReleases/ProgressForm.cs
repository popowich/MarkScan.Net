using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatorReleases
{
    public partial class ProgressForm : Form
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProgressForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public ProgressForm(int _max)
            : this()
        {
            this.processProgressBar.Maximum = _max;
        }

        /// <summary>
        /// Устанвить значение
        /// </summary>
        /// <param name="_value"></param>
        /// <param name="_mess"></param>
        public void SetValue(int _value, string _mess)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                this.processProgressBar.Value += _value;
                this.prcTextBox.Text = ((this.processProgressBar.Value * 100) / this.processProgressBar.Maximum).ToString() + " %";
                this.fileTextBox.Text = _mess;
            }));
        }
    }
}
