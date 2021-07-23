using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public enum ShipType { 
        Carrier = 5,
        Battleship = 4,
        Cruiser = 3,
        Submarine = 3,
        Destroyer = 2
    }

    public class ShipTypeDictionary
    {
    
    }

    /* Up and Left decrease index, Right and Down increase index. */
    public enum ShipDirection
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Ship
    {
        public ShipType Type { get; }
        public int Hits { get; set; }

        /* Coordinates and direction mainly for rendering purposes on the client side */
        public int X { get; }
        public int Y { get; }
        public ShipDirection Direction { get; }

        public Ship(ShipType type, int x, int y, ShipDirection direction)
        {
            Hits = 0;

            Type = type;
            X = x;
            Y = y;
            Direction = direction;
        }

        public bool isSunk()
        {
            return Hits >= (int)Type;
        }

        public void Hit()
        {
            Hits++;
        }
    }
}
