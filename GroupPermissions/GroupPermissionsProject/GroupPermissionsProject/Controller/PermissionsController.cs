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
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionsRepository _dbContext;
        public PermissionsController(IPermissionsRepository permissionsRepository)
        {
            _dbContext = permissionsRepository;
        }

        // GET: api/Permissions/GetAllPermissionss
        [HttpGet]
        [Route("API/[controller]/[action]")]
        [AllowAnonymous]
        public ActionResult GetAllPermissions()
        {
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = _dbContext.GetAll().OrderBy(x => x.userGroup.ID), messages = null, errors = null });
        }

        // GET api/Permissions/GetPermissionById
        [HttpGet]
        [Route("API/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        public ActionResult GetPermissionById(int id, [FromHeader] string language = "en")
        {
            Permission permission = _dbContext.GetPermissionById(id);

            if (permission == null)
            {
                return BadRequest(new ResponseModel { messages = language == "ar" ? "غير موجود" : "Not Found" });
            }
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = permission });
        }

        // GET api/Permissions/GetPermissionById
        [HttpGet]
        [Route("API/[controller]/[action]")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [AllowAnonymous]
        public ActionResult GetPermissionsByGroupId(int Groupid, [FromHeader] string language = "en")
        {
            var data = _dbContext.GetPermissionsByGroupId(Groupid);
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = data, messages = null, errors = null });
        }

        // POST  api/Permissions/UpdatePermission
        [HttpPost]
        [Route("API/[controller]/[action]")]
        public IActionResult UpdatePermission([FromBody] Permission permission)
        {
            try
            {
                _dbContext.Update(permission);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(permission.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = permission, messages = null, errors = null });
        }

        // POST: api/Permission/AddPermission
        [HttpPost]
        [Route("API/[controller]/[action]")]
        public IActionResult AddPermission([FromBody] Permission permission)
        {
            _dbContext.Insert(permission);
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = permission, messages = null, errors = null });
        }

        // DELETE: api/Permission/DeletePermission
        [HttpGet]
        [Route("API/[controller]/[action]")]
        public ActionResult<Permission> DeletePermission(int ID)
        {
            var permission = _dbContext.GetById(ID);
            if (permission == null)
            {
                return NotFound();
            }
            _dbContext.Delete(permission);
            return Ok(new ResponseModel { status = Status.ok.ToString(), data = permission, messages = null, errors = null });
        }

        private bool PermissionExists(int ID)
        {
            return _dbContext.Exists(ID);
        }
    }
}
