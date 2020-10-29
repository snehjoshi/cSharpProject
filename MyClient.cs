using Assignment03;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assign04
{
    public class MyClient
    {
        string type;
        Appoint innerClient;
        

        public string Type { get => type; set => type = value; }
        public Appoint InnerClient { get => innerClient; set => innerClient = value; }
    }
}
