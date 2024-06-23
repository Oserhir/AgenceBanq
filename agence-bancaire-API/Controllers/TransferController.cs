using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        private static readonly object _lock = new object();

        [HttpPost]
        public async Task<IActionResult> Transfer([FromBody] TransferDTO request)
        {

            if(request.Amount <= 0)
            {
                return BadRequest("amount must be greater than zero.");
            }

            if ( !clsCheckingAccount.isCheckingAccountExist(request.checkingaccount_id) || !clsCheckingAccount.isCheckingAccountExist(request.targetAccount_id))
            {
                return NotFound("One or both accounts not found.");
            }

            if (request.checkingaccount_id == request.targetAccount_id)
            {
                return NotFound("Transaction failed. Please ensure the source and destination accounts are different.");
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

                var tasks = new List<Task<bool>>();

                lock (_lock)
                {
                    tasks.Add(Task.Run(async () => await _Trandfer.Save()));
                }
                try
                {
                    var results = await Task.WhenAll(tasks);

                    if (Array.TrueForAll(results, result => result))
                    {
                        return CreatedAtAction(nameof(Transfer), null);
                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to transfer money. Internal server error occurred.");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
                }

                //try
                //{
                //    if (_Trandfer.Save())
                //    {
                //        return CreatedAtAction(nameof(Transfer), null);
                //    }
                //    else
                //    {
                //        return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to Transfer Money. Internal server error occurred.");
                //    }

                //}
                //catch (Exception ex)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError, $"{ex}");
                //}

            }
            else
            {
                return BadRequest("Insufficient funds including overdraft limit.");
            }

        }

    }
}
