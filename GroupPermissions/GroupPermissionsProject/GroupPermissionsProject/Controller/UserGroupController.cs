using GroupPermissionsProject.Core.Entities;
using GroupPermissionsProject.Core.Interfaces;
using GroupPermissionsProject.Infrastructure.Helper;
using GroupPermissionsProject.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroupPermissionsProject.Controller
{
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupRepository _dbContext;
        public UserGroupController(IUserGroupRepository userGroupRepository)
        {
            _dbContext = userGroupRepository;
        }

        // GET: api/UserGroup/GetAllUserGroups
        [HttpGet]
        [Route("API/[controller]/[action]")]
        [AllowAnonymous]
        public ActionResult GetAllUserGroups()
        {
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = _dbContext.GetAll().OrderBy(x => x.ArabicName), messages = null, errors = null });
        }

        // GET api/UserGroup/GetUserGroupById
        [HttpGet]
        [Route("API/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        public ActionResult GetUserGroupById(int id, [FromHeader] string language = "en")
        {
            UserGroup userGroup = _dbContext.GetUserGroupById(id);

            if (userGroup == null)
            {
                return BadRequest(new ResponseModel { messages = language == "ar" ? "غير موجود" : "Not Found" });
            }
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = userGroup });
        }

        // POST  api/UserGroup/UpdateUserGroup
        [HttpPost]
        [Route("API/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        public IActionResult UpdateUserGroup([FromBody] UserGroup userGroup)
        {
            try
            {
                _dbContext.Update(userGroup);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserGroupExists(userGroup.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = userGroup, messages = null, errors = null });
        }

        // POST: api/UserGroup/AddUserGroup
        [HttpPost]
        [Route("API/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        public IActionResult AddUserGroup([FromBody] UserGroup userGroup)
        {
            _dbContext.Insert(userGroup);
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = userGroup, messages = null, errors = null });
        }

        // DELETE: api/UserGroup/DeleteUserGroup
        [HttpGet]
        [Route("API/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        public ActionResult<UserGroup> DeleteUserGroup(int ID)
        {
            var userGroup = _dbContext.GetById(ID);
            if (userGroup == null)
            {
                return NotFound();
            }
            _dbContext.Delete(userGroup);
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = userGroup, messages = null, errors = null });
        }

        private bool UserGroupExists(int ID)
        {
            return _dbContext.Exists(ID);
        }
    }
}
