using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler {
    internal class Player {
        public int PlayerID {  get; set; }
        public string Name { get; set; }
        public int Health { get; set; } = 100;
        public int Score { get; set; } = 0;
    }
}
