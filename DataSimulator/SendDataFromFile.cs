using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;


namespace DataSimulator
{
    class SendDataFromFile
    {
        bool sendFlag = false;
        TcpClientActions tca;
        private string fileName;
        private int delay;
        public delegate void SimStatusChangedEventHandler(bool curFlag);
        public event SimStatusChangedEventHandler SimStatusChanged;
        public bool SendFlag
        {
            get { return this.sendFlag; }
            set
            {
                this.sendFlag = value;
                if (SimStatusChanged != null)
                {
                    this.SimStatusChanged(sendFlag);
                }
            }
        }
        public SendDataFromFile(TcpClientActions mTcp, string mFileName, int mDelay)
        {
            tca = mTcp;
            fileName = mFileName;
            delay = mDelay;
        }
        Thread t;
        public void StartSendingData()
        {
            t = new Thread(new ThreadStart(SendData));
            t.Start();
        }
        public void SendData()
        {
            this.SendFlag = true;

            StreamReader sr = new StreamReader(fileName);            
            while (sendFlag)
            {
                string read = string.Empty;
                try
                {
                    while ((read = sr.ReadLine()) != null)
                    {
                        try
                        {                            
                            tca.SendDataToClients(read);
                            Thread.Sleep(delay);
                            if (sr.EndOfStream == true)
                            {
                                sr = new StreamReader(fileName);
                            }
                        }
                        catch
                        {
                            this.SendFlag = false;
                            break;
                        }
                    }
                }
                catch
                {
                    this.SendFlag = false;
                    break;
                }
            }
        }
        public void StopSendingData()
        {
            this.SendFlag = false;
            t.Abort();
        }
    }
}
