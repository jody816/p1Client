using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace p1Client.Class
{
    class ChattingRoomViewModel : ChattingRoomViewModelBase
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream;

        public RelayCommand SendBtn { get; set; }

        public ChattingRoomViewModel()
        {
            SendBtn = new RelayCommand(Send);

            clientSocket.Connect(UserIp.Ip, 7777);
            serverStream = clientSocket.GetStream();

            byte[] outByte = Encoding.UTF8.GetBytes(UserIp.User + "$"); // 아이디
            serverStream.Write(outByte, 0, outByte.Length);
            serverStream.Flush();

            Thread clThread = new Thread(getMessage);
            clThread.Start();
        }

        private void getMessage()
        {
            try
            {
                if (clientSocket.Connected == true)
                {
                    byte[] inBytes = new byte[clientSocket.ReceiveBufferSize];
                    MsgBox += "client connected!" + Environment.NewLine;
                    while (true)
                    {
                        using (NetworkStream serverStream = clientSocket.GetStream())
                        {
                            int length;
                            try
                            {
                                while ((length = serverStream.Read(inBytes, 0, clientSocket.ReceiveBufferSize)) != 0)
                                {
                                    var incommingData = new byte[length];
                                    Array.Copy(inBytes, 0, incommingData, 0, length);
                                    string serverMsg = Encoding.UTF8.GetString(incommingData);
                                    MsgBox += serverMsg + Environment.NewLine;
                                    if (serverMsg.Contains("님"))
                                        getplz(MsgBox);
                                }
                            }
                            catch (Exception e)
                            {
                                clientSocket.Close();
                                MsgBox += "connection close" + Environment.NewLine;
                            }
                        }
                    }
                }
            }
            catch (Exception qe)
            {
                MessageBox.Show("Socket exception: " + qe);
            }
        }

        void getplz(string p)
        {
            try
            {
                string[] a = p.Split('\'');

                if (a[a.Length - 2] != null && User == null)
                {
                    User = a[a.Length - 2];
                }
                else if (a[a.Length - 4] != null)
                {
                    User = a[a.Length - 4];
                    User2 = a[a.Length - 2];
                }

                if(User2 == null)
                {
                    UserImg1 = "C:/Users/jody8/source/repos/p1Client/Images/턱괴짤.jpg";
                }
                else
                {
                    UserImg1 = "C:/Users/jody8/source/repos/p1Client/Images/턱괴짤.jpg";
                    UserImg2 = "C:/Users/jody8/source/repos/p1Client/Images/턱괴짤.jpg";
                }
            }
            catch (Exception f)
            {
                MessageBox.Show("미치겠다 진짜로");
            }
        }

        //void getId()
        //{
        //    try
        //    {
        //        byte[] inBytes = new byte[clientSocket.ReceiveBufferSize];
        //        using (NetworkStream serverStream = clientSocket.GetStream())
        //        {
        //            int length = serverStream.Read(inBytes, 0, clientSocket.ReceiveBufferSize);
        //            var incommingData = new byte[length];
        //            Array.Copy(inBytes, 0, incommingData, 0, length);
        //            string serverMsg = Encoding.UTF8.GetString(incommingData);
        //            if(serverMsg.Contains("님이 입장함"))
        //            {
        //                string[] plz = serverMsg.Split('님');

        //                if(User == null)
        //                {
        //                    User = plz[0];
        //                }
        //                else if(User != null)
        //                {
        //                    User2 = plz[0];
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception qe)
        //    {
        //        MessageBox.Show("Socket exception: " + qe);
        //    }
        //}

        private void Send(object obj)
        {
            if (clientSocket == null)
            {
                return;
            }

            try
            {
                string clientMsg = SendBox;
                byte[] outByte = Encoding.UTF8.GetBytes(clientMsg + "$");
                serverStream.Write(outByte, 0, outByte.Length);
                serverStream.Flush();
                //MsgBox += "[" + UserIp.User + "] " + clientMsg + Environment.NewLine;
            }
            catch (SocketException se)
            {
                MessageBox.Show("Socket exception: " + se);
            }
            SendBox = "";
        }
    }
}
