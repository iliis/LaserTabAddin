using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaserTabAddin
{
    public partial class LaserTabForm : Form
    {
        public LaserTabForm()
        {
            InitializeComponent();
        }

        public void setLabel(string text)
        {
            hw_label.Text = text;
        }

        public void setEdgeInfo(string shortedge, string longedge)
        {
            label_shortedge.Text = shortedge;
            label_longedge.Text = longedge;
        }

        private void LaserTabForm_Load(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
