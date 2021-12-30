using System;
using System.Collections.Generic;

namespace FindParentheses
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = CountValidParentheses("())");
            Console.WriteLine(t);
        }

        //method to find mathching parenthesis

        static int CountValidParentheses(string s)
        {
            if (s.Trim().Equals("") || s.Length == 1) return 0;

            var maxLength = 0;

            for (var i = 0; i < s.Length - 1; i++)
            {
                for (var j = s.Length - i; j >= 2; j--)
                {
                    var subStr = s.Substring(i, j);
                    if (subStr.Length >= s.Substring(i + 1, s.Length - i - 1).Length && IsValid(subStr))
                        return subStr.Length;

                    if (IsValid(subStr)) maxLength = Math.Max(maxLength, subStr.Length);
                }
            }

            return maxLength;
        }

        static bool IsValid(string s)
        {
            var stack = new Stack<char>();
            var isValid = true;

            foreach (var c in s)
            {
                if (c == '(') stack.Push(c);

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
    }
}