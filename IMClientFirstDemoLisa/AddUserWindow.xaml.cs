using IMCommonLisa;
using System.Windows;

namespace IMClientFirstDemoLisa
{

    /// <summary>
    /// AddUserWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddUserWindow : Window
    {
        private Client client = Client.GetClient();
        public AddUserWindow()
        {
            InitializeComponent();
            client.AddUsers += Client_AddUsers;
        }
        private void Client_AddUsers(object obj, Package pac)
        {
            if (pac.Data.State != 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    //DialogResult = true;
                    this.Close();
                    MessageBox.Show("注册成功");
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    //DialogResult = false;
                    MessageBox.Show("注册失败");
                });
            }
        }
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            string UserID = tbxaddid.Text;
            string UserName = tbxaddname.Text;
            string Sex = comboBox.Text;
            string Password = tbxaddpassword.Text;
            if (string.IsNullOrEmpty(UserID))
            {
                return;
            }
            if (string.IsNullOrEmpty(UserName))
            {
                return;
            }
            if (string.IsNullOrEmpty(Sex))
            {
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                return;
            }

            Package p =
                new Package(DataType.AddUsers,
                new { UserID = UserID, UserName = UserName, Sex = Sex, Password = Password });

            client.Send(p.GetBytes());
        }
    }
}
