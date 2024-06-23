using agence_bancaire_Business_Layer.Enums;
using agence_bancaire_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_Business_Layer
{
    public class clsCheckingAccount : clsAccount
    {
        public enMode _Mode = enMode.addNew;

        public int checking_account_id { get; set; }
        public int Account_Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public float Balance { get; set; }
        public float overdraftLimit { get; set; }
        public clsClient ClientInfo { get; set; }


        public clsCheckingAccount()
        {
            this.checking_account_id = 0;
            this.Account_Id = 0;
            this.CreatedDate = DateTime.Now;
            this.Balance = 0;
            this.overdraftLimit = 0;

            _Mode = enMode.addNew;
        }


        public clsCheckingAccount(int checking_account_id, int Account_Id,
            DateTime CreatedDate, float Balance, float overdraftLimit, int ClientID, Guid AccountNumber)
        {
            this.checking_account_id = checking_account_id;
            this.Account_Id = Account_Id;
            this.CreatedDate = CreatedDate;
            this.Balance = Balance;
            this.overdraftLimit = overdraftLimit;
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;

            this.ClientInfo = clsClient.Find(ClientID );

            _Mode = enMode.Update;
        }


        private bool _AddNewCheckingAccount()
        {
            this.checking_account_id = clsCheckingAccountData.AddNewCheckingAccount
                (this.Account_Id, this.CreatedDate, this.Balance, this.overdraftLimit);

            return this.checking_account_id != -1;
        }

        private bool _UpdateCheckingAccount()
        {
            return clsCheckingAccountData.UpdateCheckingAccount
                 (this.checking_account_id, this.Account_Id, this.CreatedDate , this.Balance, this.overdraftLimit);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.addNew:
                    if (_AddNewCheckingAccount())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateCheckingAccount();
            }

            return false;
        }

        public static DataTable GetAllCheckingAccount()
        {
            return clsCheckingAccountData.GetAllCheckingAccount();
        }

        public static bool DeleteCheckingAccount(int ID)
        {
            return clsCheckingAccountData.DeleteCheckingAccount(ID);
        }

        public new static clsCheckingAccount Find(int Checking_Account_ID)
        {
            int ClientID = -1;
            int AccountID = -1;
            float Balance = -1;
            float overdraftLimit = -1;
            Guid AccountNO = Guid.Empty;
            DateTime CreatedDate = DateTime.Now;


            if (clsCheckingAccountData.GetCheckingAccountInfoByID (Checking_Account_ID, ref AccountID, ref CreatedDate, ref Balance, ref overdraftLimit))
            {
                return new clsCheckingAccount(Checking_Account_ID, AccountID, CreatedDate, Balance, overdraftLimit, ClientID, AccountNO);
            }
            else
            {
                return null;
            }

        }


        public bool CanApplyOverdraft( float amount)
        {
            return (amount <= (this.overdraftLimit + this.Balance));
        }

        public static int CalculateOverdraftFee()
        {
            return 35; 
        }

        public static bool isCheckingAccountExist(int ID)
        {
            return clsCheckingAccountData.IsCheckingAccounttExist(ID);
        }

        public static bool IsCheckingAccounttExistByAccountIdAsync(int AccountID)
        {
            return clsCheckingAccountData.IsCheckingAccounttExistByAccountID(AccountID);
        }

        public bool Update_Checking_Account_OverdraftLimit()
        {
            return clsCheckingAccountData.Update_Checking_Account_OverdraftLimit(this.checking_account_id, this.overdraftLimit);
        }

    }
}
