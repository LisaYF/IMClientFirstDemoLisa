using IMCommonLisa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IMClientFirstDemoLisa
{
    public class Client : UDPSocket
    {
        private static Client client = new Client();


        public static Client GetClient()
        {
            return client;
        }



        public event Action<object, Package> LoginResp;

        public event Action<object, Package> GetFrendsResp;
        public event Action<object, Package> ReceiveMessage;

        public event Action<object, Package> SendStateResp;
        public event Action<object, Package> AddUsers;
        public event Action<object, Package> AddFriends;
        public override void ProcessData(byte[] data, IPEndPoint ip)
        {
            Package pac = Package.GetPackage(data);

            if (pac.DataType == DataType.LoginResp)
            {
                LoginResp?.Invoke(this, pac);
            }
            else if (pac.DataType == DataType.GetFrendsResp)
            {
                GetFrendsResp?.Invoke(this, pac);
            }
            else if (pac.DataType == DataType.SendState)
            {
                SendStateResp?.Invoke(this, pac);
            }
            else if (pac.DataType == DataType.SendMesResp)
            {
                ReceiveMessage?.Invoke(this, pac);
            }
            else if (pac.DataType == DataType.AddUsers)
            {
                AddUsers?.Invoke(this, pac);
            }
            else if (pac.DataType == DataType.AddFriends)
            {
                AddFriends?.Invoke(this, pac);
            }
        }
        public void Send(byte[] data)
        {
            Send(data, new IPEndPoint(IPAddress.Parse(Tools.getConfig("serverip")), int.Parse(Tools.getConfig("serverport"))));
        }

    }
}
