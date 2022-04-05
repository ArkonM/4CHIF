using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

namespace EightQueensGA
{
    public partial class Board : UserControl
    {
        private Image queen;
        private int[] genes;
        public int[] Genes
        {
            set 
            { 
                genes = value;
                Refresh();
            } 
        }
        public Board()
        {
            InitializeComponent();
            genes = new int[8];
            ResourceManager resourceManager = new ResourceManager("EightQueensGA.Board", GetType().Assembly);
            Bitmap image = (Bitmap)resourceManager.GetObject("qeen");
            queen = (Image)image;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;                        
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int size = this.Height/8;
            if (genes != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (queen != null)
                    {
                        g.DrawImage(queen, new Rectangle(i * size, (7-genes[i]) * size, size, size));
                    }
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {           
            Graphics g = e.Graphics;
            int size = this.Height/8;
            bool isBlack = true;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Brush b;
                    if(isBlack)
                        b= Brushes.Black;
                    else
                        b = Brushes.White;
                    g.FillRectangle(b,new Rectangle(j*size,i*size,size,size));
                    isBlack = !isBlack;
                }
                isBlack = !isBlack;
            }
        }

        private void Board_Load(object sender, EventArgs e)
        {

        }
    }
}
