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
    public class TodosController : ControllerBase
    {
        private readonly TodosService _tdService;
        private readonly GoalsService _goalsService;

        public TodosController(TodosService tdService, GoalsService goalsService)
        {
            _tdService = tdService;
            _goalsService = goalsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Todo>> Get()
        {
            try
            {
                return Ok(_tdService.Get());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            try
            {
                return Ok(_tdService.Get(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}/goals")]
        public ActionResult<IEnumerable<TodoGoal>> GetGoals(int id)
        {
            try
            {
                return Ok(_goalsService.GetByTodo(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateAsync([FromBody] Todo todo)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                todo.CreatorId = userInfo.Id;
                return Ok(_tdService.Create(todo));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> Edit(int id, [FromBody] Todo todo)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                todo.CreatorId = userInfo.Id;
                todo.Id = id;
                return Ok(_tdService.Edit(todo));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _tdService.Delete(id, userInfo.Id);
                return Ok("Deleted.");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}