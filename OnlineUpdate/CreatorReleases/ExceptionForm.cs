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
    public partial class ExceptionForm : Form
    {
        private bool isDeatil = false;

        public ExceptionForm(Exception paramss)
        {
            InitializeComponent();

            Size = new Size(600, 290);
            splitContainer1.Panel2Collapsed = true;

            descriptionRichTextBox.Text = paramss.Message;

            if (paramss.InnerException != null)
            {
                descriptionRichTextBox.Text += "\n->" + paramss.InnerException.Message;
            }

            descriptionRichTextBox.Text += "\n" + paramss.StackTrace;

            fillTreeView(detailTreeView.Nodes.Add(""), paramss);
            detailTreeView.Nodes[0].ExpandAll();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void fillTreeView(TreeNode _node, Exception _ex)
        {
            if (_ex != null)
            {
                _node.Text = _ex.Message;
                _node.Tag = _ex;
                if (_ex.InnerException != null)
                    fillTreeView(_node.Nodes.Add(""), _ex.InnerException);
            }
        }

        private void tw_except_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Exception ex = (Exception)e.Node.Tag;
            descriptionRichTextBox.Text = "Message: " + ex.Message + "\n\n";
            descriptionRichTextBox.Text += "Source: " + ex.Source + "\n\n";
            descriptionRichTextBox.Text += "StackTrace: " + ex.StackTrace;
        }

        private void detailLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (isDeatil == false)
            {
                Size = new Size(600, 500);
                splitContainer1.Panel2Collapsed = false;
            }
            else
            {
                Size = new Size(600, 290);
                splitContainer1.Panel2Collapsed = true;
            }

            isDeatil = !isDeatil;
        }
    }
}
