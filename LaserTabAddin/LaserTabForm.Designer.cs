namespace LaserTabAddin
{
    partial class LaserTabForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.force_parity = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.parity_even = new System.Windows.Forms.RadioButton();
            this.parity_odd = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mode_count = new System.Windows.Forms.RadioButton();
            this.mode_size = new System.Windows.Forms.RadioButton();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.tab_input_group = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.tab_size_mode = new System.Windows.Forms.ComboBox();
            this.tab_size_input = new System.Windows.Forms.TextBox();
            this.button_invert = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.tab_depth_input = new System.Windows.Forms.TextBox();
            this.auto_depth = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tab_input_group.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.AutoSize = true;
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(381, 3);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(105, 30);
            this.button_cancel.TabIndex = 3;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.AutoSize = true;
            this.button_ok.Location = new System.Drawing.Point(299, 3);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(76, 30);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tab_input_group, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_invert, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.groupBox2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(495, 678);
            this.tableLayoutPanel1.TabIndex = 7;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.button_cancel);
            this.flowLayoutPanel1.Controls.Add(this.button_ok);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 633);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(489, 42);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // force_parity
            // 
            this.force_parity.AutoSize = true;
            this.force_parity.Location = new System.Drawing.Point(3, 3);
            this.force_parity.Name = "force_parity";
            this.force_parity.Size = new System.Drawing.Size(117, 24);
            this.force_parity.TabIndex = 7;
            this.force_parity.Text = "force parity:";
            this.force_parity.UseVisualStyleBackColor = true;
            this.force_parity.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.force_parity);
            this.flowLayoutPanel2.Controls.Add(this.parity_even);
            this.flowLayoutPanel2.Controls.Add(this.parity_odd);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 363);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(489, 84);
            this.flowLayoutPanel2.TabIndex = 8;
            // 
            // parity_even
            // 
            this.parity_even.AutoSize = true;
            this.parity_even.Location = new System.Drawing.Point(126, 3);
            this.parity_even.Name = "parity_even";
            this.parity_even.Size = new System.Drawing.Size(68, 24);
            this.parity_even.TabIndex = 8;
            this.parity_even.Text = "even";
            this.parity_even.UseVisualStyleBackColor = true;
            // 
            // parity_odd
            // 
            this.parity_odd.AutoSize = true;
            this.parity_odd.Checked = true;
            this.parity_odd.Location = new System.Drawing.Point(200, 3);
            this.parity_odd.Name = "parity_odd";
            this.parity_odd.Size = new System.Drawing.Size(61, 24);
            this.parity_odd.TabIndex = 9;
            this.parity_odd.TabStop = true;
            this.parity_odd.Text = "odd";
            this.parity_odd.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.flowLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(489, 84);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "tab size driving mode";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // mode_count
            // 
            this.mode_count.AutoSize = true;
            this.mode_count.Location = new System.Drawing.Point(3, 3);
            this.mode_count.Name = "mode_count";
            this.mode_count.Size = new System.Drawing.Size(141, 24);
            this.mode_count.TabIndex = 0;
            this.mode_count.Text = "number of tabs";
            this.mode_count.UseVisualStyleBackColor = true;
            this.mode_count.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // mode_size
            // 
            this.mode_size.AutoSize = true;
            this.mode_size.Checked = true;
            this.mode_size.Location = new System.Drawing.Point(150, 3);
            this.mode_size.Name = "mode_size";
            this.mode_size.Size = new System.Drawing.Size(89, 24);
            this.mode_size.TabIndex = 1;
            this.mode_size.TabStop = true;
            this.mode_size.Text = "tab size";
            this.mode_size.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.mode_count);
            this.flowLayoutPanel3.Controls.Add(this.mode_size);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 22);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(483, 59);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // tab_input_group
            // 
            this.tab_input_group.AutoSize = true;
            this.tab_input_group.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tab_input_group.Controls.Add(this.flowLayoutPanel4);
            this.tab_input_group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_input_group.Location = new System.Drawing.Point(3, 93);
            this.tab_input_group.Name = "tab_input_group";
            this.tab_input_group.Size = new System.Drawing.Size(489, 84);
            this.tab_input_group.TabIndex = 10;
            this.tab_input_group.TabStop = false;
            this.tab_input_group.Text = "tab count/size";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.tab_size_mode);
            this.flowLayoutPanel4.Controls.Add(this.tab_size_input);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 22);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(483, 59);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // tab_size_mode
            // 
            this.tab_size_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tab_size_mode.FormattingEnabled = true;
            this.tab_size_mode.Items.AddRange(new object[] {
            ">=",
            "<=",
            "approx."});
            this.tab_size_mode.Location = new System.Drawing.Point(3, 3);
            this.tab_size_mode.Name = "tab_size_mode";
            this.tab_size_mode.Size = new System.Drawing.Size(121, 28);
            this.tab_size_mode.TabIndex = 0;
            // 
            // tab_size_input
            // 
            this.tab_size_input.Dock = System.Windows.Forms.DockStyle.Left;
            this.tab_size_input.Location = new System.Drawing.Point(130, 3);
            this.tab_size_input.Name = "tab_size_input";
            this.tab_size_input.Size = new System.Drawing.Size(329, 26);
            this.tab_size_input.TabIndex = 1;
            this.tab_size_input.Text = "10 mm";
            // 
            // button_invert
            // 
            this.button_invert.AutoSize = true;
            this.button_invert.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_invert.Location = new System.Drawing.Point(3, 183);
            this.button_invert.Name = "button_invert";
            this.button_invert.Size = new System.Drawing.Size(57, 30);
            this.button_invert.TabIndex = 11;
            this.button_invert.Text = "invert";
            this.button_invert.UseVisualStyleBackColor = true;
            this.button_invert.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.flowLayoutPanel5);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 273);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(489, 84);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "tab depth";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.auto_depth);
            this.flowLayoutPanel5.Controls.Add(this.tab_depth_input);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 22);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(483, 59);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // tab_depth_input
            // 
            this.tab_depth_input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab_depth_input.Enabled = false;
            this.tab_depth_input.Location = new System.Drawing.Point(235, 3);
            this.tab_depth_input.Name = "tab_depth_input";
            this.tab_depth_input.Size = new System.Drawing.Size(224, 26);
            this.tab_depth_input.TabIndex = 0;
            // 
            // auto_depth
            // 
            this.auto_depth.AutoSize = true;
            this.auto_depth.Checked = true;
            this.auto_depth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.auto_depth.Location = new System.Drawing.Point(3, 3);
            this.auto_depth.Name = "auto_depth";
            this.auto_depth.Size = new System.Drawing.Size(226, 24);
            this.auto_depth.TabIndex = 1;
            this.auto_depth.Text = "same as material thickness";
            this.auto_depth.UseVisualStyleBackColor = true;
            this.auto_depth.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // LaserTabForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(495, 678);
            this.Controls.Add(this.tableLayoutPanel1);
            this.HelpButton = true;
            this.Name = "LaserTabForm";
            this.ShowInTaskbar = false;
            this.Text = "LaserTabForm";
            this.Load += new System.EventHandler(this.LaserTabForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tab_input_group.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public System.Windows.Forms.CheckBox force_parity;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        public System.Windows.Forms.RadioButton parity_even;
        public System.Windows.Forms.RadioButton parity_odd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        public System.Windows.Forms.RadioButton mode_size;
        public System.Windows.Forms.RadioButton mode_count;
        public System.Windows.Forms.GroupBox tab_input_group;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        public System.Windows.Forms.ComboBox tab_size_mode;
        public System.Windows.Forms.TextBox tab_size_input;
        public System.Windows.Forms.Button button_ok;
        public System.Windows.Forms.Button button_cancel;
        public System.Windows.Forms.Button button_invert;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        public System.Windows.Forms.CheckBox auto_depth;
        public System.Windows.Forms.TextBox tab_depth_input;
    }
}