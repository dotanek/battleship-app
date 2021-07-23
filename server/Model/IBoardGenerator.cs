using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipAPI.Model
{
    public interface IBoardGenerator
    {
       Board Generate();
    }
}
