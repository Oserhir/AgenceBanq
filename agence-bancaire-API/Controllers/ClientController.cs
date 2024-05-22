using agence_bancaire_API.DTO;
using agence_bancaire_API.Global_Classes;
using agence_bancaire_Business_Layer;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> createClient([FromBody] CreateClientRequestDTO request)
        {

            if(ModelState.IsValid)
            {
                clsClient _Client = new clsClient();
           
                _Client.CreatedByUserID = request.CreatedByUserID;
                _Client.PersonID = request.PersonID;
                _Client.CreatedDate = DateTime.Now;

                try
                {
                    if (_Client.Save())
                    {
                        return CreatedAtAction(nameof(createClient), null);

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create Client. Internal server error occurred.");
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
                }
            }else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            DataTable dt = clsClient.GetAllClients();

            //Map DataTable to DTO
            var list = new List<clsClientDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new clsClientDTO(row.Field<string>("FirstName").Trim(), row.Field<string>("LastName").Trim()
                    , row.Field<string>("PhoneNumber").Trim(), row.Field<string>("Address").Trim(), row.Field<string>("Email").Trim(),
                    row.Field<string>("CIN").Trim(), Convert.ToDateTime(row["CreatedDate"]), Convert.ToDateTime(row["CreatedDate"])
                    ));
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetClientID([FromRoute] int id)
        {
            clsClient client = clsClient.Find(id);

            if (client is null) { return NotFound(); }

            var NewClient =  new clsClientDTO(client.PersonInfo.firstName.Trim(), client.PersonInfo.lastName.Trim()
                    , client.PersonInfo.PhoneNumber.Trim() , client.PersonInfo.Address.Trim(), client.PersonInfo.Email.Trim(),
                    client.PersonInfo.CIN.Trim(), client.PersonInfo.DateOfBirth, client.CreatedDate
                    );

            return Ok(NewClient);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, CreateClientRequestDTO request)
        {
            clsClient client = clsClient.Find(id);

            if (client is null) { return NotFound(); }
            
            client.PersonID = request.PersonID;

            if (client.Save())
            {
                var NewClient = new clsClientDTO(client.PersonInfo.firstName.Trim(), client.PersonInfo.lastName.Trim()
                   , client.PersonInfo.PhoneNumber.Trim(), client.PersonInfo.Address.Trim(), client.PersonInfo.Email.Trim(),
                   client.PersonInfo.CIN.Trim(), client.PersonInfo.DateOfBirth, client.CreatedDate );

                return Ok(NewClient);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Client. Internal server error occurred.");
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int id)
        {
            clsClient client = clsClient.Find(id);

            if (client is null) { return NotFound(); }

            if (clsClient.DeleteClient(id))
            {
                return NoContent(); // Return HTTP 204
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Delete Client. Internal server error occurred.");
            }
        }

    }
}
