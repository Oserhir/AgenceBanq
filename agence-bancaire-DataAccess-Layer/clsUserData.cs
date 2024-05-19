using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_DataAccess_Layer
{
    public class clsUserData
    {
        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }

                    }

                }

            }

            return dt;

        }

        public static bool GetUserInfoByUserID(
            int UserID, ref int PersonID, ref string FirstName, ref string LastName, ref DateTime DateOfBirth,
           ref string Address, ref string Phone, ref string Email, ref string Password)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            PersonID = (int)reader["PersonID"];
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["PhoneNumber"];
                            Password = (string)reader["Password"];

                            if (reader["Email"] != DBNull.Value)
                            {
                                Email = (string)reader["Email"];
                            }
                            else
                            {
                                Email = "";
                            }

                        }
                        else
                        {
                            isFound = false;
                        }

                    }

                }
            }

            return isFound;


        }

        public static bool GetUserInfoByEmailAndPassword(
        string Email, string Password,  ref int UserID ,ref string FirstName, ref string LastName, ref DateTime DateOfBirth,
        ref string Address, ref string Phone)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetUserInfoByEmailAndPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
            
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            UserID = (int)reader["UserID"];
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["PhoneNumber"];

                        }
                        else
                        {
                            isFound = false;
                        }

                    }

                }
            }

            return isFound;


        }

        public static bool GetUserInfoByPersonID(
    int PersonID, ref int UserID,  ref string FirstName, ref string LastName, ref DateTime DateOfBirth,
   ref string Address, ref string Phone, ref string Email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetUserByPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            UserID = (int)reader["UserID"];
                            FirstName = (string)reader["FirstName"];
                            LastName = (string)reader["LastName"];
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Address = (string)reader["Address"];
                            Phone = (string)reader["PhoneNumber"];

                            if (reader["Email"] != DBNull.Value)
                            {
                                Email = (string)reader["Email"];
                            }
                            else
                            {
                                Email = "";
                            }

                        }
                        else
                        {
                            isFound = false;
                        }

                    }

                }
            }

            return isFound;


        }

        public static int AddNewUser( int PersonID, string Password )
        {
            int UserID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_AddNewUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@Password", Password);

                    SqlParameter outputIdParam = new SqlParameter("@NewUserID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    UserID = (int)command.Parameters["@NewUserID"].Value;

                }

            }


            return UserID;


        }

        public static bool UpdateUser(int UserID, int PersonID, string Password)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@PersonID", PersonID);
                    command.Parameters.AddWithValue("@Password", Password);
           
                    rowsAffected = command.ExecuteNonQuery();

                }
            }

            return (rowsAffected > 0);

        }

        public static bool DeleteUser(int UserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DeleteUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);

                    try
                    {
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        rowsAffected = 0;
                    }
                }

            }

            return (rowsAffected > 0);
        }

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_IsUserExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);

                    int result = (int)command.ExecuteScalar();

                    isFound = (result == 1);
                }

            }

            return isFound;


        }

        public static bool IsUserExist(string Email)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_IsUserExistByEmail", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", Email);

                    int result = (int)command.ExecuteScalar();

                    isFound = (result == 1);
                }

            }

            return isFound;


        }

        public static bool IsUserExisForPersonID(int PersonID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_IsUserExisForPersonID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PersonID", PersonID);

                    int result = (int)command.ExecuteScalar();

                    isFound = (result == 1);
                }

            }

            return isFound;


        }

        public static bool ChangePassword(int UserID, string Password)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_ChangeUserPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Password", Password);

                    rowsAffected = command.ExecuteNonQuery();

                }
            }

            return (rowsAffected > 0);

        }
    }
}
