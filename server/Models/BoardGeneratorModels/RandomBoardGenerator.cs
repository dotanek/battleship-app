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

    struct Placement
    {
        public PlacementType Type { get; }
        public int X { get; }
        public int Y { get; }


        public Placement(PlacementType type, int x, int y)
        {
            Type = type;
            X = x;
            Y = y;
        }
    }

    public class RandomBoardGenerator : IBoardGenerator
    {
        public Random Random { get; } = new Random();

        public Board Generate()
        {
            Board board = new Board();

            List<Ship> ships = new List<Ship>()
            {
                new Carrier(),
                new Battleship(),
                new Destroyer(),
                new Submarine(),
                new PatrolBoat(),
                new PatrolBoat(),
            };

            PlaceAll(ships, board);
            board.Ships = ships;

            return board;
        }

        // Backtracking algorithm.
        private bool PlaceAll(List<Ship> ships, Board board)
        {
            // If list is empty then all ships have been placed.
            if (ships.Count == 0)
            {
                return true;
            }

            // "Popping" first ship. 
            Ship ship = ships.First();
            List<Ship> rest = new List<Ship>(ships.GetRange(1, ships.Count - 1));

            // Generating and randomizing possible placements.
            List<Placement> placements = new List<Placement>();
            placements.AddRange(GeneratePlacements(ship.Length, board, PlacementType.Horizontal));
            placements.AddRange(GeneratePlacements(ship.Length, board, PlacementType.Vertical));

            // No placements were found.
            if (placements.Count == 0)
            {
                return false;
            }

            ShuffleList(placements);

            foreach (Placement placement in placements)
            {
                // Placing the ship.
                ApplyPlacement(ship, board, placement, true);

                /* Using recursion to place the rest of the ships.
                 * If recursion returns true then all ships were placed succesfully and we can return a value,
                 * otherwise we move on to revert the placement and try another possible placement. */
                if (PlaceAll(rest, board))
                {
                    return true;
                }

                // Reverting placement.
                ApplyPlacement(ship, board, placement, false);
            }

            // If none of the recursive calls returned true then it is impossible to fit given amount of ships on the board.
            return false;
        }

        private void ApplyPlacement(Ship ship, Board board, Placement placement, bool placing)
        {
            for (int i = 0; i < ship.Length; i++)
            {
                Field field;

                if (placement.Type == PlacementType.Horizontal)
                {
                    field = board.GetFieldByCoordinates(placement.X + i, placement.Y);
                }
                else
                {
                    field = board.GetFieldByCoordinates(placement.X, placement.Y + i);
                }

                if (placing)
                {
                    field.Ship = ship;

                    // Randomizing the direction.
                    Direction direction;

                    if (placement.Type == PlacementType.Horizontal)
                    {
                        direction = (Random.Next(0,1) == 0) ? Direction.Left : Direction.Right;
                    }
                    else
                    {
                        direction = (Random.Next(0, 1) == 0) ? Direction.Up : Direction.Down;
                    }

                    int x = placement.X;
                    int y = placement.Y;

                    // If the direction is left or up the coordinates of the start (back) of the ship need to be moved accordingly.
                    if (direction == Direction.Left)
                    {
                        x += (ship.Length - 1);
                    }
                    else if (direction == Direction.Up)
                    {
                        y += (ship.Length - 1);
                    }

                    ship.Position = new Position(x, y, direction);
                 
                }
                else
                {
                    field.Ship = null;
                    ship.Position = null;
                }
            }
        }

        private void ShuffleList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int swapIndex = Random.Next(0, list.Count - 1);

                T temp = list[i];
                list[i] = list[swapIndex];
                list[swapIndex] = temp;
            }
        }

        /* Generates a list of coodrinates that are not occupied (or in range) of another ship. */
        private List<Placement> GeneratePlacements(int length, Board board, PlacementType type)
        {
            // Board size is always 10x10 so the last possible index is 10 - (length - 1).
            List<Placement> placements = new List<Placement>();
            int range = 10 - (length - 1);

            for (int a = 0; a < range; a++)
            {
                for (int b = 0; b < 10; b++)
                {
                    bool unoccupied = true;

                    /* Fields around ship must be unoccupied aswell so we expand into every direction by 1, 
                     * the following conditions take care of edge cases (ship next to the boards start/end). */
                    int iStart = (a == 0) ? 0 : a - 1;
                    int iEnd = (a == range - 1) ? 9 : a + length;
                    int jStart = (b == 0) ? 0 : b - 1;
                    int jEnd = (b == 9) ? 9 : b + 1;

                    for (int i = iStart; i <= iEnd; i++)
                    {
                        for (int j = jStart; j <= jEnd; j++)
                        {
                            if (type == PlacementType.Horizontal)
                            {
                                if (board.GetFieldByCoordinates(i, j).Ship != null)
                                {
                                    unoccupied = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (board.GetFieldByCoordinates(j, i).Ship != null)
                                {
                                    unoccupied = false;
                                    break;
                                }
                            }
                        }

                        // Breaking from the second loop.
                        if (!unoccupied)
                        {
                            break;
                        }
                    }

                    if (unoccupied)
                    {
                        if (type == PlacementType.Horizontal)
                        {
                            placements.Add(new Placement(type, a, b));
                        }
                        else
                        {
                            placements.Add(new Placement(type, b, a));
                        }
                    }
                }
            }

            return placements;
        }
    }
}
