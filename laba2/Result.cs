using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba2
{
   public class Result
    {
        public Double Value;
        public Parser2.Errors Code;
        public Result()
        {
            Value = 0.0;
            Code = Parser2.Errors.NOERR;

        }
        public Result(double v, Parser2.Errors code)
        {
            Value = v;
            Code = code;
        }
        public bool Except()
        {
            return (Code == Parser2.Errors.NOERR || Code == Parser2.Errors.TRUE || Code == Parser2.Errors.FALSE);

        }

        public string GetValue()
        {
            try
            {
                switch (Code)
                {

                    case Parser2.Errors.NOERR: return Value.ToString();
                    case Parser2.Errors.WRONGEXP:return "#ERROR";
                    case Parser2.Errors.DIVIDEBYZERO: { MessageBox.Show("Ділення на нуль неможливе. Формула невірна"); return "#ERROR"; }
                    case Parser2.Errors.NOEXP: return "#ERROR";
                    case Parser2.Errors.UNBALPARENS: return "#ERROR";
                    case Parser2.Errors.SYNTAX: return "#ERROR";
                    case Parser2.Errors.TRUE: return "True";
                    case Parser2.Errors.FALSE: return "False";
                }
                return "";
            }
            catch (StackOverflowException) { return "#CIRCLE"; }
        }

    }
}
