using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckingAccountController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> createCheckingAccount([FromBody] CreateChekingAccountDTO request)
        {
            // is AccoundID EXIST
            int accountId = request.Account_Id;

            if ( !clsAccount.isAccountExist(accountId)  )
            {
                return StatusCode(StatusCodes.Status404NotFound, $"Account with ID '{accountId}' was not found.");
            }

            // is this Account has already cheking account
            if (clsCheckingAccount.IsCheckingAccounttExistByAccountIdAsync(accountId))
            {
                return StatusCode(StatusCodes.Status409Conflict, $"Account with ID '{accountId}' already has a checking account.");
            }

            clsCheckingAccount _CheckingAccount = new clsCheckingAccount();

            _CheckingAccount.Account_Id = request.Account_Id;
            _CheckingAccount.overdraftLimit = request.overdraftLimit ?? 1000;
            _CheckingAccount.CreatedDate = DateTime.Now;
            _CheckingAccount.Balance = request.Balance ?? 0;

            try
            {
                if (_CheckingAccount.Save())
                {
                    return CreatedAtAction(nameof(createCheckingAccount), null);

                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create CheckingAccount. Internal server error occurred.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllCheckingAccount()
        {
            DataTable dt = clsCheckingAccount.GetAllCheckingAccount();

            // Map DataTable to DTO
            var list = new List<CheckingAccountDTO>();

   
            foreach (DataRow row in dt.Rows)
            {

                list.Add(new CheckingAccountDTO(
                     row.Field<int>("AccountID"),
                   Convert.ToDateTime(row["CreatedDate"])
                    , row.Field<float>("Balance"), row.Field<float>("overdraftLimit")
                 
                    ));
            }

            return Ok(list);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetChecking_Account_ID([FromRoute] int id)
        {
            clsCheckingAccount _chekingAccount = clsCheckingAccount.Find(id);

            if (_chekingAccount is null) { return NotFound(); }

            return Ok(_chekingAccount);
        }

        [HttpPut]
        [Route("overdraftLimit/{id:int}")]
        public async Task<IActionResult> UpdateOverdraftLimit([FromRoute] int id, UpdateOverdraftLimitDTO request)
        {
            float overdraftLimit = request.overdraftLimit;

            if (overdraftLimit < 0 )
            {
                return BadRequest("overdraftLimit must be non-negative values.");
            }

            clsCheckingAccount _CheckingAccount = clsCheckingAccount.Find(id);

            if (_CheckingAccount is null) { return NotFound(); }
           
            _CheckingAccount.overdraftLimit = overdraftLimit;

            if (_CheckingAccount.Update_Checking_Account_OverdraftLimit())
            {
                return Ok(_CheckingAccount);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Update Balance. Internal server error occurred.");
            }

        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCheckingAccount([FromRoute] int id)
        {
            clsCheckingAccount _CheckingAccount = clsCheckingAccount.Find(id);

            if (_CheckingAccount is null) { return NotFound(); }

            if (clsCheckingAccount.DeleteCheckingAccount(id))
            {
                return NoContent();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Delete Checking Account. Internal server error occurred.");
            }
        }


    }
}
