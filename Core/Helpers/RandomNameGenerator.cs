using System;
using System.Linq;

namespace Kinopoisk.Core.Helpers
{
    public class RandomNameGenerator
    {
        private static readonly Random random = new Random();

        public static string RandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomName(int length = 8)
        {
            const string chars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЬЭЮЯ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
