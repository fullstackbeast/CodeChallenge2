using System;
using System.Collections.Generic;

namespace FindParentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            // var t = CountValidParentheses(")()())");
            // var t = CountValidParentheses("(())(((()()))");
            var t = CountValidParentheses("(()");
            Console.WriteLine(t);
        }

        //method to find mathching parenthesis


        static int CountValidParentheses(string s)
        {
            if (s.Trim().Equals("")) return 0;

            var stack = new Stack<char>();
            var count = 0;
            var max = 0;
            foreach (var c in s)
            {
                if (c == '(')
                {
                    if (stack.Count == 0)
                    {
                        if (max < count) max = count;
                        count = 0;
                    }

                    stack.Push(c);
                }
                else
                {
                    if (stack.Count == 0)
                    {
                        if (max < count) max = count;
                        count = count+max;
                    }
                    else
                    {
                        stack.Pop();
                        count++;
                    }
                }
            }

            if (max < count) max = count;
            return max * 2;
        }
    }
}