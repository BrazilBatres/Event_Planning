using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPlannerModels
{
    public class Token
    {
        public string token { get; set; }
        public DateTime expirationTime { get; set; }
        public GeneralResult generalResult { get; set; }
    }
}
