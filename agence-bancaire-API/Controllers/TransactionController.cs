using agence_bancaire_API.DTO;
using agence_bancaire_Business_Layer;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace agence_bancaire_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTransaction()
        {
            DataTable dt = clsTransaction.GetAllTransaction();

            // Map DataTable to DTO
            var list = new List<GetAllTransactionDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(
                    new GetAllTransactionDTO(
                        row.Field<int>("ID"),
                        row.Field<int>("AccountId"),
                        row.Field<float>("Amount"),
                        Convert.ToDateTime(row["TransactionDate"]),
                        row.Field<string>("Type").Trim(),
                        row.Field<int>("idOperation"),
                        row.Field<string>("Operation").Trim(),
                        row.IsNull("targetAccount_id") ? (int?)null : row.Field<int>("targetAccount_id")
                    )
                );
            }

            return Ok(list);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetAllTransaction([FromRoute] int id)
        {
            int accountId = id;

            if(!clsAccount.isAccountExist(accountId))
            {
                return NotFound();
            }

            DataTable dt = clsTransaction.GetAllTransaction(accountId);

            // Map DataTable to DTO
            var list = new List<GetAllTransactionDTO>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(
                    new GetAllTransactionDTO(
                        row.Field<int>("ID"),
                        row.Field<int>("AccountId"),
                        row.Field<float>("Amount"),
                        Convert.ToDateTime(row["TransactionDate"]),
                        row.Field<string>("Type").Trim(),
                        row.Field<int>("idOperation"),
                        row.Field<string>("Operation").Trim(),
                        row.IsNull("targetAccount_id") ? (int?)null : row.Field<int>("targetAccount_id")
                    )
                );
            }

            return Ok(list);
        }


    }
}
