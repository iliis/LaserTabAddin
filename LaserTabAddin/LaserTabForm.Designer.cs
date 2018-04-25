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
            this.force_parity = new System.Windows.Forms.CheckBox();
            this.parity_even = new System.Windows.Forms.RadioButton();
            this.parity_odd = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mode_count = new System.Windows.Forms.RadioButton();
            this.mode_size = new System.Windows.Forms.RadioButton();
            this.tab_input_group = new System.Windows.Forms.GroupBox();
            this.tab_size_mode = new System.Windows.Forms.ComboBox();
            this.tab_size_input = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.auto_depth = new System.Windows.Forms.CheckBox();
            this.tab_depth_input = new System.Windows.Forms.TextBox();
            this.extrude_positive = new System.Windows.Forms.RadioButton();
            this.extrude_negative = new System.Windows.Forms.RadioButton();
            this.offset = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.tab_input_group.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.AutoSize = true;
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.Location = new System.Drawing.Point(336, 385);
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
            this.button_ok.Location = new System.Drawing.Point(447, 385);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(76, 30);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // force_parity
            // 
            this.force_parity.AutoSize = true;
            this.force_parity.Location = new System.Drawing.Point(6, 25);
            this.force_parity.Name = "force_parity";
            this.force_parity.Size = new System.Drawing.Size(117, 24);
            this.force_parity.TabIndex = 7;
            this.force_parity.Text = "force parity:";
            this.force_parity.UseVisualStyleBackColor = true;
            this.force_parity.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // parity_even
            // 
            this.parity_even.AutoSize = true;
            this.parity_even.Location = new System.Drawing.Point(129, 25);
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
            this.parity_odd.Location = new System.Drawing.Point(203, 25);
            this.parity_odd.Name = "parity_odd";
            this.parity_odd.Size = new System.Drawing.Size(61, 24);
            this.parity_odd.TabIndex = 9;
            this.parity_odd.TabStop = true;
            this.parity_odd.Text = "odd";
            this.parity_odd.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mode_size);
            this.groupBox1.Controls.Add(this.mode_count);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 62);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "tab size driving mode";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // mode_count
            // 
            this.mode_count.AutoSize = true;
            this.mode_count.Location = new System.Drawing.Point(6, 25);
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
            this.mode_size.Location = new System.Drawing.Point(153, 25);
            this.mode_size.Name = "mode_size";
            this.mode_size.Size = new System.Drawing.Size(89, 24);
            this.mode_size.TabIndex = 1;
            this.mode_size.TabStop = true;
            this.mode_size.Text = "tab size";
            this.mode_size.UseVisualStyleBackColor = true;
            // 
            // tab_input_group
            // 
            this.tab_input_group.Controls.Add(this.tab_size_input);
            this.tab_input_group.Controls.Add(this.tab_size_mode);
            this.tab_input_group.Location = new System.Drawing.Point(12, 80);
            this.tab_input_group.Name = "tab_input_group";
            this.tab_input_group.Size = new System.Drawing.Size(511, 68);
            this.tab_input_group.TabIndex = 10;
            this.tab_input_group.TabStop = false;
            this.tab_input_group.Text = "tab count/size";
            // 
            // tab_size_mode
            // 
            this.tab_size_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tab_size_mode.FormattingEnabled = true;
            this.tab_size_mode.Items.AddRange(new object[] {
            ">=",
            "<=",
            "approx."});
            this.tab_size_mode.Location = new System.Drawing.Point(15, 25);
            this.tab_size_mode.Name = "tab_size_mode";
            this.tab_size_mode.Size = new System.Drawing.Size(121, 28);
            this.tab_size_mode.TabIndex = 0;
            // 
            // tab_size_input
            // 
            this.tab_size_input.Location = new System.Drawing.Point(142, 27);
            this.tab_size_input.Name = "tab_size_input";
            this.tab_size_input.Size = new System.Drawing.Size(363, 26);
            this.tab_size_input.TabIndex = 1;
            this.tab_size_input.Text = "10 mm";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tab_depth_input);
            this.groupBox2.Controls.Add(this.auto_depth);
            this.groupBox2.Location = new System.Drawing.Point(12, 154);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(511, 84);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "tab depth";
            // 
            // auto_depth
            // 
            this.auto_depth.AutoSize = true;
            this.auto_depth.Checked = true;
            this.auto_depth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.auto_depth.Location = new System.Drawing.Point(6, 25);
            this.auto_depth.Name = "auto_depth";
            this.auto_depth.Size = new System.Drawing.Size(226, 24);
            this.auto_depth.TabIndex = 1;
            this.auto_depth.Text = "same as material thickness";
            this.auto_depth.UseVisualStyleBackColor = true;
            this.auto_depth.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // tab_depth_input
            // 
            this.tab_depth_input.Enabled = false;
            this.tab_depth_input.Location = new System.Drawing.Point(238, 23);
            this.tab_depth_input.Name = "tab_depth_input";
            this.tab_depth_input.Size = new System.Drawing.Size(267, 26);
            this.tab_depth_input.TabIndex = 0;
            this.tab_depth_input.Text = "5 mm";
            this.tab_depth_input.TextChanged += new System.EventHandler(this.tab_depth_input_TextChanged);
            // 
            // extrude_positive
            // 
            this.extrude_positive.AutoSize = true;
            this.extrude_positive.Checked = true;
            this.extrude_positive.Location = new System.Drawing.Point(6, 25);
            this.extrude_positive.Name = "extrude_positive";
            this.extrude_positive.Size = new System.Drawing.Size(61, 24);
            this.extrude_positive.TabIndex = 0;
            this.extrude_positive.TabStop = true;
            this.extrude_positive.Text = "add";
            this.extrude_positive.UseVisualStyleBackColor = true;
            // 
            // extrude_negative
            // 
            this.extrude_negative.AutoSize = true;
            this.extrude_negative.Location = new System.Drawing.Point(73, 25);
            this.extrude_negative.Name = "extrude_negative";
            this.extrude_negative.Size = new System.Drawing.Size(92, 24);
            this.extrude_negative.TabIndex = 1;
            this.extrude_negative.Text = "subtract";
            this.extrude_negative.UseVisualStyleBackColor = true;
            // 
            // offset
            // 
            this.offset.AutoSize = true;
            this.offset.Location = new System.Drawing.Point(286, 37);
            this.offset.Name = "offset";
            this.offset.Size = new System.Drawing.Size(76, 24);
            this.offset.TabIndex = 14;
            this.offset.Text = "offset";
            this.offset.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.force_parity);
            this.groupBox3.Controls.Add(this.parity_even);
            this.groupBox3.Controls.Add(this.parity_odd);
            this.groupBox3.Location = new System.Drawing.Point(12, 244);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(511, 67);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "parity";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.extrude_positive);
            this.groupBox4.Controls.Add(this.extrude_negative);
            this.groupBox4.Location = new System.Drawing.Point(12, 317);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(511, 62);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "extrusion direction";
            // 
            // LaserTabForm
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(535, 427);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tab_input_group);
            this.Controls.Add(this.offset);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LaserTabForm";
            this.ShowInTaskbar = false;
            this.Text = "LaserTabForm";
            this.Load += new System.EventHandler(this.LaserTabForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tab_input_group.ResumeLayout(false);
            this.tab_input_group.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.CheckBox force_parity;
        public System.Windows.Forms.RadioButton parity_even;
        public System.Windows.Forms.RadioButton parity_odd;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton mode_size;
        public System.Windows.Forms.RadioButton mode_count;
        public System.Windows.Forms.GroupBox tab_input_group;
        public System.Windows.Forms.ComboBox tab_size_mode;
        public System.Windows.Forms.TextBox tab_size_input;
        public System.Windows.Forms.Button button_ok;
        public System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.CheckBox auto_depth;
        public System.Windows.Forms.TextBox tab_depth_input;
        public System.Windows.Forms.RadioButton extrude_negative;
        public System.Windows.Forms.RadioButton extrude_positive;
        public System.Windows.Forms.CheckBox offset;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}