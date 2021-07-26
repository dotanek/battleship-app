using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class ConsequentComputerPlayer : IComputerPlayer
    {
        private Random Random { get; } = new Random();
        public Move Decide(Board board)
        {
            List<Move> moves = new List<Move>();


            // Finding field that has an unsunk ship on it and adding all unattacked surrounding fields as targets.
            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    Field field = board.GetFieldByCoordinates(x, y);

                    if (field.Attacked && field.Ship != null && !field.Ship.isSunk())
                    {
                        if (!CheckIfAttacked(x - 1, y, board))
                        {
                            moves.Add(new Move(x - 1, y));
                        }

                        if (!CheckIfAttacked(x + 1, y, board))
                        {
                            moves.Add(new Move(x + 1, y));
                        }

                        if (!CheckIfAttacked(x, y - 1, board))
                        {
                            moves.Add(new Move(x, y - 1));
                        }

                        if (!CheckIfAttacked(x, y + 1, board))
                        {
                            moves.Add(new Move(x, y + 1));
                        }
                    }
                }
            }

            // Choosing from generated moves.
            if (moves.Count != 0)
            {
                return moves[Random.Next(0, moves.Count - 1)];
            }

            // If no such field was found we simply attack random field.
            return MakeRandomMove(board);
        }

        private Move MakeRandomMove(Board board)
        {
            List<Move> moves = new List<Move>();

            for (int x = 0; x < 10; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    if (!board.GetFieldByCoordinates(x, y).Attacked)
                    {
                        moves.Add(new Move(x, y));
                    }
                }
            }

            return moves[Random.Next(0, moves.Count - 1)];
        }

        private bool CheckIfAttacked(int x, int y, Board board)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9)
            {
                // Marking out of bound as attacked so that it doesn't get put in moves list.
                return true;
            }

            return board.GetFieldByCoordinates(x, y).Attacked;
        }
    }
}
