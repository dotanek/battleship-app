using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    /* Up and Left decrease index, Right and Down increase index. */
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    enum ShipLength
    {
        Carrier = 5,
        Battleship = 4,
        Destroyer = 3,
        Submarine = 3,
        PatrolBoat = 2
    }

    // Excusively for front rendering purposes.
    public class Position
    {
        public int X { get; }
        public int Y { get; }
        public Direction Direction { get; }
        public Position(int x, int y, Direction direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }
    }

    public abstract class Ship
    {
        public int Length { get; }
        public int Hits { get; private set; } = 0;

        public Position Position { get; set; } = null;

        public Ship(int length)
        {
            Length = length;
        }

        public bool isSunk()
        {
            return Hits >= Length;
        }

        public void Hit()
        {
            Hits++;
        }
    }

    // Keeping ship types as derivatives (instead just giving them certain length) so that it's possible to diffrenciate between them.
    public class Carrier : Ship
    {
        public Carrier() : base((int)ShipLength.Carrier) { }

    }

    public class Battleship : Ship
    {
        public Battleship() : base((int)ShipLength.Battleship) { }
    }

    public  class Destroyer : Ship
    {
        public Destroyer() : base((int)ShipLength.Destroyer) { }
    }

    public class Submarine : Ship
    {
        public Submarine() : base((int)ShipLength.Submarine) { }
    }

    public class PatrolBoat : Ship
    {
        public PatrolBoat() : base((int)ShipLength.PatrolBoat) { }
    }
}
