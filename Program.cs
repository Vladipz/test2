using System;

namespace StringWithWrongs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть першу строку : ");
            string str1 = Console.ReadLine();

            Console.WriteLine("Введіть другу строку: ");
            string str2 = Console.ReadLine();

            Console.WriteLine($"Перша строка: {str1}");
            Console.WriteLine($"Друга строка: {str2}");

            bool isEqual = CompareStrings(str1, str2, 3); // допустима кількість помилок - 3
            if (isEqual)
            {
                Console.WriteLine("Строки рівні.");
            }
            else
            {
                Console.WriteLine("Строки не рівні.");
            }

            Console.ReadLine();
        }

        static bool CompareStrings(string str1, string str2, int maxAllowedErrors)
        {
            // Перетворення рядків на масиви символів
            char[] arr1 = str1.ToLower().ToCharArray();
            char[] arr2 = str2.ToLower().ToCharArray();

            // Перевірка на рівність без будь-якої помилки
            if (str1.Equals(str2, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            // Перевірка варіанту, де ім'я та прізвище поміняні місцями
            string[] name1 = str1.Split(' ');
            string[] name2 = str2.Split(' ');
            if (name1.Length == 2 && name2.Length == 2)
            {
                if (name1[1].Equals(name2[0], StringComparison.OrdinalIgnoreCase) && name1[0].Equals(name2[1], StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            // Перевірка з врахуванням можливих помилок
            int[,] distances = new int[arr1.Length + 1, arr2.Length + 1];
            for (int i = 0; i <= arr1.Length; i++)
            {
                distances[i, 0] = i;
            }
            for (int j = 1; j <= arr2.Length; j++)
            {
                distances[0, j] = j;
            }
            for (int i = 1; i <= arr1.Length; i++)
            {
                for (int j = 1; j <= arr2.Length; j++)
                {
                    int cost = (arr1[i - 1] == arr2[j - 1]) ? 0 : 1;

                    // Знаходження мінімальної відстані
                    int deletion = distances[i - 1, j] + 1;
                    int insertion = distances[i, j - 1] + 1;
                    int substitution = distances[i - 1, j - 1] + cost;
                    int minDistance = Math.Min(Math.Min(deletion, insertion), substitution);

                    distances[i, j] = minDistance;
                }
            }

            int errorsCount = distances[arr1.Length, arr2.Length];
            return (errorsCount <= maxAllowedErrors);
        }
    }
}
