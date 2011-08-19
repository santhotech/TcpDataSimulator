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
        Methods val = new Methods();
        Dictionary<string, Manifest> _manifest = new Dictionary<string, Manifest>();
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

        private void AddLogger(string[] sc)
        {
            int prt = Int32.Parse(sc[1]);
            string fileNm = sc[3];
            int delay = Int32.Parse(sc[2]);
            TcpClientActions tca = new TcpClientActions(prt);
            SendDataFromFile sdf = new SendDataFromFile(tca, fileNm, delay);
            AddToListView(sdf, sc);
        }

        private void AddToListView(SendDataFromFile sdf, string[] sc)
        {
            string simNm = sc[0];
            ListViewItem itm = new ListViewItem(sc);
            simList.Items.Add(itm);
            Button b = new Button();
            b.Text = "Start";
            b.BackColor = SystemColors.Control;
            b.Font = this.Font;
            b.Name = simNm;
            b.Click += new EventHandler(b_Actions);
            int cnta = simList.Items.IndexOf(itm);
            simList.AddEmbeddedControl(b, 4, cnta);
            _manifest.Add(simNm, new Manifest { allContents = sc, ctrlBtn = b, currentAction = 0, listIndex = itm, sdf = sdf });
        }

        public void b_Actions(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string n = b.Name;
            SendDataFromFile sdf = _manifest[n].sdf;
            if (_manifest[n].currentAction == 2)
            {
                sdf.StartSendingData();
            }
            else if (_manifest[n].currentAction == 0)
            {
                sdf.StopSendingData();
            }
        }
      
    }
}
