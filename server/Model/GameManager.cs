using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class GameManager
    {
        private readonly IBoardGenerator _generator;

        public GameManager(IBoardGenerator generator)
        {
            _generator = generator;
        }

        public Game CreateGame(Board playerOneBoard = null, Board playerTwoBoard = null)
        {
            if (playerOneBoard == null)
            {
                playerOneBoard = _generator.Generate();
            }

            if (playerTwoBoard == null)
            {
                playerTwoBoard = _generator.Generate();
            }

            return new Game(playerOneBoard, playerTwoBoard);
        }
    }
}
