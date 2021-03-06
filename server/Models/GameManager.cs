using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class GameManager
    {
        private readonly IBoardGenerator _generator;
        private readonly IComputerPlayer _computer;

        public GameManager(IBoardGenerator generator, IComputerPlayer computer)
        {
            _generator = generator;
            _computer = computer;
        }

        public Game CreateGame(Board playerOneBoard = null, Board playerTwoBoard = null)
        {
            // If board is not provided it's generated using given generator.
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

        public Move GetComputerMove(Game game, PlayerNumber player)
        {
            if (player == PlayerNumber.One)
            {
                return _computer.Decide(game.PlayerTwoBoard);
            }
            else
            {
                return _computer.Decide(game.PlayerOneBoard);
            }
        }

        // Returns information if move was a hit.
        public void ApplyMove(Move move, Game game, PlayerNumber player)
        {
            move.Player = player;

            Board board;

            if (player == PlayerNumber.One)
            {
                board = game.PlayerTwoBoard;
            }
            else
            {
                board = game.PlayerOneBoard;
            }

            Field target = board.GetFieldByCoordinates(move.X, move.Y);

            game.Moves.Add(move);

            if (target.Attack())
            {
                move.Hit = true;

                // If the ship has been sunk all the fields around it are marked as attacked.
                if (target.Ship.isSunk())
                {
                    move.Sink = true;
                    MarkSurroundings(move, board);

                    if (board.IsDefeated())
                    {
                        move.Win = true; 
                    }
                }
            }
        }

        public void MarkSurroundings(Move move, Board board)
        {
            // Finding where the ship starts and ends and expanding the one in each direction.
            int xStart = move.X;
            int xEnd = move.X;
            int yStart = move.Y;
            int yEnd = move.Y;

            while (board.GetFieldByCoordinates(xStart, move.Y).Ship != null && xStart > 0)
            {
                xStart--;
            }

            while (board.GetFieldByCoordinates(xEnd, move.Y).Ship != null && xEnd < 9)
            {
                xEnd++;
            }

            while (board.GetFieldByCoordinates(move.X, yStart).Ship != null && yStart > 0)
            {
                yStart--;
            }

            while (board.GetFieldByCoordinates(move.X, yEnd).Ship != null && yEnd < 9)
            {
                yEnd++;
            }

            // Marking all the fields as attacked.
            for (int x = xStart; x <= xEnd; x++)
            {
                for (int y = yStart; y <= yEnd; y++)
                {
                    Field field = board.GetFieldByCoordinates(x, y);
                    if (!field.Attacked)
                    {
                        field.Attack();
                    }
                }
            }
        }

        public Game SimulateGame()
        {
            Game game = CreateGame();

            bool unfinished = true;

            while (unfinished)
            {
                PlayerNumber player;

                if (game.Moves.Count == 0 || game.Moves.Last().Player == PlayerNumber.One)
                {
                    player = PlayerNumber.Two;
                }
                else
                {
                    player = PlayerNumber.One;
                }

                Move move = GetComputerMove(game, player);

                ApplyMove(move, game, player);
                {
                    if (move.Win)
                    {
                        unfinished = false;
                    }
                }
            }

            return game;
        }
    }
}
