using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcProxy.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string PrivateKey { get; set; }
        public string PublicKey { get; set; }
    }
}
