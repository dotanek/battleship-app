using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public class Field
    {
        public bool Attacked { get; private set; }
        public Ship Ship { get; set; }

        public Field(Ship ship = null)
        {
            Attacked = false;
            Ship = ship;
        }

        public bool Attack()
        {
            Attacked = true;

            if (Ship != null)
            {
                Ship.Hit();
                return true;
            }

            return false;
        }
    }
}
