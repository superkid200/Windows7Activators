using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Win7Cracks.Properties;

namespace Win7Cracks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter(
                Path.GetTempPath() + "\\activatorcol.rtf"))
            {
                writer.Write(Resources.text);
            }
            richTextBox1.LoadFile(Path.GetTempPath() + "\\activatorcol.rtf");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var accept = MessageBox.Show(Resources.ConfirmationText,
                "Warning!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if(accept == DialogResult.Yes)
            {
                Hide();
                ConsoleClass.TakeControl();
            }
        }
    }
}
