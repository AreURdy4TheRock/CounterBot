using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CounterBot.Extensions
{
    public static class SumNumbers
    {
        /// <summary>
        /// Суммируем цифры в строке
        /// </summary>
        public static int Sum(string s)
        {
            int answer = 0;
            string[] words = s.Split(' ');
            foreach (var word in words)
            {
                try
                {
                    answer += Int32.Parse(word);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            return answer;
        }
    }
}
