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
    public class clsPerson
    {
        public enMode _Mode = enMode.addNew;

        public int PersonID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CIN { get; set; }

        public string fullName { get { return $"{firstName} {lastName}"; } }

        public clsPerson()
        {
            this.PersonID = -1;
            this.firstName = "";
            this.lastName = "";
            this.DateOfBirth = DateTime.Now;
            this.PhoneNumber = "";
            this.Email = "";
            this.Address = "";
            this.CIN = "";

            this._Mode = enMode.addNew;
        }

        public clsPerson(int PersonID , string firstName, string lastName, DateTime DateOfBirth
            , string PhoneNumber, string Email, string Address, string CIN)
        {
            this.PersonID = PersonID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.DateOfBirth = DateOfBirth;
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
            this.Address = Address;
            this.CIN = CIN;

            this._Mode = enMode.Update;
        }


        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.firstName,this.lastName,this.DateOfBirth,
                this.Address,this.PhoneNumber  ,this.Email, this.CIN);

            return this.PersonID != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(this.PersonID,this.firstName,this.lastName,this.DateOfBirth
                ,this.Address,this.PhoneNumber,this.Email, this.CIN);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.addNew:
                    if(_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int ID)
        {
            return clsPersonData.DeletePerson(ID);
        }

        public static bool isPersonExist(int ID)
        {
            return clsPersonData.IsPersonExist(ID);
        }

        public static clsPerson Find(int PersonID)
        {
            string FirstName = "";
            string LastName = "";  DateTime DateOfBirth = DateTime.Now; 
            string Address = ""; string Phone = ""; string Email = "";
            string CIN = "";

            if (clsPersonData.GetPersonInfoByID( PersonID, ref FirstName, ref LastName, ref DateOfBirth,
                  ref Address, ref Phone, ref Email, ref CIN))
            {
                return new clsPerson(  PersonID, FirstName, LastName,   DateOfBirth
            , Phone,   Email,   Address, CIN);
            }
            else
            {
                return null;
            }

        }


    }
}
