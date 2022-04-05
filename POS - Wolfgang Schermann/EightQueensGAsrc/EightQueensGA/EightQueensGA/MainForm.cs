using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EightQueensGA
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            GeneticAlgo geneticAlgo = new GeneticAlgo();
            List<Chromosome> initPopulation = GetInitialPopulation((int)txtPop.Value);
            if(chkPorgress.Checked)
                geneticAlgo.progress +=new Progress(updateProgress);
            progressBar1.Maximum = (int)txtGen.Value;
            progressBar1.Value = 0;
            geneticAlgo.DoMating(ref initPopulation, (int)txtGen.Value, (double)txtCrosProb.Value, (double)txtMutProb.Value);
            
            dgResults.Rows.Clear();
            for (int i = 0; i < initPopulation.Count - 1; i++)
            {
                String sol = "| ";
                for (int j = 0; j < 8; j++)
                {
                    sol = sol + initPopulation[i].genes[j] + " | ";
                }
                dgResults.Rows.Add(new Object[] { sol, initPopulation[i].fitness });    
                          
            }
            board1.Genes = initPopulation[0].genes;
        }

        private void updateProgress(int progress)
        {

            progressBar1.Value = progress;

            int percent = (int)(((double)(progressBar1.Value - progressBar1.Minimum) /
            (double)(progressBar1.Maximum - progressBar1.Minimum)) * 100);
            using (Graphics gr = progressBar1.CreateGraphics())
            {
                gr.DrawString(percent.ToString() + "%",
                    SystemFonts.DefaultFont,
                    Brushes.Black,
                    new PointF(progressBar1.Width / 2 - (gr.MeasureString(percent.ToString() + "%",
                        SystemFonts.DefaultFont).Width / 2.0F),
                    progressBar1.Height / 2 - (gr.MeasureString(percent.ToString() + "%",
                        SystemFonts.DefaultFont).Height / 2.0F)));
            }
        }

        private List<Chromosome> GetInitialPopulation(int population)
        {
            List<Chromosome> initPop = new List<Chromosome>();
            GeneticAlgo RandomGen = new GeneticAlgo();
            for (int i = 0; i < population; i++)
            {
                List<int> genes = new List<int>(new int[] {0, 1, 2, 3, 4, 5, 6, 7});
                Chromosome chromosome = new Chromosome();
                chromosome.genes = new int[8];
                for (int j = 0; j < 8; j++)
                {
                    int geneIndex = (int)(RandomGen.GetRandomVal(0,genes.Count-1)+0.5);
                    chromosome.genes[j] = genes[geneIndex];
                    genes.RemoveAt(geneIndex);
                }

                initPop.Add(chromosome);
            }
            return initPop;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }
              
    }
}
