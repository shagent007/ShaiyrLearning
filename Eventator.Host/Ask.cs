using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventator.Host
{
    public static class Ask
    {
        public static bool Bool(string question, string errorMessage = "не является вариантом ответа")
        {
            bool isValidAnswer;
            string answer;
            do
            {
                Console.Write($"{question} (y/n)");

                answer = Console.ReadLine();
                isValidAnswer = answer == "y" || answer == "n";
                if (!isValidAnswer)
                {
                    Console.WriteLine($"'{answer}' {errorMessage}");
                }
            } while (!isValidAnswer);

            return answer == "y";
        }

        public static string String(string question, string errorMessage, bool validate = true)
        {
            string answer;
            bool isValidAnswer;
            do
            {
                Console.Write(question);
                answer = Console.ReadLine();
                isValidAnswer = answer != "" && answer != null;
                if (!isValidAnswer && validate)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!isValidAnswer && validate);


            return answer;
        }

        public static int Int(string question, string errorMessage = "Введите валидное число")
        {
            int num;
            bool isValidNum;
            do
            {
                Console.Write(question);
                isValidNum = int.TryParse(Console.ReadLine(), out num);

                if (!isValidNum)
                {
                    Console.WriteLine(errorMessage);
                }
            } while (!isValidNum);


            return num;
        }

        public static string ItemInStringList(string question, List<string> options, string errorMessage = "не является вариантом ответа")
        {
            if (options == null || options.Count == 0)
            {
                throw new Exception("Варианты ответа не переданы");
            }
            string answer;
            bool isValidAnswer;
            do
            {
                Console.Write(question);
                answer = Console.ReadLine();
                isValidAnswer = options.Exists(item => item == answer);
                if (!isValidAnswer)
                {
                    Console.WriteLine($"'{answer}' {errorMessage}");
                }
            } while (!isValidAnswer);
            return answer;
        }
    }
}
