using agence_bancaire_Business_Layer.Enums;
using agence_bancaire_DataAccess_Layer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_Business_Layer
{
    public class clsUser
    {
        public enMode _Mode = enMode.addNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string Password { get; set; }

        public clsPerson PersonInfo;

        public clsUser()
        {
            this.UserID = -1;
            this.PersonID = -1;
            this.Password = "";

            this._Mode = enMode.addNew;
        }

        public clsUser(int UserID, int PersonID, string Password)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.Password = Password;
            this.PersonInfo = clsPerson.Find(this.PersonID);
            this._Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID ,this.Password);

            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.Password);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.addNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int ID)
        {
            return clsUserData.DeleteUser(ID);
        }

        public static bool isUserExist(int ID)
        {
            return clsUserData.IsUserExist(ID);
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string FirstName = "";
            string LastName = ""; DateTime DateOfBirth = DateTime.Now;
            string Address = ""; string Phone = ""; string Email = "";
            string Password = "";

            if (clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref FirstName, ref LastName, ref DateOfBirth,
                  ref Address, ref Phone, ref Email, ref Password))
            {
                return new clsUser(UserID, PersonID, Password);
            }
            else
            {
                return null;
            }

        }


    }
}
