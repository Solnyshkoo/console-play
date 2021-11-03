using System;

namespace PeerGrapes
{
    class Program
    {
        private static readonly Random rand = new Random();
        static void Main(string[] args)
        {
            PrintRules();

            do
            {
                Preporation();

                Console.WriteLine("Поздравляю! Вы выиграли!" + Environment.NewLine +
                                  "Хотите сыграть ещё раз? Введите 1. Для выхода - 0." + Environment.NewLine);
            } while (RepeatGame());
        }
        /// <summary>
        /// Выводит правила игры.
        /// </summary>
        static void PrintRules()
        {
            Console.WriteLine(Environment.NewLine + " Добрый вечер. Наша игра началась. Правила игры:" + Environment.NewLine + Environment.NewLine +
            "1. Компьютер загадывает N - значное число с неповторяющимися цифрами." + Environment.NewLine +
            "2. Игрок делает попытку отгадать число. Попытка — это N - значное число с" + Environment.NewLine +
            " неповторяющимися цифрами. Компьютер выдает в ответ, сколько цифр угадано" + Environment.NewLine +
            " без совпадения с их позициями в тайном числе (то есть количество коров) и" + Environment.NewLine +
            " сколько угадано вплоть до позиции в тайном числе (то есть количество быков)." + Environment.NewLine +
            "3. Выполняется пункт 2, пока игрок не отгадает число." + Environment.NewLine);
        }

        /// <summary>
        /// Считает количество коров и быков.
        /// </summary>
        /// <param name="realAnswer"> Число загаданное компьютером. </param>
        /// <param name="UserAnswer"> Число введённое пользователем. </param>
        /// <param name="n"> Кол-во цифр в числе. </param>
        static void Session(long realAnswer, long UserAnswer, int n)
        {
            while (UserAnswer != realAnswer)
            {
                int cow = 0;
                int bull = 0;
                for (int i = 0; i < n; i++)
                {
                    if (UserAnswer.ToString()[i] == realAnswer.ToString()[i])
                    {
                        bull++;
                    }
                }
                for (int i = 0; i < n; i++)
                {
                    for (int a = 0; a < n; a++)
                    {
                        if (UserAnswer.ToString()[i] == realAnswer.ToString()[a])
                        {
                            cow++;
                        }
                    }
                }
                Console.WriteLine($"{bull} б, {cow - bull} к ");
                Console.WriteLine("Всё получится. Какие ещё варианты комбинаций?");
                while (!(long.TryParse(Console.ReadLine(), out UserAnswer) && UserAnswer.ToString().Length == n &&
                         RepeatingNumber(UserAnswer, n) == n)) 
                {
                    Console.WriteLine("Так нельзя, читай правила) Попробуй ещё раз.");
                }

            }

        }

        /// <summary>
        /// Перезапуск игры.
        /// </summary>
        static bool RepeatGame()
        {
            int choice;
            while (!(int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 0)))
            {
                Console.WriteLine("Такого варианта нет. Напоминаю либо 1, либо 0");
            }
            if (choice == 1)
            {
                Console.WriteLine("Снова привет. Удачной игры.");
                return true;
            }
            else
            {
                Console.WriteLine("Спасибо за игру. Хорошего дня.");
                return false;
            }
        }
        /// <summary>
        /// Подготовка к игре. Ввод и проверка параметров.
        /// </summary>
        static void Preporation()
        {
            long UserAnswer;
            int n;
            long realAnswer;
            Console.WriteLine("Скольки значное число вы хотите загадать?");

            while (!(int.TryParse(Console.ReadLine(), out n) && n > 0 && n <= 10))
            {
                Console.WriteLine("Так нельзя, читай правила) Попробуй ещё раз.");
            }
            realAnswer = Generate(n);
            Console.WriteLine("\tРаунд начался! Введите комбинацию.");
            while (!(long.TryParse(Console.ReadLine(), out UserAnswer) && UserAnswer.ToString().Length == n && RepeatingNumber(UserAnswer, n) == n))
            {
                Console.WriteLine("Так нельзя, читай правила) Попробуй ещё раз.");
            }
            Session(realAnswer, UserAnswer, n);
        }

        /// <summary>
        /// Проверка на корректность данных (повторяющиеся числа).
        /// </summary>
        /// <param name="UserAnswer"> Число введённое пользователем. </param>
        /// <param name="n"> Кол-во цифр в числе. </param>
        /// <returns> Кол-во повторяющихся чисел. </returns>
        static long RepeatingNumber(long UserAnswer, int n)
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int a = 0; a < n; a++)
                {
                    if (UserAnswer.ToString()[i] == UserAnswer.ToString()[a])
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Генерирует число
        /// </summary>
        /// <param name="n"> Кол-во цифр в числе. </param>
        /// <returns> Возвращает сгенерированное число. </returns>
        static long Generate(int n)
        {
            int[] nums = new int[10];
            long result = rand.Next(1, 10);
            nums[result]++;
            for (int i = 1; i < n; i++)
            {
                while (true)
                {
                    int randnumber = rand.Next(0, 10);
                    if (nums[randnumber] == 0)
                    {
                        nums[randnumber]++;
                        result *= 10;
                        result += randnumber;
                        break;
                    }
                }
            }
            return result;
        }
    }

}