using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using agence_bancaire_Business_Layer.Enums;
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
                _User.Password = request.Password; // Hashed!!
                _User.RoleID = (int)enRole.User;

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

            var list = new List<clsUserDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new clsUserDTO(row.Field<string>("FirstName").Trim(), row.Field<string>("LastName").Trim()
                    , row.Field<string>("PhoneNumber").Trim(), row.Field<string>("Address").Trim(), row.Field<string>("Email").Trim(),
                    row.Field<string>("CIN").Trim(), Convert.ToDateTime(row["DateOfBirth"]), row.Field<string>("Password").Trim()
                    , row.Field<int>("RoleID") 
                    ));

                // list.Add( new clsUser(row.Field<int>("UserID"), row.Field<int>("PersonID"), row["Password"].ToString().Trim()) ) ;
            }   

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetUserID([FromRoute] int id)
        {
            clsUser User = clsUser.Find(id);

            if (User is null) { return NotFound(); }

            var newUser = new clsUserDTO(User.PersonInfo.firstName.Trim(), User.PersonInfo.lastName.Trim()
                    , User.PersonInfo.PhoneNumber.Trim(), User.PersonInfo.Address.Trim(), User.PersonInfo.Email.Trim(),
                   User.PersonInfo.CIN.Trim(), User.PersonInfo.DateOfBirth, User.Password.Trim(), User.RoleID);

            return Ok(newUser);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> ChangePassword([FromRoute] int id, ChangeUserPassword request)
        {
            clsUser user = clsUser.Find(id);
         

            if (user is null) { return NotFound(); }

            user.Password = request.Password;
            // user.PersonID = request.PersonID;

            if (user.Save())
            {
                return Ok(user);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Change Password. Internal server error occurred.");
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
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Delete User. Internal server error occurred.");
            }

        }


    }
}
