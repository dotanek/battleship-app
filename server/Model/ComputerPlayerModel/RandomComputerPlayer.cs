using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{

    public class RandomComputerPlayer : IComputerPlayer
    {
        Random Random { get; } = new Random();
        public Move Decide(Board board)
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
    }
}
