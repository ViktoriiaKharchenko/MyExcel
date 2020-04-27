using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba2
{
    public partial class Form1 : Form
    {
        CellValueManager manager = new CellValueManager();

        const int st = 100;
        int columns = 0;
        int rows = 0;
        AddNewEl newEl = new AddNewEl();
        //Dictionary<int, object> value = new Dictionary<int, object>();
        Cell[,] table = new Cell[st, st];
        //Parser parser = new Parser();
        Parser2 parser2 = new Parser2();

        public Form1()
        {

            InitializeComponent();
            for (int i = 0; i < st; i++)
            {
                for (int j = 0; j < st; j++)
                {
                    table[i, j] = new Cell();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            columns = 5;
            rows = 10;
            Cell[,] table = new Cell[st, st];
            initTable();

        }


        private void Table_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string temp = (string)Table.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            int r = Table.CurrentCell.RowIndex;

            int c = Table.CurrentCell.ColumnIndex;
            if (temp != null)//manager.AddNewVal(temp, r, c, table, Table);
                try { AddNewValue(temp, r, c, true); } catch (Exception) { }
        }
        private void Table_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = table[Table.CurrentCell.RowIndex, Table.CurrentCell.ColumnIndex].exp;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string temp = (string)textBox1.Text;
                int r = Table.CurrentCell.RowIndex;
                int c = Table.CurrentCell.ColumnIndex;

                try { AddNewValue(temp, r, c, true); } catch (Exception) { }

            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Ви впевнені, що хочете закрити файл?", "Повідомлення", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            { }
            else e.Cancel = true;
        }

        private void AddColumnButton_Click(object sender, EventArgs e)
        {
            newEl.AddColumn(Table, columns);
            columns++;
            for(int i =0; i < rows; i++)
            {
                table[i, columns - 1].exp = null;
            }
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            newEl.AddRow(Table, rows);
            rows++;
            for (int i = 0; i < columns; i++)
            {
                table[rows-1, i].exp = null;
            }
        }
        public void AddNewValue(string expression, int r, int c, bool k)
        {
            int Count=0;
            Result result1;
            table[r, c].exp = expression;
            
            int h = 0;
            // expression.Replace(" ","");
            //MessageBox.Show(expression);
            if (expression != null)
            {
                while (h < expression.Length)
                {
                    
                    string str = null;
                    int t2, t1 = (int)expression[h];
                    str += expression[h];
                    h++;
                   
                    if (t1 > 64 && t1 < 91 && h < expression.Length)
                    {
                        
                                
                                str += expression[h];
                                t2 = (int)expression[h] - 48;
                                h++;
                                if (h < expression.Length && expression[h] != ' ' && expression[h] > 47 && expression[h] < 58)
                                {
                                    str += expression[h];
                                    t2 *= 10;
                                    t2 += (int)expression[h] - 48;

                                }
                                if (k == true)
                                {
                                    table[t2, t1 - 65].depends.Add(table[r, c].getName(c, r));

                                    table[r, c].dependend.Add(table[t2, t1 - 65]);
                                }
                        if (circle(table[r, c], r, c, r, c) == false)
                        {
                            table[t2, t1 - 65].depends.RemoveAt(table[t2, t1 - 65].depends.Count - 1);
                            MessageBox.Show("Неможливо застосувати формулу, клітинка вказує сама на себе");
                            Table.Rows[Table.CurrentCell.RowIndex].Cells[Table.CurrentCell.ColumnIndex].Value = "";
                            table[r, c].value = "0";
                            table[r, c].exp = null;
                            AddNewValue(table[r, c].exp, r, c, true);
                            break;
                        }
                                expression = expression.Replace(str, table[t2, t1 - 65].value);
                        Count++;
                    
                    }
                }
            }
                if (circle(table[r, c], r, c,r,c)==true)
                {
                
                    if (expression.Contains(">") || expression.Contains("<") || expression.Contains("="))
                    {
                        result1 = parser2.Evaluate0(expression);
                       
                    }
                    else
                    {
                        result1 = parser2.Evaluate(expression);
                        
                    }
                    if (result1.Except())
                    {
                       
                        table[r, c].value = result1.GetValue();
               // MessageBox.Show(table[r, c].value);
                        

                        Table.Rows[r].Cells[c].Value = result1.GetValue();///////////
                    update(table[r, c], r, c);
                    }
                    else Table.Rows[r].Cells[c].Value = result1.GetValue();
                }
            else
            {
                MessageBox.Show("Неможливо застосувати формулу, клітинка вказує сама на себе");
                Table.Rows[Table.CurrentCell.RowIndex].Cells[Table.CurrentCell.ColumnIndex].Value = "";
                
            }

        }
        
        public void update(Cell cell, int r, int c)
        {

            int t1, t2;
            for (int i = 0; i < cell.depends.Count; i++)
            {
                
                if (cell.depends[i].Length > 2)
                {
                    t1 = (int)cell.depends[i][0] - 65;
                    t2 = (int)cell.depends[i][1] - 48;
                    t2 *= 10;
                    t2 += (int)cell.depends[i][2] - 48;
                }
                else
                {
                    t2 = (int)cell.depends[i][1] - 48;
                    t1 = (int)cell.depends[i][0] - 65;
                }

                AddNewValue(table[t2, t1].exp, t2, t1, false);

            }
        }
        public bool circle(Cell cell, int r, int c, bool k)
        {
            int t1, t2;
            for (int i = 0; i < cell.depends.Count; i++)
            {

                if (cell.depends[i].Length > 2)
                {
                    t1 = (int)cell.depends[i][0] - 65;
                    t2 = (int)cell.depends[i][1] - 48;
                    t2 *= 10;
                    t2 += (int)cell.depends[i][2] - 48;
                }
                else
                {
                    t2 = (int)cell.depends[i][1] - 48;
                    t1 = (int)cell.depends[i][0] - 65;
                }
                //MessageBox.Show(t1.ToString());
               // MessageBox.Show(t2.ToString());
                if (table[t2, t1].getName(t1, t2) == table[r, c].getName(c, r)) return false;
                if (circle(table[t2, t1], t2, t1, r, c) == false) return false;
            }
                return true;
        }
        public bool circle(Cell cell, int r, int c,int r2,int c2)
        {
            int t1, t2;
           
            for (int i = 0; i < cell.depends.Count; i++)
            {
              //  MessageBox.Show("index " + i.ToString());
                if (cell.depends[i].Length > 2)
                {
                    t1 = (int)cell.depends[i][0] - 65;
                    t2 = (int)cell.depends[i][1] - 48;
                    t2 *= 10;
                    t2 += (int)cell.depends[i][2] - 48;
                }
                else
                {
                    t2 = (int)cell.depends[i][1] - 48;
                    t1 = (int)cell.depends[i][0] - 65;
                }


              //  MessageBox.Show(t1.ToString());
               // MessageBox.Show(t2.ToString());
                if (circle(table[t2,t1],r2,c2,true)==false)
                {
                        return false;
                 }
                
            }
            //MessageBox.Show("TRUE");
            return true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EnterButton_Click(object sender, EventArgs e)
        {


            string temp = (string)textBox1.Text;
            int r = Table.CurrentCell.RowIndex;
            int c = Table.CurrentCell.ColumnIndex;
            try { AddNewValue(temp, r, c, true); } catch (Exception) { }

        }

        private void DeleteColumnButton_Click(object sender, EventArgs e)
        {
            bool k=true;
            if (Table.ColumnCount <= 2) { MessageBox.Show("Подальше выдалення неможливе"); }
            else { 
            for (int i =0; i< rows; i++)
            {
                if(table[i,columns-1].exp != null) 
                {
                        if (DialogResult.Yes == MessageBox.Show("Цей стопчик містить заповнені поля, все рівно видалити? ", "Повідомлення", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            k = true;
                            break;
                        }
                        else { k = false;break; }  
                }
            }
                
                if (k == true)
                {

                    for (int i = 0; i < rows; i++)
                    {
                        try
                        {
                            AddNewValue("0", i, columns - 1, true);


                            foreach (Cell cell in table[i, columns - 1].dependend)
                            {
                                foreach (string str in cell.depends)
                                {
                                    if (str == table[i, columns - 1].getName(columns - 1, i)) { cell.depends.Remove(str); }
                                }
                            }

                        }
                        catch (Exception) { }
                    }


                    Table.Columns.Remove(Table.Columns[columns - 1]);
                    columns--;
                }
            }

        }

        private void DeleteRowButton_Click(object sender, EventArgs e)
        {
            bool k = true;
            if (Table.RowCount <= 2) { MessageBox.Show("Подальше выдалення неможливе"); }
            else
            {
                for (int i = 0; i < columns; i++)
                {
                    if (table[rows - 1, i].exp != null)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Цей рядок містить заповнені поля, все рівно видалити? ", "Повідомлення", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
                        {
                            k = true;
                            break;
                        }
                        else { k = false; break; }
                    }
                }
                if (k == true)

                {
                    for (int i = 0; i < columns; i++)
                    {
                        try
                        {
                            AddNewValue("0", rows - 1, i, true);
                            foreach (Cell cell in table[rows - 1, i].dependend)
                            {
                                foreach (string str in cell.depends)
                                {
                                    if (str == table[rows - 1, i].getName(i, rows - 1)) { cell.depends.Remove(str); }
                                }
                            }
                        }
                        catch (Exception) { }
                    }

                    Table.Rows.Remove(Table.Rows[rows - 1]);

                    rows--;
                }
            }

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "GridFile|*.grd";
            save.Title = "Save Grid File";
            save.ShowDialog();
            if (save.FileName != "")
            {
                FileStream fs = (FileStream)save.OpenFile();
                StreamWriter sw = new StreamWriter(fs);
                Save(sw);
                sw.Close();
                fs.Close();
            }
        }
        private void Save(StreamWriter sw)
        {
            sw.WriteLine(rows);
            sw.WriteLine(columns);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (table[i, j].exp != null)
                    {
                        sw.WriteLine(i);
                        sw.WriteLine(j);
                        sw.WriteLine(table[i, j].exp);
                        sw.WriteLine(table[i, j].value);
                       
                        
                    }
                }
            }
            sw.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DialogResult.Cancel == MessageBox.Show("Якщо Ви відкриєте новий файл, Ви втратитe всі незбережені дані", "Повідомлення", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
            {

            }
            else
            {
                Table.Rows.Clear();
                Table.Columns.Clear();
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        table[i, j].exp = null;
                        table[i, j].value = "0";
                    }
                }
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    openFile(openFileDialog1.FileName);
                }
            }

        }
        private void openFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            DataGridView filedataGridView = new DataGridView();
            rows = Convert.ToInt32(sr.ReadLine());
            columns = Convert.ToInt32(sr.ReadLine());
            initTable();

            while (!sr.EndOfStream)
            {
                int i = Convert.ToInt32(sr.ReadLine());
                int j = Convert.ToInt32(sr.ReadLine());
                table[i, j].exp = sr.ReadLine();
                //MessageBox.Show(table[i, j].exp);
                table[i, j].value = sr.ReadLine();
            }
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (table[i, j].value != "0")
                        Table.Rows[i].Cells[j].Value = table[i, j].value;
                }
            }
            for(int i =0; i < rows; i++)
            {
                for(int j =0; j< columns; j++)
                {
                    if(table[i,j].exp != null)
                    {
                        AddNewValue(table[i, j].exp, i, j, true);
                    }
                }
            }
            sr.Close();
        }
        public void initTable()
        {
            string name = null;
            char c = 'A';

            for (int i = 0; i < columns; i++)
            {
                name += c;
                Table.Columns.Add(Name, name);
                c++;
                name = null;
            }
            for (int i = 0; i < rows; i++)
            {
                Table.Rows.Add();
            }
            for (int i = 0; i < (Table.Rows.Count); i++)
            {
                Table.Rows[i].HeaderCell.Value = i.ToString();
            }
            foreach (DataGridViewColumn column in Table.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ця програма виконує наступні бінарні операції: +, -, *, /. Крім того можна застосувати операції: mod, div, знаходження мінімуму та максимуму двох чисел, логічні операції порівняння: =, <, >, <=, >=, <>");
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
   
