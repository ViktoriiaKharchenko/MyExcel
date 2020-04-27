using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba2
{

    class CellValueManager
    {
        Parser2 parser2 = new Parser2();
        int index;
        public void AddNewVal(string exp, int row, int column, Cell[,] table, DataGridView Table)
        {//replace Cell Name with value
            table[row, column].exp = exp;
            index = 0;
            if (exp != null)
            {
                while (index < exp.Length)
                {
                    string str = null;
                    int t2, t1 = (int)exp[index];
                    str += exp[index];
                    index++;
                    if (t1 > 64 && t1 < 91 && index < exp.Length)
                    {
                        str += exp[index];
                        t2 = (int)exp[index] - 48;
                        index++;
                        if (index < exp.Length && exp[index] != ' ' && "+-/*<>=".IndexOf(exp[index]) != -1)
                        {
                            str += exp[index];
                            t2 *= 10;
                            t2 += (int)exp[index] - 48;

                        }
                        table[t2, t1 - 65].dependend.Add(table[row, column]);
                        exp = exp.Replace(str, table[t2, t1 - 65].value);
                        MessageBox.Show(exp);
                    }
                }
            }
            Result result = parser2.Evaluate(exp);
            if (result.Except())
            {
                table[row, column].value = result.GetValue();
                Table.Rows[row].Cells[column].Value = result.GetValue();
                //UpdateForm(row, column, table, Table);
            }
            Table.Rows[row].Cells[column].Value = result.GetValue();
            //MessageBox.Show("У підрахунках виникла помилка, перевірте будь ласка правильність формули");
        }
        public void UpdateForm(int row, int column, Cell[,] table, DataGridView Table)
        {
            string exp;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    exp = table[i, j].exp;
                    if (exp != null)
                    {
                        while (index < exp.Length)
                        {
                            string str = null;
                            int t2, t1 = (int)exp[index];
                            str += exp[index];
                            index++;
                            if (t1 > 64 && t1 < 91 && index < exp.Length)
                            {
                                str += exp[index];
                                t2 = (int)exp[index] - 48;
                                index++;
                                if (index < exp.Length && exp[index] != ' ' && "+-/*<>=".IndexOf(exp[index]) != -1)
                                {
                                    str += exp[index];
                                    t2 *= 10;
                                    t2 += (int)exp[index] - 48;

                                }
                                // table[t2, t1 - 65].dependend.Add(table[row, column]);
                                exp = exp.Replace(str, table[t2, t1 - 65].value);
                                MessageBox.Show(exp);
                            }
                        }

                    }
                    Result result = parser2.Evaluate(exp);
                    if (result.Except())
                    {
                        table[i, j].value = result.GetValue();
                        Table.Rows[row].Cells[column].Value = result.GetValue();
                        //UpdateForm(row, column, table, Table);
                    }
                    MessageBox.Show("У підрахунках виникла помилка, перевірте будь ласка правильність формули");
                }
            }
        }
    }
}

