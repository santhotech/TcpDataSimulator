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
            SendDataFromFile sdf = new SendDataFromFile(tca, fileNm, delay, sc[0]);
            sdf.SimStatusChanged +=new SendDataFromFile.SimStatusChangedEventHandler(sdf_SimStatusChanged);
            AddToListView(sdf, sc);
        }

        public void sdf_SimStatusChanged(bool flag, string name)
        {            
            if (flag == true)
            {
                _manifest[name].currentAction = 2;
                SimViewActions(name, false, true, "Stop", "Active", Color.DarkGreen);
            }
            if (flag == false)
            {
                _manifest[name].currentAction = 0;
                SimViewActions(name, true, true, "Start", "Stopped", Color.Red);
            }
        }


        private void AddToListView(SendDataFromFile sdf, string[] sc)
        {
            string simNm = sc[0];
            string[] scList = new string[] { sc[0], sc[1], sc[2], "Stopped" };
            ListViewItem itm = new ListViewItem(scList);
            simList.Items.Add(itm);
            Button b = new Button();
            b.Text = "Start";
            b.BackColor = SystemColors.Control;
            b.Font = this.Font;
            b.Name = simNm;
            b.Click += new EventHandler(b_Actions);
            int cnta = simList.Items.IndexOf(itm);
            simList.AddEmbeddedControl(b, 4, cnta);
            simList.Items[cnta].ForeColor = Color.Red;
            _manifest.Add(simNm, new Manifest { allContents = sc, ctrlBtn = b, currentAction = 0, listIndex = itm, sdf = sdf });
        }

        public void b_Actions(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            string n = b.Name;
            SendDataFromFile sdf = _manifest[n].sdf;
            if (_manifest[n].currentAction == 2)
            {
                sdf.StopSendingData();
            }
            else if (_manifest[n].currentAction == 0)
            {
                sdf.StartSendingData();
            }
        }

        public void Form_closing(object sender, CancelEventArgs cargs)
        {
            Environment.Exit(0);
        }

        private void fldrBrwsBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fldrNameTxt.Text = openFileDialog1.FileName;
            }   
        }

        public void SimViewActions(string n, bool delBtn, bool ctrlBtn, string btnTxt, string lstTxt, Color lstColor)
        {
            Button b = _manifest[n].ctrlBtn;            
            ListViewItem indexItm = _manifest[n].listIndex;
            int index = 0;
            simList.BeginInvoke((MethodInvoker)(() => index = simList.Items.IndexOf(indexItm)));            
            b.BeginInvoke((MethodInvoker)(() => b.Enabled = ctrlBtn));
            b.BeginInvoke((MethodInvoker)(() => b.Text = btnTxt));
            simList.BeginInvoke((MethodInvoker)(() => simList.Items[index].SubItems[3].Text = lstTxt));
            simList.BeginInvoke((MethodInvoker)(() => simList.Items[index].ForeColor = lstColor));
        }   
      
    }
}
