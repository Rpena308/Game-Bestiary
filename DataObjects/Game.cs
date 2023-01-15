using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Game
    {
        public string GameID { get; set; }
        public string GameCompanyID { get; set; }
        public string GameVersion { get; set; }
        public bool Active { get; set; }
    }
}
