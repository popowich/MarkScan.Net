using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace OnlineUpdate
{
    public partial class UpdateDescriptionForm : Form
    {
        public UpdateDescriptionForm(UpdateDescription description)
        {
            InitializeComponent();

            updateVersionLabel.Text = description.UpgradeToVersion.ToString();
            descriptionRichTextBox.Text = description.UpdateDescriptiones;
        }

        public UpdateDescriptionForm(UpdateDescription description, string titleForm, Icon iconForm)
            :this(description)
        {
            Text = titleForm;
            Icon = iconForm;
        }

        private void instalNowButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void remindLaterButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
