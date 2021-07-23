using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    enum PlacementType
    {
        Horizontal,
        Vertical
    }

    class Placement
    {
        public PlacementType Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }


        public Placement(PlacementType type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
        }
    }

    public class RandomBoardGenerator : IBoardGenerator
    {
        public Random Random { get; set; }

        public RandomBoardGenerator()
        {
            Random = new Random();
        }

        public Board Generate()
        {
            Board board = new Board();

            IEnumerable<ShipType> shipTypes = Enum.GetValues(typeof(ShipType)).Cast<ShipType>();

            throw new NotImplementedException();
        }

        private void Place(ShipType shipType, Board board)
        {
            List<Placement> possiblePlacements = new List<Placement>();
        }

        /*private List<Placement> GenerateHorizontalPlacements(ShipType shipType, Board board)
        {
            
        }*/
    }
}
