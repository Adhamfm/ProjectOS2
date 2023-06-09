﻿namespace ProjectOS2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Process = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArrivalTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessingTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_generate = new System.Windows.Forms.Button();
            this.comboBox = new System.Windows.Forms.ComboBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.quantumPanel = new System.Windows.Forms.Panel();
            this.quantumInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.premPanel = new System.Windows.Forms.Panel();
            this.radbtn_nonprem = new System.Windows.Forms.RadioButton();
            this.radbtn_prem = new System.Windows.Forms.RadioButton();
            this.btn_showform2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdn_instant = new System.Windows.Forms.RadioButton();
            this.rdn_live = new System.Windows.Forms.RadioButton();
            this.btn_rmv = new System.Windows.Forms.Button();
            this.btn_prc_add = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.processBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.processPriorityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.quantumPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantumInput)).BeginInit();
            this.premPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processPriorityBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AccessibleRole = System.Windows.Forms.AccessibleRole.Sound;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Process,
            this.ArrivalTime,
            this.ProcessingTime});
            this.dataGridView1.Location = new System.Drawing.Point(58, 34);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(417, 247);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.Visible = false;
            // 
            // Process
            // 
            this.Process.HeaderText = "Process";
            this.Process.MinimumWidth = 6;
            this.Process.Name = "Process";
            this.Process.Width = 50;
            // 
            // ArrivalTime
            // 
            this.ArrivalTime.HeaderText = "Arrival Time";
            this.ArrivalTime.MinimumWidth = 6;
            this.ArrivalTime.Name = "ArrivalTime";
            this.ArrivalTime.Width = 125;
            // 
            // ProcessingTime
            // 
            this.ProcessingTime.HeaderText = "Processing Time";
            this.ProcessingTime.MinimumWidth = 6;
            this.ProcessingTime.Name = "ProcessingTime";
            this.ProcessingTime.Width = 125;
            // 
            // btn_generate
            // 
            this.btn_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_generate.Location = new System.Drawing.Point(508, 251);
            this.btn_generate.Margin = new System.Windows.Forms.Padding(2);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(140, 45);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "GENERATE";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // comboBox
            // 
            this.comboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox.Font = new System.Drawing.Font("Microsoft YaHei", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox.FormattingEnabled = true;
            this.comboBox.Items.AddRange(new object[] {
            "FCFS",
            "SJF",
            "Priority",
            "Round Robin"});
            this.comboBox.Location = new System.Drawing.Point(668, 34);
            this.comboBox.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox.Name = "comboBox";
            this.comboBox.Size = new System.Drawing.Size(156, 38);
            this.comboBox.TabIndex = 3;
            this.comboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // btn_add
            // 
            this.btn_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add.Location = new System.Drawing.Point(70, 300);
            this.btn_add.Margin = new System.Windows.Forms.Padding(2);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(76, 29);
            this.btn_add.TabIndex = 5;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // quantumPanel
            // 
            this.quantumPanel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.quantumPanel.Controls.Add(this.quantumInput);
            this.quantumPanel.Controls.Add(this.label1);
            this.quantumPanel.Location = new System.Drawing.Point(668, 110);
            this.quantumPanel.Margin = new System.Windows.Forms.Padding(2);
            this.quantumPanel.Name = "quantumPanel";
            this.quantumPanel.Size = new System.Drawing.Size(154, 35);
            this.quantumPanel.TabIndex = 6;
            this.quantumPanel.Visible = false;
            // 
            // quantumInput
            // 
            this.quantumInput.Location = new System.Drawing.Point(87, 9);
            this.quantumInput.Margin = new System.Windows.Forms.Padding(2);
            this.quantumInput.Name = "quantumInput";
            this.quantumInput.Size = new System.Drawing.Size(59, 20);
            this.quantumInput.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quantum Time  ";
            // 
            // premPanel
            // 
            this.premPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.premPanel.Controls.Add(this.radbtn_nonprem);
            this.premPanel.Controls.Add(this.radbtn_prem);
            this.premPanel.Enabled = false;
            this.premPanel.Location = new System.Drawing.Point(520, 86);
            this.premPanel.Margin = new System.Windows.Forms.Padding(2);
            this.premPanel.Name = "premPanel";
            this.premPanel.Size = new System.Drawing.Size(124, 58);
            this.premPanel.TabIndex = 7;
            // 
            // radbtn_nonprem
            // 
            this.radbtn_nonprem.AutoSize = true;
            this.radbtn_nonprem.Location = new System.Drawing.Point(14, 30);
            this.radbtn_nonprem.Margin = new System.Windows.Forms.Padding(2);
            this.radbtn_nonprem.Name = "radbtn_nonprem";
            this.radbtn_nonprem.Size = new System.Drawing.Size(117, 19);
            this.radbtn_nonprem.TabIndex = 1;
            this.radbtn_nonprem.TabStop = true;
            this.radbtn_nonprem.Text = "Non-Preemptive";
            this.radbtn_nonprem.UseVisualStyleBackColor = true;
            // 
            // radbtn_prem
            // 
            this.radbtn_prem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.radbtn_prem.AutoSize = true;
            this.radbtn_prem.Location = new System.Drawing.Point(14, 9);
            this.radbtn_prem.Margin = new System.Windows.Forms.Padding(2);
            this.radbtn_prem.Name = "radbtn_prem";
            this.radbtn_prem.Size = new System.Drawing.Size(90, 19);
            this.radbtn_prem.TabIndex = 0;
            this.radbtn_prem.TabStop = true;
            this.radbtn_prem.Text = "Preemptive";
            this.radbtn_prem.UseVisualStyleBackColor = true;
            // 
            // btn_showform2
            // 
            this.btn_showform2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_showform2.Enabled = false;
            this.btn_showform2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_showform2.Location = new System.Drawing.Point(508, 300);
            this.btn_showform2.Margin = new System.Windows.Forms.Padding(2);
            this.btn_showform2.Name = "btn_showform2";
            this.btn_showform2.Size = new System.Drawing.Size(160, 50);
            this.btn_showform2.TabIndex = 9;
            this.btn_showform2.Text = "Show Graph";
            this.btn_showform2.UseVisualStyleBackColor = true;
            this.btn_showform2.Click += new System.EventHandler(this.btn_showform2_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(517, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 29);
            this.label2.TabIndex = 10;
            this.label2.Text = "Schedular Type:";
            // 
            // txtConsole
            // 
            this.txtConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConsole.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.ForeColor = System.Drawing.Color.Lime;
            this.txtConsole.Location = new System.Drawing.Point(58, 369);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(785, 174);
            this.txtConsole.TabIndex = 23;
            this.txtConsole.WordWrap = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Orange;
            this.panel1.Controls.Add(this.rdn_instant);
            this.panel1.Controls.Add(this.rdn_live);
            this.panel1.Location = new System.Drawing.Point(690, 170);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 58);
            this.panel1.TabIndex = 24;
            // 
            // rdn_instant
            // 
            this.rdn_instant.AutoSize = true;
            this.rdn_instant.Location = new System.Drawing.Point(14, 30);
            this.rdn_instant.Margin = new System.Windows.Forms.Padding(2);
            this.rdn_instant.Name = "rdn_instant";
            this.rdn_instant.Size = new System.Drawing.Size(64, 19);
            this.rdn_instant.TabIndex = 1;
            this.rdn_instant.TabStop = true;
            this.rdn_instant.Text = "Instant";
            this.rdn_instant.UseVisualStyleBackColor = true;
            this.rdn_instant.CheckedChanged += new System.EventHandler(this.rdn_instant_CheckedChanged);
            // 
            // rdn_live
            // 
            this.rdn_live.AutoSize = true;
            this.rdn_live.Location = new System.Drawing.Point(14, 9);
            this.rdn_live.Margin = new System.Windows.Forms.Padding(2);
            this.rdn_live.Name = "rdn_live";
            this.rdn_live.Size = new System.Drawing.Size(50, 19);
            this.rdn_live.TabIndex = 0;
            this.rdn_live.TabStop = true;
            this.rdn_live.Text = "Live";
            this.rdn_live.UseVisualStyleBackColor = true;
            this.rdn_live.CheckedChanged += new System.EventHandler(this.rdn_live_CheckedChanged);
            // 
            // btn_rmv
            // 
            this.btn_rmv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_rmv.Location = new System.Drawing.Point(159, 300);
            this.btn_rmv.Margin = new System.Windows.Forms.Padding(2);
            this.btn_rmv.Name = "btn_rmv";
            this.btn_rmv.Size = new System.Drawing.Size(76, 29);
            this.btn_rmv.TabIndex = 25;
            this.btn_rmv.Text = "Remove";
            this.btn_rmv.UseVisualStyleBackColor = true;
            this.btn_rmv.Click += new System.EventHandler(this.btn_rmv_Click);
            // 
            // btn_prc_add
            // 
            this.btn_prc_add.Enabled = false;
            this.btn_prc_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_prc_add.Location = new System.Drawing.Point(652, 251);
            this.btn_prc_add.Margin = new System.Windows.Forms.Padding(2);
            this.btn_prc_add.Name = "btn_prc_add";
            this.btn_prc_add.Size = new System.Drawing.Size(140, 46);
            this.btn_prc_add.TabIndex = 29;
            this.btn_prc_add.Text = "ADD PROCESS";
            this.btn_prc_add.UseVisualStyleBackColor = true;
            this.btn_prc_add.Click += new System.EventHandler(this.btn_prc_add_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.Lime;
            this.button1.Cursor = System.Windows.Forms.Cursors.Default;
            this.button1.Location = new System.Drawing.Point(899, 11);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 47);
            this.button1.TabIndex = 31;
            this.button1.Text = "RESET";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(509, 189);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 38);
            this.label3.TabIndex = 32;
            this.label3.Text = " ";
            // 
            // processBindingSource
            // 
            this.processBindingSource.DataSource = typeof(ProjectOS2.Process);
            // 
            // processPriorityBindingSource
            // 
            this.processPriorityBindingSource.DataSource = typeof(ProjectOS2.ProcessPriority);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(962, 589);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_prc_add);
            this.Controls.Add(this.btn_rmv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_showform2);
            this.Controls.Add(this.premPanel);
            this.Controls.Add(this.quantumPanel);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.comboBox);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gantt Chart";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.quantumPanel.ResumeLayout(false);
            this.quantumPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantumInput)).EndInit();
            this.premPanel.ResumeLayout(false);
            this.premPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processPriorityBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.ComboBox comboBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorityDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource processBindingSource;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.BindingSource processPriorityBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn pidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrivalTimeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn burstTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn waitingTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn turnaroundTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn remainingBurstTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.Panel quantumPanel;
        private System.Windows.Forms.NumericUpDown quantumInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel premPanel;
        private System.Windows.Forms.RadioButton radbtn_nonprem;
        private System.Windows.Forms.RadioButton radbtn_prem;
        private System.Windows.Forms.Button btn_showform2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.Button btn_rmv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Process;
        private System.Windows.Forms.DataGridViewTextBoxColumn ArrivalTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessingTime;
        public System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.RadioButton rdn_instant;
        public System.Windows.Forms.RadioButton rdn_live;
        private System.Windows.Forms.Button btn_prc_add;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
    }
}

