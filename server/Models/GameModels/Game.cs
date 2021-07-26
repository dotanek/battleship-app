using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class Game
    {
        public Board PlayerOneBoard { get; }
        public Board PlayerTwoBoard { get; }
        public List<Move> Moves { get; } = new List<Move>();

        public Game(Board playerOneBoard, Board playerTwoBoard)
        {
            PlayerOneBoard = playerOneBoard;
            PlayerTwoBoard = playerTwoBoard;
        }

        public bool IsFinished()
        {
            if (PlayerOneBoard.IsDefeated() || PlayerTwoBoard.IsDefeated())
            {
                return true;
            }

            return false;
        }
    }
}
