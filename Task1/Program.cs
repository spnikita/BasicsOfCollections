using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Task1
{
    class Program
    {
        /// <summary>
        /// Имя файла, содержащего текстовые данные
        /// </summary>
        private const string TextFilename = "Task1.Text.txt";

        /// <summary>
        /// Массив разделителей
        /// </summary>
        private static readonly string[] _delimiters = new[] { " ", Environment.NewLine };

        /// <summary>
        /// Список
        /// </summary>
        private static readonly List<string> _list = new List<string>();

        /// <summary>
        /// Связанный список
        /// </summary>
        private static readonly LinkedList<string> _linkedList = new LinkedList<string>();

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

                Console.WriteLine($"Выполняется разбиение текста файла по разделителям.");

                var words = text.Split(_delimiters, StringSplitOptions.RemoveEmptyEntries);

                Console.WriteLine($"Количество полученных подстрок: {words.Length}.");

                Console.WriteLine("Выполняется заполнение стандартного списка.");

                var stopWatch = Stopwatch.StartNew();

                FillCollection(_list, words);

                Console.WriteLine("Время заполнения списка: " + stopWatch.Elapsed.TotalMilliseconds);

                Console.WriteLine("Выполняется заполнение связанного списка.");

                stopWatch = Stopwatch.StartNew();

                FillCollection(_linkedList, words);

                Console.WriteLine("Время заполнения списка: " + stopWatch.Elapsed.TotalMilliseconds);
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

        /// <summary>
        /// Заполнить целеую коллекцию элементами исходной коллекции
        /// </summary>
        /// <param name="targetCollection">Целевая коллекция</param>
        /// <param name="sourceCollection">Исходная коллекция</param>
        private static void FillCollection(ICollection<string> targetCollection, IEnumerable<string> sourceCollection)
        {
            foreach (var element in sourceCollection)
            {
                targetCollection.Add(element);
            }
        }
    }
}
