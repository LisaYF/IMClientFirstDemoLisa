using IMCommonLisa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IMClientFirstDemoLisa
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 好友用户列表
        /// </summary>
        private List<UserInfo> _userList = new List<UserInfo>();

        public UserInfo CurrentUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Client client = Client.GetClient();
            Random ran = new Random(); //随机数
            int port= ran.Next(10000, 50000);
            while (PortInUse(port)) port = ran.Next(10000, 50000);
            client.Run(port);
            login();
            label.Content = CurrentUser.UserName;//返回名字
        }
        private void login()
        {
            Client client = Client.GetClient();
            LoginWindow login = new LoginWindow();//Lisa this.label
            //login.Show();//Lisa
            bool? result = login.ShowDialog();
            if (result != true)
            {
                this.Close();
            }
            CurrentUser = login.UserInfo;
            //获取好友列表
            client.GetFrendsResp += Client_GetFrendsResp;
            //收到消息
            client.ReceiveMessage += Client_ReveiveMessage;

            //好友上下线通知
            client.SendStateResp += Client_SendStateResp; 
            Package pac = new Package(DataType.GetFrendsReq,
                new { UserID = CurrentUser.UserID });
            client.Send(pac.GetBytes());
        }
        internal static bool PortInUse(int port)
        {
            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
        private void Client_SendStateResp(object arg1,Package pac)
        {
            int userID = pac.Data.UserID;
            foreach (UserInfo u in listBoxUsers.ItemsSource)
            {
                if (u.UserID == userID) u.UserState = pac.Data.State;
            }
            this.Dispatcher.Invoke(() => {
                listBoxUsers.ItemsSource = listBoxUsers.ItemsSource;
            });
        }
        private void Client_ReveiveMessage(object arg1, Package pac)
        {
            int userID = pac.Data.FormUserID;
            //获取用户信息
            UserInfo fromUser = null;
            foreach (var item in _userList)
            {
                if (item.UserID == userID)
                {
                    fromUser = item;
                }
            }
            this.Dispatcher.Invoke(() => {
                var chatWindow = ChatWindowManager.OpenChatWindow(CurrentUser, fromUser);//传递给ChatWindow这个窗体
                chatWindow.ReceiveMessage(pac.Data.Message.ToString());//动态类型不能使用提示
            });
        }
        private void logout()
        {
            Client client = Client.GetClient();
            Package pac = new Package(DataType.LogoutReq,
                new { UserID = CurrentUser.UserID });
            client.Send(pac.GetBytes());
        }
        private void Client_GetFrendsResp(object arg1, Package pac)
        {
            _userList = JsonConvert.DeserializeObject<List<UserInfo>>(pac.Data.ToString());

            this.Dispatcher.Invoke(() => {
                listBoxUsers.ItemsSource = _userList;
            });
        }

        private void listBoxUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            UserInfo user = (UserInfo)item.Content;
            ChatWindowManager.OpenChatWindow(CurrentUser, user);
        }

        private void listBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            logout();
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {
            logout();
            login();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddFriends add = new AddFriends(CurrentUser);
            add.ShowDialog();
        }
    }
}
