using agence_bancaire_API.DTO;
using agence_bancaire_API.Global_Classes;
using agence_bancaire_Business_Layer;
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
            clsClient _Client = new clsClient();
            clsAccount _Account = new clsAccount();

            _Client.CreatedByUserID = request.CreatedByUserID;
            _Client.PersonID = request.PersonID;
            _Client.CreatedDate = request.CreatedDate;

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

        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            DataTable dt = clsClient.GetAllClients();

            // Map DataTable to DTO
            var list = new List<clsClient>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new clsClient(row.Field<int>("PersonID"), row.Field<int>("ClientID")
                    , row.Field<int>("CreatedByUserID"), Convert.ToDateTime(row["CreatedDate"])  ));
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetClientID([FromRoute] int id)
        {
            clsClient client = clsClient.Find(id);

            if (client is null) { return NotFound(); }

            return Ok(client);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateClient([FromRoute] int id, CreateClientRequestDTO request)
        {
            clsClient client = clsClient.Find(id);

            if (client is null) { return NotFound(); }

            client.CreatedDate = request.CreatedDate;
            client.PersonID = request.PersonID;
            client.CreatedDate = request.CreatedDate;

            if (client.Save())
            {
                return Ok(client);
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
                return Ok(client);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Client. Internal server error occurred.");
            }
        }

    }
}
