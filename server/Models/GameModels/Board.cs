using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class Board
    {
        public List<Ship> Ships { get; set; }
        
        /* Must be 100 length at all times */
        private Field[] Fields { get; }

        public Board()
        {
            Ships = new List<Ship>();
            Fields = new Field[100];

            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = new Field();
            }
        }

        public Field GetFieldByCoordinates(int x, int y)
        {
            return Fields[y * 10 + x];
        }

        public bool IsDefeated()
        {
            foreach (Ship ship in Ships)
            {
                if (!ship.isSunk())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
