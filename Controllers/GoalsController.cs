using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GoalsController : ControllerBase
    {

        private readonly GoalsService _goalsService;

        public GoalsController(GoalsService goalsService)
        {
            _goalsService = goalsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Goal>> Get()
        {
            try
            {
                return Ok(_goalsService.Get());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Goal> Get(int id)
        {
            try
            {
                return Ok(_goalsService.Get(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost("{todoId}")]
        public async Task<ActionResult<Goal>> CreateAsync(int todoId, [FromBody] Goal goal)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                goal.TodoId = todoId;
                goal.CreatorId = userInfo.Id;
                return Ok(_goalsService.Create(goal));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Goal>> Edit(int id, [FromBody] Goal goal)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                goal.CreatorId = userInfo.Id;
                goal.Id = id;
                return Ok(_goalsService.Edit(goal));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}