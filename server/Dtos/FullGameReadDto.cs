using BattleshipAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Dtos
{
    public class ShipReadDto
    {
        public string ShipType { get; }
        public int Length { get; }
        public Position Position { get; set; }

        public ShipReadDto(Ship ship)
        {
            // Getting derivative name as string for potantial rendering on client side.
            ShipType = ship.GetType().Name;
            Length = ship.Length;
            Position = ship.Position;
        }
    }

    public class FullGameReadDto
    {
        public List<ShipReadDto> PlayerOneShips { get; }
        public List<ShipReadDto> PlayerTwoShips { get; }
        public List<Move> Moves { get; }

        public FullGameReadDto(Game game)
        {
            PlayerOneShips = game.PlayerOneBoard.Ships.Select(s => new ShipReadDto(s)).ToList();
            PlayerTwoShips = game.PlayerTwoBoard.Ships.Select(s => new ShipReadDto(s)).ToList();
            Moves = game.Moves;
        }
    }
}
