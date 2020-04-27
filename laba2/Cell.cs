using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace laba2
{
    public class Cell
    {
        public string value;
        public string exp;
        public List<Cell> dependend = new List<Cell>();
        public List<string> depends = new List<string>();
        public Cell()
        {
            value = "0";
        }
        public string getName(int column, int row)
        {if(column <0 || row < 0) { return "ERROR"; }
            int k=0;
           
            string name = null;
           
            
                name += (char)(column + 65);
            
                name += row.ToString();
            
            return name;
        }
    }
}
