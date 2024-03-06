using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.additionalForms
{

    class ClassPrimeTime
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();

        public void GetNumberOfPatients(Label label) {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select count(*) as number from patient_in_room;", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            label.Text = "Количество пациентов в стационаре "+reader["number"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }
    }
}
