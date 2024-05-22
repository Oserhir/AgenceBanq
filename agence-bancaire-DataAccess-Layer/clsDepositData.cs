using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agence_bancaire_DataAccess_Layer
{
   
    public class clsDepositData
    {
        public static int deposit(float amount, DateTime date_operation, int checkingaccount_id, int LevelID)
        {
            int Depose_id = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_AddNewDepose", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@amount", amount);
                    command.Parameters.AddWithValue("@date_operation", date_operation);
                    command.Parameters.AddWithValue("@checkingaccount_id", checkingaccount_id);
                    command.Parameters.AddWithValue("@LevelID", LevelID);

                    SqlParameter outputIdParam = new SqlParameter("@Newdepose_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(outputIdParam);

                    command.ExecuteNonQuery();

                    Depose_id = (int)command.Parameters["@Newdepose_id"].Value;

                }
            }

            return Depose_id;

        }

    }
}
