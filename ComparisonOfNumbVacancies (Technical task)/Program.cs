using System;
using System.Text.RegularExpressions;
using System.Threading;
using ComparisonOfNumbVacanciesBL;

namespace ComparisonOfNumbVacancies__Technical_task_
{
    class Program
    {
        private static ComparisonOfNumbVacancies comparison;

        static void Main(string[] args)
        {
            comparison = new ComparisonOfNumbVacancies();
            comparison.ActionMessages += EventMessage;

            Thread.Sleep(3000);

            MainWrite();

            while (true)
            {
                Console.Write("Введите каманду: ");
                string com = Console.ReadLine();
                switch (com)
                {
                    case "help":
                        HelpWrite();
                        break;

                    default:
                        try
                        {
                            ConverCommandToFilter(com);
                            Console.Write("\nВведите количество ожидаемых вакансий:");
                            int count;
                            if (int.TryParse(Console.ReadLine(), out count) == false)
                            {
                                Console.WriteLine("Число должно быть четным");
                                break;
                            }

                            comparison.Start(count);
                            break;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }
                }
            }
        }

        static void ConverCommandToFilter(string text)
        {
            string kw = Parser.FindStringInText(text, @"kw=\s[0-9\|а-я\|a-z\|\s\|А-Я\|A-Z\|-]+\.\s", "kw= ");
            string dep = Parser.FindStringInText(text, @"dep=\s[0-9\|а-я\|a-z\|\s\|А-Я\|A-Z\|-]+\.", "dep= ");
            string[] lang = Parser.FindStringArrayInText(text, @"lang=\s[0-9\|а-я\|a-z\|А-Я\|A-Z\|,\|\s\|-]+\.", "lang= ");
            string exp = Parser.FindStringInText(text, @"exp=\s[0-9\|а-я\|a-z\|\s\|А-Я\|A-Z\|-]+\.", "exp= ");
            string reg = Parser.FindStringInText(text, @"reg=\s[0-9\|а-я\|a-z\|\s\|А-Я\|A-Z\|,\|-]+\.", "reg= ");
            reg = ConvectorReg(reg);

            comparison.InstalFilterVacancies(kw, lang, dep, exp, reg);
        }

        public static void EventMessage(string mes)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(mes);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void HelpWrite()
        {
            Console.WriteLine("Отделы:   \n" +
                              "1)Все отделы; 2)Продажи 3)QA / Тестирование; 4)Разработка продуктов;\n" +
                              "5)Разработка внутренних систем; 6)Тех.поддержка; 7)Инф.технологии; 8)Маркетинг;\n" +
                              "9)HR; 10)Разработка тех.документации; 11)Продакт менеджмент; 12)Дизайн;\n" +
                              "13)Финансовый отдел; 14)Разработка инф.ресурсов; 15)Административный отдел; 16)Другое\n");
            Console.WriteLine("Языки:\n" +
                              "1)Английский; 2)Русский; 3)Немецкий;\n");
            Console.WriteLine("Опыт:\n" +
                              "1)Более 3-х лет; 2)Нет опыта; 3)1-3 года;\n");
            Console.WriteLine("Регион:\n" +
                              "1)СПб (Санкт-Петербург); 2)ЛО (Ленинградская область); 3)ВЛД (Владивосток);\n");
        }

        static void MainWrite()
        {
            Console.WriteLine("\n\nЭта программа сравнивает количество вакансий компании VEEAM по заданному фильтру и сравнивает с количеством ожидаемых вакансий");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Задайте фильтр поиска по след шаблону => \nkw= Application Security Engineer. dep= Продажи. lang= Английский, Русский. exp= 1-3 года. reg= СПб.\n" +
                              "или dep= Разработка продуктов. lang= Английский.\n\n" +
                              "kw - Поле \"Ключевые слова\"\n" +
                              "dep - Поле \"Отделы\"\n" +
                              "lang - Поле \"Языки\"\n" +
                              "exp - Поле \"Опыт работы\"\n" +
                              "reg - Поле \"Регион\"\n " +
                              "\n Обязательно в конце строки ставить точку \"dep= Продажи.\"\n");
            Console.WriteLine("Елси вы не параметры фильтра поиска, то введите команду help.\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static string ConvectorReg(string reg)
        {
            switch (reg)
            {
                case "СПб":
                    return "Russian Federation, Saint-Petersburg";

                case "ВЛД":
                    return "Russian Federation, Vladivostok";

                default:
                    return "";
            }
        }
    }
}
