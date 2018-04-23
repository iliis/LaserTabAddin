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
            
            tab_size_mode.SelectedIndex = 2;
        }

        public void setLabel(string text)
        {
            
        }

        public void setEdgeInfo(string shortedge, string longedge)
        {
            
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

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            parity_even.Enabled = force_parity.Checked;
            parity_odd.Enabled  = force_parity.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tab_size_mode.Visible = mode_size.Checked;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            tab_depth_input.Enabled = !auto_depth.Checked;
        }
    }
}
