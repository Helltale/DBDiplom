using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AndreevNIR
{
    class CheckFields
    {
        //заполненность всех полей
        public List<int> CheckAllFields(params TextBox[] txtb)
        {
            List<int> emptyFieldIndexes = new List<int>();

            for (int i = 0; i < txtb.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(txtb[i].Text))
                {
                    emptyFieldIndexes.Add(i);
                }
            }
            return emptyFieldIndexes;
        }

        //отображенте ошибки
        // Метод для генерации сообщения об ошибке
        public string GenerateErrorMessageEmptyTextBox(List<int> emptyFieldIndexes, params string[] fieldNames)
        {
            List<string> emptyFieldNames = new List<string>();
            foreach (int index in emptyFieldIndexes)
            {
                if (index >= 0 && index < fieldNames.Length)
                    emptyFieldNames.Add(fieldNames[index]);
            }

            string errorMessage = "Следующие поля не были заполнены: " + string.Join(", ", emptyFieldNames);
            return errorMessage;
        }


        public string GenerateErrorMessageEmptyComboBox(List<bool> emptyFieldIndexes, params string[] fieldNames)
        {
            List<string> emptyFieldNames = new List<string>();
            for (int i = 0; i < emptyFieldIndexes.Count; i++)
            {
                if (emptyFieldIndexes[i] && i < fieldNames.Length)
                {
                    emptyFieldNames.Add(fieldNames[i]);
                }
            }

            string errorMessage = "Значения в следующих выпадающих меню не были выбраны: " + string.Join(", ", emptyFieldNames);
            return errorMessage;
        }

        public string GenerateErrorMessageEmptyDGV(List<bool> emptyFieldIndexes, params string[] fieldNames)
        {
            List<string> emptyFieldNames = new List<string>();
            for (int i = 0; i < emptyFieldIndexes.Count; i++)
            {
                if (emptyFieldIndexes[i] && i < fieldNames.Length)
                {
                    emptyFieldNames.Add(fieldNames[i]);
                }
            }

            string errorMessage = "Не были выбраны: " + string.Join(", ", emptyFieldNames);
            return errorMessage;
        }

        //генерация сообщения ошибки для bool
        public string GenerateErrorMessageErrors(List<bool> emptyFieldIndexes, params string[] fieldNames)
        {
            List<string> emptyFieldNames = new List<string>();
            for (int i = 0; i < emptyFieldIndexes.Count; i++)
            {
                if (emptyFieldIndexes[i] && i < fieldNames.Length)
                {
                    emptyFieldNames.Add(fieldNames[i]);
                }
            }

            string errorMessage = "Следующие поля были заполнены с ошибками: " + string.Join(", ", emptyFieldNames);
            return errorMessage;
        }

        //выбрано значение кобобокса
        public bool CheckedCombobox(params ComboBox[] cbb)
        {
            bool error = false;
            for (int i = 0; i < cbb.Count(); i++)
            {
                if (cbb[i].SelectedIndex == -1)
                {
                    error = true;
                    break;
                }
            }
            return error;
        }

        //выбрано значение в DGV
        public bool SelectedDGV(params DataGridView[] dgv)
        {
            bool error = false;
            for (int i = 0; i < dgv.Count(); i++)
            {
                if (dgv[i].SelectedRows == null)
                {
                    error = true;
                    break;
                }
            }
            return error;
        }

        //буквы и пробелы
        public bool LetterAndSpace(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsLetter(letter) && !Char.IsWhiteSpace(letter))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else {
                    error = true;
                }
            }
            return error;
        }

        //буквы и пробелы и знаки препинания
        public bool LetterAndSpaceAndPunctuation(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsLetter(letter) && !Char.IsWhiteSpace(letter) && !Char.IsPunctuation(letter))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }

        //цифры и тире
        public bool DigitAndDash(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsDigit(letter) && !Char.Equals(letter, '-'))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }

        //цифры и тире
        public bool DigitAndColon(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsDigit(letter) && !Char.Equals(letter, ':'))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }

        //всё
        //public bool Anything(params TextBox[] txtb)
        //{
        //    bool error = false;
        //    for (int i = 0; i < txtb.Count(); i++)
        //    {
        //        if (txtb[i].TextLength < 1)
        //        {
        //            error = true;
        //            MessageBox.Show("Не все поля заполнены");
        //            break;
        //        }
        //    }
        //    return error;
        //}

        //только цифры
        public bool Digit(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsDigit(letter))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }

        //буквы и знаки препинания
        public bool LetterAndDotAndCommaAndSpace(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsLetter(letter) && !Char.Equals(letter, '.') && !Char.Equals(letter, ',') && !Char.IsWhiteSpace(letter))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }

        //буквы и знаки препинания и цифры
        public bool LetterAndDotAndCommaAndSpaceAndDigitAndDash(params TextBox[] txtb)
        {
            bool error = false;
            for (int i = 0; i < txtb.Count(); i++)
            {
                if (!error)
                {
                    string text = txtb[i].Text;
                    for (int j = 0; j < text.Length; j++)
                    {
                        char letter = text[j];
                        if (!Char.IsLetter(letter) && !Char.Equals(letter, '.') && !Char.Equals(letter, ',') && !Char.IsWhiteSpace(letter) && Char.IsDigit(letter) && !Char.Equals(letter, '-'))
                        {
                            error = true;
                            break;
                        }
                    }
                }
                else
                {
                    error = true;
                }
            }
            return error;
        }


    }
}
