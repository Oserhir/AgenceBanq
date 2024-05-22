using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using Microsoft.AspNetCore.Mvc;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO request)
        {
            if ( !clsCheckingAccount.isCheckingAccountExist(request.checkingaccount_id) || !clsCheckingAccount.isCheckingAccountExist(request.targetAccount_id))
            {
                return NotFound("One or both accounts not found.");
            }

            clsCheckingAccount _CheckingAccount = clsCheckingAccount.Find(request.checkingaccount_id);

            clsTransfer _Trandfer = new clsTransfer();

            bool transfer = (_CheckingAccount.Balance > request.Amount) ? true : _CheckingAccount.CanApplyOverdraft(request.Amount) ?
                  true : false;

            if (transfer)
            {
                _Trandfer.targetAccount_id = request.targetAccount_id;
                _Trandfer.checkingaccount_id = request.checkingaccount_id;
                _Trandfer.date_operation = DateTime.Now;
                _Trandfer.Amount = request.Amount;

                try
                {
                    if (_Trandfer.Save())
                    {
                        return CreatedAtAction(nameof(Transfer), null);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Transfer Money. Internal server error occurred.");
                    }

                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
                }

            }else
            {
                return BadRequest("Insufficient funds including overdraft limit.");
            }

        }

    }
}
