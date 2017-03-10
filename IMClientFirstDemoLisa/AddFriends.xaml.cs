using IMCommonLisa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IMClientFirstDemoLisa
{
    /// <summary>
    /// AddFriends.xaml 的交互逻辑
    /// </summary>
    public partial class AddFriends : Window
    {
        private Client client = Client.GetClient();
        private UserInfo user;
        public AddFriends(UserInfo user)
        {
            this.user = user;
            InitializeComponent();
            client.AddFriends += addFriend_Success;
        }
        private void addFriend_Success(object obj,Package pac)
        {
            
            if(((int)pac.Data.state)==1)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //DialogResult = true;
                    this.Close();
                    MessageBox.Show("添加好友成功！");
                });
                //获取新添加的好友列表
                Package pac1 = new Package(DataType.GetFrendsReq,
                new { UserID = user.UserID });
                client.Send(pac1.GetBytes());
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    //DialogResult = false;
                    MessageBox.Show("他已经是你的好友了!");
                });
            }
            
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            Package pac = new Package(DataType.AddFriends,new {UserId=user.UserID, FriendId= textBox.Text });
            client.Send(pac.GetBytes());
        }
    }
}
