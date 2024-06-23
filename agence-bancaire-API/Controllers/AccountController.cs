using agence_bancaire_API.DTO;
using agence_bancaire_API.Global_Classes;
using agence_bancaire_Business_Layer;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> createAccount([FromBody] CreateAccountRequestDTO request)
        {

            if(ModelState.IsValid)
            {
                // 
                int clientId = request.ClientID;
                //float Balance = request.Balance ?? 0;
                //int overdraftLimit = request.overdraftLimit ?? 1000;

                if (!clsClient.isClientExist(clientId))
                {
                    return StatusCode(StatusCodes.Status404NotFound, $"The clientId {clientId} does not exist.");
                }

                // IsClientHaveAccount
                if (clsAccount.isAccountExistByClientID(clientId))
                {
                    return StatusCode(StatusCodes.Status409Conflict, $"This client already has an account.");
                }

                clsAccount _Account = new clsAccount();

                _Account.AccountNumber = util.GenerateGUID();
                _Account.ClientID = request.ClientID;
                _Account.CreatedDate = DateTime.Now;

                try
                {
                    if (_Account.Save())
                    {
                        return CreatedAtAction(nameof(createAccount), new { id = _Account.AccountNumber }, _Account);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create Account. Internal server error occurred.");
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
                }

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            DataTable dt = clsAccount.GetAllAccounts();

            // Map DataTable to DTO
            var list = new List<clsAccount>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(
                    new clsAccount(
                        row.Field<int>("AccountID"),
                        row.Field<int>("ClientID"),
                        row.Field<Guid>("AccountNumber"),  
                        Convert.ToDateTime(row["CreatedDate"])
                    )
                );
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAccountID([FromRoute] int id)
        {
            clsAccount Account = clsAccount.Find(id);

            if (Account is null) { return NotFound(); }

            return Ok(Account);
        }

        //[HttpPut]
        //[Route("{id:int}")]
        //public async Task<IActionResult> UpdateAccount([FromRoute] int id, CreateAccountRequestDTO request)
        //{
        //    clsAccount Account = clsAccount.Find(id);

        //    if (Account is null) { return NotFound(); }

        //    Account.ClientID = request.ClientID;

        //    if (Account.Save())
        //    {
        //        return Ok(Account);
        //    }
        //    else
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Account. Internal server error occurred.");
        //    }

        //}

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] int id)
        {
            clsAccount Account = clsAccount.Find(id);

            if (Account is null) { return NotFound(); }

            if (clsAccount.DeleteAccount(id))
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Delete Account. Internal server error occurred.");
            }
        }
    }
}
