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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_generate = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arrivalTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart
            // 
            this.chart.BorderlineWidth = 0;
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.Area3DStyle.WallWidth = 2;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelAutoFitStyle = System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap;
            chartArea1.AxisX.ScaleBreakStyle.StartFromZero = System.Windows.Forms.DataVisualization.Charting.StartFromZero.No;
            chartArea1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled;
            chartArea1.CursorX.AutoScroll = false;
            chartArea1.CursorX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(0, 0);
            this.chart.Name = "chart";
            this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Pastel;
            this.chart.Size = new System.Drawing.Size(915, 352);
            this.chart.TabIndex = 0;
            this.chart.Text = "chart1";
            this.chart.Click += new System.EventHandler(this.chart1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.arrivalTimeDataGridViewTextBoxColumn,
            this.processTimeDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.processBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 348);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(915, 179);
            this.dataGridView1.TabIndex = 1;
            // 
            // btn_generate
            // 
            this.btn_generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_generate.Location = new System.Drawing.Point(802, 470);
            this.btn_generate.Name = "btn_generate";
            this.btn_generate.Size = new System.Drawing.Size(101, 45);
            this.btn_generate.TabIndex = 2;
            this.btn_generate.Text = "Generate";
            this.btn_generate.UseVisualStyleBackColor = true;
            this.btn_generate.Click += new System.EventHandler(this.btn_generate_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "FCFS",
            "Prememtive SJF",
            "NonPrememtive SJF"});
            this.comboBox1.Location = new System.Drawing.Point(731, 415);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 24);
            this.comboBox1.TabIndex = 3;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "name";
            this.nameDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // arrivalTimeDataGridViewTextBoxColumn
            // 
            this.arrivalTimeDataGridViewTextBoxColumn.DataPropertyName = "arrivalTime";
            this.arrivalTimeDataGridViewTextBoxColumn.HeaderText = "arrivalTime";
            this.arrivalTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.arrivalTimeDataGridViewTextBoxColumn.Name = "arrivalTimeDataGridViewTextBoxColumn";
            this.arrivalTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // processTimeDataGridViewTextBoxColumn
            // 
            this.processTimeDataGridViewTextBoxColumn.DataPropertyName = "processTime";
            this.processTimeDataGridViewTextBoxColumn.HeaderText = "processTime";
            this.processTimeDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.processTimeDataGridViewTextBoxColumn.Name = "processTimeDataGridViewTextBoxColumn";
            this.processTimeDataGridViewTextBoxColumn.Width = 125;
            // 
            // processBindingSource
            // 
            this.processBindingSource.DataSource = typeof(ProjectOS2.Process);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 527);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_generate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.chart);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gantt Chart";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_generate;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn arrivalTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn processTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.BindingSource processBindingSource;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

