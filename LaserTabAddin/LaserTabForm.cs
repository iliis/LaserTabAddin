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
        public class LaserTabFormState
        {
            public bool force_parity;
            public bool parity_even;
            public bool parity_odd;
            public bool mode_size;
            public bool mode_count;
            public int tab_size_mode;
            public String tab_size_input;
            public bool auto_depth;
            public String tab_depth_input;
            public bool extrude_negative;
            public bool extrude_positive;
            public bool offset;
        }

        public LaserTabForm(LaserTabFormState state = null)
        {
            InitializeComponent();

            if (state == null)
            {
                // load default state
                tab_size_mode.SelectedIndex = 2;
            }
            else
            {
                // restore state
                this.force_parity.Checked       = state.force_parity;
                this.parity_even.Checked        = state.parity_even;
                this.parity_odd.Checked         = state.parity_odd;
                this.mode_size.Checked          = state.mode_size;
                this.mode_count.Checked         = state.mode_count;
                this.tab_size_mode.SelectedIndex = state.tab_size_mode;
                this.tab_size_input.Text        = state.tab_size_input;
                this.auto_depth.Checked         = state.auto_depth;
                this.tab_depth_input.Text       = state.tab_depth_input;
                this.extrude_negative.Checked   = state.extrude_negative;
                this.extrude_positive.Checked   = state.extrude_positive;
                this.offset.Checked             = state.offset;
            }
        }

        public LaserTabFormState getState()
        {
            LaserTabFormState s = new LaserTabFormState();
            s.force_parity      = this.force_parity.Checked;
            s.parity_even       = this.parity_even.Checked;
            s.parity_odd        = this.parity_odd.Checked;
            s.mode_size         = this.mode_size.Checked;
            s.mode_count        = this.mode_count.Checked;
            s.tab_size_mode     = this.tab_size_mode.SelectedIndex;
            s.tab_size_input    = this.tab_size_input.Text;
            s.auto_depth        = this.auto_depth.Checked;
            s.tab_depth_input   = this.tab_depth_input.Text;
            s.extrude_negative  = this.extrude_negative.Checked;
            s.extrude_positive  = this.extrude_positive.Checked;
            s.offset            = this.offset.Checked;

            return s;
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

        private void tab_depth_input_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
