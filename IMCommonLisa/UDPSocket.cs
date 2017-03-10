using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IMCommonLisa
{
    public class UDPSocket
    {
        private UdpClient _udpClient;


        private Thread _thread;

        public void Run(int port)
        {
            _udpClient = new UdpClient(port);

            _thread = new Thread(Receive);
            //_thread.IsBackground = true;
            _thread.Start();
        }

        private void Receive()
        {
            while (true)
            {
                try
                {
                    IPEndPoint ip = null;
                    byte[] data = _udpClient.Receive(ref ip);
                    ProcessData(data, ip);
                }
                catch (Exception exp)
                {
                    Console.WriteLine("接收数据异常，异常信息：" + exp.Message);
                }

            }
        }

        public void Send(byte[] data, IPEndPoint ip)
        {
            try
            {
                _udpClient.Send(data, data.Length, ip);
            }
            catch (Exception exp)
            {
                Console.WriteLine("发送数据异常，异常信息：" + exp.Message);
            }

        }

        public virtual void ProcessData(byte[] data, IPEndPoint ip)
        {

        }

    }
}
