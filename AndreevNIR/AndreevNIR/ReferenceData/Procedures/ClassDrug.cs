using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;



namespace AndreevNIR.ReferenceData.Procedures
{
    class ClassDrug
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();


        public void CreateDrug(TextBox tbName) {

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into drug (id_drug, name_drug) values " +
                    $"(@id_drug, @name_drug);", connection))
                {
                    try
                    {
                        string lastID = cl.GetLastIdFromQueryCast("drug", "id_drug");

                        command.Parameters.Add("@id_drug", NpgsqlTypes.NpgsqlDbType.Varchar).Value = lastID;
                        command.Parameters.Add("@name_drug", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbName.Text;
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void GetDrug(string id_drug, TextBox tbName) {
            //получение поля с именем
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select name_drug from drug where id_drug = '{id_drug}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbName.Text = reader["name_drug"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void ChangeDrug(string id_drug, TextBox tbName) {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update drug set name_drug = @name_drug where id_drug = '{id_drug}'", connection))
                {
                    try
                    {
                        command.Parameters.Add("@name_drug", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbName.Text;
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDrug(string id_drug) {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from drug where id_drug = '{id_drug}'", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
