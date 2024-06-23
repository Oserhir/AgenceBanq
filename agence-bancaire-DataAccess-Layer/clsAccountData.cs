using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_DataAccess_Layer
{
    public class clsAccountData
    {

        public static DataTable GetAllAccounts()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAllAccounts", connection))
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

        public static bool GetAccountInfoByID(
            int AccountID, ref int ClientID, ref Guid AccountNumber, ref DateTime CreatedDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAccountByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", AccountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            ClientID = (int)reader["ClientID"];
                            AccountNumber = (Guid) reader["AccountNumber"];
                            CreatedDate = (DateTime)reader["CreatedDate"];

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

        public static bool GetAccountByClientID(
            int ClientID, ref int AccountID, ref Guid AccountNumber, ref DateTime CreatedDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAccountByClientID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClientID", ClientID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            AccountID = (int)reader["AccountID"];
                            AccountNumber = (Guid)reader["AccountNumber"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
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

        public static bool GetAccountByAccountNO(
          Guid AccountNumber, ref int AccountID, ref int ClientID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAccountByAccountNO", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountNumber", AccountNumber);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            AccountID = (int)reader["AccountID"];
                            ClientID = (int)reader["ClientID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
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
        public static int AddNewAccount( int ClientID,Guid AccountNO, DateTime CreatedDate )
        {
            int AccountID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_AddNewAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientID", ClientID);
                    command.Parameters.AddWithValue("@AccountNO", AccountNO);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                    SqlParameter outputIdParam = new SqlParameter("@NewAccountID", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    AccountID = (int)command.Parameters["@NewAccountID"].Value;

                }

            }

            return AccountID;
        }

        public static bool UpdateAccount(int AccountID, int ClientID, Guid AccountNO, DateTime CreatedDate)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_UpdateAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", AccountID);
                    command.Parameters.AddWithValue("@ClientID", ClientID);
                    command.Parameters.AddWithValue("@AccountNO", AccountNO);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                    rowsAffected = command.ExecuteNonQuery();

                }
            }

            return (rowsAffected > 0);

        }

        public static bool DeleteAccount(int AccountID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DeleteAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", AccountID);

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

        public static bool IsAccounttExist(int AccountID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_IsAccountExist", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", AccountID);

                    int result = (int)command.ExecuteScalar();

                    isFound = (result == 1);
                }

            }

            return isFound;


        }

        public static bool IsAccounttExistByClientID(int ClientID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_IsAccountExistByClientID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClientID", ClientID);

                    int result = (int)command.ExecuteScalar();

                    isFound = (result == 1);
                }

            }

            return isFound;


        }

    }
}
