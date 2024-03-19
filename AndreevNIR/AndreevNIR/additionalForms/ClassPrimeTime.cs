using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Data;

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
                            label.Text = "Общее количество пациентов: "+reader["number"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void GetNumberOfPatientsHirHopatal(Label label, ComboBox comboBox)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(db._connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT COUNT(DISTINCT t3.omc) AS \"omc\" FROM patient_in_room t1 JOIN hir_hospital t2 ON t1.code_hir_department = t2.code_hir_department JOIN patient t3 ON t1.omc = t3.omc JOIN department t4 ON t4.id_department = t1.id_department JOIN type_department t5 ON t5.id_department = t4.id_department JOIN initial_inspection t6 ON t1.omc = t6.omc WHERE t2.name_hir_department = '{comboBox.SelectedItem.ToString()}';", connection))
                {
                    using (NpgsqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            label.Text = "Кол-во пациентов в выбранном стационаре: " + reader["omc"].ToString();
                        }
                    }
                    //command.ExecuteNonQuery();
                }
            }
        }

        public void GetTop5Diagnoses(Chart chart)
        {
            // Выполнение SQL-запроса
            string sqlQuery = "SELECT diagnosis, COUNT(diagnosis) AS \"Количество\" FROM initial_inspection GROUP BY diagnosis ORDER BY COUNT(diagnosis) DESC LIMIT 5";

            try
            {
                // Создание подключения к БД
                using (NpgsqlConnection conn = new NpgsqlConnection(db._connectionString))
                {
                    conn.Open();

                    // Создание команды для выполнения SQL-запроса
                    using (NpgsqlCommand cmd = new NpgsqlCommand(sqlQuery, conn))
                    {
                        // Создание объекта для чтения данных из БД
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Создание таблицы данных для графика
                            DataTable table = new DataTable();
                            table.Columns.Add("Диагноз", typeof(string));
                            table.Columns.Add("Количество", typeof(int));
                            
                            // Начальный сдвиг для серий данных
                            double seriesOffset = 0;

                            // Заполнение таблицы данными из базы данных
                            while (reader.Read())
                            {
                                string diagnosis = reader["diagnosis"].ToString();
                                int count = Convert.ToInt32(reader["Количество"]);

                                // Создание новой серии данных для гистограммы
                                Series series = new Series(diagnosis);
                                series.ChartType = SeriesChartType.Column;

                                // Устанавливаем сдвиг по оси X для создания отступа между сериями данных
                                series["PointWidth"] = "1.5"; // Устанавливаем ширину столбца
                                series.Points.AddXY(seriesOffset, count);

                                // Выбор уникального цвета из предопределенного списка
                                Color[] colors = { Color.Red, Color.Blue, Color.Green, Color.Purple, Color.Orange };
                                series.Color = colors[chart.Series.Count % colors.Length];

                                // Добавляем серию в график
                                chart.Series.Add(series);

                                // Увеличиваем сдвиг для следующей серии
                                seriesOffset += 0.5;
                            }

                            // Привязка таблицы к серии данных графика
                            chart.DataBind();
                        }
                    }

                    // Отключаем подписи по оси X
                    chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

                    // Устанавливаем интервал по оси Y на 1 для целочисленных значений
                    chart.ChartAreas[0].AxisY.Interval = 1;

                    // Закрытие подключения
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при выполнении SQL-запроса: " + ex.Message);
            }
        }
    }
}
