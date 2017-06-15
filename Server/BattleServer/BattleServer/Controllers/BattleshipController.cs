using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BattleServer.Controllers
{

    [RoutePrefix("api")]
    public class StartGameController : ApiController
    {   [HttpGet]
        [Route("start_game")]
        public IEnumerable<String> start_game()
        {
            return new String[] { "value1", "value2" };
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
    }
}