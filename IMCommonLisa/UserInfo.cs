using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IMCommonLisa
{
    public class UserInfo : INotifyPropertyChanged
    {
        private int _userID;

        private string _loginID;

        //private string _password;

        private string _userName;

        private string _userState;

        private string _sex;

        private string _image;

        private IPEndPoint _ip;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 用户名
        /// </summary>
        public int UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                _userID = value;
            }
        }
        /// <summary>
        /// 登录ID
        /// </summary>
        public string LoginID
        {
            get
            {
                return _loginID;
            }

            set
            {
                _loginID = value;
            }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                _userName = value;
            }
        }
        /// <summary>
        /// 用户状态 1在线 0离线
        /// </summary>
        public string UserState
        {
            get
            {
                return _userState;
            }

            set
            {
                _userState = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DisplayState"));
            }
        }

        public string DisplayState
        {
            get
            {
                if (_userState == "0")
                {
                    return "（离线）";
                }
                else
                {
                    return "（在线）";
                }
            }
        }

        /// <summary>
        /// 性别 男，女
        /// </summary>
        public string Sex
        {
            get
            {
                return _sex;
            }

            set
            {
                _sex = value;
            }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string Image
        {
            get
            {
                if (Sex == "男")
                    return "Images/male.png";
                else
                    return "Images/female.png";
            }

            set
            {
                _image = value;
            }
        }

        [JsonIgnore]
        public IPEndPoint Ip
        {
            get
            {
                return _ip;
            }

            set
            {
                _ip = value;
            }
        }
    }
}
