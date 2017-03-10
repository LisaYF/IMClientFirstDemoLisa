using IMCommonLisa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace IMServer
{
    public class UserManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserInfo Login(string name, string password)
        {
            UserInfo user = null;
            string connString = @"Data Source=.;Initial Catalog=im;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "select * from UserInfo where LoginID = '" + name + "' and Password='" + password + "'";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new UserInfo();
                            user.UserID = (int)reader["Id"];
                            user.UserName = reader["UserName"].ToString();
                            user.LoginID = reader["LoginID"].ToString();
                            user.Sex = reader["Sex"].ToString();
                        }
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// 获取用户好友列表
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static List<UserInfo> GetFrends(int userID)
        {
            List<UserInfo> list = new List<UserInfo>();

            string connString = @"Data Source=.;Initial Catalog=im;Integrated Security=True";
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "select * from UserInfo,Frends where UserInfo.Id=Frends.FrendID and Frends.Id = " + userID;
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserInfo user = new UserInfo();
                            user.UserID = (int)reader["Id"];
                            user.UserName = reader["UserName"].ToString();
                            user.LoginID = reader["LoginID"].ToString();
                            user.Sex = reader["Sex"].ToString();
                            list.Add(user);
                        }
                    }
                }
            }

            return list;
        }
        public static bool addUser(Package pac)
        {
            string sql = string.Format("insert into UserInfo(LoginID,UserName,Sex,Password)values('{0}','{1}','{2}','{3}')", pac.Data.UserID, pac.Data.UserName, pac.Data.Sex, pac.Data.Password);
            string sql2 = string.Format("select * from UserInfo where LoginID='{0}'", pac.Data.UserID);
            string conn = @"Data Source=.;Initial Catalog=im;Integrated Security=True";
            bool isHava;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command2 = new SqlCommand(sql2, connection))
                {
                    using (var reader = command2.ExecuteReader())
                    {
                        isHava = reader.Read();

                    }
                }
            }

            if (!isHava)
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();

                    }

                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public static bool addFriend(int userId,int friendId)
        {
            string sql = string.Format("insert into Frends(Id,FrendID)values('{0}','{1}'),('{2}','{3}')", userId, friendId, friendId, userId);
            string sql2 = string.Format("select * from Frends where Id='{0}' and FrendID='{1}'", userId,friendId);
            string conn = @"Data Source=.;Initial Catalog=im;Integrated Security=True";
            bool isHava;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command2 = new SqlCommand(sql2, connection))
                {
                    using (var reader = command2.ExecuteReader())
                    {
                        isHava = reader.Read();

                    }
                }
            }
            if (!isHava)
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                        return true;
                    }

                }
            }return false;
            

        }


    }
}
