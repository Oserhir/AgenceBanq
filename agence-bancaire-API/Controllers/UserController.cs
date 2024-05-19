using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> createUser([FromBody] CreateUserRequestDTO request)
        {
            clsUser _User = new clsUser();

            _User.PersonID = request.PersonID;
            _User.Password = request.Password;

            try
            {
                if (_User.Save())
                {
                    return CreatedAtAction(nameof(createUser), new { id = _User.UserID }, _User);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create User. Internal server error occurred.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            DataTable dt = clsUser.GetAllUsers();

            var list = new List<clsUser>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add( new clsUser(row.Field<int>("UserID"), row.Field<int>("PersonID"), row["Password"].ToString().Trim()) ) ;
            }   

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserID([FromRoute] int id)
        {
            clsUser User = clsUser.Find(id);

            if (User is null) { return NotFound(); }

            return Ok(User);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, CreateUserRequestDTO request)
        {
            clsUser user = clsUser.Find(id);

            if (user is null) { return NotFound(); }

            user.Password = request.Password;
            user.PersonID = request.PersonID;

            if (user.Save())
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update User. Internal server error occurred.");
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            clsUser User = clsUser.Find(id);

            if (User is null) { return NotFound(); }

            if (clsUser.DeleteUser(id))
            {
                return Ok(User);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Delete User. Internal server error occurred.");
            }

        }


    }
}
