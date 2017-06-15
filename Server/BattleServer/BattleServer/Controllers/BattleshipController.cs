using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Game.Enums;
using Game.Extensions;
using Game.Models;

namespace BattleServer.Controllers
{

    [RoutePrefix("")]
    public class StartGameController : ApiController
    {   [HttpGet]
        [Route("start_game")]
        public OkNegotiatedContentResult<Grid> start_game()
        {
            var game = new Game.Game(10, 10);
            String[,] startGrid = game.ToArray();
            var returableGrid = new Grid {grid = startGrid};

            return Ok(returableGrid);
        }

        [HttpPost]
        [Route("next_turn")]
        public IEnumerable<String> next_turn()
        {
            return new String[] { "nxt", "turn" };

            
        }

        [HttpPost]
        [Route("end_game")]
        public IEnumerable<String> end_game()
        {
            return new String[] { "end", "game" };
        }

        [HttpGet]
        [Route("end_game")]
        public IEnumerable<String> game_state()
        {
            return new String[] { "game", "state" };
        }
    }

    
}