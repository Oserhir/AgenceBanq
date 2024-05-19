using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_DataAccess_Layer
{
    public class clsCheckingAccountData
    {
        public static DataTable GetAllCheckingAccount()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetAllCheckingAccount", connection))
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

        public static bool GetCheckingAccountInfoByID(
            int checking_account_id, ref int AccountID, ref DateTime CreatedDate,
           ref float Balance, ref float overdraftLimit)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetCheckingAccountInfoByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@checking_account_id", checking_account_id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            AccountID = (int)reader["AccountID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
                            Balance = (float)reader["Balance"];
                            overdraftLimit = (float)reader["overdraftLimit"];

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

        public static bool GetCheckingAccountInfoByAccountID(
         int AccountID , ref int checking_account_id, ref DateTime CreatedDate,
        ref float Balance, ref int overdraftLimit)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetCheckingAccountInfoByAccountID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@AccountID", AccountID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            checking_account_id = (int)reader["checking_account_id"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
                            Balance = (float)reader["Balance"];
                            overdraftLimit = (int)reader["overdraftLimit"];

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

        public static bool GetCheckingAccountInfoByClientID(
       int ClientID , ref int AccountID, ref int checking_account_id, ref DateTime CreatedDate,
      ref float Balance, ref int overdraftLimit)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetCheckingAccountInfoByClientID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClientID", ClientID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            isFound = true;

                            checking_account_id = (int)reader["checking_account_id"];
                            AccountID = (int)reader["AccountID"];
                            CreatedDate = (DateTime)reader["CreatedDate"];
                            Balance = (float)reader["Balance"];
                            overdraftLimit = (int)reader["overdraftLimit"];
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

        public static int AddNewCheckingAccount(int AccountID, DateTime CreatedDate,
              float Balance, float overdraftLimit)
        {
            int checking_account_id = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_AddNewCheckingAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@AccountID", AccountID);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    command.Parameters.AddWithValue("@Balance", Balance);
                    command.Parameters.AddWithValue("@overdraftLimit", overdraftLimit);

                    SqlParameter outputIdParam = new SqlParameter("@NewChecking_Account_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    checking_account_id = (int)command.Parameters["@NewChecking_Account_id"].Value;

                }

            }


            return checking_account_id;


        }

        public static bool UpdateCheckingAccount(int checking_account_id, int AccountID, DateTime CreatedDate,
              float Balance, float overdraftLimit)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_UpdateCheckingAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@checking_account_id", checking_account_id);
                    command.Parameters.AddWithValue("@AccountID", AccountID);
                    command.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    command.Parameters.AddWithValue("@Balance", Balance);
                    command.Parameters.AddWithValue("@overdraftLimit", overdraftLimit);

                    rowsAffected = command.ExecuteNonQuery();
                }
            }

            return (rowsAffected > 0);

        }


        public static bool DeleteCheckingAccount(int checking_account_id)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_DeleteCheckingAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@checking_account_id", checking_account_id);

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


    }
}
