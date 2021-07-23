using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class Game
    {
        public Board PlayerOneBoard { get; set; }
        public Board PlayerTwoBoard { get; set; }

        public Game(Board playerOneBoard, Board playerTwoBoard)
        {
            PlayerOneBoard = playerOneBoard;
            PlayerTwoBoard = playerTwoBoard;
        }
    }
}
