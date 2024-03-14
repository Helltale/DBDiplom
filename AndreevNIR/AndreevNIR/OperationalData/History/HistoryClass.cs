using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreevNIR.OperationalData.History
{
    class HistoryClass
    {

        public string[] getOmcAndID(string omc) {
            DBLogicConnection dB = new DBLogicConnection();
            string[] result = new string[2];

            using (NpgsqlConnection connection = new NpgsqlConnection(dB._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from patient_in_room where omc = @omc", connection))
                {
                    command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc;
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            try
                            {
                                result[0] = omc;
                                result[1] = reader["id_patient"].ToString();
                            }
                            catch (NpgsqlException ex) { ex.ToString(); }

                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
            return result;
        }

    }
}
