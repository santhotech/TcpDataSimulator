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
        Methods val;
        public Form1()
        {
            InitializeComponent();
            DrawHeightsForList();
            val = new Methods();
        }

        private void DrawHeightsForList()
        {            
            ImageList HeightControlImageList = new System.Windows.Forms.ImageList(this.components);
            HeightControlImageList.ImageSize = new System.Drawing.Size(1, 25);
            HeightControlImageList.TransparentColor = System.Drawing.Color.Transparent;
            simList.SmallImageList = HeightControlImageList;     
        }

        

        private void strtBtn_Click(object sender, EventArgs e)
        {
            string[] txtboxStr = new string[] { simNameTxt.Text, prtTxt.Text, delayTxt.Text, fldrNameTxt.Text };
            ValidateLogger(txtboxStr);      
        }

        private void Error(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }   

        private void ValidateLogger(string[] sc)
        {
            if (!(val.ValidateForm(sc)))
            {
                Error("Enter all the fields");
            }           
            else
            {               
                AddLogger(sc);
            }
        }
      
    }
}
