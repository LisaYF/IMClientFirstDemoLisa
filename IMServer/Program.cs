using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Server server = new Server();
            server.Run(8889);
            Console.WriteLine("服务启动");
        }
    }
}
