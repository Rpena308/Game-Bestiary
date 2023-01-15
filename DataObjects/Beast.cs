using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class Beast
    {
        public int BeastID { get; set; }
        public string GameID { get; set; }
        public string AlignmentID { get; set; }
        public string BeastTypeID { get; set; }
        public string BeastSubTypeID { get; set; }
        public string TerrainID { get; set; }
        public string BeastSizeID { get; set; }
        public string BeastName { get; set; }
        public int ChallengeRating { get; set; }
        public string Treasure { get; set; }
        public int Experience { get; set; }
        public string BeastDescription { get; set; }
        public bool Active { get; set; }
    }
}
