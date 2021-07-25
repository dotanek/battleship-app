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
        public PlayerNumber Player { get; set; }

        public Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Move(int x, int y, PlayerNumber player)
        {
            X = x;
            Y = y;
            Player = player;
        }
    }
}
