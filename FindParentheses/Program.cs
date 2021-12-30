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
            // var t = CountValidParentheses("())");
            var t = CountValidParentheses(
                ")()))(())((())))))())()(((((())())((()())(())((((())))())((()()))(()(((()()(()((()()))(())()))(((");
            Console.WriteLine(t);
        }

        //method to find mathching parenthesis
        static int CountValidParentheses(string s)
        {
            if (s.Trim().Equals("") || s.Length == 1) return 0;

            var maxCount = 0;

            for (var i = 0; i < s.Length - 1; i++)
            {
                for (var j = 2; j < s.Length - i + 1; j++)
                {
                    var subStr = s.Substring(i, j);
                    if (subStr.Length > maxCount && IsValid(subStr)) maxCount = subStr.Length;
                }
            }

            return maxCount;
        }

        static bool IsValid(string s)
        {
            var stack = new Stack<char>();
            var isValid = true;

            foreach (var c in s)
            {
                if (c == '(')
                {
                    stack.Push(c);
                }
                else
                {
                    if (stack.Count > 0) stack.Pop();
                    else
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid && stack.Count == 0;
        }

        // static int IsValid(string s)
        // {
        //     var stack = new Stack<char>();
        //     var count = 0;
        //     var max = 0;
        //     foreach (var c in s)
        //     {
        //         if (c == '(')
        //         {
        //             if (stack.Count == 0) count = 0;
        //
        //             stack.Push(c);
        //         }
        //         else
        //         {
        //             if (stack.Count == 0)
        //             {
        //                 if (max < count) max = count;
        //                 count = count + max;
        //             }
        //             else
        //             {
        //                 stack.Pop();
        //                 count++;
        //             }
        //         }
        //     }
        //
        //     if (max < count) max = count;
        //     return max * 2;
        // }
    }
}