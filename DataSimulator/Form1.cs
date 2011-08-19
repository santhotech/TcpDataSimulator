using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;


namespace DataSimulator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawHeightsForList();
        }

        private void DrawHeightsForList()
        {            
            ImageList HeightControlImageList = new System.Windows.Forms.ImageList(this.components);
            HeightControlImageList.ImageSize = new System.Drawing.Size(1, 25);
            HeightControlImageList.TransparentColor = System.Drawing.Color.Transparent;
            simList.SmallImageList = HeightControlImageList;     
        }

      
    }
}
