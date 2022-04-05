namespace EightQueensGA
{
    partial class MainForm
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
            this.btnStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPop = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGen = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCrosProb = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMutProb = new System.Windows.Forms.NumericUpDown();
            this.dgResults = new System.Windows.Forms.DataGridView();
            this.colSol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColFit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.chkPorgress = new System.Windows.Forms.CheckBox();
            this.board1 = new EightQueensGA.Board();
            ((System.ComponentModel.ISupportInitialize)(this.txtPop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrosProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMutProb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(284, 122);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(60, 24);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Population Size";
            // 
            // txtPop
            // 
            this.txtPop.Location = new System.Drawing.Point(224, 18);
            this.txtPop.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.txtPop.Name = "txtPop";
            this.txtPop.Size = new System.Drawing.Size(120, 20);
            this.txtPop.TabIndex = 2;
            this.txtPop.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Generations";
            // 
            // txtGen
            // 
            this.txtGen.Location = new System.Drawing.Point(224, 44);
            this.txtGen.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtGen.Name = "txtGen";
            this.txtGen.Size = new System.Drawing.Size(120, 20);
            this.txtGen.TabIndex = 2;
            this.txtGen.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Crossover Probability";
            // 
            // txtCrosProb
            // 
            this.txtCrosProb.DecimalPlaces = 2;
            this.txtCrosProb.Location = new System.Drawing.Point(224, 70);
            this.txtCrosProb.Name = "txtCrosProb";
            this.txtCrosProb.Size = new System.Drawing.Size(120, 20);
            this.txtCrosProb.TabIndex = 2;
            this.txtCrosProb.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Mutation Probability";
            // 
            // txtMutProb
            // 
            this.txtMutProb.DecimalPlaces = 2;
            this.txtMutProb.Location = new System.Drawing.Point(224, 96);
            this.txtMutProb.Name = "txtMutProb";
            this.txtMutProb.Size = new System.Drawing.Size(120, 20);
            this.txtMutProb.TabIndex = 2;
            this.txtMutProb.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // dgResults
            // 
            this.dgResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colSol,
            this.ColFit});
            this.dgResults.Location = new System.Drawing.Point(12, 152);
            this.dgResults.Name = "dgResults";
            this.dgResults.RowHeadersVisible = false;
            this.dgResults.Size = new System.Drawing.Size(332, 347);
            this.dgResults.TabIndex = 3;
            // 
            // colSol
            // 
            this.colSol.FillWeight = 235F;
            this.colSol.HeaderText = "Solution";
            this.colSol.Name = "colSol";
            this.colSol.Width = 235;
            // 
            // ColFit
            // 
            this.ColFit.FillWeight = 50F;
            this.ColFit.HeaderText = "Fitness";
            this.ColFit.Name = "ColFit";
            this.ColFit.Width = 50;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(33, 122);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(245, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(347, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "The Best Solution";
            // 
            // chkPorgress
            // 
            this.chkPorgress.AutoSize = true;
            this.chkPorgress.Location = new System.Drawing.Point(15, 125);
            this.chkPorgress.Name = "chkPorgress";
            this.chkPorgress.Size = new System.Drawing.Size(16, 15);
            this.chkPorgress.TabIndex = 6;
            this.chkPorgress.UseVisualStyleBackColor = true;
            // 
            // board1
            // 
            this.board1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.board1.Location = new System.Drawing.Point(350, 34);
            this.board1.Name = "board1";
            this.board1.Size = new System.Drawing.Size(467, 465);
            this.board1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(826, 511);
            this.Controls.Add(this.chkPorgress);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.board1);
            this.Controls.Add(this.dgResults);
            this.Controls.Add(this.txtMutProb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCrosProb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGen);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPop);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "8 Qeens Puzzle - Solve with GA";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtPop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCrosProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMutProb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown txtPop;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown txtGen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtCrosProb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown txtMutProb;
        private System.Windows.Forms.DataGridView dgResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSol;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColFit;
        private Board board1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkPorgress;
    }
}

