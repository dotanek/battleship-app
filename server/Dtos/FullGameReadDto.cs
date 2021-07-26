using BattleshipAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Dtos
{
    public class FullGameReadDto
    {
        public List<Ship> PlayerOneShips { get; }
        public List<Ship> PlayerTwoShips { get; }
        public List<Move> Moves { get; }

        public FullGameReadDto(List<Ship> playerOneShips, List<Ship> playerTwoShips, List<Move> moves)
        {
            PlayerOneShips = playerOneShips;
            PlayerTwoShips = playerTwoShips;
            Moves = moves;
        }

        public FullGameReadDto(Game game)
        {
            PlayerOneShips = game.PlayerOneBoard.Ships;
            PlayerTwoShips = game.PlayerTwoBoard.Ships;
            Moves = game.Moves;
        }
    }
}
