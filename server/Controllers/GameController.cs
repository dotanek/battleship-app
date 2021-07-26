using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BattleshipAPI.Model;
using BattleshipAPI.Dtos;

namespace BattleshipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameManager _gameManager;

        public GameController(IBoardGenerator boardGenerator, IComputerPlayer computerPlayer)
        {
            _gameManager = new GameManager(boardGenerator, computerPlayer);
        }

        // GET api/[controller]/simulate
        [HttpGet("simulate")]
        public ActionResult<FullGameReadDto> SimulateGame()
        {
            try
            {
                Game game = _gameManager.SimulateGame();
                FullGameReadDto gameRead = new FullGameReadDto(game);

                return Ok(gameRead);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
