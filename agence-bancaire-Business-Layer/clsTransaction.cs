using agence_bancaire_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_Business_Layer
{
    public class clsTransaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Type { get; set; }
        public int idOperation { get; set; }
        public int opeartionID { get; set; }
        public int? targetAccount_id { get; set; }

        public clsTransaction()
        {
            this.TransactionId = -1;
            this.AccountId = -1;
            this.Amount = -1;
            this.TransactionDate = DateTime.Now;
            this.Type = -1;
            this.opeartionID = -1;
            this.targetAccount_id = -1;

        }

        public clsTransaction(int TransactionId, int AccountId, float Amount, DateTime TransactionDate
            , int Type, int idOperation, int opeartionID, int targetAccount_id)
        {
            this.TransactionId = TransactionId;
            this.AccountId = AccountId;
            this.Amount = Amount;
            this.TransactionDate = TransactionDate;
            this.Type = Type;
            this.idOperation = idOperation;
            this.opeartionID = opeartionID;
            this.targetAccount_id = targetAccount_id;
        }

        public static DataTable GetAllTransaction()
        {
            return clsTransactionData.GetAllTransaction();
        }

        public static DataTable GetAllTransaction(int AccountID)
        {
            return clsTransactionData.GetAllTransaction(AccountID);
        }

    }
}
