using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace laba2
{
    class Parser
    {

        const int alphabet = 26;
        enum Types { NONE, DELIMITER, VARIABLE, NUMBER };
        public enum Errors { NOERR, SYNTAX, UNBALPARENS, NOEXP, DIVIDEBYZERO };
        public Errors tokErrors;
        private string exp;
        private int expIndex=0;
        private string token;
        private Types tokType;
        private double[] vars = new double[alphabet];

        public Parser()
        {
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i] = 0.0;
            }
        }
        bool IsDelim(char c)
        {

            if ("+-/*<>=()|".IndexOf(c) != -1) return true;
            return false;
        }
        void GetToken()
        {
            tokType = Types.NONE;
            token = "";
            
            if (expIndex == exp.Length) return;
           while (expIndex < exp.Length && Char.IsWhiteSpace(exp[expIndex])) ++expIndex;
            if (expIndex == exp.Length) return;
            if (IsDelim(exp[expIndex]))
            {
                token += exp[expIndex];
                expIndex++;
                tokType = Types.DELIMITER;
            }
            else if (Char.IsLetter(exp[expIndex]))
            {
                while (!IsDelim(exp[expIndex]))
                {
                    token += exp[expIndex];
                    expIndex++;
                    if (expIndex >= exp.Length-1) break;
                }
                tokType = Types.VARIABLE;
            }
            else if (Char.IsDigit(exp[expIndex]))
            {

                while (!IsDelim(exp[expIndex]))
                {
                    token += exp[expIndex];
                    expIndex++;
                    if (expIndex > exp.Length-1) break;
                }
                tokType = Types.NUMBER;
            }
        }

        void Atom(out double result)
        {
            switch (tokType)
            {
                case Types.NUMBER:
                    try
                    {
                        result = Double.Parse(token);

                    }
                    catch (FormatException)
                    {
                        result = 0.0;
                        tokErrors = Errors.SYNTAX;
                    }
                    GetToken();
                    return;
                case Types.VARIABLE:
                    result = FindVar(token);
                    GetToken();
                    return;
                default:
                    result = 0.0;
                    tokErrors = Errors.SYNTAX;
                    break;
            }
        }

        double FindVar(string name)
        {
            if (name.Contains("min("))
            {
                string t1 = null;
                double x, y;
                for (int i = 4; i < name.Length - 1; i++)
                {
                    t1 += name[i];
                }
                string[] k = t1.Split(',');
                x = Convert.ToDouble(k[1]);
                y = Convert.ToDouble(k[0]);
                if (x < y) return x;
                else return y;
            }
            if (name.Contains("max("))
            {
                string t1 = null;
                double x, y;
                for (int i = 4; i < name.Length - 1; i++)
                {
                    t1 += name[i];
                }
                string[] k = t1.Split(',');
                x = Convert.ToDouble(k[1]);
                y = Convert.ToDouble(k[0]);
                if (x > y) return x;
                else return y;
            }
            tokErrors = Errors.SYNTAX;
            return 0.0;
        }
        void EvalExp1(out double result)
        {
            int varIndex;
            Types ttokType;
            string temptoken;
            if(tokType == Types.VARIABLE)
            {
                temptoken = string.Copy(token);
                ttokType = tokType;
                varIndex = Char.ToUpper(token[0]) - 'A';
                GetToken();
                if (token != "=")
                {
                    PutBack();
                    token = string.Copy(temptoken);
                    tokType = ttokType;

                }
                else
                {
                    GetToken();
                    EvalExp2(out result);
                    vars[varIndex] = result;
                    return;
                }
            }
            EvalExp2(out result); 
        }
        void PutBack()
        {
            for (int i = 0; i < token.Length; i++) expIndex--;
        }

        void EvalExp2(out double result)
        {
            string op;
            double partialResult;
            EvalExp3(out result);
            while ((op =token)=="+"|| op == "-")
            {
                GetToken();
                EvalExp3(out partialResult);
                switch (op)
                {
                    case "-":
                        result = result - partialResult;
                        break;
                    case "+":
                        result = result + partialResult;
                        break;
                }
            }

        }
        void EvalExp3(out double result)
        {
            string op;
            double partialResult = 0.0;
            EvalExp4(out result);
            while ((op = token)=="*" || op == "/" || op == "%" || op == "|")
            {
                GetToken();
                EvalExp4(out partialResult);
                switch (op)
                {
                    case "*":
                        result  *= partialResult;
                        break;
                    case "/":
                        if(partialResult == 0.0)
                        {
                            MessageBox.Show("Ділення на нуль неможливе. Формула невірна");
                            result = partialResult;
                        }else 
                        result = result / partialResult;
                        break;
                    case "%":
                        if (partialResult == 0.0)
                        {
                            MessageBox.Show("Ділення на нуль неможливе. Формула невірна");
                            result = partialResult;
                        }
                        else
                            result = (int)result % (int)partialResult;
                        break;
                    case "|":
                        if (partialResult == 0.0)
                        {
                            MessageBox.Show("Ділення на нуль неможливе. Формула невірна");
                            result = partialResult;
                        }
                        result = (double)((int)result / (int)partialResult);
                        break;
                }
            }

        }

        void EvalExp4(out double result)
        {
            double partialResult, ex;
            int t;
            EvalExp5(out result);
            if (token == "^")
            {
                GetToken();
                EvalExp4(out partialResult);
                ex = result;
                if (partialResult == 0.0)
                {
                    result = 1.0;
                    return;
                }
                for(t=(int)partialResult-1; t > 0; t--)
                {
                    result = result * (double)ex;
                }
            }
        }
        void EvalExp5(out double  result)
        {
            string op="";
            if ((tokType == Types.DELIMITER) && token == "+" || token == "-")
            {
                op = token;
                GetToken();
            }
            EvalExp6(out result);
            if (op == "-") result -= result;
        }
        void EvalExp6(out double result)
        {
            if (token == "(")
            {
                GetToken();
                EvalExp2(out result);
                if (token != ")") tokErrors = Errors.UNBALPARENS;

                GetToken();
            }
            else Atom(out result);
        }
      /*  public Result Evaluate(string expstr)
        {
            double result;
            tokErrors = Errors.NOERR;
            if (expstr == null)
            {
                return new Result(0.0, tokErrors);
            }
            
            exp = expstr;
            expIndex = 0;
            
            GetToken();
            if(token == "")
            {
                tokErrors = Errors.NOEXP;
                return new Result(0.0, tokErrors);
            }
            EvalExp1(out result);
            if (token != "")
                tokErrors = Errors.SYNTAX;
            return new Result(result, tokErrors);

        
        }*/

    }
}