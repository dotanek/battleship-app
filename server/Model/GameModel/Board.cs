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
        public Field[] Fields { get; }

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

        // FOR DEBUG PURPOSES TODO REMOVE
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Fields.Length; i++)
            {
                Field field = Fields[i];
                if (field.Attacked)
                {
                    if (field.Ship != null)
                    {
                        stringBuilder.Append(" # ");
                    }
                    else
                    {
                        stringBuilder.Append(" x ");
                    }
                }
                else
                {
                    if (field.Ship != null)
                    {
                        stringBuilder.Append(" " + field.Ship.ToString() + " ");
                    }
                    else
                    {
                        stringBuilder.Append(" - ");
                    }
                }

                if ((i + 1) % 10 == 0)
                {
                    stringBuilder.Append("\n");
                }
            }

            return stringBuilder.ToString();
        }
    }
}
