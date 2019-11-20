using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTCBDD.ComponentHelper
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

        public static Func<string, string> IncreaseSalary(int raise)
        {
            return (string salary) =>
            {
                var intSalary = int.Parse(salary);
                intSalary += intSalary * raise / 100;
                return intSalary.ToString();
            };
        }
    }
}
