using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using agence_bancaire_DataAccess_Layer;
using Microsoft.AspNetCore.Mvc;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositController : Controller
    {

        [HttpPost]
        public async Task<IActionResult> Deposit([FromBody] DepositDTO request)
        {
            clsdeposit _deposit = new clsdeposit();
            clsCheckingAccount _CheckingAccount = clsCheckingAccount.Find(request.checkingaccount_id);

            if (_CheckingAccount == null)
            {
                return NotFound();
            }
           
            _deposit.amount = request.amount ;
            _deposit.checkingaccount_id = request.checkingaccount_id;
            _deposit.date_operation = DateTime.Now;
            _deposit.LevelID = request.LevelID;

            try
            {
                if (_deposit.Save())
                {
                    return CreatedAtAction(nameof(Deposit), null);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Depose Money. Internal server error occurred.");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
            }

        }

    }
}
