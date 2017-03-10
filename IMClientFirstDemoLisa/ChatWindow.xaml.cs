using IMCommonLisa;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace IMClientFirstDemoLisa
{
    /// <summary>
    /// ChatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChatWindow : Window
    {
        UserInfo _chatUserInfo;
        UserInfo _fromUser;

        public ChatWindow(UserInfo fromUser, UserInfo toUser)
        {
            _fromUser = fromUser;
            _chatUserInfo = toUser;
            InitializeComponent();
            this.Title = _chatUserInfo.UserName;

        }
        public void ReceiveMessage(string mes)//接收消息
        {
            Run r1 = new Run(_chatUserInfo.UserName + ":" + DateTime.Now.ToString()); //显示一个文本，名字+当前时间
            r1.Foreground = Brushes.Red;//设置字体颜色
            
            Run r2 = new Run(mes);

            Paragraph p = new Paragraph();//段落
            p.Inlines.Add(r1);
            p.Inlines.Add(r2);
            p.Inlines.Add("\n\r");
            this.Dispatcher.Invoke(() => {
                rtbReceiveMes.Document.Blocks.Add(p);//文档里可以放一些元素,把段落放到文档里面
            });

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            ChatWindowManager.CloseChatWindow(_chatUserInfo.UserID);
        }

        private void btnSend_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Client client = Client.GetClient();
            Package pac = new Package(DataType.SendMesReq, new { FormUserID=_fromUser.UserID, ToUserID = _chatUserInfo.UserID, Message = this.rtbSendMes.Text });
            client.Send(pac.GetBytes());
            rtbReceiveMes.AppendText("我:" + rtbSendMes.Text + "\r\n");
            rtbSendMes.Clear();
        }
    }
}
