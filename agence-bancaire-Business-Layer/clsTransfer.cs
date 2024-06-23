using agence_bancaire_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_Business_Layer
{
    public class clsTransfer
    {
        public int transferID { get; set; }
        public float Amount { get; set; }
        public DateTime date_operation { get; set; }
       
        public int checkingaccount_id { get; set; }
        public int targetAccount_id { get; set; }
       

        public clsTransfer()
        {
            this.transferID = -1;
            this.Amount = -1;
            this.date_operation = DateTime.Now;
          
            this.checkingaccount_id = -1;
            this.targetAccount_id = -1;
            
        }

        public clsTransfer(int transferID, float Amount, DateTime date_operation, int checkingaccount_id, int targetAccount_id)
        {
            this.transferID = transferID;
            this.Amount = Amount;
            this.date_operation = date_operation;
           
            this.checkingaccount_id = checkingaccount_id;
            this.targetAccount_id = targetAccount_id;
            
        }
        private async Task<bool> _transfer()
        {
            return await clsCheckingAccountData.Transfer(this.Amount, this.date_operation, this.checkingaccount_id, this.targetAccount_id) != -1;
        }

        public async Task<bool> Save()
        {
            return await _transfer();
        }

    }
}
