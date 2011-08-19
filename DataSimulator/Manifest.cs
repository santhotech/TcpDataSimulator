using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace DataSimulator
{
    public class Manifest
    {
        public string[] allContents { get; set; }
        public int currentAction { get; set; }
        public ListViewItem listIndex { get; set; }
        public Button ctrlBtn { get; set; }
        public SendDataFromFile sdf { get; set; }
    }
}
