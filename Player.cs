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

        // declare delegates
        public delegate void DungeonEventDelegate();

        // declare events
        public event DungeonEventDelegate OnMonsterEncounter;
        public event DungeonEventDelegate OnBattle;
        public event DungeonEventDelegate OnTrapTriggered;
        public event DungeonEventDelegate OnTreasureFound;

        // raisers
        public void CallMonster() {
            OnMonsterEncounter?.Invoke();
        }
        public void CallBattle() {
            OnBattle?.Invoke();
        }

        public void CallTrap() {
            Console.WriteLine("\nWatch out! There's a tra...");
            OnTrapTriggered?.Invoke();
        }

        public void CallTreasure() {
            Console.WriteLine("\nOh look, it's a treasure chest. Wonder what's inside?");
            OnTreasureFound?.Invoke();
        }
    }
}
