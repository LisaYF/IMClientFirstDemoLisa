using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCommonLisa
{
    public class Package
    {
        public Package(DataType type, dynamic data)
        {
            DataType = type;
            Data = data;
        }


        /// <summary>
        /// 包类型
        /// </summary>
        public DataType DataType { get; set; }

        public dynamic Data { get; set; }


        public byte[] GetBytes()
        {
            string json = JsonConvert.SerializeObject(this);
            return Encoding.UTF8.GetBytes(json);
        }

        public static Package GetPackage(byte[] data)
        {
            string sdata = Encoding.UTF8.GetString(data);

            return JsonConvert.DeserializeObject<Package>(sdata);
        }

    }


    public enum DataType
    {
        LoginReq,
        LoginResp,
        GetFrendsReq,
        GetFrendsResp,
        SendMesReq,
        SendMesResp,
        SendState,
        LogoutReq,
        AddUsers,
        AddFriends
    }

}
