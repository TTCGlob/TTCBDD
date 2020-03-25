using System;
using System.Collections.Generic;

namespace TTCBDD.Helpers.Rest
{
    public static class BasicHelperMethods
    {
        public static string RandomString(int min, int max)
        {
            var rand = new Random();
            var length = rand.Next(min, max);
            string randomString(int _length)
            {
                var character = ((char)rand.Next(32, 127)).ToString();
                return character + (_length == 1 ? "" : randomString(_length - 1));
            }
            return randomString(length);
        }

        public static string RandomString(int length)
        {
            return RandomString(length, length + 1);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable) action(item);
        }

        public static void Deconstruct(this string[] strings, out string s0, out string s1, out string s2)
        {
            s0 = strings[0];
            s1 = strings[1];
            s2 = strings[2];
        }

        public static Func<string, string> RaiseBy(int raise)
        {
            return (oldSalary) =>
            {
                int intSalary = int.Parse(oldSalary);
                intSalary += intSalary * raise;
                string newSalary = intSalary.ToString();
                return newSalary;
            };
        }

        public static string RaiseSalary(string oldSalary, double raise)
        {
            double salary = double.Parse(oldSalary);
            salary += salary * (raise / 100);
            return Math.Round(salary, 2).ToString();
        }

        public static string RandomNumber(int high, int low = 0)
        {
            return new Random().Next(low, high).ToString();
        }
    }
}
