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
    public class clsClient
    {

        public enMode _Mode = enMode.addNew;

        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }
  
        public clsClient()
        {
            this.PersonID = -1;
            this.ClientID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;


            this._Mode = enMode.addNew;
        }

        public clsClient(int PersonID, int ClientID, int CreatedByUserID, DateTime CreatedDate )
        {
            this.PersonID = PersonID;
            this.ClientID = ClientID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;

            this._Mode = enMode.Update;
        }


        private bool _AddNewClient()
        {
            this.ClientID = clsClientData.AddNewClient(this.PersonID, this.CreatedByUserID, this.CreatedDate);

            return this.ClientID != -1;
        }

        private bool _UpdateClient()
        {
            return clsClientData.UpdateClient(this.ClientID, this.PersonID, this.CreatedByUserID, this.CreatedDate);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.addNew:
                    if (_AddNewClient())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateClient();
            }

            return false;
        }

        public static DataTable GetAllClients()
        {
            return clsClientData.GetAllClients();
        }

        public static bool DeleteClient(int ID)
        {
            return clsClientData.DeleteClient(ID);
        }

        public static bool isClientExist(int ID)
        {
            return clsClientData.IsClientExist(ID);
        }

        public static clsClient Find(int ClientID)
        {
            int PersonID = -1;
            int CreatedByUserID = -1;  
            DateTime CreatedDate = DateTime.Now;


            if (clsClientData.GetClientInfoByID(ClientID, ref PersonID , ref CreatedByUserID, ref CreatedDate))
            {
                return new clsClient(PersonID, ClientID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }

        }

    }
}
