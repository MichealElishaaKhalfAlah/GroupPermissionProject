using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupPermissionsProject.Models.Response
{
    public class ResponseModel
    {
        public object data { get; set; }
        public object messages { get; set; }
        public string status { get; set; }
        public object errors { get; set; }
    }
}
