using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace laba2
{
  public  class Parser2
    {

        public enum Errors { NOERR, SYNTAX, UNBALPARENS, WRONGEXP, DIVIDEBYZERO, NOEXP, TRUE,FALSE };
        int length;
        int Ncomp;
        int[] temp = new int[100];
        int num = 0;
        int sig = 0;
        byte[] F;
        int i = 0;
        string[] compar;
        double[] result;
        int lengthM;
        string[] expresions = new string[1000];
        int IndexStart = -1;
        int IndexEnd = -1;
        int temp1 = 0;
        int Nexp;
        Errors tokErrors;
        

        public Result Evaluate0 (string expression) 
        {
            //MessageBox.Show(expression);
            lengthM = expression.Length;
            Nexp = 0;
            compar = new string[lengthM];
            result = new double[lengthM];
            F = new byte[lengthM];
            Ncomp = 0;
            //expression =  expression.Replace("true", "1");
            //expression =expression.Replace("false", "0");
           // MessageBox.Show(expression);
                for (int i = 0; i < lengthM; i++)
                {
                    if (((i + 1) < lengthM) && ((((int)expression[i]) == 62) && ((int)expression[i + 1] == 61)))
                    {
                        compar[Ncomp] = ">=";
                        Ncomp++;
                        i++;
                    }
                    else if(((i + 1) < lengthM) && ((((int)expression[i]) == 60) && ((int)expression[i + 1] == 62)))
                {
                    compar[Ncomp] = "<>";
                    Ncomp++;
                    i++;
                }
                    else if ((i + 1 < lengthM) && ((((int)expression[i]) == 60) && ((int)expression[i + 1] == 61)))
                    {
                        compar[Ncomp] = "<=";
                        Ncomp++;
                        i++;
                    }
                    else if (((int)expression[i]) == 61)
                    {
                        compar[Ncomp] = "=";
                        Ncomp++;
                    }
                    else if (((int)expression[i]) == 60)
                    {
                        compar[Ncomp] = "<";
                        Ncomp++;
                    }
                    else if (((int)expression[i]) == 62)
                    {
                        compar[Ncomp] = ">";
                        Ncomp++;
                    }
                }
                expression = expression.Replace(">=", "@");
                expression = expression.Replace("<>", "@");
                expression = expression.Replace("<=", "@");
                expression = expression.Replace(">", "@");
                expression = expression.Replace("<", "@");
                expression = expression.Replace("=", "@");
                expression = expression + "@";
                expresions = expression.Split(new char[] { '@' });

                for (int i = 0; i < (Ncomp + 1); i++)
                {

                    result[i] = (Evaluate(expresions[i]).Value);
                }

                for (int i = 0; i < (Ncomp); i++)
                {
                    if (compar[i] == "=")
                    {
                        if (result[i] == result[i + 1])
                        {
                            F[i] = 1;
                        }
                        else
                        {
                            F[i] = 0;
                        }

                    }
                    else  if (compar[i] == "<>")
                    {
                        if (result[i] != result[i + 1])
                        {
                            F[i] = 1;
                        }
                        else
                        {
                            F[i] = 0;
                        }

                    }
                else if (compar[i] == ">=")
                    {
                        if (result[i] >= result[i + 1])
                        {
                            F[i] = 1;
                        }
                        else
                        {
                            F[i] = 0;
                        }
                    }
                    else if (compar[i] == "<=")
                    {
                        if (result[i] <= result[i + 1])
                        {
                            F[i] = 1;
                        }
                        else
                        {
                            F[i] = 0;
                        }
                    }
                    else if (compar[i] == ">")
                    {
                        if (result[i] > result[i + 1])
                        {
                            F[i] = 1;
                        }
                        else
                        {
                            F[i] = 0;
                        }
                    }
                    else if (compar[i] == "<")
                    {
                        if (result[i] < result[i + 1])
                        {
                            F[i] = 1;
                        }
                        else
                        {
                            F[i] = 0;
                        }
                    }
                }
                int output = 1;
                for (int i = 0; i < Ncomp; i++)
                {
                    output = output * F[i];
                }
                if (output == 0)
                {
                    return new Result(0, Parser2.Errors.FALSE);
                }
                else
                {
                    return  new Result(0, Parser2.Errors.TRUE);
                }
            
        }
        public Result Evaluate(string exp)
        {
            if (exp == "True") return new Result(1, Errors.TRUE);
            if (exp == "False") return new Result(0, Errors.FALSE);
            if (exp.Contains("min(") || exp.Contains("max(") || exp.Contains("mod") || exp.Contains("div")) { }
            else
            {
                for (int i = 0; i < exp.Length; i++)
                {
                    if ((int)exp[i] < 40 && (int)exp[i] > 32 || (int)exp[i] > 90 || (int)exp[i] == 63 || (int)exp[i] == 64) {   return new Result(0, Errors.WRONGEXP); }
                }
            }
            if (exp == "" || exp == null) {  return new Result(0, Errors.NOERR); }
            try
            {
                if (exp.Contains("min(") || exp.Contains("max("))
                {
                    exp = exp.Replace("min(", "min{");
                    exp = exp.Replace("max(", "max{");

                }

                while (exp.Split('{').Length != exp.Split('}').Length)
                {
                    temp1 = 0;
                    for (i = exp.IndexOf('{'); i < exp.Length; i++)
                    {

                        if (exp[i] == '(') { temp1++; }
                        if (exp[i] == ')')
                        {

                            if (temp1 > 0) temp1--;
                            else if (temp1 == 0) break;
                        }
                    }

                    exp = exp.Remove(i, 1).Insert(i, "}");
                   // MessageBox.Show(exp);

                }
            }
            catch (Exception) {  return new Result(0, Errors.SYNTAX); }

            exp = MinMax(exp);
            if (exp == "SYNTAX ERROR") { return new Result(0, Errors.SYNTAX); }
            exp = Brackets(exp);
            if (exp == "SYNTAX ERROR") { return new Result(0, Errors.SYNTAX); }
            exp = EvalSubExp(exp);
            if (exp == "SYNTAX ERROR") { return new Result(0, Errors.SYNTAX); }
            if (exp == "DIVIDE_BY_ZERO_ERROR") return new Result(0, Errors.DIVIDEBYZERO);
            if (exp == "NOT_INTEGER_VALUE_AFTER_MOD") return new Result(0, Errors.WRONGEXP);
            Result res = new Result(Convert.ToDouble(exp), Errors.NOERR);
            
            return res;

        }
       
        public string Brackets(string exp)
        {
            if (exp.Split('(').Length != exp.Split(')').Length) return "SYNTAX ERROR";
            while (exp.Contains('('))
            {
                try
                {
                    for (i = 0; i < exp.Length; i++)
                    {
                        if (exp[i] == '(') IndexStart = i;
                        if (exp[i] == ')') { IndexEnd = i; break; }
                    }
                }
                catch (Exception) { return "SYNTAX ERROR"; }
                if (i == exp.Length) { tokErrors = Errors.SYNTAX; break; }
                exp = exp.Substring(0, IndexStart) + EvalSubExp(exp.Substring(IndexStart + 1, IndexEnd - IndexStart - 1)) + exp.Substring(IndexEnd + 1, exp.Length - 1 - IndexEnd);
                //MessageBox.Show(exp);
            }
           // MessageBox.Show(exp);
            return exp;
        }
        public string MinMax(string exp)
        {
            int IndexStart1 =0;
            int IndexEnd1=0;
            string subexp1;
            string subexp2;
            if (exp.Contains(" "))
            {
                exp = exp.Replace(" ", "");
            }

            while (exp.Contains("min{") || exp.Contains("max{"))
            {
                try
                {
                    for (i = 0; i < exp.Length; i++)
                    {
                        if (exp[i] == '{') {  IndexStart1 = i; }
                        if (exp[i] == '}') { IndexEnd1 = i; break; }
                    }
                }
                catch (Exception) {
                    return "SYNTAX ERROR"; }
                subexp1 = exp.Substring(IndexStart1 + 1, IndexEnd1 - IndexStart1 - 1);
                subexp2 = subexp1.Split(',')[1];
                subexp1 = subexp1.Split(',')[0];
                subexp1 = Brackets(subexp1);
                if (subexp1 == "SYNTAX ERROR") { return "SYNTAX ERROR"; }
                subexp2 = Brackets(subexp2);
                if (subexp2 == "SYNTAX ERROR") { return "SYNTAX ERROR"; }
                subexp1 = EvalSubExp(subexp1);
                if (subexp1 == "SYNTAX ERROR") { return "SYNTAX ERROR"; }
                subexp2 = EvalSubExp(subexp2);
                if (subexp2 == "SYNTAX ERROR") { return "SYNTAX ERROR"; }
                //MessageBox.Show(IndexStart1.ToString());
                if (exp[IndexStart1 - 1] == 'n')
                {
                    if (Convert.ToDouble(subexp1) > Convert.ToDouble(subexp2)) { subexp1 = subexp2; }
                }
                if (exp[IndexStart1 - 1] == 'x')
                {
                   
                    if (Convert.ToDouble(subexp1) < Convert.ToDouble(subexp2)) { subexp1 = subexp2; }
                }
                exp = exp.Substring(0, IndexStart1 - 3) + subexp1 + exp.Substring(IndexEnd1 + 1, exp.Length - 1 - IndexEnd1);
               // MessageBox.Show(exp);
                //Console.WriteLine("exp: " + exp);


            }
            //MessageBox.Show(exp);
            return exp;

        }
        public string EvalSubExp(string exp)
        {

            if(exp == null || exp == "") { return "SYNTAX ERROR"; }

            if (exp.Contains(" "))
            {
                exp = exp.Replace(" ", "");
            }
            if (exp.Contains("--"))
            {
                exp = exp.Replace("--", "+");
            }
            if (exp.Contains("-+"))
            {
                exp = exp.Replace("-+", "-");
            }
            length = exp.Length;
            double[] list = new double[length];
            num = 0;
            string[] listsig = new string[length];
            sig = 0;

          //  Console.WriteLine(exp);
            for (int i = 0; i < length; i++)
            {

                if ((int)exp[i] == 43 && i > 0)
                {
                    listsig[sig] = "+";

                    sig++;
                }
                else if ((int)exp[i] == 45 && i > 0)
                {
                    if ((i > 0) && (((int)exp[i - 1] == 42) || ((int)exp[i - 1] == 43) || ((int)exp[i - 1] == 47)))
                    {

                    }
                    else
                    {
                        listsig[sig] = "+";
                        sig++;
                    }
                }
                else if ((int)exp[i] == 42 && i > 0)
                {

                    listsig[sig] = "*";
                    sig++;
                }
                else if ((int)exp[i] == 47 && i > 0)
                {
                    listsig[sig] = "/";
                    sig++;
                }
                else if ((((length - 1) - i) >= 3) && ((int)exp[i] == 109) && ((int)exp[i + 1] == 111) && ((int)exp[i + 2] == 100))
                {
                    listsig[sig] = "mod";
                    sig++;
                }
                else if ((((length - 1) - i) >= 3) && ((int)exp[i] == 100) && ((int)exp[i + 1] == 105) && ((int)exp[i + 2] == 118))
                {
                    listsig[sig] = "div";
                    sig++;
                }

                if ((int)exp[i] >= 48 && (int)exp[i] <= 57)
                {

                    int a = i;
                    int tempnum = 0;
                    int dot = -1;

                    while ((a < (length)) && (((int)exp[a] >= 48 && (int)exp[a] <= 57) || ((int)exp[a] == 46) || ((int)exp[a] == 44)))
                    {

                        if ((int)exp[a] == 46)
                        {
                            dot = tempnum;
                        }
                        else if ((int)exp[a] == 44)
                        {
                            dot = tempnum;
                        }
                        else
                        {
                            temp[tempnum] = (int)char.GetNumericValue(exp[a]);
                            tempnum++;
                        }
                        a++;

                    }
                    a--;
                    tempnum--;
                    if (dot != (-1))
                    {
                        dot = (tempnum + 1) - (dot);
                    }

                    int power = 0;
                    double result = 0;

                    for (int j = tempnum; j >= 0; j--)
                    {
                        result = result + temp[j] * Math.Pow(10, power);
                        power++;
                    }
                    if (dot != (-1))
                    {
                        double decim = Math.Pow(10, dot);
                        result = result / (decim);
                    }


                    if (i > 0)
                    {
                        if (exp[i - 1].Equals('-'))
                        {
                            result = result * (-1);
                        }
                    }
                    list[num] = result;
                    result = 0;
                    num = num + 1;
                    i = a;
                }
            }
            sig = sig - 1;
            num = num - 1;
            while (sig >= 0)
            {

                for (int i = 0; i <= sig; i++)
                {
                    if (listsig[i] == "/")
                    {
                        if (list[i + 1] == 0) return "DIVIDE_BY_ZERO_ERROR";
                        list[i] = list[i] / list[i + 1];
                        sortoutint(list, i, num);
                        sortoutstr(listsig, i, sig);
                        i--;
                        sig = sig - 1;

                    }
                    else if (listsig[i] == "*")
                    {
                        list[i] = list[i] * list[i + 1];
                        sortoutint(list, i, num);
                        sortoutstr(listsig, i, sig);
                        i--;
                        sig = sig - 1;
                    }
                    else if (listsig[i] == "mod")
                    {
                   
                        if (list[i + 1] % 1 != 0) return "NOT_INTEGER_VALUE_AFTER_MOD";
                        if (list[i + 1] == 0) return "DIVIDE_BY_ZERO_ERROR";
                        list[i] = list[i] % list[i + 1];
                       // Console.WriteLine(list[i]);
                        sortoutint(list, i, num);
                        sortoutstr(listsig, i, sig);
                        i--;
                        sig = sig - 1;
                    }
                    else if (listsig[i] == "div")
                    {
                        list[i] = (Math.Truncate(list[i] / list[i + 1]));
                        sortoutint(list, i, num);
                        sortoutstr(listsig, i, sig);
                        i--;
                        sig = sig - 1;
                    }
                }
                for (int i = 0; i <= sig; i++)
                {
                    if (listsig[i] == "+")
                    {
                        list[i] = list[i] + list[i + 1];
                        sortoutint(list, i, num);
                        sortoutstr(listsig, i, sig);
                        i--;
                        sig = sig - 1;
                    }
                }

                sig--;
            }
            //MessageBox.Show(list[0].ToString());
            return list[0].ToString();
        }
        static double[] sortoutint(double[] array, int start, int finish)
        {
            int i = start;
            while ((i + 2) <= finish)
            {
                array[i + 1] = array[i + 2];
                i++;
            }
            return array;
        }
        static string[] sortoutstr(string[] array, int start, int finish)
        {
            int i = start;
            while ((i + 1) <= finish)
            {
                array[i] = array[i + 1];
                i++;
            }
            return array;
        }


    }
}
