using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace AndreevNIR.ReferenceData.Procedures
{
    class ClassProcedures
    {
        DBLogicConnection db = new DBLogicConnection();
        CoreLogic cl = new CoreLogic();


        public void CreateProcedures(string id_drug, TextBox tbNameProc, TextBox tbValue, ComboBox tbValueName) {

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"insert into procedures_ (id_procedure, id_drug, name_drocedure, value_drug, value_name) values " +
                    $"(@id_procedure, @id_drug, @name_drocedure, @value_drug, @value_name);", connection))
                {
                    try
                    {
                        string id_procedure = cl.GetLastIdFromQueryCast("procedures_", "id_procedure");

                        command.Parameters.Add("@id_procedure", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_procedure;
                        command.Parameters.Add("@id_drug", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_drug;
                        command.Parameters.Add("@name_drocedure", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbNameProc.Text;
                        command.Parameters.AddWithValue("@value_drug", int.Parse(tbValue.Text));
                        command.Parameters.Add("@value_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbValueName.SelectedValue.ToString();
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteProcedures(string id_procedure) {
            
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"delete from procedures_ where id_procedure = @id_procedure;", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_procedure", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_procedure;
                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }

        public void GetProcedure(string id_procedure, DataGridView dgv, TextBox tbNameProc, TextBox tbValue, ComboBox cbValueName) {
            
            //получение поля с именем
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"select * from procedures_ where id_procedure = '{id_procedure}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tbNameProc.Text = reader["name_drocedure"].ToString();
                            tbValue.Text = reader["value_drug"].ToString();
                            cbValueName.SelectedItem = reader["value_name"].ToString();
                            string id_drug = reader["id_drug"].ToString();


                            //for (int i = 0; i < dgv.Columns.Count; i++) {
                            //    if (dgv.Rows[i].Cells[0].Value.ToString() == id_drug) {
                            //        dgv.Rows[i].Selected = true;
                            //    }
                            //}
                            
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void SetProcedure(string id_procedure, string id_drug, TextBox tbNameProc, TextBox tbValue, ComboBox cbValueName) {

            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"update procedures_ set id_drug = @id_drug, name_drocedure = @name_drocedure, value_drug = @value_drug, " +
                    $"value_name = @value_name where id_procedure = '{id_procedure}'", connection))
                {
                    try
                    {
                        command.Parameters.Add("@id_drug", NpgsqlTypes.NpgsqlDbType.Varchar).Value = id_drug;
                        command.Parameters.Add("@name_drocedure", NpgsqlTypes.NpgsqlDbType.Varchar).Value = tbNameProc.Text;
                        command.Parameters.Add("@value_drug", NpgsqlTypes.NpgsqlDbType.Integer).Value = int.Parse(tbValue.Text);
                        command.Parameters.Add("@value_name", NpgsqlTypes.NpgsqlDbType.Varchar).Value = cbValueName.Text;

                    }
                    catch (NpgsqlException ex) { MessageBox.Show(ex.Message); }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
