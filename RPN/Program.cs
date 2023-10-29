using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace RPM
{
    class Programm
    {

        static void Main()

        {
            string pattern = @"[+\-*/]{2,}";

            Stack<char> operators = new Stack<char>();
            string rpm = "";
            int sw = 1;
            
            do
            {
                Console.Clear();
                Console.WriteLine("Enter your expression: ");
                string expression = Console.ReadLine();
                Match match = Regex.Match(expression, pattern);
                int i = -1;
                rpm = "";
                if (expression.Length > 1 && !match.Success && expression.EndsWith('='))
                {

                    switch (sw)
                    {
                        case 1:
                            i++;
                            if (expression[i] == '=')
                            {
                                break;
                            }
                            else
                            {
                                goto case 2;
                            }
                        case 2:
                            if (GetPrecedense(expression[i]) < 0)
                            {
                                rpm += expression[i];
                                goto case 1;
                            }
                            else
                            {
                                goto case 3;
                            }
                        case 3:
                            if (GetPrecedense(expression[i]) == 0 || operators.Count() == 0 || GetPrecedense(expression[i]) > GetPrecedense(operators.Peek()))
                            {
                                operators.Push(expression[i]);
                                goto case 4;

                            }
                            else
                            {
                                goto case 5;
                            }

                        case 4:
                            if (GetPrecedense(operators.Peek()) == 1)
                            {
                                operators.Pop();
                                operators.Pop();
                            }
                            goto case 1;

                        case 5:
                            rpm += operators.Pop();
                            goto case 3;
                    }
                    while (operators.Count > 0)
                    {
                        rpm += operators.Pop();
                    }
                  
                    Console.WriteLine($"Your expression: {expression}");
                    Console.WriteLine($"RPM: {rpm}=");
                }
                else
                {
                    Console.WriteLine("error");
                }

            }
            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
        //public static void RPM_Convert(operators[] opers, string expression)
        //{


        //}
        public static int GetPrecedense(char op)
        {
            switch (op)
            {
                case '(':
                    return 0;
                case ')':
                    return 1;
                case '+':
                case '-':
                    return 2;
                case '*':
                case '/':
                    return 3;
                case '^':
                    return 4;
                default:
                    return -1;
            }
        }

    }


}
