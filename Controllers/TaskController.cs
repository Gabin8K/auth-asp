
using Fullstack.Entities;
using Fullstack.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fullstack
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ILogger<TaskController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public TaskController(ILogger<TaskController> logger, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize]
        public IActionResult listOfTask()
        {
            _logger.Log(LogLevel.Information, "user request list of task");
            string email = User.Identity.Name;
            {
                _logger.Log(LogLevel.Warning, email);
            }
            return Ok(new String[] { });
        }

        [HttpPost]
        public IActionResult newTask(TaskModel taskModel)
        {
            _logger.Log(LogLevel.Information, taskModel.ToString());
            return Accepted(taskModel);
        }
    }
}