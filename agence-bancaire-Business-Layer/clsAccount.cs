using agence_bancaire_Business_Layer.Enums;
using agence_bancaire_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_Business_Layer
{
    public class clsAccount
    {
        enMode _Mode = enMode.addNew;

        public int AccountID { get; set; }
        public int ClientID { get; set; }
        public Guid AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public clsClient ClientInfo { get; set; }

        public clsAccount()
        {
            this.AccountID = -1;
            this.ClientID = -1;
            this.AccountNumber = Guid.Empty;
            this.CreatedDate = DateTime.Now;

            _Mode = enMode.addNew;
        }

        public clsAccount(int AccountID, int ClientID, Guid AccountNumber, DateTime CreatedDate)
        {
            this.AccountID = AccountID;
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;
            this.CreatedDate = CreatedDate;
            this.ClientInfo = clsClient.Find(this.ClientID);

            _Mode = enMode.Update;
        }

        private bool _AddNewAccount()
        {
            this.AccountID = clsAccountData.AddNewAccount(this.ClientID,this.AccountNumber,this.CreatedDate);

            return this.AccountID != -1;
        }

        private bool _UpdateAccount()
        {
            return clsAccountData.UpdateAccount(this.AccountID,this.ClientID,this.AccountNumber,this.CreatedDate);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.addNew:
                    if (_AddNewAccount())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateAccount();
            }

            return false;
        }

        public static DataTable GetAllAccounts()
        {
            return clsAccountData.GetAllAccounts();
        }

        public static bool DeleteAccount(int ID)
        {
            return clsAccountData.DeleteAccount(ID);
        }

        public static bool isAccountExist(int AccountID)
        {
            return clsAccountData.IsAccounttExist(AccountID);
        }

        public static bool isAccountExistByClientID(int ClientID)
        {
            return clsAccountData.IsAccounttExistByClientID(ClientID);
        }

        public static clsAccount Find(int AccountID)
        {
            int ClientID = -1;
            Guid AccountNO = Guid.Empty;
            DateTime CreatedDate = DateTime.Now;


            if (clsAccountData.GetAccountInfoByID(AccountID, ref ClientID, ref AccountNO, ref CreatedDate))
            {
                return new clsAccount(AccountID, ClientID, AccountNO, CreatedDate);
            }
            else
            {
                return null;
            }

        }


    }
}
