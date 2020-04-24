using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcProxy.Models
{
    public class Session
    {
        public Guid AuthID { get; set; }
        public Guid SessionKey { get; set; }
    }
}
