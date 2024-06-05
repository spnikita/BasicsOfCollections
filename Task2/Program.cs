using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Task2
{
    class Program
    {
        /// <summary>
        /// Имя файла, содержащего текстовые данные
        /// </summary>
        private const string TextFilename = "Task2.Text.txt";

        /// <summary>
        /// Массив разделителей
        /// </summary>
        private static readonly string[] _delimiters = new[] { " ", Environment.NewLine };

        /// <summary>
        /// Словарь соответствий "слово - количество повторений"
        /// </summary>
        private static readonly Dictionary<string, int> _words = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            try
            {
                var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

                var textFileDirectory = currentDirectory.Parent.Parent.Parent;

                Console.WriteLine($"Выполняется поиск файла {TextFilename} в директории {textFileDirectory.FullName}.");

                var textFile = textFileDirectory.GetFiles().FirstOrDefault(fi => fi.Name.Equals(TextFilename));

                if (textFile == default)
                    throw new Exception($"Не найден файл {TextFilename}");

                Console.WriteLine("Файл найден.");

                Console.WriteLine("Выполняется чтение файла.");

                var text = File.ReadAllText(textFile.FullName);

                Console.WriteLine("Чтение файла завершено.");

                Console.WriteLine("Выполняется удаление знаков пунктуации из исходного текста.");

                var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());

                Console.WriteLine($"Выполняется разбиение текста файла по разделителям.");

                var words = text.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine($"Количество полученных слов: {words.Length}.");

                foreach (var word in words)
                {
                    if (_words.ContainsKey(word))
                        _words[word]++;
                    else
                        _words[word] = 1;
                }

                Console.WriteLine($"10 наиболее часто встречающихся слов:");

                var sortedWords = _words.OrderByDescending(el => el.Value).ToDictionary(x => x.Key, x => x.Value).Take(10);

                foreach (var word in sortedWords)
                {
                    Console.WriteLine("\t" + word.Key + ": " + word.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Во время выполнения задачи произошла ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Задача завершена.");
                Console.ReadKey();
            }
        }
    }
}
