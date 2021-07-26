using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public enum PlayerNumber {
        One = 1,
        Two = 2
    }
    public class Move
    {
        public int X { get; }
        public int Y { get; }
        public bool Hit { get; set; } = false;
        public bool Sink { get; set; } = false;
        public bool Win { get; set; } = false;
        public PlayerNumber Player { get; set; }

        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
