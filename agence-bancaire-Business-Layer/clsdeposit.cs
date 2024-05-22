using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_DataAccess_Layer
{
    public class clsdeposit
    {
        public int DepositID { get; set; }
        public float amount { get; set; }
        public DateTime date_operation { get; set; }
        public int checkingaccount_id { get; set; }
        public int LevelID { get; set; }


        public clsdeposit()
        {
            this.DepositID = -1;
            this.amount = -1;
            this.date_operation = DateTime.Now;
            this.checkingaccount_id = -1;
            this.LevelID = -1;
        }

        public clsdeposit(int DepositID, float amount, DateTime date_operation, 
            int checkingaccount_id, int LevelID)
        {
            this.DepositID = DepositID;
            this.amount = amount;
            this.date_operation = date_operation;
            this.checkingaccount_id = checkingaccount_id;
            this.LevelID = LevelID;
        }

        private bool _deposit()
        {
            return clsDepositData.deposit(this.amount,this.date_operation,this.checkingaccount_id,this.LevelID) != -1;
        }

        public bool Save()
        {
            if (_deposit())
            {
                 return true;
            }
            else
            {
                return false;
            }
        }

    }
}
