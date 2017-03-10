using IMCommonLisa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IMClientFirstDemoLisa
{
    public class ChatWindowManager
    {
        private static Dictionary<int, ChatWindow> _chatWindows = new Dictionary<int, ChatWindow>();

        /// <summary>
        /// 关闭聊天窗口
        /// </summary>
        /// <param name="userID"></param>
        public static void CloseChatWindow(int userID)
        {
            if (_chatWindows.ContainsKey(userID))
            {
                _chatWindows.Remove(userID);
            }
        }

        public static ChatWindow OpenChatWindow(UserInfo fromUser, UserInfo toUser)
        {
            if (!_chatWindows.ContainsKey(toUser.UserID))//先查看有没有
            {
                ChatWindow chatWindow = new ChatWindow(fromUser,toUser);
                _chatWindows.Add(toUser.UserID, chatWindow);
                chatWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                chatWindow.Show();
                return chatWindow;//Lisa 返回窗体
            }
            else//如果有的话，从里面读取出来
            {
                ChatWindow chatWindow = _chatWindows[toUser.UserID];
                if (chatWindow.WindowState == WindowState.Minimized)
                {
                    chatWindow.WindowState = WindowState.Normal;
                }
                chatWindow.Activate();
                return chatWindow;//Lisa 返回窗体
            }


        }

    }
}
