using IMCommonLisa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private Client client = Client.GetClient();
        //private Label lb;//Lisa
       
        public LoginWindow()//Lisa Label lb
        {
            InitializeComponent();
            client.LoginResp += Client_LoginResp;
            //this.lb = lb;//Lisa
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            string password = pbPassword.Password;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名或密码不能为空");
            }
            //this.lb.Content =name;//Lisa

            Package pac = new Package(DataType.LoginReq,
                new
                {
                    Name = name,
                    Password = password
                });

            client.Send(pac.GetBytes());

        }

        public UserInfo UserInfo { get; set; }


        private void Client_LoginResp(object arg1, Package p)
        {
            if (p.Data.Status == 1)
            {
                UserInfo = JsonConvert.DeserializeObject<UserInfo>(p.Data.UserInfo.ToString());
                //UserInfo = p.Data.UserInfo;


                this.Dispatcher.Invoke(() => {
                    this.DialogResult = true;
                    this.Close();
                });
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            client.LoginResp -= Client_LoginResp;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            addUserWindow.ShowDialog();
        }
    }
}
