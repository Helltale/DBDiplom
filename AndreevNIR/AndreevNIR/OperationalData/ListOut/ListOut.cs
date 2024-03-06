using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;

namespace AndreevNIR.OperationalData.NotWorkingList
{
    class ClassNotWorking
    {
        CoreLogic cl = new CoreLogic();
        DBLogicConnection db = new DBLogicConnection();

        public void CreateNotWorking()
        {
            //создание листа
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into extract_document (omc, diagnosis_enter, snils, full_name) values (@omc, @diagnosis_enter, @snils, @full_name);", connection))
                {
                    try
                    {
                        //command.Parameters.Add("@omc", NpgsqlTypes.NpgsqlDbType.Varchar).Value = omc.Text;
                        //command.Parameters.Add("@diagnosis_enter", NpgsqlTypes.NpgsqlDbType.Varchar).Value = diagnosis_enter.Text;
                        //command.Parameters.Add("@snils", NpgsqlTypes.NpgsqlDbType.Varchar).Value = snils.Text;
                        //command.Parameters.Add("@full_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = full_name.Text;
                    }
                    catch (Exception ex) { MessageBox.Show("" + ex); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
