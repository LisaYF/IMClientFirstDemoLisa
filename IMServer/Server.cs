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


namespace IMServer
{
    public class Server : UDPSocket
    {
        private static Dictionary<int, UserInfo> _onlineUsers =
            new Dictionary<int, UserInfo>();


        public override void ProcessData(byte[] data, IPEndPoint ip)
        {
            Console.WriteLine("online:");
            foreach(KeyValuePair<int, UserInfo> kvp in _onlineUsers)
            {
                Console.WriteLine(kvp.Key);
            }
            Package pac = Package.GetPackage(data);
            if (pac.DataType == DataType.LoginReq)
            {
                dynamic dataResp = null;
                UserInfo user = UserManager.Login(
                    pac.Data.Name.ToString(),
                    pac.Data.Password.ToString());

                if (user != null)
                {
                    user.Ip = ip;
                    if (_onlineUsers.ContainsKey(user.UserID))
                    {

                        _onlineUsers[user.UserID] = user;
                        //TODO 通知其他用户在其他地方登陆
                    }
                    else
                    {
                        _onlineUsers.Add(user.UserID, user);
                    }
                    sendState(user, "1");
                    dataResp = new { Status = 1, UserInfo = user };
                }
                else
                {
                    dataResp = new { Status = 0,msg="登录错误，用户或密码不存在!" };
                }
                Package resp = new Package(DataType.LoginResp, dataResp);
                Send(resp.GetBytes(), ip);

            }
            else if (pac.DataType == DataType.GetFrendsReq)
            {
                int userid = pac.Data.UserID;

                List<UserInfo> list = UserManager.GetFrends(userid);
                foreach(UserInfo x in list)
                {
                    if(!_onlineUsers.Keys.Contains( x.UserID))x.UserState = "0";
                    else x.UserState = "1";
                }
                Package usersPac = new Package(DataType.GetFrendsResp, list);

                Send(usersPac.GetBytes(), ip);

            }
            else if (pac.DataType == DataType.SendMesReq)
            {
                int toUser = pac.Data.ToUserID;
                IPEndPoint Ip=null;
                foreach (var x in _onlineUsers)
                {
                    if (x.Key == toUser) Ip = x.Value.Ip;
                }
                Package sendpac = new Package(DataType.SendMesResp,pac.Data);
                Send(sendpac.GetBytes(), Ip);
            }
            else if (pac.DataType == DataType.LogoutReq)
            {
                sendState(_onlineUsers[(int)pac.Data.UserID], "0");
                _onlineUsers.Remove((int)pac.Data.UserID);
            }
            else if (pac.DataType == DataType.AddUsers)
            {
                int state = 0;
                if (UserManager.addUser(pac)) { state = 1; }
                Send(new Package(DataType.AddUsers, new { State = state }).GetBytes(), ip);
            }
            else if (pac.DataType == DataType.AddFriends)
            {
                if(UserManager.addFriend((int)pac.Data.UserId, (int)pac.Data.FriendId))
                {
                    Send(new Package(DataType.AddFriends, new { state = 1 }).GetBytes(), ip);
                }
                else
                {
                    Send(new Package(DataType.AddFriends, new { state = 0 }).GetBytes(), ip);
                }
            }
        }
        private void sendState(UserInfo user,string state)
        {
            Package statePac = new Package(
                        DataType.SendState,
                    new { UserID = user.UserID, State = state });
            //查找好友
            List<UserInfo> list = UserManager.GetFrends(user.UserID);
            foreach (var item in list)
            {
                if (_onlineUsers.ContainsKey(item.UserID))
                {
                    var ipFrend = _onlineUsers[item.UserID].Ip;
                    Send(statePac.GetBytes(), ipFrend);//告诉好友已经上线
                }
            }
        }

    }
}
