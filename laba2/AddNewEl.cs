using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace laba2
{
    class AddNewEl
    {
        const int b = 65;
        const int c = 26;
        char letter = 'A';
        char firstLetter = 'A';
        int nFirstLetter = 1;
        string temp;
        int r;
        public int AddColumn(DataGridView dgv,int columnCount)
        {
            if (columnCount < c)
            {
                int t = b + columnCount;
                letter = (char)t;
                temp += letter;
            }
            else
            {
                int y = 0;
                while (columnCount > c)
                {
                    columnCount -= c;
                    ++y;
                }
                firstLetter = (char)(y + (b - 1));
                letter = (char)(columnCount + b);
                temp += firstLetter;
                temp += letter;
            }
            try
            {
                DataGridViewColumn col = (DataGridViewColumn)dgv.Columns[0].Clone();
                col.HeaderCell.Value = temp;
                dgv.Columns.Add(col);
            }
            catch {  }
            
            temp = null;
            if(firstLetter != 'Z') 
            {
                if (letter != 'Z') ++letter;
                else letter = 'A';
                firstLetter++;
            }
            else
            {
                firstLetter = 'A';
                nFirstLetter++;
            }
            return 0;

        }
        public int AddRow( DataGridView dgv, int _c)
        {
            r = _c;
            //++_c;
            string t = null;
            t += r;
            DataGridViewRow row = new DataGridViewRow();
            try
            {
                dgv.Rows.Add(row);
                dgv.Rows[r].HeaderCell.Value = _c.ToString();
            }
            catch { }


            return 0;
        }
        

    }
}

